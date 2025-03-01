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
    
        public BookDTO? AddBook(BookToCreateDTO bookToCreateDTO)
        {
            if (bookToCreateDTO.Stock >= 0)
            {
                var newBook = _mapper.Map<Book>(bookToCreateDTO);
                _bookRepository.AddBook(newBook);
                _bookRepository.SaveChanges();
                return _mapper.Map<BookDTO>(newBook);
            }
            return null;
        }
        public BookDTO? GetBookById(int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            return _mapper.Map<BookDTO>(book);
        }

        public Book? GetBookByTitle(string title)
        {
            return _bookRepository.GetBookByTitle(title);
        }


        public IEnumerable<BookDTO> GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks();

            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public BookDTO? EditBook(int bookId, BookToUpdateDTO newUpdateBookDTO)
        {
            // Buscar el libro en la base de datos
            var book = _bookRepository.GetBookById(bookId);
            if (book == null)
            {
                return null; // Retorna null si el libro no existe
            }

            // Mapear los nuevos valores
            _mapper.Map(newUpdateBookDTO, book);

            // Guardar los cambios
            _bookRepository.SaveChanges();

            // Retornar el libro actualizado en formato DTO
            return _mapper.Map<BookDTO>(book);
        }


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
            _bookRepository.UpdateBook(bookToUpdate); // Llamada al repositorio para guardar cambios

            return _mapper.Map<BookDTO>(bookToUpdate);
        }



        public void RemoveStock(int quantity, Book bookToRemoveFromStock)
        {
            bookToRemoveFromStock.Stock -= quantity;
            _bookRepository.SaveChanges();
        }

        public int GetBookStock(int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);

            // Si el libro no existe, retornamos 0 (o un valor adecuado)
            if (book == null)
            {
                return 0; // O puedes lanzar una excepción si prefieres manejarlo de otra manera
            }

            return book.Stock; // Devolvemos el stock del libro
        }


        /*  public bool VerificateBookStock(int bookId, int requiredStock)
          {
              var book = _bookRepository.GetBookById(bookId);

              if (book == null || book.Stock < requiredStock)
                  return false; 

              return true;
          }

          */
    }
}
