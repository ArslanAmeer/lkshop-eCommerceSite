using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYProject1Classes.CartManagment
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public float Sale { get; set; }

        public string ImageURL { get; set; }

        public int Quantity { get; set; }

        public int Amount
        {
            get
            {
                if (!(Sale == null) || Sale != 0)
                {
                    float v = Sale / 100;
                    return (int)((Price * Quantity) - (Price * Quantity * v));
                }

                return (int)(Price * Quantity);
            }
        }
    }
}
