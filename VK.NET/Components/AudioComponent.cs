// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AudioComponent.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Audio API wrapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Components
{
    using System.Collections.Generic;

    using VKNet.Api;
    using VKNet.Api.Objects;
    using VKNet.Api.Responses;

    /// <summary>
    /// Audio API wrapper.
    /// </summary>
    public class AudioComponent : ApiComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioComponent"/> class.
        /// </summary>
        /// <param name="vkApi">
        /// Api engine object.
        /// </param>
        public AudioComponent(Engine vkApi) : base(vkApi) { }

        /// <summary>
        /// Searches vkontakte audio storage.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="count">Results count to return.</param>
        /// <returns>List of audio objects matching the search query.</returns>
        public List<Audio> Search(string query, int count)
        {
            var request = new Request("audio.search");
            request.Parameters.Add("q", query);
            request.Parameters.Add("count", count.ToString());
            return Api.Call<AudioListResponse>(request).Audios;
        }
    }
}
