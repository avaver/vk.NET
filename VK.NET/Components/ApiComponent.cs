// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiComponent.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Base abstract class for Api wrappers (i.e. User\Message\Audio api methods wrapper).
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Components
{
    using VKNet.Api;

    /// <summary>
    /// Base abstract class for Api wrappers (i.e. User\Message\Audio api methods wrapper).
    /// </summary>
    public abstract class ApiComponent
    {
        /// <summary>
        /// Api engine object.
        /// </summary>
        protected readonly Engine Api;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiComponent"/> class.
        /// </summary>
        /// <param name="vkApi">
        /// Api engine object.
        /// </param>
        protected ApiComponent(Engine vkApi)
        {
            this.Api = vkApi;
        }
    }
}
