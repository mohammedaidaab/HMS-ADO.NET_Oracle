//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using HMS.Data.Entities;
using HMS.Domain.Entities.Shared;
using HMS.Domain.Entities.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HMS.Domain.Interfaces.Repositories
{
    public interface IHallrepository
    {
        Task<IEnumerable<HallBuildingVM>> GetAll();

        Task<Hall> GetById(int Id);

        Task<BaseResponse> Add(Hall model);

        Task<BaseResponse> Update(Hall model);

        Task<BaseResponse> Delete(int Id);

    }
}
