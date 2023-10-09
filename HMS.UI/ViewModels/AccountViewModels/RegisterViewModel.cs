using System.ComponentModel.DataAnnotations;

namespace HMS.MVC.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "هذا الحقل الزامي")]
        [Display(Name = "اسم المستخدم")]
        public string Username { get; set; }

        [Required(ErrorMessage="هذا الحقل الزامي")]
        [EmailAddress]
        [Display(Name = "البريد الالكتروني")]
        public string Email { get; set; }


        [Required(ErrorMessage = "هذا الحقل الزامي")]
        [Display(Name = " رقم الهاتف")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage ="هذا الحقل الزامي")]
        [StringLength(100, ErrorMessage = "هذا الحقل يجب ان لا يقل عن 6 خانات", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تاكيد كلمة المرور")]
        [Compare("Password", ErrorMessage = "كلمة المرور يجب ان تتطابق")]
        public string ConfirmPassword { get; set; }

        [Display(Name ="صلاحية المستخدم")]
        public string RoleName{ get; set; }

        public int RoleId { get; set; }







    }
}
