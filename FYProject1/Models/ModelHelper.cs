using FYProject1Classes.ProductMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYProject1.Models
{
    public class ModelHelper
    {

        // taking Dyanimc values and adding them into SelectListItem's List

        public static List<SelectListItem> ToSelectItemList(dynamic values)
        {
            List<SelectListItem> templist = null;

            if (values!=null)
            {
                templist = new List<SelectListItem>();

                foreach (var v in values)
                {
                    templist.Add(new SelectListItem { Text = v.Name, Value = Convert.ToString(v.Id)});
                }
                templist.TrimExcess();
            }

            return templist;
        }

        // Getting few data from mobile entity class to Camera Summary Class

        public static CameraSummaryModel ToCameraSummary(Camera camera)
        {
            return new CameraSummaryModel
            {
                Id = camera.Id,
                Title = camera.Title,
                Brand_Id = camera.Brand.Id,
                Brand = camera.Series.Brand.Name,
                Series = camera.Series.Name,
                Level = camera.Level,
                Price = camera.Price,
                Sale = camera.Sale,
                bluetooth = camera.Bluetooth,
                gps = camera.GPS,
                wifi = camera.Wifi,
                Kit = camera.Kit,
                RelDate = (DateTime) camera.AnnounceDate,
                image_Url = (camera.Images.Count > 0) ? camera.Images.First().Image_Url : null  
            };
        }

        // Convert Camera List Items to Camera Summry List Items

        public static List<CameraSummaryModel> ToCameraSummaryList(IEnumerable<Camera> camera)
        {
            List<CameraSummaryModel> camList = new List<CameraSummaryModel>();
            if (camera!=null)
            {
                foreach (var c in camera)
                {
                    camList.Add(ToCameraSummary(c));
                }
                camList.TrimExcess();
            }

            return camList;
        }

    }
}