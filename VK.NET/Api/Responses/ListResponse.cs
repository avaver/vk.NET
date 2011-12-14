// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListResponse.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Base class for deserializing responses with total objects count.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Api.Responses
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Base class for deserializing responses with total objects count.
    /// </summary>
    [Serializable, XmlType(AnonymousType = true), XmlRoot(Namespace = "", IsNullable = false, ElementName = "response")]
    public abstract class ListResponse
    {
        /// <summary>
        /// Indicates whether it's list.
        /// </summary>
        [XmlAttribute("list")]
        public bool IsList;

        /// <summary>
        /// Total elements count in list.
        /// </summary>
        [XmlElement("count")]
        public uint Total;
    }
}
