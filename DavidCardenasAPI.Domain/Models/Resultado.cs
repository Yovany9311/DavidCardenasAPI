namespace DavidCardenasAPI.Domain.Models
{
    public class Resultado
    {
        public int Id { get; set; }
        public int Arranque { get; set; }
        public int Envion { get; set; }
        public int TotalPeso { get; set; }
        public int DeportistaId { get; set; }
        public Deportista Deportista { get; set; }
    }
}
