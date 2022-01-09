using F1.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace F1.Data
{
    /// <summary>
    /// The databse context connecting the database and the application.
    /// </summary>
    public class FormulaDbContext : IdentityDbContext<User, UserRole, Guid>
    {
       /// <summary>
       /// The collection of teams through which the teams are accessible.
       /// </summary>
        public DbSet<FormulaTeam> Teams { get; set; }

        /// <summary>
        /// Parametered constructor for the data context.
        /// </summary>
        /// <param name="opts">Database options for configuring the context.</param>
        public FormulaDbContext(DbContextOptions<FormulaDbContext> opts) 
            : base(opts)
        {
        }
    }
}
