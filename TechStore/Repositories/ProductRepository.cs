using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> ReadProducts()
        {
            return _context.Products.ToList();
        }

        public async Task DeleteProductAsync(int productId)
        {
            var productToDelete = await _context.Products.FindAsync(productId);

            if (productToDelete != null)
            {
                var productSpecificationToDelete = await _context.ProductSpecification
                    .FirstOrDefaultAsync(x => x.ProductId == productId);

                if (productSpecificationToDelete != null)
                {
                    _context.ProductSpecification.Remove(productSpecificationToDelete);
                }

                _context.Products.Remove(productToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

     
		public Product GetProductById(int id)
		{
			return _context.Products.FirstOrDefault(p => p.Id == id);
		}

        public bool ChangeProduct(Product entity)
        {
            bool isCreated = false;
            _context.Products.Add(entity);

            if (_context.SaveChanges() == 1)
            {
                isCreated = true;
            }
            return isCreated;

        }
        public bool UpdateProduct(Product product)
        {
            try
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
        
  