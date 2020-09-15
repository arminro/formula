using System;

namespace F1Web.ViewModels
{
    /// <summary>
    /// The VM for the user
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// The VM id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The VM name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The vm username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The vm token.
        /// </summary>
        public string Token { get; set; }
    }
}
