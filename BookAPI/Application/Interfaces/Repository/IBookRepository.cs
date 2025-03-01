using Domain.Entities.Entities;

namespace Application.Interfaces.Repository
{
    public interface IBookRepository : IRepository
    {
        public void AddBook(Book newBook);

        public Book? GetBookById(int bookId);

        public Book? GetBookByTitle(string title);
        public IEnumerable<Book> GetAllBooks();

        public void UpdateBook(Book book);

    }
}
