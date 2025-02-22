using Domain.Models.Entities;

namespace Domain.Interfaces
{
    public interface IBookRepository : IRepository
    {
        public IEnumerable<Book> GetAllBooks();
        public Book? GetBookById(int bookId);
        public void AddBook(Book newBook);
    }
}
