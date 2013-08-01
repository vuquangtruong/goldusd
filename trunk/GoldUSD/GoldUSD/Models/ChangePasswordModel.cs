using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoldUSD.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(50,ErrorMessage = "Mật khẩu tối thiểu 6 kí tự và tối đa 50 kí tự", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không giống")]
        public string ConfirmPassword { get; set; }
    }
}