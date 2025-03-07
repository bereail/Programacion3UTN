using Application.Dtos.BookDTOs;
using AutoMapper;
using Domain.Entities.Entities;

namespace Application.AutoMapperProfiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDTO>().ReverseMap();

            CreateMap<BookToUpdateDTO, Book>();

            CreateMap<BookToCreateDTO, Book>();

            CreateMap<Book, BookGetDTO>();
            
        }
    }
}
