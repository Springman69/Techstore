using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Repositories
{
    public class ProductSpecificationRepository : IProductSpecificationRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductSpecificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ProductSpecification> GetAll()
        {
            return _context.ProductSpecification.ToList() ?? new List<ProductSpecification>();
        }
        public List<ProductSpecification> ReadProducts()
        {
			return _context.ProductSpecification?.ToList() ?? new List<ProductSpecification>();
		}

        public ProductSpecification GetSpecificationByProductId(int productId)
        {
            return _context.ProductSpecification.FirstOrDefault(x => x.ProductId == productId);
        }
        public ProductSpecification GetByProductId(int productId)
        {
            return _context.ProductSpecification.FirstOrDefault(ps => ps.ProductId == productId);
        }

        public async Task AddProductSpecificationAsync(ProductSpecification productSpecification)
        {
            _context.ProductSpecification.Add(productSpecification);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductSpecification(ProductSpecification productSpecification)
        {
            _context.ProductSpecification.Update(productSpecification);
            await _context.SaveChangesAsync();
        }
    }
}