using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BaiTapDoAn.Areas.Admin.Models
{
    public partial class Member
    {
        public Member()
        {
            Posts = new HashSet<Post>();
        }
        [DisplayName("Tài khoản")]
        [Required]
        public string Username { get; set; } = null!;
        [DisplayName("Mật khẩu")]
        [Required]
        public string Password { get; set; } = null!;
        [DisplayName("Quyền hạn")]
        [Required]
        public int Role { get; set; }
        [DisplayName("Trạng thái")]
        [Required]
        public int Status { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
