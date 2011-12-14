// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AudioListResponse.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Class used to deserialize responses from api methods which returns list of audio objects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Api.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using VKNet.Api.Objects;

    /// <summary>
    /// Class used to deserialize responses from api methods which returns list of audio objects.
    /// </summary>
    [Serializable, XmlType(AnonymousType = true), XmlRoot(Namespace = "", IsNullable = false, ElementName = "response")]
    public class AudioListResponse : ListResponse
    {
        /// <summary>
        /// Gets or sets list of audio objects.
        /// </summary>
        [XmlElement("audio")]
        public List<Audio> Audios { get; set; }
    }
}
