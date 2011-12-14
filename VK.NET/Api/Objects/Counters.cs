// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Counters.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Represents user counters.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Api.Objects
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents user counters.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "counters")]
    public class Counters
    {
        /// <summary>
        /// Photo albums count.
        /// </summary>
        [XmlElement("albums")]
        public int Albums;

        /// <summary>
        /// Videos count.
        /// </summary>
        [XmlElement("videos")]
        public int Videos;

        /// <summary>
        /// Songs count.
        /// </summary>
        [XmlElement("audios")]
        public int Audios;

        /// <summary>
        /// Notes count.
        /// </summary>
        [XmlElement("notes")]
        public int Notes;

        /// <summary>
        /// Friends count.
        /// </summary>
        [XmlElement("friends")]
        public int Friends;

        /// <summary>
        /// Online friends count.
        /// </summary>
        [XmlElement("online_friends")]
        public int OnlineFriends;

        /// <summary>
        /// Videos with user count.
        /// </summary>
        [XmlElement("user_videos")]
        public int UserVideos;

        /// <summary>
        /// Photos with user count.
        /// </summary>
        [XmlElement("user_photos")]
        public int UserPhotos;

        /// <summary>
        /// User followers count.
        /// </summary>
        [XmlElement("followers")]
        public int Followers;

        /// <summary>
        /// User subscriptions count.
        /// </summary>
        [XmlElement("subscriptions")]
        public int Subscriptions;
    }
}