//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using HMS.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;

namespace HMS.Domain.Interfaces.Repositories
{
    public interface IDashBordRepository
    {
        public int gettodayreservations();
        public int getnumberofalls();
        public int GetActiveHalls();
        public List<ReservationHallVM> reservations();
        public DateTime GetLastReservation();

    }
}
