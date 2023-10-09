using System.ComponentModel.DataAnnotations;

namespace HMS.MVC.ViewModels.ManageViewModels
{
    public class IndexViewModel
    {
        [Display(Name="اسم المستخدم")]
        public string Username { get; set; }

        [Display(Name = "الاسم الاول")]
        public string FirstName { get; set; }
        [Display(Name = "اسم العائلة")]
        public string Surname { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Display(Name = "البريد الاكتروني")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Phone]
        [Display(Name = "رقم الهاتف")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }

        
    }
}
