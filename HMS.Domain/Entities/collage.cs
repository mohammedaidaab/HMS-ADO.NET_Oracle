//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using System;
using System.ComponentModel.DataAnnotations;

namespace HMS.Data
{
    public class collage
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "هذا الحقل الزامي")]
        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "اسم الكلية")]
        public String Name { get; set; }

        [Required(ErrorMessage = "هذا الحقل الزامي")]
        [Display(Name = "كود الكلية")]
        public int Code { get; set; }
    }
}
