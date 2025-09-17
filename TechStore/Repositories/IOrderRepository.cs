using TechStore.Models;

namespace TechStore.Repositories
{
    public interface IOrderRepository
    {
        public List<Order> GetAll();
        public void Update(Order order);
    }
}
