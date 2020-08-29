using System;
using System.ComponentModel.DataAnnotations;

namespace devboost.dronedelivery.felipe.DTO.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Peso deve ser informado!")]
        [Range(1, int.MaxValue, ErrorMessage = "A Peso minimo deve ser 1.")]
        public int Peso { get; set; }

        public int ClientId { get; set; }
        public Cliente Cliente { get; set; }

        public DateTime DataHoraInclusao { get; set; }

        [Required(ErrorMessage = "Situacao deve ser informada!")]
        public int Situacao { get; set; }
        
        public DateTime DataUltimaAlteracao { get; set; }
        public DateTime DataHoraFinalizacao { get; set; }
    }
}
