// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VK.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Container for API wrappers (components).
// </summary>
// --------------------------------------------------------------------------------------------------------------------

[assembly:System.Runtime.CompilerServices.SuppressIldasm]
namespace VKNet
{
    using VKNet.Api;
    using VKNet.Components;

    /// <summary>
    /// Container for API wrappers (components).
    /// </summary>
    public class VK : ApiComponent
    {
        /// <summary>
        /// User api methods wrapper.
        /// </summary>
        public readonly UserComponent User;

        /// <summary>
        /// Message api methods wrapper.
        /// </summary>
        public readonly MessageComponent Message;

        /// <summary>
        /// Audio api methods wrapper.
        /// </summary>
        public readonly AudioComponent Audio;

        /// <summary>
        /// Initializes a new instance of the <see cref="VK"/> class.
        /// </summary>
        /// <param name="vkApi">Api Engine object.</param>
        public VK(Engine vkApi) : base(vkApi)
        {
            this.User = new UserComponent(vkApi);
            this.Message = new MessageComponent(vkApi);
            this.Audio = new AudioComponent(vkApi);
        }
    }
}
