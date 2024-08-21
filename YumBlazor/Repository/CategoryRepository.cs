using YumBlazor.Data;
using YumBlazor.Data.Models;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public Category Create(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return category;
        }

        public bool Delete(int id)
        {
            var category = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public Category Get(int id)
        {
            var category = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (category != null)
            {
                return category;
            }
            return new Category();
        }

        public IEnumerable<Category> GetAll()
        {
            var categories = _db.Categories.ToList();
            return categories;
        }

        public Category Update(Category category)
        {
            var categoryFromDb = _db.Categories.FirstOrDefault(u => u.Id == category.Id);
            if (categoryFromDb != null)
            {
                categoryFromDb.Name = category.Name;
                _db.Categories.Update(categoryFromDb);
                _db.SaveChanges();
                return categoryFromDb;
            }
            return category;
        }
    }
}
