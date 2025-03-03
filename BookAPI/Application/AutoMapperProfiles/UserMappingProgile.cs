﻿using AutoMapper;
using Application.Dtos.UserDto;
using Domain.Entities;
using Domain.Entities.Entities;

namespace Application.AutoMapperProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // Configura el mapeo de UserForAddRequest a User
            CreateMap<UserForAddRequest, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            // Configura el mapeo de User a UserDto
            CreateMap<User, UserDto>();
        }
    }
}
