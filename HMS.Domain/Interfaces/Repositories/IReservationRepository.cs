﻿//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using HMS.Domain.Entities;
using HMS.Domain.Entities.Shared;
using HMS.Domain.Entities.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        public Task<BaseResponse> create(Reservation reservation);

        public Task<IEnumerable<ReservationHallVM>> GetAll();
        public IEnumerable<ReservationHallVM> GetReservationPagination(int start, string searchvalue,int Length, string SortColumn, string sortDirection,int pagenumber);


        public Task<IEnumerable<ReservationHallVM>> GetByUserId(string id);


        public Task<ReservationHallVM> GetById(int id);


        public Task<BaseResponse> update(Reservation reservation);

        public Task<BaseResponse> Delete(int id);

	}
}
