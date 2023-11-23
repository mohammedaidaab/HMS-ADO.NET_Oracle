//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using HMS.Data;
using HMS.Domain.Entities.Shared;
using HMS.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HMS.Domain.Interfaces.Repositories
{
    public interface ICollageRepository
    {
        Task<IEnumerable<collage>> GetAll();
        Task<collage> GetById(int Id);
        Task<BaseResponse> Add(collage model);
        Task<BaseResponse> Update(collage model);
        Task<BaseResponse> Delete(int Id);
        public CollegePAgingVM GetAllpaging(Nullable<int> pageno, string filter, Nullable<int> pagesize, string sorting, string sortOrder);

    }
}
