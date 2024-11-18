using AutoMapper;
using LibraryApi.Dtos;
using LibraryApi.Exceptions;
using LibraryApi.Models;
using LibraryApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Services
{
    public class AuthorDetailsService : IAuthorDetailsService
    {
        private readonly IRepository<AuthorDetails> _authorDetailsRepository;
        private readonly IMapper _mapper;
        public AuthorDetailsService(IRepository<AuthorDetails> authorDetailsRepository,IMapper mapper)
        {
            _authorDetailsRepository = authorDetailsRepository;
            _mapper = mapper;
        }

        public int AddAuthorDetails(AuthorDetailsDto authorDetailsDto)
        {
            var authorDetails = _mapper.Map<AuthorDetails>(authorDetailsDto);
            _authorDetailsRepository.Add(authorDetails);
            return authorDetails.Id;
        }

        public bool DeleteAuthorDetails(int id)
        {
            var authorDetails = _authorDetailsRepository.GetById(id);
            if (authorDetails == null)
            {
                throw new AuthorDetailsNotFoundException($"No author details found with id-> {id}");
            }

            _authorDetailsRepository.Delete(authorDetails);
            return true;
        }

        //ad means author detail
        public AuthorDetailsDto GetAuthorDetail(int id)
        {
            var authorDetail = _authorDetailsRepository.GetAll().Include(ad => ad.Author).FirstOrDefault(ad => ad.Id == id);
            if (authorDetail == null)
            {
                throw new AuthorDetailsNotFoundException($"No author details found with id-> {id}");
            }
            return _mapper.Map<AuthorDetailsDto>(authorDetail);
        }

        public AuthorDetailsDto FindAuthorDetailsByAuthorId(int authorId)
        {
            var authorDetail = _authorDetailsRepository.GetAll().Include(ad => ad.Author).FirstOrDefault(ad => ad.AuthorId == authorId);
            if (authorDetail == null)
            {
                throw new AuthorDetailsNotFoundException($"No author details found for author id-> {authorId}");
            }
            return _mapper.Map<AuthorDetailsDto>(authorDetail);
        }

        public List<AuthorDetailsDto> GetAuthorDetails()
        {
            var authorDetails = _authorDetailsRepository.GetAll().Include(ad => ad.Author).ToList();
            if (!authorDetails.Any())
            {
                throw new AuthorDetailsNotFoundException("No author details found");
            }
            return _mapper.Map<List<AuthorDetailsDto>>(authorDetails);
        }

        public AuthorDetailsDto UpdateAuthorDetails(AuthorDetailsDto authorDetailsDto)
        {
            var authorDetails = _mapper.Map<AuthorDetails>(authorDetailsDto);
            var existingAuthorDetails = _authorDetailsRepository.GetAll().Include(ad => ad.Author).AsNoTracking()
                .FirstOrDefault(ad => ad.Id == authorDetails.Id);

            if (existingAuthorDetails == null)
            {
                throw new AuthorDetailsNotFoundException($"No author details found with id-> {authorDetails.Id}");
            }
            _authorDetailsRepository.Update(authorDetails);
            return authorDetailsDto;
        }
    }
}
