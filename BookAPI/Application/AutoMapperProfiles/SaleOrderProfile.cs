using Application.Dtos.SaleOrderDTOs;
using AutoMapper;
using Domain.Entities.Entities;

namespace Application.AutoMapperProfiles
{
    public class SaleOrderProfile : Profile
    {
        public SaleOrderProfile()
        {
            //Mapeo la entidad al DTO
            CreateMap<SaleOrder, SaleOrderDTO>()
     .ForMember(dest => dest.Total, opt => opt.MapFrom(src => (src.Book != null) ? src.Total : 0));

            CreateMap<SaleOrderToCreateDTO, SaleOrder>();
            CreateMap<SaleOrder, SaleOrderStatusDTO>();
        }
    }
}
