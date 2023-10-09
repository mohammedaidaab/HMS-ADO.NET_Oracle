////////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab 
//For : King Faisual University 
//Under : ISB integrated sulution business Company 
//Halls ManageMent System 
/////////////////////////////////////////////////////


using HMS.Domain.Entities;
using HMS.Domain.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HMS.Domain.Interfaces.Repositories
{
    public interface IPermissionRepository
    {
        public Task<IEnumerable<Permission>> GetAll();
        public Task<List<Permission>> GetAllByRole(SiteRole role);

		public Task<List<Permission>> GetByName(string[] permissions);
		public Task<BaseResponse> update(List<Permission> permission, int roleId);
		public Task<Permission> GetSingleByName(string permition);

		public Task<bool> hasPermission(string id, string permission, CancellationToken cancellation);
	}
}
