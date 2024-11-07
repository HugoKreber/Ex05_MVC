using BO;
using Exercice_5_MVC.Service.Exercice_4_MVC.Service;

namespace Ex05_MVC.Service
{
    public class OrderService
    {
        private static ApplicationDbContext dbContext = new ApplicationDbContext();

        public void Add(Order order)
        {
            //incr ID
            int maxIdOrder = GetOrders().OrderByDescending(o => o.Id).FirstOrDefault().Id;
            order.Id = maxIdOrder + 1;

            
            Warehouse warhouseOfORder = dbContext.Warehouses.SingleOrDefault(w => w.Id == order.WarehouseId);
            warhouseOfORder.Orders.Add(order);
            dbContext.Orders.Add(order); 
        }

        public List<Order> GetOrders()
        {
            return dbContext.Orders;
        }
        
        public void Remove(Order order)
        {
            dbContext.Orders.Remove(order);
        }

        internal void Update(Order orderUpdated)
        {
            int index = dbContext.Orders.FindIndex(o => o.Id == orderUpdated.Id);


            if (index != -1)
            {
                dbContext.Orders[index] = orderUpdated;
            }
        }
    }
}
