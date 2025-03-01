using Application.Dtos.BookDTOs;
using AutoMapper;
using Domain.Entities.Entities;

namespace Application.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Book, BookDTO>();
            CreateMap<BookToCreateDTO, Book>();
        }
    }
}
