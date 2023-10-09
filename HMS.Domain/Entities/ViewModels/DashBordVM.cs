//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities.ViewModels
{
    public class DashBordVM
    {
        public int Number_of_halls { get; set; }

        public int Number_Of_buildings { get; set; }

        public int Number_Of_reservations { get; set; }

        public int Number_of_active_halls { get; set; }

        public DateTime Lastreservation { get; set; }

        public IEnumerable<ReservationHallVM> reservation { get; set; }
       

    }
}
