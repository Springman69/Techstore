using TechStore.Models;

namespace TechStore.Repositories
{
	public interface ICategoryRepository
	{
		List<Category> ReadCategories();
		public bool CreateCategory(Category entity);

		public Category GetCategoryById(int categoryId);

    }
}
