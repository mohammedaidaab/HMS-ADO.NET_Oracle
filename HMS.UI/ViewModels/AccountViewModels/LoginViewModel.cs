using System.ComponentModel.DataAnnotations;

namespace HMS.MVC.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
		[Required(ErrorMessage ="الرجاء التحقق من البريد الالكتروني")]
		[EmailAddress(ErrorMessage ="الصيغة المدخلة لا تطابقة صيغة البردي الاكتروني الصحيحة ")]
        [Display(Name ="البريد الالكتروني")]
        public string Email { get; set; }

        [Required(ErrorMessage ="الرجاء كتابة كلمة المرور الخاصة بك ")]
        [DataType(DataType.Password)]
		[Display(Name = "كلمة المرور")]
		public string Password { get; set; }

        [Display(Name = "تذكرني ")]
        public bool RememberMe { get; set; }
    }
}
