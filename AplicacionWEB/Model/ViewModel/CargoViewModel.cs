using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class CargoViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal? Salario { get; set; }
    }
}
