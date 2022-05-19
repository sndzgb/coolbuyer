using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.Services
{
    /// <summary>
    /// Returns the authenticated user information.
    /// </summary>
    public interface IAuthenticatedUserService
    {
        int Id { get; }
        string Username { get; }

        /// <summary>
        /// Token which contains infomation about the authenticated user.
        /// </summary>
        string Token { get; }
    }
}
