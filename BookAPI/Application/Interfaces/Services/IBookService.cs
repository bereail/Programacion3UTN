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
        BookDTO? AddBook(BookToCreateDTO bookToCreateDTO);
         BookDTO? GetBookById(int bookId);
        IEnumerable<BookGetDTO> GetBookByTitle(string title);
        IEnumerable<BookGetDTO> GetAllBooks();
        BookDTO? UpdateBookStock(int newStock, int bookId);

        BookDTO? EditBook(int bookId, BookToUpdateDTO newUpdateBookDTO);
        BookDTO? DisableBook(int bookId);

        int GetBookStock(int bookId);
      
    }
}
