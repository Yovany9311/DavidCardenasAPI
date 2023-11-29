using DavidCardenasAPI.Models;

namespace DavidCardenasAPI.DTOS
{
    public class DTOResultado
    {
        public int Id { get; set; }
        public int Arranque { get; set; }
        public int Envion { get; set; }
        public int TotalPeso { get; set; }
        public int DeportistaId { get; set; }
    }
}
