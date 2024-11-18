using AutoMapper;
using LibraryApi.Dtos;
using LibraryApi.Exceptions;
using LibraryApi.Models;
using LibraryApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IMapper _mapper;
        public BookService(IRepository<Book> bookRepository,IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public int AddBook(BookDto bookDto)
        {
            Book book = _mapper.Map<Book>(bookDto);
            _bookRepository.Add(book);
            return book.Id;
        }

        public bool DeleteBook(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                throw new BookNotFoundException($"No book found with id-> {id}");
            }
            _bookRepository.Delete(book);
            return true;
        }

        public BookDto GetBook(int id)
        {
            var book = _bookRepository.GetAll().Include(b => b.Author).FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                throw new BookNotFoundException($"No book found with id-> {id}");
            }
            return _mapper.Map<BookDto>(book);
        }

        public List<BookDto> GetBooks()
        {
            var books = _bookRepository.GetAll().Include(b => b.Author).ToList();
            if (!books.Any())
            {
                throw new BookNotFoundException("No books found");
            }
            return _mapper.Map<List<BookDto>>(books);
        }

        public List<BookDto> FindBookByAuthorId(int authorId)
        {
            var books = _bookRepository.GetAll().Include(b => b.Author).Where(b => b.AuthorId == authorId).ToList();
            if (!books.Any())
            {
                throw new BookNotFoundException($"No books found for author id-> {authorId}");
            }
            return _mapper.Map<List<BookDto>>(books);
        }
        
        public AuthorDto FindAuthorByBookId(int bookId)
        {
            var book = _bookRepository.GetAll().Include(b => b.Author).FirstOrDefault(b => b.Id == bookId);
            if (book == null || book.Author == null)
            {
                throw new BookNotFoundException($"No book found with id-> {bookId}");
            }
            var author = book.Author;
            author.Books = _bookRepository.GetAll().Where(b => b.AuthorId == author.Id).ToList();

            return _mapper.Map<AuthorDto>(author);
        }

        public BookDto UpdateBook(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            var existingBook = _bookRepository.GetAll().Include(b => b.Author).AsNoTracking().FirstOrDefault(b => b.Id == book.Id);
            if (existingBook == null)
            {
                throw new BookNotFoundException($"No book found with id-> {book.Id}");
            }
            _bookRepository.Update(book);
            return bookDto;
        }
    }
}
