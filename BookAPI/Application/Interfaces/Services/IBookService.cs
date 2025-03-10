using Application.Dtos.BookDTOs;
using Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IBookService
    {
        public BookDTO? AddBook(BookToCreateDTO bookToCreateDTO);
        public BookDTO? GetBookById(int bookId);
        public IEnumerable<BookGetDTO> GetBookByTitle(string title);
        public IEnumerable<BookGetDTO> GetAllBooks();
        public BookDTO? UpdateBookStock(int newStock, int bookId);

        public BookDTO? EditBook(int bookId, BookToUpdateDTO newUpdateBookDTO);
        public BookDTO? DisableBook(int bookId);

        public int GetBookStock(int bookId);
      
    }
}
