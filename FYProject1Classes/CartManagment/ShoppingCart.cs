using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYProject1Classes.CartManagment
{
    public class ShoppingCart
    {
        private List<ShoppingCartItem> items;
        public ShoppingCart()
        {
            items = new List<ShoppingCartItem>();
        }

        public List<ShoppingCartItem> Items
        {
            get { return items; }
        }

        public void Add(ShoppingCartItem newItem)
        {
            ShoppingCartItem itemFound = items.Find(i => i.Id == newItem.Id);
            if (itemFound == null)
            {
                items.Add(newItem);
                items.TrimExcess();
            }
            else
            {
                itemFound.Quantity += newItem.Quantity;
            }
        }

        public void Remove(int id)
        {
            items.RemoveAt(items.FindIndex(i => i.Id == id));
        }

        public void Update(int id, int qty)
        {
            ShoppingCartItem itemFound = items.Find(i => i.Id == id);
            if (itemFound != null)
            {
                itemFound.Quantity = qty;
            }
        }

        public int NumberOfItems
        {
            get
            {
                return items.Count();
            }
        } 

    }
}
