using LibraryApi.Dtos;
using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService service)
        {
            _bookService = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var bookDtos = _bookService.GetBooks();
            return Ok(bookDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = _bookService.GetBook(id);
            return Ok(book);
        }

        //this is get books by author id
        [HttpGet("Author/{authorId}")]
        public IActionResult FindBooksByAuthorId(int authorId)
        {
            var bookDtos = _bookService.FindBookByAuthorId(authorId);
            return Ok(bookDtos);
        }
        
        //this is get author by book id
        [HttpGet("Book/{bookId}")]
        public IActionResult FindAuthorByBookId(int bookId)
        {
            var authorDto = _bookService.FindAuthorByBookId(bookId);
            return Ok(authorDto);
        }

        [HttpPost]
        public IActionResult Add(BookDto bookDto)
        {
            int id = _bookService.AddBook(bookDto);
            return Ok($"Book added successfully and id -> {id}");
        }

        [HttpPut]
        public IActionResult Update(BookDto updatedBookDto)
        {
            var bookDto = _bookService.UpdateBook(updatedBookDto);
            return Ok(bookDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookService.DeleteBook(id);
            return Ok("Book deleted successfully");
        }
    }
}