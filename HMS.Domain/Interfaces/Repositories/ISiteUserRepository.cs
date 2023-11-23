//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using HMS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HMS.Domain.Interfaces.Repositories
{
    public interface ISiteUserRepository
    {
        Task<IdentityResult> CreateUser(SiteUser user, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteUser(SiteUser user, CancellationToken cancellationToken);
        Task<SiteUser> FindByEmail(string normalizedEmail, CancellationToken cancellationToken);
        Task<SiteUser> FindById(string userId, CancellationToken cancellationToken);
        Task<SiteUser> FindByName(string normalizedUserName, CancellationToken cancellationToken);
        IQueryable<SiteUser> GetUserList();
        Task<IdentityResult> UpdateUser(SiteUser user, CancellationToken cancellationToken);
    }
}