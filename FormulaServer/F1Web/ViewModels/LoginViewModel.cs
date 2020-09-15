using System.ComponentModel.DataAnnotations;

namespace F1Web.ViewModels
{
    /// <summary>
    /// VM storing information for logins.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Username.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Generated password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
