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
using System.Data.Entity.Core.Objects;
using System.Threading.Tasks;

namespace HMS.Domain.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        public Task<BaseResponse> create(Reservation reservation);

        public Task<IEnumerable<ReservationHallVM>> GetAll();


        public  Task<List<ReservationHallVM>> GetAllpaging(Nullable<int> pageno, string filter, Nullable<int> pagesize, string sorting, string sortOrder);



        public Task<IEnumerable<ReservationHallVM>> GetByUserId(string id);


        public Task<ReservationHallVM> GetById(int id);


        public Task<BaseResponse> update(Reservation reservation);

        public Task<BaseResponse> Delete(int id);

	}
}
