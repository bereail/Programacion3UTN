using Application.Dtos.SaleOrderDTOs;
using AutoMapper;
using Domain.Entities.Entities;

namespace Application.AutoMapperProfiles
{
    public class SaleOrderProfile : Profile
    {
        public SaleOrderProfile()
        {
            CreateMap<SaleOrder, SaleOrderDTO>();
            CreateMap<SaleOrderToCreateDTO, SaleOrder>();
            CreateMap<SaleOrder, SaleOrderStatusDTO>();
        }
    }
}
