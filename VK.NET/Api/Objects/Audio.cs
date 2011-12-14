// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Audio.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Represents Audio object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Api.Objects
{
    using System;
    using System.Web;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents Audio object.
    /// </summary>
    [Serializable, XmlType(AnonymousType = true), XmlRoot(Namespace = "", ElementName = "audio")]
    public class Audio : IEquatable<Audio>, IComparable<Audio>
    {
        /// <summary>
        /// Audio identificator.
        /// </summary>
        [XmlElement("aid")]
        public uint Id;

        /// <summary>
        /// Owner identificator.
        /// </summary>
        [XmlElement("owner_id")]
        public int OwnerId;

        /// <summary>
        /// Audio artist.
        /// </summary>
        [XmlElement("artist")]
        public string Artist;

        /// <summary>
        /// Audio title.
        /// </summary>
        [XmlElement("title")]
        public string Title;

        /// <summary>
        /// Audio duration in seconds.
        /// </summary>
        [XmlElement("duration")]
        public uint Duration;

        /// <summary>
        /// The URL of the audio file on the VK server.
        /// </summary>
        [XmlElement("url")]
        public string Url;

        /// <summary>
        /// Id of lyrics.
        /// </summary>
        [XmlElement("lyrics_id")]
        public uint LyricsId;

        /// <summary>
        /// Gets the caption in following format: [Artist] - [Title]
        /// </summary>
        [XmlIgnore]
        public string Caption
        {
            get
            {
                return string.Format("{0} - {1}", HttpUtility.HtmlDecode(this.Artist.Trim()), HttpUtility.HtmlDecode(this.Title.Trim()));
            }
        }

        /// <summary>
        /// Gets the caption in following format: [Artist] - [Title] [[Minutes]:[Seconds]]
        /// </summary>
        [XmlIgnore]
        public string CaptionFull
        {
            get
            {
                var d = TimeSpan.FromSeconds(this.Duration);
                return string.Format("{0} - {1} [{2}:{3}]", HttpUtility.HtmlDecode(this.Artist.Trim()), HttpUtility.HtmlDecode(this.Title.Trim()), d.Minutes.ToString("00"), d.Seconds.ToString("00"));
            }
        }

        /// <summary>
        /// Gets the duration in following format: [[Minutes]:[Seconds]]
        /// </summary>
        [XmlIgnore]
        public string DurationString
        {
            get
            {
                var d = TimeSpan.FromSeconds(this.Duration);
                return string.Format("[{0}:{1}]", d.Minutes.ToString("00"), d.Seconds.ToString("00"));
            }
        }

        /// <summary>
        /// Gets the caption in following format: [Artist]-[Title].[Extension]
        /// </summary>
        [XmlIgnore]
        public string Filename
        {
            get
            {
                return string.Format("{0}-{1}{2}", HttpUtility.HtmlDecode(this.Artist.Trim()), HttpUtility.HtmlDecode(this.Title.Trim()), this.Url.Substring(this.Url.LastIndexOf('.')));
            }
        }

        #region IEquatable<Audio> Members

        /// <summary>
        /// Checks audios for equality.
        /// </summary>
        /// <param name="other">Audio object to compare with.</param>
        /// <returns>True if audios are equal, otherwise false.</returns>
        public bool Equals(Audio other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.Id == other.Id &&
                   this.OwnerId == other.OwnerId &&
                   this.Artist == other.Artist &&
                   this.Title == other.Title &&
                   this.Duration == other.Duration &&
                   this.Url == other.Url &&
                   this.LyricsId == other.LyricsId;
        }

        /// <summary>
        /// Checks audios for equality.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>True if audios are equal, otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Audio)) return false;
            return this.Equals((Audio)obj);
        }

        /// <summary>
        /// Gets object hashcode.
        /// </summary>
        /// <returns>
        /// The hashcode.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = this.Id.GetHashCode();
                result = (result * 397) ^ this.OwnerId.GetHashCode();
                result = (result * 397) ^ (this.Artist != null ? this.Artist.GetHashCode() : 0);
                result = (result * 397) ^ (this.Title != null ? this.Title.GetHashCode() : 0);
                result = (result * 397) ^ this.Duration.GetHashCode();
                result = (result * 397) ^ (this.Url != null ? this.Url.GetHashCode() : 0);
                result = (result * 397) ^ this.LyricsId.GetHashCode();
                return result;
            }
        }

        #endregion

        #region IComparable<Audio> Members

        /// <summary>
        /// Compares two audio objects using artist and title information.
        /// </summary>
        /// <param name="other">Audio object to compare with.</param>
        /// <returns>Comparison result.</returns>
        public int CompareTo(Audio other)
        {
            const string format = "{0} - {1}";
            return string.Format(format, this.Artist, this.Title).CompareTo(string.Format(format, other.Artist, other.Title));
        }

        #endregion
    }
}
