// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorResponse.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Base class for deserializing errors in responses.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Api.Responses
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Base class for deserializing errors in responses.
    /// </summary>
    [Serializable, XmlType(AnonymousType = true), XmlRoot(Namespace = "", IsNullable = false, ElementName = "error")]
    public class ErrorResponse
    {
        /// <summary>
        /// The error code.
        /// </summary>
        [XmlElement("error_code")]
        public int ErrorCode;

        /// <summary>
        /// The error message.
        /// </summary>
        [XmlElement("error_msg")]
        public string ErrorMessage;
    }
}
