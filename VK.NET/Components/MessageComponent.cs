// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageComponent.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Message API wrapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Components
{
    using System;
    using System.Collections.Generic;

    using VKNet.Api;
    using VKNet.Api.Objects;
    using VKNet.Api.Responses;

    /// <summary>
    /// Enumeration of private message filters.
    /// </summary>
    [Flags]
    public enum MessageFilter
    {
        None = 0,
        Unread = 1,
        NotFromChat = 2,
        FromFriends = 4
    }

    /// <summary>
    /// Message API wrapper.
    /// </summary>
    public class MessageComponent : ApiComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageComponent"/> class.
        /// </summary>
        /// <param name="vkApi">
        /// Api engine object.
        /// </param>
        public MessageComponent(Engine vkApi) : base(vkApi) { }

        /// <summary>
        /// Gets unread messages for current user.
        /// </summary>
        /// <returns>List of messages.</returns>
        public List<Message> GetUnread()
        {
            return this.GetList(MessageDirection.Incoming, null, 100, MessageFilter.Unread, 0, null);
        }

        /// <summary>
        /// Gets messages filtered by different criteria.
        /// </summary>
        /// <param name="direction">Incoming\outgoing messages.</param>
        /// <param name="offset">Used for paged results.</param>
        /// <param name="count">Count of messages to return.</param>
        /// <param name="filter">MessageFilter to filter messages.</param>
        /// <param name="maxMsgLength">Max message length to load. Pass 0 to load complete messages.</param>
        /// <param name="timeOffset">If specified, returns messages not older than timeOffset seconds.</param>
        /// <returns>List of messages.</returns>
        public List<Message> GetList(MessageDirection direction, int? offset, int? count, MessageFilter filter, int? maxMsgLength, int? timeOffset)
        {
            var request = new Request("messages.get");
            request.Parameters.Add("out", ((int)direction).ToString());
            if (offset.HasValue)
                request.Parameters.Add("offset", offset.ToString());
            if (count.HasValue)
                request.Parameters.Add("count", count.ToString());
            if (filter != MessageFilter.None)
                request.Parameters.Add("filters", ((int)filter).ToString());
            if (maxMsgLength.HasValue)
                request.Parameters.Add("preview_length", maxMsgLength.ToString());
            if (timeOffset.HasValue)
                request.Parameters.Add("time_offset", timeOffset.ToString());
            return Api.Call<MessageListResponse>(request).Messages;
        }
    }
}
