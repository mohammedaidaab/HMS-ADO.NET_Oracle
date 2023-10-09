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

namespace HMS.Domain.Entities
{
    public class Reservation
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "يرجى ادخال اسم حجز القاعة")]
        [MaxLength(100)]
        [Display(Name = "اسم الحجز")]
        public String Name { get; set; }

        [Required(ErrorMessage = "الرجاء اختيار القاعة")]
        [Display(Name = "اسم القاعة")]
        public int Hall_Id { get; set; }

        [Required(ErrorMessage = "الرجاء التحقق من تاريخ حجز القاعة")]
        [Display(Name = "تاريخ الحجز")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "يرجى التحقق من وقت بدا حجز القاعة")]
        [Display(Name = " بداية وقت الحجز")]
        public DateTime Time_Start { get; set; }

        [Required(ErrorMessage = "الرجاء التحقق من وقت انتهاء حجز القاعة")]
        [Display(Name = " نهاية وقت الحجز")]
        public DateTime Time_End { get; set; }

        [Required(ErrorMessage = "الرجاء التحقق من اسم مستخدم القاعة")]
        [Display(Name = "اسم المستخدم")]
        public int User_id { get; set; }
    }

}
