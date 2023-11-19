using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities.ViewModels
{
    public class HallBuildingPagingVM
    {
        public List<HallBuildingVM> HallBuildings { get; set; }

        public int totalPages { get; set; }

        public int CurrentPage { get; set; }

    }
}
