using Domain.Interfaces;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infraestructure.Repositories
{
    public class BookRepository : Repository, IBookRepository
    {
        public BookRepository(ApplicationContext context, IBookRepository bookRepository) : base(context)
        {
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books
                .OrderBy(p => p.Genre)
                .ThenBy(p => p.Title)
                .ToList();
        }

        public Book? GetBookById(int bookId)
        {
            return _context.Books.Find(bookId);
        }

        public void AddBook(Book newBook)
        {
            _context.Books.Add(newBook);
        }
    }
}
