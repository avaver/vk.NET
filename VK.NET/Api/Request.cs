// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Request.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Request class used to hold data required by api calls.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Api
{
    using System.Collections.Generic;

    /// <summary>
    /// Request class used to hold data required by api calls.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Api method name.
        /// </summary>
        public readonly string MethodName;

        /// <summary>
        /// Additional parameters.
        /// </summary>
        public Dictionary<string, string> Parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="method">
        /// Api method name.
        /// </param>
        public Request(string method)
        {
            this.MethodName = method;
            this.Parameters = new Dictionary<string, string>();
        }
    }
}
