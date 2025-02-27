/*using Microsoft.EntityFrameworkCore;
using Shop.API.Data.Interfaces;
using Shop.API.DBContexts;
using Shop.API.Entities;

namespace Application.Data.Implementations
{
    public class BookRepository : Repository, IProductRepository
    {
        public BookRepository(ShopContext context) : base(context)
        {
        }

        public IEnumerable<Book> GetAllProducts()
        {
            return _context.Products
                .OrderBy(p => p.Category)
                .ThenBy(p => p.Name)
                .ToList();
        }
        public Book? GetProductById(int productId)
        {
            return _context.Products.Find(productId);
        }
        public void AddProduct(Book newProduct)
        {
            _context.Products.Add(newProduct);
        }

    }
}
*/