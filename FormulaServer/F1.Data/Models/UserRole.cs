using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1.Data.Models
{
    /// <summary>
    /// A class representing user roles.
    /// </summary>
    public class UserRole : IdentityRole<Guid>
    {
        /// <summary>
        /// The ID of the user role.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid Id { get; set; }

        /// <summary>
        /// The name of the role.
        /// </summary>
        public string RoleName { get; set; }
    }
}
