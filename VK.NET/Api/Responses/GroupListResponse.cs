// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GroupListResponse.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Class used to deserialize responses from api methods which returns list of group objects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Api.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using VKNet.Api.Objects;

    /// <summary>
    /// Class used to deserialize responses from api methods which returns list of group objects.
    /// </summary>
    [Serializable, XmlType(AnonymousType = true), XmlRoot(Namespace = "", IsNullable = false, ElementName = "response")]
    public class GroupListResponse : ListResponse
    {
        /// <summary>
        /// Gets or sets list of group objects.
        /// </summary>
        [XmlElement("group")]
        public List<Group> Groups { get; set; }
    }
}
