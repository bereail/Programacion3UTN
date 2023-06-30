using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class EmpleadoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int IdEmpresa { get; set; }
        public int IdCargo { get; set; }
    }
}
