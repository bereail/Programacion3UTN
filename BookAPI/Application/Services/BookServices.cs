using Application.Dtos.BookDTOs;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;

namespace Application.Data.Implementations
{
    public class BookServices : IBookService
    {
        private readonly IBookRepository _bookRepository;
        internal readonly IMapper _mapper;

        public BookServices(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public IEnumerable<BookGetDTO> GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks();

            return _mapper.Map<IEnumerable<BookGetDTO>>(books);
        }

        public IEnumerable<BookGetDTO> GetBookByTitle(string title)
        {
            var books = _bookRepository.GetAllBooks()
                .Where(b => b.Title.ToLower().Contains(title.ToLower()))
                .ToList();

            if (!books.Any())
            {
                return Enumerable.Empty<BookGetDTO>(); //200 OK
            }

            return _mapper.Map<IEnumerable<BookGetDTO>>(books);
        }



        public BookDTO? AddBook(BookToCreateDTO bookToCreateDTO)
        {
            if (string.IsNullOrWhiteSpace(bookToCreateDTO.Title) || bookToCreateDTO.Stock < 0)
            {
                return null; 
            }           
            var existingBook = _bookRepository.GetAllBooks()
                .FirstOrDefault(b => b.Title.ToLower() == bookToCreateDTO.Title.ToLower());

            if (existingBook != null)
            {
                return null; 
            }

            var newBook = _mapper.Map<Book>(bookToCreateDTO);
            _bookRepository.AddBook(newBook);
            _bookRepository.SaveChanges();
            return _mapper.Map<BookDTO>(newBook);
        }

        public BookDTO? GetBookById(int bookId)
        {
            try
            {
                var book = _bookRepository.GetBookById(bookId);
                if (book == null)
                {
                    return null;
                }
                return _mapper.Map<BookDTO>(book);
            }
            catch (Exception ex)
            {
              
                throw; 
            }
        }


        public BookDTO? EditBook(int bookId, BookToUpdateDTO newUpdateBookDTO)
        {
           
            var book = _bookRepository.GetBookById(bookId);
            if (book == null)
            {
                return null; 
            }

          
            _mapper.Map(newUpdateBookDTO, book);

          
            _bookRepository.SaveChanges();

           
            return _mapper.Map<BookDTO>(book);
        }


        //Para desactivar un libro que no se visible para el user, modifica su stock a 0
        public BookDTO? DisableBook(int bookId)
        {
          
            var bookToUpdate = _bookRepository.GetBookById(bookId);

            if (bookToUpdate != null)
            {
                
                bookToUpdate.Stock = 0;

               
                _bookRepository.UpdateBook(bookToUpdate);

               
                return _mapper.Map<BookDTO>(bookToUpdate);
            }

            
            return null;
        }


        public BookDTO? UpdateBookStock(int bookId, int newStock)
        {
            var bookToUpdate = _bookRepository.GetBookById(bookId);

            if (bookToUpdate == null)
            {
                return null;
            }

            if (newStock < 0)
            {
                throw new ArgumentException("El stock no puede ser negativo.");
            }

            bookToUpdate.Stock = newStock;
            _bookRepository.UpdateBook(bookToUpdate); 

            return _mapper.Map<BookDTO>(bookToUpdate);
        }


        public int GetBookStock(int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
        
            if (book == null)
            {
                return 0; 
            }

            return book.Stock; 
        }
    }
}
