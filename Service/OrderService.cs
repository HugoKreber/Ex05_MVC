using BO;

namespace Ex05_MVC.Service
{
    public class OrderService
    {
        private static ApplicationDbContext dbContext = new ApplicationDbContext();

        public void Add(Order order)
        {
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
            var orderBdd = dbContext.Orders.SingleOrDefault(o => o.Id == orderUpdated.Id);
            orderBdd = orderUpdated;
        }
    }
}
