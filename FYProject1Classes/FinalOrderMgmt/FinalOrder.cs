using System.Collections.Generic;
using FYProject1Classes.CartManagment;

namespace FYProject1Classes.FinalOrderMgmt
{
    public sealed class FinalOrder
    {
        public FinalOrder()
        {
            ShoppingCartItem = new List<ShoppingCartItem>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string FullAddress { get; set; }

        public long Phone { get; set; }

        public ICollection<ShoppingCartItem> ShoppingCartItem { get; set; }

    }
}
