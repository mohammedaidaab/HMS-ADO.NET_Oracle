//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using HMS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HMS.Domain.Interfaces.Repositories
{
    public interface IExternalLoginRepository
    {
        Task<List<UserLoginInfo>> ListForUserId(SiteUser user, CancellationToken cancellationToken);

        Task CreateExternalLoginUser(SiteUser user, UserLoginInfo login, CancellationToken cancellationToken);

        Task<int> GetUserIdByLoginProvider(string loginProvider, string providerKey, CancellationToken cancellationToken);

        Task RemoveLogin(SiteUser user, string loginProvider, string providerKey, CancellationToken cancellationToken);
    }
}