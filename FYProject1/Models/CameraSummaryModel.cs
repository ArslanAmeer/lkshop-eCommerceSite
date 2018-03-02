using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYProject1.Models
{
    public class CameraSummaryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public float Price { get; set; }

        public float Sale { get; set; }

        public int Brand_Id { get; set; }

        public string Brand { get; set; }

        public string Series { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string Level { get; set; }

        public string Kit { get; set; }

        public bool gps { get; set; }

        public bool wifi { get; set; }

        public bool bluetooth { get; set; }

        public DateTime RelDate { get; set; }

        public string image_Url { get; set; }
    }
}