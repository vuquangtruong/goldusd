using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoldUSD.Models
{
    public class EditUserModel
    {
        public Guid? UserId { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string Username { get; set; }

        [StringLength(50, ErrorMessage = "Mật khẩu tối thiểu 6 kí tự và tối đa 50 kí tự", MinimumLength = 6)]
        public string Password { get; set; }

        public DateTime? ExpireDate { get; set; }

        public List<SelectListItem> Months { get; set; }

        public int? SelectedExtendMethod { get; set; }

        public int SelectedMonth { get; set; }

        public string ExtendExpireDate { get; set; }
    }
}