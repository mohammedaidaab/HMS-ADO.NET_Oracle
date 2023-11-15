using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities.ViewModels
{
    public class ReservationHallPagingVM
    {
        public List<ReservationHallVM> reservations { get; set; }

        public int totalPages { get; set; }

        public int CurrentPage { get; set; }

    }
}
