using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BaiTapDoAn.Areas.Admin.Models
{
    public partial class Member
    {
        public Member()
        {
            Posts = new HashSet<Post>();
        }
        [DisplayName("Tài khoản")]
        public string Username { get; set; } = null!;
        [DisplayName("Mật khẩu")]
        public string Password { get; set; } = null!;
        [DisplayName("Quyền hạn")]
        public int Role { get; set; }
        [DisplayName("Trạng thái")]
        public int Status { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
