//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities.ViewModels
{
    public class BuildingCollegeVM
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "هذا الحقل الزامي")]
        [MaxLength(500)]
        [Display(Name = "اسم المبنى")]
        public string buldingName { get; set; }

        [Required(ErrorMessage = "هذا الحقل الزامي")]
        [Display(Name = "رقم المبنى")]
        public int buldingnumber { get; set; }

        [Display(Name = "الكليةالتي يتبع لها المبنى")]
        public string BuldingCollageName { get; set; }


        [Required(ErrorMessage = "يجب اخيار الكلية التي يتبع لها االمبنى")]
        [Display(Name = "الكليةالتي يتبع لها المبنى")]
        public int BuldingCollageNumber { get; set; }
    }
}
