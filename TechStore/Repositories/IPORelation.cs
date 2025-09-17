using TechStore.Models;

namespace TechStore.Repositories
{
    public interface IPORelation
    {
        public List<ProductOrderRelation> GetAll();
    }
}
