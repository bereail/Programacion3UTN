using Application.Interfaces.Repository;
using Domain.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace Infraestructure.Repositories
{
    public class BookRepository : Repository, IBookRepository
    {
        public BookRepository(ApplicationContext context) : base(context)
        {
        }

        public void AddBook(Book newBook)
        {
            _context.Books.Add(newBook);
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


        public void UpdateBook(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }



        public Book? GetBookByTitle(string title)
        {
            return _context.Books.FirstOrDefault(b => b.Title == title);
        }

    }
}
