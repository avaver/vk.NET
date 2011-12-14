// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Permissions.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Defines possible application permissions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Api
{
    using System;

    /// <summary>
    /// Defines possible application permissions.
    /// </summary>
    [Flags]
    [Obsolete("Not supported anymore. Use constants from Permissions2 class instead.")]
    public enum Permissions
    {
        Notifications = 1,
        Friends = 2,
        Pictures = 4,
        Music = 8,
        Video = 16,
        Proposals = 32,
        Questions = 64,
        Wiki = 128,
        AppMenuLink = 256,
        AppWallLink = 512,
        Status = 1024,
        Notes = 2048,
        ExtendedMessageApi = 4096,
        Wall = 8192
    }

    /// <summary>
    /// Defines possible application permissions.
    /// </summary>
    public static class Permissions2
    {
        /// <summary>
        /// Send notifications.
        /// </summary>
        public const string Notify = "notify";
        
        /// <summary>
        /// Access to friends.
        /// </summary>
        public const string Friends = "friends";

        /// <summary>
        /// Access to photos.
        /// </summary>
        public const string Photos = "photos";

        /// <summary>
        /// Access to audio.
        /// </summary>
        public const string Audio = "audio";

        /// <summary>
        /// Access to video.
        /// </summary>
        public const string Video = "video";

        /// <summary>
        /// Access to documents.
        /// </summary>
        public const string Docs = "docs";

        /// <summary>
        /// Access to notes.
        /// </summary>
        public const string Notes = "notes";

        /// <summary>
        /// Access to wiki pages.
        /// </summary>
        public const string Pages = "pages";

        /// <summary>
        /// Access to offers.
        /// </summary>
        [Obsolete("Obsolete methods.")]
        public const string Offers = "offers";

        /// <summary>
        /// Access to questions.
        /// </summary>
        [Obsolete("Obsolete methods.")]
        public const string Questions = "questions";

        /// <summary>
        /// Access to wall.
        /// </summary>
        public const string Wall = "wall";

        /// <summary>
        /// Access to groups.
        /// </summary>
        public const string Groups = "groups";

        /// <summary>
        /// Access to messages (For standalone applications).
        /// </summary>
        public const string Messages = "messages";

        /// <summary>
        /// Access to notifications about answers to the user.
        /// </summary>
        public const string Notifications = "notifications";

        /// <summary>
        /// Access to ads API methods.
        /// </summary>
        public const string Ads = "ads";

        /// <summary>
        /// Anytime api access.
        /// </summary>
        public const string Offline = "offline";

        /// <summary>
        /// Call API without HTTPS (experimental, can be removed).
        /// </summary>
        public const string NoHttps = "nohttps";
    }
}
