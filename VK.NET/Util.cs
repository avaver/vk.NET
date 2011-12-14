// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Util.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Helper class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Helper class.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Concatenates pairs of key and value.
        /// </summary>
        /// <param name="list">Key value dictionary to concat.</param>
        /// <param name="separator">Separator between concatenated objects.</param>
        /// <returns>Concatenated string.</returns>
        public static string Concat(this Dictionary<string, string> list, char separator)
        {
            var s = new StringBuilder();
            foreach (var i in list)
            {
                s.Append(i.Key + "=" + i.Value + separator);
            }

            return s.ToString().Trim(separator);
        }

        /// <summary>
        /// Converts Unix date to .NET object.
        /// </summary>
        /// <param name="unixDate">Unix timestamp.</param>
        /// <returns>.NET DateTime object.</returns>
        public static DateTime UnixDateToLocal(ulong unixDate)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(unixDate).ToLocalTime();
        }
    }
}
