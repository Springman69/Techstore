using TechStore.Data;
using TechStore.Models;

namespace TechStore.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

		public bool CreateCategory(Category entity)
		{
			bool isCreated = false;
			_context.Categories.Add(entity);

			if (_context.SaveChanges() == 1)
			{
				isCreated = true;
			}
			return isCreated;
			
		}

		public List<Category> ReadCategories()
		{
            return _context.Categories?.ToList() ?? new List<Category>();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories.Find(categoryId);
        }
    }
}
