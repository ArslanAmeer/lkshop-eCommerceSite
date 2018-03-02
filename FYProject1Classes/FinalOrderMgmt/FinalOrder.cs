using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FYProject1Classes.CartManagment;

namespace FYProject1Classes.FinalOrderMgmt
{
    public class FinalOrder
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FullAddress { get; set; }

        public long Phone { get; set; }

        public virtual ShoppingCartItem ShoppingCartItem { get; set; }

    }
}
