//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using HMS.Domain.Entities;
using HMS.Domain.Entities.Shared;
using HMS.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Interfaces.Repositories
{
    public interface IBuildingRepository
    {

        public Task<IEnumerable<BuildingCollegeVM>> GetAll();
        public Task<BaseResponse> create(BuildingCollegeVM buildingCollegeVM);
        Task<BuildingCollegeVM> GetById(int id);
        public Task<BaseResponse> update(Building building);
        public Task<BaseResponse> delete(int id);
        public BuildingCollegePagingVM GetAllpaging(Nullable<int> pageno, string filter, Nullable<int> pagesize, string sorting, string sortOrder);

    }
}
