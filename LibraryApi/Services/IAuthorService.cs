using LibraryApi.Dtos;
using LibraryApi.Models;

namespace LibraryApi.Services
{
    public interface IAuthorService
    {
        public List<AuthorDto> GetAuthors();
        public AuthorDto GetAuthor(int id);
        public int AddAuthor(AuthorDto authorDto);
        public bool DeleteAuthor(int id);
        public AuthorDto UpdateAuthor(AuthorDto authorDto);
        public AuthorDto FindAuthorByName(string name);
    }
}
