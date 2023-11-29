using System.Text.Json.Serialization;

namespace DavidCardenasAPI.Models
{
    public class Deportista
    {
        public int Id { get; set; }
        public string Pais { get; set; }
        public string Nombre { get; set; }
    }
}
