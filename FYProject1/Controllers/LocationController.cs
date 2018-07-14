using FYProject1Classes.LocationMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FYProject1Classes;

namespace FYProject1.Controllers
{
    public class LocationController : Controller
    {
        // -- Countries ---
        public ActionResult LocationManagment()
        {
            List<Country> countries = new LocationHandler().GetCountries();
            return View(countries);
        }

        public ActionResult AddCountry()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCountry([Bind(Include = "Id,Name,CountryCode,Image_URL")] Country country)
        {
            if (ModelState.IsValid)
            {
                new LocationHandler().AddCountry(country);
                return RedirectToAction("LocationManagment");
            }
            return View(country);
        }

        public ActionResult DeleteCountry(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = new LocationHandler().GetCountryById(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }


        [HttpPost, ActionName("DeleteCountry")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCountryConfirmed(int id)
        {
            Country country = new LocationHandler().GetCountryById(id);
            new LocationHandler().DeleteCountry(country);
            return RedirectToAction("LocationManagment");
        }

        // -- Cities ---

        public ActionResult CityList(int id)
        {
            List<City> cities = new LocationHandler().GetCitiesByCountryId(id);
            ViewBag.CountryID = id;
            ViewBag.CountryName = new LocationHandler().GetCountryById(id).Name;
            return View(cities);
        }

        public ActionResult AddCity(int id, string countryName)
        {
            ViewBag.CountryID = id;
            ViewBag.CountryName = countryName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCity([Bind(Include = "Id,Name")] City city, int id)
        {
            City c = new City();
            if (ModelState.IsValid)
            {
                c.Name = city.Name;
                c.Country = new LocationHandler().GetCountryById(id);
                new LocationHandler().AddCity(c);
                return RedirectToAction("CityList", new { Id = id });
            }
            return View();
        }

        public ActionResult DeleteCity(int? id, int countryId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            City city = new LocationHandler().getCityById(id);

            if (city == null)
            {
                return HttpNotFound();
            }
            ViewBag.countryId = countryId;
            TempData["countryId"] = countryId;
            return View(city);
        }

        [HttpPost, ActionName("DeleteCity")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCityConfirmed(int id)
        {
            City city = new LocationHandler().getCityById(id);
            new LocationHandler().DeleteCity(city);
            return RedirectToAction("CityList", new { Id = Convert.ToUInt32(TempData["countryId"]) });
        }

        public int GetCountriesCount()
        {
            return (new DBContextClass().Countries.Count());
        }

    }
}
