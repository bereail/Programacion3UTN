using Application.Dtos.BookDTOs;
using Application.Interfaces.Services;
using Domain.Entities.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Net.WebRequestMethods;
using static System.Reflection.Metadata.BlobBuilder;

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
        public ActionResult<ICollection<BookGetDTO>> GetAllBooks()
        {
            try
            {
                var userRole = HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                var books = _bookService.GetAllBooks();
                if (userRole != "Admin")
                {
                    books = books.Where(b => b.Stock > 0).ToList();
                }
                if (books == null || !books.Any())
                {
                    return Ok(new { message = "Por el momento no hay libros disponibles." });
                }
                return Ok(books);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error en el servidor. Por favor, inténtalo nuevamente." });
            }
        }


        [HttpGet("GetBookByTitle")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<BookGetDTO>> GetBook(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest(new { message = "El título no puede estar vacío." });
            }

            var userRole = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            try
            {
                var books = _bookService.GetBookByTitle(title);

                if (!books.Any())
                {
                    return NotFound(new { message = "No se encontraron libros con ese título." });
                }

                if (userRole != "Admin")
                {
                    books = books.Where(b => b.Stock > 0);
                }

                return Ok(books);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
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
                return BadRequest("No se pudo crear el libro. Verifique que el título no esté duplicado y que los datos sean correctos.");

            // CreatedAtRoute es un método que se usa para devolver una respuesta HTTP 201 Created(creado) junto con una ubicación de la nueva entidad creada.
            return CreatedAtRoute("GetBook", new { bookId = createdBook.BookId }, createdBook);
        }

        [HttpGet("{bookId}", Name = "GetBook")]
        public ActionResult<BookDTO> GetBook(int bookId)
        {
            try
            {
                var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (userRole != "Admin")
                {
                    return Forbid();
                }

                var book = _bookService.GetBookById(bookId);
                if (book == null)
                {
                    return NotFound(new { Message = $"No se encontró el libro con ID {bookId}." });
                }

                return Ok(book);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = "Ocurrió un error al procesar la solicitud." });
            }
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
                return Forbid();
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
                return Forbid();
            }
            var updatedBook = _bookService.DisableBook(bookId);

            if (updatedBook == null)
                return BadRequest("El libro no existe o no se pudo actualizar.");

            return Ok("El stock del libro se ha establecido a 0 exitosamente.");
        }

    }
}
