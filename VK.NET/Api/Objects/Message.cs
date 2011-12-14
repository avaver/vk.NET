// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Message.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Represents private message.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Api.Objects
{
    using System;
    using System.Xml.Serialization;
    
    /// <summary>
    /// Message direction enumeration.
    /// </summary>
    public enum MessageDirection
    {
        Incoming = 0,
        Outgoing = 1
    }

    /// <summary>
    /// Represents private message.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "message")]
    public class Message : IEquatable<Message>, IComparable<Message>
    {
        /// <summary>
        /// Message identificator.
        /// </summary>
        [XmlElement("mid")]
        public uint Id;

        /// <summary>
        /// User identificator (sender for incoming message, recipient for the outgoing).
        /// </summary>
        [XmlElement("uid")]
        public uint UserId;

        /// <summary>
        /// Message date in UNIX format.
        /// </summary>
        [XmlElement("date")]
        public ulong RawUnixDate;

        /// <summary>
        /// Gets message date.
        /// </summary>
        [XmlIgnore]
        public DateTime Date
        {
            get { return Util.UnixDateToLocal(this.RawUnixDate); }
        }

        /// <summary>
        /// Message subject.
        /// </summary>
        [XmlElement("title")]
        public string Subject;

        /// <summary>
        /// Message body.
        /// </summary>
        [XmlElement("body")]
        public string Body;

        /// <summary>
        /// Indicates if message is read. 1 - read, 0 - unread.
        /// </summary>
        [XmlElement("read_state")]
        public byte RawReadState;

        /// <summary>
        /// Gets a value indicating whether message is read.
        /// </summary>
        [XmlIgnore]
        public bool IsRead
        {
            get { return this.RawReadState == 1; }
        }

        /// <summary>
        /// Indicates if the message is incoming or outgoing. 0 - incoming, 1 - outgoing.
        /// </summary>
        [XmlElement("out")]
        public byte RawOut;

        /// <summary>
        /// Gets the incoming\outgoing direction of the message.
        /// </summary>
        [XmlIgnore]
        public MessageDirection Direction
        {
            get { return (MessageDirection)this.RawOut; }
        }

        #region IEquatable<Message> Members

        /// <summary>
        /// Checks message for equality.
        /// </summary>
        /// <param name="other">Message object to compare with.</param>
        /// <returns>True if messages are equal, otherwise false.</returns>
        public bool Equals(Message other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == this.Id && other.UserId == this.UserId && other.RawUnixDate == this.RawUnixDate &&
                   other.Subject == this.Subject && other.Body == this.Body && other.RawReadState == this.RawReadState &&
                   other.RawOut == this.RawOut;
        }

        /// <summary>
        /// Checks message for equality.
        /// </summary>
        /// <param name="obj">Message object to compare with.</param>
        /// <returns>True if messages are equal, otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Message)) return false;
            return this.Equals((Message)obj);
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
                result = (result * 397) ^ this.UserId.GetHashCode();
                result = (result * 397) ^ this.RawUnixDate.GetHashCode();
                result = (result * 397) ^ (this.Subject != null ? this.Subject.GetHashCode() : 0);
                result = (result * 397) ^ (this.Body != null ? this.Body.GetHashCode() : 0);
                result = (result * 397) ^ this.RawReadState.GetHashCode();
                result = (result * 397) ^ this.RawOut.GetHashCode();
                return result;
            }
        }

        #endregion

        #region IComparable<Message> Members

        /// <summary>
        /// Compares two message objects using message ids.
        /// </summary>
        /// <param name="other">Message object to compare with.</param>
        /// <returns>Comparison result.</returns>
        public int CompareTo(Message other)
        {
            return this.Id.CompareTo(other.Id);
        }

        #endregion
    }
}