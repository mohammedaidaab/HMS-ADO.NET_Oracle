using HMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities.ViewModels
{
    public class BuildingCollegePagingVM
    {
        public List<BuildingCollegeVM> colleges { get; set; }

        public int totalPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
