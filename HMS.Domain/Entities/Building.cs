//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using System.ComponentModel.DataAnnotations;

namespace HMS.Domain.Entities
{
    public class Building
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "هذا الحقل الزامي"), MaxLength(100)]
        [Display(Name = "اسم المبنى")]
        public string Name { get; set; }

        [Required(ErrorMessage = "هذا الحقل الزامي")]
        [Display(Name = "رقم المبنى")]
        public int Number { get; set; }

        [Required(ErrorMessage = "يجب اخيار الكلية التي يتبع لها االمبنى")]
        [Display(Name = "كلية المبنى")]
        public int Collage_ID { get; set; }

    }
}
