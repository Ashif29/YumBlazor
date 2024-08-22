using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Data.Models;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Product> GetAsync(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(u => u.Id == id);
            if (product != null)
            {
                return product;
            }
            return new Product();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _db.Products.Include(u => u.Category).ToListAsync();
            return products;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var productFromDb = await _db.Products.FirstOrDefaultAsync(u => u.Id == product.Id);
            if (productFromDb != null)
            {
                productFromDb.Name = product.Name;
                productFromDb.Description = product.Description;
                productFromDb.Price = product.Price;
                productFromDb.CategoryId = product.CategoryId;
                productFromDb.ImageUrl = product.ImageUrl;

                _db.Products.Update(productFromDb);
                await _db.SaveChangesAsync();
                return productFromDb;
            }
            return product;
        }
    }
}
