using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYProject1Classes.ProductMgmt
{
    public class Camera
    {
        public Camera()
        {
            Images = new List<CameraImages>();
        }
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Stock { get; set; }
        public Brand Brand { get; set; }
        public Series Series { get; set; }
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
        public string Level { get; set; }
        public float Price { get; set; }
        public float Sale { get; set; }
        public string Kit { get; set; }
        public string MegaPixel { get; set; }
        public string SensorFormat { get; set; }
        public string SensorType { get; set; }
        public string FocusSystem { get; set; }
        public string ISORange { get; set; }
        public string VideoRecording { get; set; }
        public string ShutterSpeed { get; set; }
        public string VFType { get; set; }
        public string ImageProcessor { get; set; }
        public string LCDType { get; set; }
        public string LCDDetail { get; set; }
        public string BurstShot { get; set; }
        public string LensMount { get; set; }
        public bool Wifi { get; set; }
        public bool Bluetooth { get; set; }
        public bool GPS { get; set; }
        public bool ExtMic { get; set; }
        public bool BuiltinFlash { get; set; }
        public bool WeatherSeal { get; set; }
        public bool cardslots { get; set; }
        public string Description { get; set; }
        public Nullable<DateTime> AnnounceDate { get; set; }
        public ICollection<CameraImages> Images { get; set; }

    }
}
