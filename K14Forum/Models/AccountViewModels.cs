using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace K14Forum.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage ="Bạn chưa nhập địa chỉ email.")]
        [Display(Name = "Địa chỉ email")]
        [EmailAddress(ErrorMessage ="Địa chỉ email không hợp lệ.")]
        public string Email { get; set; }

        [Display(Name = "Họ tên mặc định")]
        public string DefaultUserName { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage ="Bạn chưa nhập địa chỉ email.")]
        [Display(Name = "Địa chỉ email (*)")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ email.")]
        [Display(Name = "Địa chỉ email (*)")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu (*)")]
        public string Password { get; set; }

        [Display(Name = "Nhớ đăng nhập?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Bạn chưa nhập họ tên.")]
        [Display(Name = "Họ tên (*)")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ email.")]
        [EmailAddress(ErrorMessage = ("Địa chỉ email không hợp lệ."))]
        [Display(Name = "Địa chỉ email (*)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu.")]
        [StringLength(100, ErrorMessage = "{0} phải có ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu (*)")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu (*)")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác thực không khớp.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ email.")]
        [EmailAddress(ErrorMessage = ("Địa chỉ email không hợp lệ."))]
        [Display(Name = "Địa chỉ email (*)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu.")]
        [StringLength(100, ErrorMessage = "{0} phải có ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu (*)")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu (*)")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác thực không khớp.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ email.")]
        [EmailAddress(ErrorMessage = ("Địa chỉ email không hợp lệ."))]
        [Display(Name = "Địa chỉ email (*)")]
        public string Email { get; set; }
    }
}
