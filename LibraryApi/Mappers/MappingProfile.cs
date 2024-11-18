using AutoMapper;
using LibraryApi.Dtos;
using LibraryApi.Models;

namespace LibraryApi.Mappers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>().ForMember(dest => dest.TotalBooks, value => value.MapFrom(s => s.Books.Count));

            CreateMap<AuthorDto, Author>();
            CreateMap<Book, BookDto>().ForMember(dest => dest.AuthorName, value => value.MapFrom(s => s.Author.Name));

            CreateMap<BookDto, Book>();
            CreateMap<AuthorDetails, AuthorDetailsDto>().ForMember(dest => dest.AuthorName, value => value.MapFrom(s => s.Author.Name));
            CreateMap<AuthorDetailsDto, AuthorDetails>();
        }
    }
}
