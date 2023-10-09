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
    public interface ISiteRoleRepository
    {
        Task<IdentityResult> CreateAsync(SiteRole role, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteAsync(SiteRole role, CancellationToken cancellationToken);
        Task<SiteRole> FindByIdAsync(string roleId, CancellationToken cancellationToken);
        Task<SiteRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken);
        Task<IList<string>> GetRolesByUserIdAsync(SiteUser user, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateAsync(SiteRole role, CancellationToken cancellationToken);

        Task<IEnumerable<SiteRole>> GetAllRoles();

        public Task<SiteRole> GetRolesByUserId(SiteUser user, CancellationToken cancellationToken);

	}
}