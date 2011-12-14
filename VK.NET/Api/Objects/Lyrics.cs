// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Lyrics.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Represents Lyrics object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Api.Objects
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents Lyrics object.
    /// </summary>
    [Serializable, XmlType(AnonymousType = true), XmlRoot(Namespace = "", ElementName = "lyrics")]
    public class Lyrics : IEquatable<Lyrics>, IComparable<Lyrics>
    {
        /// <summary>
        /// Lyrics identificator.
        /// </summary>
        [XmlElement("lyrics_id")]
        public uint Id;

        /// <summary>
        /// Lyrics text.
        /// </summary>
        [XmlElement("text")]
        public string Text;

        #region IEquatable<Lyrics> Members

        /// <summary>
        /// Checks lyrics for equality.
        /// </summary>
        /// <param name="other">Lyrics object to compare with.</param>
        /// <returns>True if lyrics are equal, otherwise false.</returns>
        public bool Equals(Lyrics other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.Id == other.Id && this.Text == other.Text;
        }

        /// <summary>
        /// Checks lyrics for equality.
        /// </summary>
        /// <param name="obj">Lyrics object to compare with.</param>
        /// <returns>True if lyrics are equal, otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Lyrics)) return false;
            return this.Equals((Lyrics)obj);
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
                result = (result * 397) ^ (this.Text != null ? this.Text.GetHashCode() : 0);
                return result;
            }
        }
        #endregion

        #region IComparable<Lyrics> Members

        /// <summary>
        /// Compares two lyrics objects using lyrics text.
        /// </summary>
        /// <param name="other">Lyrics object to compare with.</param>
        /// <returns>Comparison result.</returns>
        public int CompareTo(Lyrics other)
        {
            return this.Text.CompareTo(other.Text);
        }

        #endregion
    }
}
