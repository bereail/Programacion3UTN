using Application.Dtos.AdminDTOs;
using AutoMapper;
using Domain.Models.Entities;


namespace Application.AutoMapperProfiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<AdminToCreateDTO, Admin>();
            CreateMap<AdminToUpdateDTO, Admin>();

            CreateMap<User, AdminDTO>();
        }
    }
}
