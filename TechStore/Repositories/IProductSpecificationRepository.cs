using TechStore.Models;

namespace TechStore.Repositories
{
	public interface IProductSpecificationRepository
	{
		public List<ProductSpecification> GetAll();
		public ProductSpecification GetSpecificationByProductId(int productId);
		ProductSpecification GetByProductId(int productId);
		public Task AddProductSpecificationAsync(ProductSpecification productSpecification);
		public Task SaveChangesAsync();

		public Task UpdateProductSpecification(ProductSpecification productSpecification);
    }
}
