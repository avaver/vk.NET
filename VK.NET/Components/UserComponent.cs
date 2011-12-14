// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserComponent.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   User API wrapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Components
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml.Linq;

    using VKNet.Api;
    using VKNet.Api.Objects;
    using VKNet.Api.Responses;

    /// <summary>
    /// Enumeration of user fields that can be requested.
    /// </summary>
    [Flags]
    public enum UserFields
    {
        /// <summary>
        /// [none]
        /// </summary>
        None = 0,

        /// <summary>
        /// empty, returns uid, first_name, last_name
        /// </summary>
        Default = 1,

        /// <summary>
        /// nickname
        /// </summary>
        Nickname = 2,

        /// <summary>
        /// sex
        /// </summary>
        Sex = 4,

        /// <summary>
        /// online
        /// </summary>
        OnlineStatus = 8,

        /// <summary>
        /// bdate
        /// </summary>
        BirthDate = 16,

        /// <summary>
        /// city
        /// </summary>
        City = 32,

        /// <summary>
        /// country
        /// </summary>
        Country = 64,

        /// <summary>
        /// photo
        /// </summary>
        Photo = 128,

        /// <summary>
        /// photo_medium
        /// </summary>
        PhotoMedium = 256,

        /// <summary>
        /// photo_medium_rec
        /// </summary>
        PhotoMediumRec = 512,

        /// <summary>
        /// photo_big
        /// </summary>
        PhotoBig = 1024,

        /// <summary>
        /// photo_rec
        /// </summary>
        PhotoRec = 2048,

        /// <summary>
        /// lists
        /// </summary>
        Lists = 4096,

        /// <summary>
        /// domain
        /// </summary>
        Domain = 8192,

        /// <summary>
        /// has_mobile
        /// </summary>
        HasMobile = 16384,

        /// <summary>
        /// rate
        /// </summary>
        Rate = 32768,

        /// <summary>
        /// contacts
        /// </summary>
        Contacts = 65536,

        /// <summary>
        /// education
        /// </summary>
        Education = 131072,

        /// <summary>
        /// can_post
        /// </summary>
        CanPost = 262144,

        /// <summary>
        /// can_write_private_message
        /// </summary>
        CanWriteMessages = 524288,

        /// <summary>
        /// counters
        /// </summary>
        Counters = 1048576,

        /// <summary>
        /// all available fields
        /// </summary>
        All = Nickname | Sex | OnlineStatus | BirthDate | City |
              Country | Photo | PhotoMedium | PhotoMediumRec | PhotoBig |
              PhotoRec | Lists | Domain | HasMobile | Rate | Contacts |
              Education | CanPost | CanWriteMessages | Counters
    }

    /// <summary>
    /// User API wrapper.
    /// </summary>
    public class UserComponent : ApiComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserComponent"/> class.
        /// </summary>
        /// <param name="vkApi">
        /// Api engine object.
        /// </param>
        public UserComponent(Engine vkApi) : base(vkApi) { }

        /// <summary>
        /// Gets User object for current user.
        /// </summary>
        /// <param name="fields">Bitmask of fields to get.</param>
        /// <returns>User object.</returns>
        public User GetCurrent(UserFields fields)
        {
            return this.GetSingle(this.Api.UserId, fields);
        }

        /// <summary>
        /// Gets user by id.
        /// </summary>
        /// <param name="userId">Identificator of user.</param>
        /// <param name="fields">Bitmask of fields to get.</param>
        /// <returns>User object.</returns>
        public User GetSingle(uint userId, UserFields fields)
        {
            var request = new Request("getProfiles");
            request.Parameters.Add("uids", userId.ToString());
            request.Parameters.Add("fields", GetFields(fields));
            return Api.Call<User>(request);
        }

        /// <summary>
        /// Gets list of users.
        /// </summary>
        /// <param name="userIds">Identificator of user.</param>
        /// <param name="fields">Bitmask of fields to get.</param>
        /// <returns>User object.</returns>
        public List<User> GetList(uint[] userIds, UserFields fields)
        {
            var request = new Request("getProfiles");
            request.Parameters.Add("uids", string.Join(",", userIds));
            request.Parameters.Add("fields", GetFields(fields));
            return Api.Call<UserListResponse>(request).Users;
        }

        /// <summary>
        /// Checks if user is application user.
        /// </summary>
        /// <param name="userId">Identificator of user.</param>
        /// <returns>True if the user the application user, otherwise false.</returns>
        public bool IsAppUser(uint userId)
        {
            var request = new Request("isAppUser");
            request.Parameters.Add("uid", userId.ToString());
            var response = Api.Call<XDocument>(request);
            if (response == null || response.Root == null) throw new ApplicationException("Request failed.");
            return response.Root.Value.Equals("1");
        }

        /// <summary>
        /// Gets user balance in the application.
        /// </summary>
        /// <returns>User balance.</returns>
        public float GetBalance()
        {
            var request = new Request("getUserBalance");
            var response = Api.Call<XDocument>(request);
            if (response == null || response.Root == null) throw new ApplicationException("Request failed.");
            var balanceElement = response.Root.Element(XName.Get("balance"));
            if (balanceElement == null) throw new ApplicationException("Cannot parse response.");
            return int.Parse(balanceElement.Value) / 100.0F;
        }

        /// <summary>
        /// Gets the permissions set for the current application.
        /// </summary>
        /// <returns>Bitmask of permissions.</returns>
        public string GetSettings()
        {
            var request = new Request("getUserSettings");
            var response = Api.Call<XDocument>(request);
            if (response == null || response.Root == null) throw new ApplicationException("Request failed.");
            var settingsElement = response.Root.Element(XName.Get("settings"));
            if (settingsElement == null) throw new ApplicationException("Cannot parse response.");
            return settingsElement.Value;
        }

        /// <summary>
        /// Gets group ids the user is associated with.
        /// </summary>
        /// <returns>List of group ids.</returns>
        public List<int> GetGroupIds()
        {
            var request = new Request("getGroups");
            return Api.Call<List<int>>(request);
        }

        /// <summary>
        /// Gets groups information the user is associated with.
        /// </summary>
        /// <returns>List of Group objects.</returns>
        public List<Group> GetGroups()
        {
            var request = new Request("getGroupsFull");
            return Api.Call<GroupListResponse>(request).Groups;
        }

        /// <summary>
        /// Gets friend ids for the current user.
        /// </summary>
        /// <returns>List of user ids.</returns>
        public List<int> GetFriendIds()
        {
            return this.GetFriendIds(this.Api.UserId);
        }

        /// <summary>
        /// Gets friends ids for the specified user.
        /// </summary>
        /// <param name="userId">User identificator to get friends of.</param>
        /// <returns>List of user ids.</returns>
        public List<int> GetFriendIds(uint userId)
        {
            var request = new Request("friends.get");
            request.Parameters.Add("uid", userId.ToString());
            return Api.Call<List<int>>(request);
        }

        /// <summary>
        /// Gets friends for the current user.
        /// </summary>
        /// <param name="fields">Bitmask of fields to get.</param>
        /// <returns>List of users.</returns>
        public List<User> GetFriends(UserFields fields)
        {
            return this.GetFriends(this.Api.UserId, fields);
        }

        /// <summary>
        /// Gets friends for the specified user.
        /// </summary>
        /// <param name="userId">User id to get friends for.</param>
        /// <param name="fields">Bitmask of fields to get.</param>
        /// <returns>List of users.</returns>
        public List<User> GetFriends(uint userId, UserFields fields)
        {
            var request = new Request("friends.get");
            request.Parameters.Add("uid", userId.ToString());
            request.Parameters.Add("fields", GetFields(fields));
            return Api.Call<UserListResponse>(request).Users;
        }

        /// <summary>
        /// Gets online friend ids for the current user.
        /// </summary>
        /// <returns>List of user ids.</returns>
        public List<int> GetOnlineFriendIds()
        {
            return this.GetOnlineFriendIds(this.Api.UserId);
        }

        /// <summary>
        /// Gets online friend ids for the specified user.
        /// </summary>
        /// <param name="userId">User id to get online friends for.</param>
        /// <returns>List of user ids.</returns>
        public List<int> GetOnlineFriendIds(uint userId)
        {
            var request = new Request("friends.getOnline");
            request.Parameters.Add("uid", userId.ToString());
            return Api.Call<List<int>>(request);
        }

        /// <summary>
        /// Gets fields string to pass in the Api call from the bit mask.
        /// </summary>
        /// <param name="fields">Fields bit mask.</param>
        /// <returns>Fields string.</returns>
        private static string GetFields(UserFields fields)
        {
            var fieldsParam = new StringBuilder();
            if ((fields & UserFields.Default) == UserFields.Default)
                return "first_name,last_name";
            if ((fields & UserFields.BirthDate) == UserFields.BirthDate)
                fieldsParam.Append("bdate,");
            if ((fields & UserFields.CanPost) == UserFields.CanPost)
                fieldsParam.Append("can_post,");
            if ((fields & UserFields.CanWriteMessages) == UserFields.CanWriteMessages)
                fieldsParam.Append("can_write_private_message,");
            if ((fields & UserFields.City) == UserFields.City)
                fieldsParam.Append("city,");
            if ((fields & UserFields.Contacts) == UserFields.Contacts)
                fieldsParam.Append("contacts,");
            if ((fields & UserFields.Counters) == UserFields.Counters)
                fieldsParam.Append("counters,");
            if ((fields & UserFields.Country) == UserFields.Country)
                fieldsParam.Append("country,");
            if ((fields & UserFields.Domain) == UserFields.Domain)
                fieldsParam.Append("domain,");
            if ((fields & UserFields.Education) == UserFields.Education)
                fieldsParam.Append("education,");
            if ((fields & UserFields.HasMobile) == UserFields.HasMobile)
                fieldsParam.Append("has_mobile,");
            if ((fields & UserFields.Lists) == UserFields.Lists)
                fieldsParam.Append("lists,");
            if ((fields & UserFields.Nickname) == UserFields.Nickname)
                fieldsParam.Append("nickname,");
            if ((fields & UserFields.OnlineStatus) == UserFields.OnlineStatus)
                fieldsParam.Append("online,");
            if ((fields & UserFields.Photo) == UserFields.Photo)
                fieldsParam.Append("photo,");
            if ((fields & UserFields.PhotoBig) == UserFields.PhotoBig)
                fieldsParam.Append("photo_big,");
            if ((fields & UserFields.PhotoMedium) == UserFields.PhotoMedium)
                fieldsParam.Append("photo_medium,");
            if ((fields & UserFields.PhotoMediumRec) == UserFields.PhotoMediumRec)
                fieldsParam.Append("photo_medium_rec,");
            if ((fields & UserFields.PhotoRec) == UserFields.PhotoRec)
                fieldsParam.Append("photo_rec,");
            if ((fields & UserFields.Rate) == UserFields.Rate)
                fieldsParam.Append("rate,");
            if ((fields & UserFields.Sex) == UserFields.Sex)
                fieldsParam.Append("sex,");

            return fieldsParam.ToString().TrimEnd(',');
        }
    }
}
