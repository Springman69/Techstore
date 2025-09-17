using TechStore.Data;
using TechStore.Models;

namespace TechStore.Repositories
{
    public class PORelation : IPORelation
    {
        private readonly ApplicationDbContext _context;
        public PORelation(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public List<ProductOrderRelation> GetAll()
        {
            return _context.ProductOrderRelations?.ToList()??new List<ProductOrderRelation>();
        }
    }
}
