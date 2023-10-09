//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using System.ComponentModel.DataAnnotations;

namespace HMS.Data.Entities
{
    public class Hall
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "هذا الحقل الزامي")]
        [MaxLength(100)]
        [Display(Name = "اسم القاعة")]
        public string Name { get; set; }

        [Required(ErrorMessage = "هذا الحقل الزامي")]
        [Display(Name = "رقم القاعة")]
        public int Number { get; set; }

        [Required(ErrorMessage ="يجب اخيار المبنى التي توجد به القاعة المرد اضافتها")]
        [Display(Name="مينى القاعة")]
        public int Building_ID { get; set; }

    }
}
