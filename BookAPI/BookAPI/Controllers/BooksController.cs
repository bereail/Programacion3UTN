using Application.Dtos.BookDTOs;
using Application.Interfaces.Services;
using Domain.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookAPI.Controllers
{
    [ApiController]
    [Route("api/books")]

    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }



        [HttpGet("GetAllBooks")]
        [AllowAnonymous]
        public ActionResult<ICollection<BookDTO>> GetAllBooks()
        {
           
            var books = _bookService.GetAllBooks();
            return Ok(books);
        }


        [HttpGet("GetBookByTitle")]
        [AllowAnonymous]
        public ActionResult<BookDTO> GetBook(string title)
        {
            var book = _bookService.GetBookByTitle(title);
            if (book == null)
                return NotFound();
            return Ok(book);
        }



        [HttpPost("CreateNewBook")]
        public ActionResult CreateNewBook([FromBody] BookToCreateDTO bookToCreateDTO)
        {
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;


            if (userRole != "Admin")
            {
                return Forbid();
            }

            var createdBook = _bookService.AddBook(bookToCreateDTO);

            if (createdBook == null)
                return BadRequest();

            return CreatedAtRoute("GetBook", new { bookId = createdBook.BookId }, createdBook);
        }

        [HttpGet("{bookId}", Name = "GetBook")]
        public ActionResult<BookDTO> GetBook(int bookId)
        {
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid();
            }

            var book = _bookService.GetBookById(bookId);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPut("EditBook/{bookId}")]
        public IActionResult EditBook(int bookId, [FromBody] BookToUpdateDTO newUpdateBookDTO)
        {
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid(); // Retorna 403 Forbidden si no es Admin
            }
            var updatedBook = _bookService.EditBook(bookId, newUpdateBookDTO);
            if (updatedBook == null)
                return NotFound("El libro no existe.");

            return Ok(updatedBook);
        }



        [HttpPut("UpdateBookStock/{id}")]
        public IActionResult UpdateBookStock(int id, [FromBody] BookStockDTO dto)
        {
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid(); // Retorna 403 Forbidden si no es Admin
            }
            try
            {
                var updatedBook = _bookService.UpdateBookStock(id, dto.Stock);
                if (updatedBook == null)
                {
                    return NotFound($"El libro con ID {id} no fue encontrado.");
                }

                return Ok($"El stock del libro con ID {id} fue actualizado a {dto.Stock}.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }



        [HttpPut("DisableBook")]
        public ActionResult DisableBook(int bookId)
        {
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid(); // Retorna 403 Forbidden si no es Admin
            }
            var updatedBook = _bookService.DisableBook(bookId);

            if (updatedBook == null)
                return BadRequest("El libro no existe o no se pudo actualizar.");

            return Ok("El stock del libro se ha establecido a 0 exitosamente.");
        }

        [HttpGet("stock/{bookId}")]
        public ActionResult<int> GetBookStock(int bookId)
        {
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                return Forbid(); // Retorna 403 Forbidden si no es Admin
            }
            var stock = _bookService.GetBookStock(bookId);

            if (stock == 0)
            {
                return NotFound(new { message = "Book not found." });
            }

            return Ok(stock);
        }
    }


    //DE PRUEBA -- ELIMINAR


    /*   [HttpGet("verify-stock")]
       public IActionResult VerifyBookStock(int bookId, int requiredStock)
       {
           if (bookId <= 0 || requiredStock <= 0)
               return BadRequest("El ID del libro y la cantidad requerida deben ser válidos.");

           bool isStockAvailable = _bookService.GetBookStock(bookId);

           if (!isStockAvailable)
               return NotFound("No hay suficiente stock disponible para el libro.");

           return Ok("Stock disponible.");
       }
    */

}

