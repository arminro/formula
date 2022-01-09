using F1.Data;
using F1.Data.Models;
using F1Web.DataAccess.Implementations;
using F1Web.DataAccess.Interfaces;

namespace F1Web.DataAccess.Implementations
{
    /// <summary>
    /// A class allowing access to the database for Formula Teams.
    /// </summary>
    /// With my hack, adding new types to the db is this easy. I might still not use this under production circumstances.
    public class UserDao : DaoBase<FormulaDbContext, User>, IRepository<User>
    {
        /// <summary>
        /// The constructor for the class.
        /// </summary>
        /// <param name="context">The data context.</param>
        public UserDao(FormulaDbContext context)
            : base(context)
        {
        }
    }
}
