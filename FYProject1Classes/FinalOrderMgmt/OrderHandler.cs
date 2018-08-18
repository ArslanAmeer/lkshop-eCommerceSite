using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYProject1Classes.FinalOrderMgmt
{
    public class OrderHandler : IDisposable
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }
    }
}
