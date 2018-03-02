using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYProject1Classes.LocationMgmt
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Country Country { get; set; }

    }
}
