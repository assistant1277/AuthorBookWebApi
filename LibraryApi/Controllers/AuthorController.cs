using LibraryApi.Dtos;
using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService service)
        {
            _authorService = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var authorDtos = _authorService.GetAuthors();
            return Ok(authorDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var author = _authorService.GetAuthor(id);
            return Ok(author);
        }

        [HttpGet("Name/{name}")]
        public IActionResult GetByName(string name)
        {
            var authorDto = _authorService.FindAuthorByName(name);
            return Ok(authorDto);
        }

        [HttpPost]
        public IActionResult Add(AuthorDto authorDto)
        {
            int id = _authorService.AddAuthor(authorDto);
            return Ok($"Author added successfully and author id-> {id}");
        }

        [HttpPut]
        public IActionResult Update(AuthorDto updatedAuthorDto)
        {
            var authorDto = _authorService.UpdateAuthor(updatedAuthorDto);
            return Ok(authorDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _authorService.DeleteAuthor(id);
            return Ok("Author deleted successfully");
        }
    }
}