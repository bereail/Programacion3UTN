/*using Application.Dtos.BookDTOs;
using Domain.Entities.Dtos.BookDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Authorize(Roles = "Admin")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("GetAllProducts")]
        [AllowAnonymous]
        public ActionResult<ICollection<BookDTO>> GetAllProducts()
        {
            var products = _bookService.GetAllBooks();
            return Ok(products);
        }

        [HttpGet("{bookId}", Name = "GetBook")]
        [AllowAnonymous]
        public ActionResult<BookDTO> GetProduct(int bookId)
        {
            var book = _bookService.GetBookById(bookId);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpPost("CreateNewBook")]
        public ActionResult AddBook(BookToCreateDTO book)
        {
            var createdBook = _bookService.AddBook(book);

            if (createdBook == null)
                return BadRequest();

            return CreatedAtRoute("GetBook", new { productId = createdBook.Id }, createdBook);
        }

        [HttpPut("UpdateBookStock")]
        public ActionResult UpdateBookStock(BookStockDTO newStock, int bookId)
        {
            var updatedBook = _bookService.UpdateBookStock(newStock.Stock, bookId);

            if (updatedBook == null)
                return BadRequest();

            return CreatedAtRoute("GetProduct", new { bookId = updatedBook.Id }, updatedBook);
        }

        [HttpPut("DeleteProduct")]
        public ActionResult DeleteBook(int productId)
        {
            var deletedBook = _bookService.DeleteProduct(productId);

            if (deletedBook == null)
                return BadRequest();

            return Ok("Libro eliminado con exito");
        }


    }
}
*/