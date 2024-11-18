using LibraryApi.Dtos;
using LibraryApi.Models;

namespace LibraryApi.Services
{
    public interface IAuthorDetailsService
    {
        public List<AuthorDetailsDto> GetAuthorDetails();
        public AuthorDetailsDto GetAuthorDetail(int id);
        public int AddAuthorDetails(AuthorDetailsDto authorDetails);
        public bool DeleteAuthorDetails(int id);
        public AuthorDetailsDto UpdateAuthorDetails(AuthorDetailsDto authorDetailsDto);
        public AuthorDetailsDto FindAuthorDetailsByAuthorId(int id);
    }
}
