namespace ApiEcoMapa.Models
{
    public class PontoSustentavel
    {
        public int Id { get; set; } 
        public string? Nome { get; set; }
        public double Latitude  { get; set; }
        public double Longitude  { get; set; }
    }
}