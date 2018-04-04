using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYProject1Classes.FinalOrderMgmt
{
    public class OrderHandler
    {
        private readonly DBContextClass _db = new DBContextClass();

        public void AddOrder(FinalOrder order)
        {
            using (_db)
            {
                _db.FinalOrders.Add(order);
                _db.SaveChanges();
            }
        }
    }
}
