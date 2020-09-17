using F1.Data.Models;
using F1Web.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1Web.Service.Implementations
{
    /// <summary>
    /// A service for formulateams
    /// </summary>
    public class FormulaTeamService : ServiceBase<FormulaTeam>
    {
        public FormulaTeamService(IRepository<FormulaTeam> repository)
            :base(repository)
        {
        }
    }
}
