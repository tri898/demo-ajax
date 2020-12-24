using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin_0306181083.Models
{
    public class Category
    {
       
        [Key]
        public int ID { get; set; }
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Loại bài viết từ 5 đến 200 kí tự")]

        public string Name { get; set; }
        public ICollection<Post> lstPost { get; set; }
        
    }
}