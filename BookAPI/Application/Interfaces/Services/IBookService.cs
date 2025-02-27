using Application.Dtos.BookDTOs;
using Domain.Models.Entities;
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
        public Book? GetBookByTitle(string title);
        public IEnumerable<BookDTO> GetAllBooks();
        public BookDTO? UpdateBookStock(int newStock, int bookId);

        public BookDTO? EditBook(int bookId, BookToUpdateDTO newUpdateBookDTO);
        public BookDTO? DisableBook(int bookId);

        public int GetBookStock(int bookId);
        /*public bool VerificateBookStock(int bookid, int requiredStock);*/
    }
}
