using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEcoMapa.DTOs
{
    public class PontoSustentavelDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A latitude é obrigatória")]
        [Column(TypeName = "double precision")] 
        public double Latitude { get; set; }

        [Required(ErrorMessage = "A longitude é obrigatória")]
        [Column(TypeName = "double precision")] 
        public double Longitude { get; set; }
    }
}