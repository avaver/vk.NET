// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageListResponse.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Class used to deserialize responses from api methods which returns list of message objects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Api.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using VKNet.Api.Objects;

    /// <summary>
    /// Class used to deserialize responses from api methods which returns list of message objects.
    /// </summary>
    [Serializable, XmlType(AnonymousType = true), XmlRoot(Namespace = "", IsNullable = false, ElementName = "response")]
    public class MessageListResponse : ListResponse
    {
        /// <summary>
        /// Gets or sets list of message objects.
        /// </summary>
        [XmlElement("message")]
        public List<Message> Messages { get; set; }
    }
}
