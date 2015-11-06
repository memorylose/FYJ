using System.ComponentModel.DataAnnotations;

namespace FYJ.Model
{
    public class AccountModel
    {
        public int UserId { get; set; }
        public string Photo { get; set; }
        public string City { get; set; }
        public string Contact { get; set; }
        public string Favorite { get; set; }
        public string Description { get; set; }
        public string NickName { get; set; }

    }

    public class ChangePassword
    {
        [Required(ErrorMessage = "原始密码不能为空")]
        public string Password { get; set; }

        [Required(ErrorMessage = "新密码不能为空")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "密码长度为6 - 16位")]
        [Compare("ConfirmPassword", ErrorMessage = "两次密码不一致")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "确认密码不能为空")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "验证码不能为空")]
        public string VerifyCode { get; set; }
    }
}
