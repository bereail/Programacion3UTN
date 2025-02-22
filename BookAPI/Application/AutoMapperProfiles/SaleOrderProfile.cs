using Application.Dtos.SaleOrderDTOs;
using AutoMapper;
using Domain.Models.Entities;


namespace Application.AutoMapperProfiles
{
    public class SaleOrderProfile : Profile
    {
        public SaleOrderProfile()
        {
            //Mapeo la entidad al DTO
            CreateMap<SaleOrder, SaleOrderDTO>();
            CreateMap<SaleOrderToCreateDTO, SaleOrder>();
            CreateMap<SaleOrder, SaleOrderStatusDTO>();
        }
    }
}
