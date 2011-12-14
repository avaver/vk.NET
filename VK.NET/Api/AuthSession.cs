// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthSession.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Class used to store authentication session data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Api
{
    using System;

    /// <summary>
    /// Class used to store authentication session data.
    /// </summary>
    public class AuthSession
    {
        /// <summary>
        /// Gets user id.
        /// </summary>
        public uint UserId { get; private set; }

        /// <summary>
        /// Gets the security token for api calls.
        /// </summary>
        public string SecurityToken { get; private set; }

        /// <summary>
        /// Gets security token expiration date.
        /// </summary>
        public DateTime Expires { get; private set; }

        /// <summary>
        /// Gets a value indicating whether current session has expired.
        /// </summary>
        public bool Expired
        {
            get { return DateTime.Now >= this.Expires; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthSession"/> class.
        /// </summary>
        /// <param name="userId">Authenticated user identificator.</param>
        /// <param name="securityToken">Security token.</param>
        /// <param name="validSeconds">Duration of session in seconds.</param>
        public AuthSession(uint userId, string securityToken, int validSeconds)
        {
            this.UserId = userId;
            this.SecurityToken = securityToken;

            if (validSeconds == 0)
                this.Expires = DateTime.MaxValue; // We've got offline access
            else
                this.Expires = DateTime.Now.AddSeconds(validSeconds - 10); // Set expiration 10 seconds earlier just to make sure we don't return expired token
        }
    }
}
