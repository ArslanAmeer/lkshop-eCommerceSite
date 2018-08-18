using System;

namespace FYProject1Classes.CartManagment
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public int Sale { get; set; }

        public string ImageURL { get; set; }

        public int Quantity { get; set; }

        public int Amount
        {
            get
            {
                if (Sale > 0)
                {
                    int v = Sale / 100;
                    return (int)((Price * Quantity) - (Price * Quantity * v));
                }

                return (int)(Price * Quantity);
            }
        }
    }
}
