using AutoMapper;
using LibraryApi.Dtos;
using LibraryApi.Exceptions;
using LibraryApi.Models;
using LibraryApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;
        private readonly IMapper _mapper;
        public AuthorService(IRepository<Author> authorRepository,IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public int AddAuthor(AuthorDto authorDto)
        {
            Author author = _mapper.Map<Author>(authorDto);
            _authorRepository.Add(author);
            return author.Id;
        }

        public bool DeleteAuthor(int id)
        {
            var author = _authorRepository.GetById(id);
            if (author == null)
            {
                throw new AuthorNotFoundException($"No author found with id-> {id}");
            }
            _authorRepository.Delete(author);
            return true;
        }

        public AuthorDto GetAuthor(int id)
        {
            var author = _authorRepository.GetAll().Include(a => a.AuthorDetails).Include(a => a.Books).FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                throw new AuthorNotFoundException($"No author found with id-> {id}");
            }
            return _mapper.Map<AuthorDto>(author);
        }

        public List<AuthorDto> GetAuthors()
        {
            var authors = _authorRepository.GetAll().Include(a => a.AuthorDetails).Include(a => a.Books).ToList();
            if (!authors.Any())
            {
                throw new AuthorNotFoundException("No authors found do add authors please");
            }
            return _mapper.Map<List<AuthorDto>>(authors);
        }

        public AuthorDto UpdateAuthor(AuthorDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);
            var existingAuthor = _authorRepository.GetAll().Include(a => a.Books).Include(a => a.AuthorDetails).AsNoTracking()
                .FirstOrDefault(a => a.Id == author.Id);
            if (existingAuthor == null)
            {
                throw new AuthorNotFoundException($"No author found with id-> {author.Id}");
            }
            _authorRepository.Update(author);
            return authorDto;
        }

        public AuthorDto FindAuthorByName(string name)
        {
            var author = _authorRepository.GetAll().Include(a => a.AuthorDetails).Include(a => a.Books)
                .FirstOrDefault(a => a.Name == name);
            if (author == null)
            {
                throw new AuthorNotFoundException($"No author found with the name '{name}'");
            }
            return _mapper.Map<AuthorDto>(author);
        }
    }
}