using System.ComponentModel.DataAnnotations;

namespace devboost.dronedelivery.felipe.DTO.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        [Required(ErrorMessage = "Latitude deve ser informada!")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Longitude deve ser informada!")]
        public double Longitude { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
