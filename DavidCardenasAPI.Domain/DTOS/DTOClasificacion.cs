using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidCardenasAPI.Domain.DTOS
{
    public class DTOClasificacion
    {
        public string Pais { get; set; }
        public string Nombre { get; set; }
        public int Arranque { get; set; }
        public int Envion { get; set; }
        public int TotalPeso { get; set; }
    }
}
