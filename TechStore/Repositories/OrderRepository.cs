using TechStore.Data;
using TechStore.Models;

namespace TechStore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Order> GetAll()
        {
            return _context.Orders?.ToList() ?? new List<Order>();
        }

        public void Update(Order order)
        {
            _context.Orders?.Update(order);
            _context.SaveChanges();
        }
    }
}
