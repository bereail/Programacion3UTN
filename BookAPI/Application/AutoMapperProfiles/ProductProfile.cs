using Application.Dtos.BookDTOs;
using AutoMapper;
using Domain.Models.Entities;


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
