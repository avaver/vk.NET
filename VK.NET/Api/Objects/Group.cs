// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Group.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Represents Group object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Api.Objects
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents Group object.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "group")]
    public class Group : IEquatable<Group>, IComparable<Group>
    {
        /// <summary>
        /// Group identificator.
        /// </summary>
        [XmlElement("gid")]
        public uint Id;

        /// <summary>
        /// Group name.
        /// </summary>
        [XmlElement("name")]
        public string Name;

        /// <summary>
        /// Group picture url.
        /// </summary>
        [XmlElement("photo")]
        public string PictureUrl;

        /// <summary>
        /// Indicates if group is closed. 0 - false, 1 - true.
        /// Use IsClosed property instead.
        /// </summary>
        [XmlElement("is_closed")]
        public byte RawIsClosed;

        /// <summary>
        /// Gets a value indicating whether group is closed.
        /// </summary>
        [XmlIgnore]
        public bool IsClosed
        {
            get
            {
                return this.RawIsClosed == 1;
            }
        }

        #region IEquatable<Group> Members

        /// <summary>
        /// Checks groups for equality.
        /// </summary>
        /// <param name="other">Group object to compare with.</param>
        /// <returns>True if groups are equal, otherwise false.</returns>
        public bool Equals(Group other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.Id == other.Id && this.Name == other.Name && this.PictureUrl == other.PictureUrl && this.RawIsClosed == other.RawIsClosed;
        }

        /// <summary>
        /// Checks groups for equality.
        /// </summary>
        /// <param name="obj">Group object to compare with.</param>
        /// <returns>True if groups are equal, otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Group)) return false;
            return this.Equals((Group)obj);
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
                result = (result * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                result = (result * 397) ^ (this.PictureUrl != null ? this.PictureUrl.GetHashCode() : 0);
                result = (result * 397) ^ this.RawIsClosed.GetHashCode();
                return result;
            }
        }

        #endregion

        #region IComparable<Group> Members

        /// <summary>
        /// Compares two group objects by name.
        /// </summary>
        /// <param name="other">Group object to compare with.</param>
        /// <returns>Comparison result.</returns>
        public int CompareTo(Group other)
        {
            return this.Name.CompareTo(other.Name);
        }

        #endregion
    }
}