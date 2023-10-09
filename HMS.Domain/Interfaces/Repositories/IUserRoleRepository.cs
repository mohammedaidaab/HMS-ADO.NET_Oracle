//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using HMS.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HMS.Domain.Interfaces.Repositories
{
    public interface IUserRoleRepository
    {
        Task AddUserToRoleAsync(int UserId, int RoleId, CancellationToken cancellationToken);
        Task<List<SiteUser>> GetUsersInRoleAsync(string NormalizedRoleName, CancellationToken cancellationToken);
        Task<bool> IsUserInRoleAsync(int UserId, int RoleId, CancellationToken cancellationToken);
        Task RemoveUserFromRole(int UserId, int RoleId, CancellationToken cancellationToken);
    }
}