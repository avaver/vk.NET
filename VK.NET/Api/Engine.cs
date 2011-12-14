// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Engine.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Vkontakte api wrapper class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Api
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Security.Authentication;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    
    using VKNet.Api.Responses;

    using WatiN.Core;

    /// <summary>
    /// Vkontakte api wrapper class.
    /// </summary>
    public class Engine
    {
        #region Private Fields

        /// <summary>
        /// VK Application Id.
        /// </summary>
        private readonly int appId;

        /// <summary>
        /// App permissions.
        /// </summary>
        private readonly string accessRights;

        /// <summary>
        /// VK user email.
        /// </summary>
        private readonly string userEmail;

        /// <summary>
        /// VK user password.
        /// </summary>
        private readonly string userPassword;

        /// <summary>
        /// Authentication session.
        /// </summary>
        private AuthSession session;

        /// <summary>
        /// Gets a value indicating whether perform autologin or not.
        /// </summary>
        private bool AutoLogin
        {
            get
            {
                return !(string.IsNullOrEmpty(this.userEmail) || string.IsNullOrEmpty(this.userPassword));
            }
        }

        /// <summary>
        /// Gets authentication url.
        /// </summary>
        private string AuthUrl
        {
            get
            {
                return string.Format(
                            "https://api.vkontakte.ru/oauth/authorize?client_id={0}&scope={1}&response_type=token&redirect_uri=http://api.vkontakte.ru/blank.html",
                            this.appId,
                            this.accessRights);
            }
        }

        /// <summary>
        /// Gets security token.
        /// </summary>
        private string SecurityToken
        {
            get
            {
                this.EnsureAuth();
                return this.session.SecurityToken;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="Engine"/> class.
        /// </summary>
        static Engine()
        {
            AppDomain.CurrentDomain.AssemblyResolve += Resolver;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class. 
        /// Public constructor for disabled autologin.
        /// </summary>
        /// <param name="appId">Vkontakte application Id.</param>
        /// <param name="accessRights">Set of permissions to request.</param>
        public Engine(int appId, string[] accessRights)
        {
            if (appId == 0)
                throw new ArgumentException(@"Application ID cannot be zero.", "appId");

            if (accessRights == null || accessRights.Length == 0)
                throw new ArgumentException(@"Application must require at least one permission type.", "accessRights");

            this.appId = appId;
            this.accessRights = string.Join(",", accessRights);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class. 
        /// Public constructor for enabled autologin.
        /// </summary>
        /// <param name="appId">Vkontakte application Id.</param>
        /// <param name="email">User email.</param>
        /// <param name="pass">User password.</param>
        /// <param name="accessRights">Set of permissions to request.</param>
        public Engine(int appId, string email, string pass, params string[] accessRights) : this(appId, accessRights)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException(@"Email cannot be empty.", "email");
            }

            if (string.IsNullOrEmpty(pass))
            {
                throw new ArgumentException(@"Password cannot be empty.", "pass");
            }

            this.userEmail = email;
            this.userPassword = pass;

            Settings.MakeNewIeInstanceVisible = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets UserId.
        /// </summary>
        public uint UserId
        {
            get
            {
                this.EnsureAuth();
                return this.session.UserId;
            }
        }

        #endregion

        #region Api Call

        /// <summary>
        /// Executes Vkontakte api call.
        /// </summary>
        /// <typeparam name="T">
        /// The type to deserialize response to.
        /// If the type is not derived from ListResponse class, the response xml (excluding root 'response' element) 
        /// will be deserialized to a single object.
        /// If the type is XDocument, raw document is returned.
        /// </typeparam>
        /// <param name="request">Request object.</param>
        /// <returns>Api method response.</returns>
        public T Call<T>(Request request) where T : class
        {
            if (request == null)
                throw new ArgumentNullException("request");

            var paramsString = request.Parameters.Count == 0 ? string.Empty : request.Parameters.Concat('&');
            paramsString = string.IsNullOrEmpty(paramsString) ? string.Empty : paramsString + "&";
            var requestUrl = string.Format(
                "https://api.vkontakte.ru/method/{0}.xml?{1}access_token={2}",
                request.MethodName,
                paramsString,
                this.SecurityToken);
            Console.Out.WriteLine(requestUrl);

            var httpReq = (HttpWebRequest)WebRequest.Create(requestUrl);
            var response = httpReq.GetResponse();
            if (response == null)
                throw new ApplicationException("Api call failed.");

            var stream = response.GetResponseStream();
            if (stream == null)
                throw new ApplicationException("Api call failed.");

            var rawResponse = new StreamReader(stream).ReadToEnd();
            Console.Out.WriteLine(rawResponse);

            // Load response into xml document and do validations\adjustments.
            var doc = XDocument.Parse(rawResponse);
            if (doc.Root == null)
                throw new ApplicationException("Invalid response xml.");

            // Error response received.
            if (doc.Root.Name.LocalName.Equals("error"))
            {
                var error = new XmlSerializer(typeof(ErrorResponse)).Deserialize(new StringReader(rawResponse)) as ErrorResponse;
                if (error == null)
                    throw new ApplicationException("Unknown error.\n" + rawResponse);

                throw new ApplicationException(
                    string.Format(
                        "Error response received\nError code: {0}\nError message:{1}",
                        error.ErrorCode,
                        error.ErrorMessage));
            }

            if (typeof(T) == typeof(XDocument))
                return doc as T;

            // If simple list of integer is expected, deserialize it manually 
            // as we don't need to have separate deserialization object for every possible subelement name (uid, gid etc.)
            if (typeof(T) == typeof(List<int>))
                return doc.Root.Elements().Select(el => int.Parse(el.Value)).ToList() as T;

            // If expected response is not a list, but single object, remove the root 'response' node in order to deserialize single object.
            if (typeof(T).BaseType != typeof(ListResponse))
            {
                if (doc.Root.Name.LocalName.Equals("response"))
                    rawResponse = doc.Root.FirstNode.ToString();
            }

            return new XmlSerializer(typeof(T)).Deserialize(new StringReader(rawResponse)) as T;
        }

        #endregion

        #region Authentication

        /// <summary>
        /// Performs authentication.
        /// </summary>
        private void EnsureAuth()
        {
            if (this.session != null && !this.session.Expired)
                return;

            try
            {
                using (var browser = new IE())
                {
                    var timer = new System.Diagnostics.Stopwatch();
                    timer.Start();

                    browser.AutoClose = true;
                    browser.GoTo(this.AuthUrl);

                    // Do autologin if needed.
                    if (this.AutoLogin && !browser.Url.ToLowerInvariant().Contains("access_token=") &&
                        !browser.Url.ToLowerInvariant().Contains("error="))
                    {
                        // Fill email password.
                        if (browser.TextField(Find.ByName("email")).Exists &&
                            browser.TextField(Find.ByName("pass")).Exists)
                        {
                            browser.TextField(Find.ByName("email")).Value = this.userEmail;
                            browser.TextField(Find.ByName("pass")).Value = this.userPassword;
                            browser.Button("install_allow").Click();
                        }

                        // Accept application permissions.
                        if (browser.Button("install_allow").Exists)
                        {
                            browser.Button("install_allow").Click();
                        }

                        if (!browser.Url.ToLowerInvariant().Contains("access_token=") &&
                            !browser.Url.ToLowerInvariant().Contains("error="))
                        {
                            throw new AuthenticationException("Most likely wrong email\\password.");
                        }
                    }

                    // Wait for user to login.
                    if (!this.AutoLogin)
                    {
                        while (!browser.Url.ToLowerInvariant().Contains("access_token="))
                        {
                            System.Threading.Thread.Sleep(3000);
                        }
                    }

                    if (browser.Url.ToLowerInvariant().Contains("error="))
                    {
                        var error = Regex.Match(browser.Url, "error=(?<error>[^&$]*)", RegexOptions.IgnoreCase).Groups["error"].Value;
                        var desc = Regex.Match(browser.Url, "error_description=(?<desc>[^&$]*)", RegexOptions.IgnoreCase).Groups["desc"].Value;
                        throw new AuthenticationException(
                            string.Format(
                                "Error: {0}\nDescription: {1}",
                                HttpUtility.UrlDecode(error),
                                HttpUtility.UrlDecode(desc)));
                    }

                    // Create auth session.
                    var token = Regex.Match(browser.Url, "access_token=(?<token>[^&$]*)", RegexOptions.IgnoreCase).Groups["token"].Value;
                    var expires = int.Parse(Regex.Match(browser.Url, "expires_in=(?<expires>\\d+)", RegexOptions.IgnoreCase).Groups["expires"].Value);
                    var uid = uint.Parse(Regex.Match(browser.Url, "user_id=(?<uid>\\d+)", RegexOptions.IgnoreCase).Groups["uid"].Value);

                    timer.Stop();
                    this.session = new AuthSession(uid, token, expires - (int)timer.Elapsed.TotalSeconds);

                    // Clears IE cache, all cookies (!) and kills IE process. Weird, I know :)
                    browser.ClearCache();
                    browser.ClearCookies();
                    browser.ForceClose();
                }
            }
            catch (AuthenticationException)
            {
                // WTF?! Browser closed? Wrong auth url?!
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Authentication failed.", ex);
            }
        }

        #endregion

        #region Load References From Resources

        /// <summary>
        /// Handler which is called when assemby tries to locate its references.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>Loaded assembly.</returns>
        private static Assembly Resolver(object sender, ResolveEventArgs args)
        {
            var resource = args.Name;
            if (resource.StartsWith("WatiN.Core, "))
                return Assembly.Load(Properties.Resources.Watin);

            if (resource.StartsWith("Interop.SHDocVw, "))
                return Assembly.Load(Properties.Resources.Shdoc);

            return null;
        }

        #endregion
    }
}