using Microsoft.AspNetCore.Identity;
using F1.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1.Data.Models
{
    /// <summary>
    /// A class representing users.
    /// </summary>
    public class User : IdentityUser<Guid>, IDbEntry
    {
        /// <summary>
        /// The ID of the user.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid Id { get; set; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        [Required, MaxLength(20)]
        public override string UserName { get; set; }

        /// <summary>
        /// The hashed password of the user.
        /// </summary>
        [Required, MinLength(8)]
        public override string PasswordHash { get; set; }

        /// <summary>
        /// The foreign key pointing to the role of the user.
        /// </summary>
        [Required, ForeignKey(nameof(UserRole))]
        public Guid RoleId { get; set; }
    }
}
