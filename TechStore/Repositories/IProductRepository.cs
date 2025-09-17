using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechStore.Models;

namespace TechStore.Repositories
{
    public interface IProductRepository
    {
        List<Product> ReadProducts();
        Task DeleteProductAsync(int productId);
		Product GetProductById(int id);
		public Task AddProductAsync(Product product);
        Task SaveChangesAsync();

        public bool ChangeProduct(Product entity);

        public bool UpdateProduct(Product product);

    }
}