using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Slug { get; set; } = null!;
        [DisplayName("Trạng thái")]
        [Required]
        public int Status { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
