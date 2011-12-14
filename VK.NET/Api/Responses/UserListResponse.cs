// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserListResponse.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Class used to deserialize responses from api methods which returns list of user objects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Api.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using VKNet.Api.Objects;

    /// <summary>
    /// Class used to deserialize responses from api methods which returns list of user objects.
    /// </summary>
    [Serializable, XmlType(AnonymousType = true), XmlRoot(Namespace = "", IsNullable = false, ElementName = "response")]
    public class UserListResponse : ListResponse
    {
        /// <summary>
        /// Gets or sets list of user objects.
        /// </summary>
        [XmlElement("user")]
        public List<User> Users { get; set; }
    }
}