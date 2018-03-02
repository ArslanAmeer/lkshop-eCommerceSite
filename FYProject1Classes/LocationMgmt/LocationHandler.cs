using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYProject1Classes.LocationMgmt
{
    public class LocationHandler
    {
        public List<Country> GetCountries()
        {
            DBContextClass db = new DBContextClass();
            using (db)
            {
                return (from u in db.Countries select u).ToList();
            } 
        }
        public Country GetCountryById(int? id)
        {
            DBContextClass db = new DBContextClass();
            using (db)
            {
                return (from c in db.Countries where c.Id == id select c).FirstOrDefault();
            }
        }
        public void AddCountry(Country country)
        {
            DBContextClass db = new DBContextClass();
            using (db)
            {
                db.Countries.Add(country);
                db.SaveChanges();
            }
        }

        public void DeleteCountry(Country country)
        {
            DBContextClass db = new DBContextClass();
            using (db)
            {
                db.Entry(country).State = EntityState.Deleted;
                db.Countries.Remove(country);
                db.SaveChanges();
            }
        }

        public List<City> GetCities(Country country)
        {
            DBContextClass db = new DBContextClass();
            using (db)
            {
                return (from u in db.Cities
                        where u.Country.Id==country.Id
                        select u).ToList();
            }
        }

        public City getCityById(int? id)
        {
            DBContextClass db = new DBContextClass();
            using (db)
            {
                return (from c in db.Cities where c.Id == id
                        select c).FirstOrDefault();
            }
        }
       
        public List<City> GetCitiesByCountryId(int id)
        {
            DBContextClass db = new DBContextClass();
            using (db)
            {
                return (from c in db.Cities
                        where c.Country.Id == id
                        select c).ToList();
            }
            
        }
        public void AddCity(City city)
        {
            DBContextClass db = new DBContextClass();
            using (db)
            {
                db.Entry(city.Country).State = EntityState.Unchanged;
                db.Cities.Add(city);
                db.SaveChanges();
            }
        }

        public void DeleteCity(City city)
        {
            DBContextClass db = new DBContextClass();
            using (db)
            {
                db.Entry(city).State = EntityState.Deleted;
                db.Cities.Remove(city);
                db.SaveChanges();
            }

        }
    }
}
