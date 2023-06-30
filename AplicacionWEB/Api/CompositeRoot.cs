using Service.IServices;
using Service.Services;

namespace Api
{
    public static class CompositeRoot
    {
        public static void DependencyInjection(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
            builder.Services.AddScoped<IEmpresaService, EmpresaService>();
            builder.Services.AddScoped<ICargoService, CargoService>();
        }
    }
}
