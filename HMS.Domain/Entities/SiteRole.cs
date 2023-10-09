//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using HMS.Domain.Entities.Shared;
using System.ComponentModel.DataAnnotations;

namespace HMS.Domain.Entities
{
    public class SiteRole : BaseEntity
    {
        public int Id { get; set; }


		[Required(ErrorMessage = "يرجى كتابة اسم الصلاحية")]
		[Display(Name = "اسم دور المستخدم في النظام")]
		[MaxLength(100, ErrorMessage = "لقد تخطيت اقصى عدد مسموح للحروف المدخلة")]
		[MinLength(3, ErrorMessage = "اقل حد مسموح للوصف 3 حرف")]
		public string Name { get; set; }

       
        public string NormalizedName { get; set; }

		/// <summary>
		/// Describe the roles purpose etc...
		/// </summary>
		/// 

		[Required(ErrorMessage = "الرجاء ادخال وصف مناسب للصلاحية")]
		[Display(Name = "وصف دور المستخدم في النظام")]
		[MaxLength(100,ErrorMessage ="لقد تخطيت اقصى عدد مسموح للحروف المدخلة")]
		[MinLength(15,ErrorMessage ="اقل حد مسموح للوصف 15 حرف")]
		public string Description { get; set; }
    }
}
