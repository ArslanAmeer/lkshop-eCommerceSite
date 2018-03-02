using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYProject1Classes.ProductMgmt
{
    public class CameraImages
    {
        [Key]
        public int Id { get; set; }
        public string Caption { get; set; }
        [Required]
        public string Image_Url { get; set; }

    }
}
