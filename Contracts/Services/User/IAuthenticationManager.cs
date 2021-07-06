using Data.DataTransferObjects.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services.User
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuthenticationDto);
        string CreateToken();
        Task<bool> ExistsUser(UserForRefreshSession userForRefreshSession);
    }
}
