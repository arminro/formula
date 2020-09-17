using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1Web.Service.Implementations
{
    /// <summary>
    /// Class respresenting the result of authentication, with the user
    /// </summary>
    public class AuthResult<TUser>
    {
        public AuthResults Result { get; set; }
        public TUser User { get; set; }
    }
}
