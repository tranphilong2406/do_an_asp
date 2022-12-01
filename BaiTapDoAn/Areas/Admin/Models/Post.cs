using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BaiTapDoAn.Areas.Admin.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        [DisplayName("Tiêu đề")]
        [Required(ErrorMessage ="Tiêu đề không đươc bỏ trống")]
        public string Title { get; set; } = null!;
        [DisplayName("Mô tả ngắn")]
        [Required(ErrorMessage = "Mô tả ngắn không đươc bỏ trống")]
        public string ShortDecription { get; set; } = null!;
        [DisplayName("Nội dung")]
        [Required(ErrorMessage = "Nội dung không được bỏ trống không đươc bỏ trống")]
        public string FullContent { get; set; } = null!;
        [DisplayName("Hình ảnh")]
        public string? Img { get; set; }
        [DisplayName("Trạng thái")]
        public int Status { get; set; }
        [DisplayName("Chuyên mục")]
        public int CatId { get; set; }
        [DisplayName("Tác giả")]
        public string Author { get; set; } = null!;

        public virtual Member? AuthorNavigation { get; set; }
        public virtual Category? Cat { get; set; }
    }
}
