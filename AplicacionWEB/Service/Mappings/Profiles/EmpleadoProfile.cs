using AutoMapper;
using Model.DTO;
using Model.Models;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mappings.Profiles
{
    public class EmpleadoProfile : Profile
    {
        public EmpleadoProfile()
        {
            CreateMap<Empleado, EmpleadoDTO>()
                .ForMember(dest => dest.NombreEmpleado, opt => opt.MapFrom(src => src.Nombre));

            CreateMap<List<Empleado>, List<EmpleadoDTO>>()
                .ConvertUsing(src => src.Select(e => new EmpleadoDTO { NombreEmpleado = e.Nombre, Id = e.Id }).ToList());

            CreateMap<EmpleadoViewModel, Empleado>();

            CreateMap<EmpleadoViewModel, EmpleadoDTO>()
                .ForMember(dest => dest.NombreEmpleado, opt => opt.MapFrom(src => src.Nombre));
        }
    }
}
