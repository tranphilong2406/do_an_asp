using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BaiTapDoAn.Areas.Admin.Models
{
    public partial class Category
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        [DisplayName("Tên chuyên mục")]
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        [DisplayName("Trạng thái")]
        public int Status { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
