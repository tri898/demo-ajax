using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin_0306181083.Models
{
    public class Post
    {
        [Key]
        public int ID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(maximumLength: 200, MinimumLength = 10, ErrorMessage = "Độ dài chuỗi từ 10 đến 200")]
        public string Title { get; set; }
        [Required]
        public string Img { get; set; }      
        [Required]
        [StringLength(maximumLength: 5000, MinimumLength = 100, ErrorMessage = "Nội dung bài viết từ 100 đến 5000 kí tự")]
        public string Content { get; set; }
        [Required]
        public int CategoryPost { get; set; }
        [ForeignKey("CategoryPost")]
        public virtual Category Loai{ get; set; }
     
    }
}
