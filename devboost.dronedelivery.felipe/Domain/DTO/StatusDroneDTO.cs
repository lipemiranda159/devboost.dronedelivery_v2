namespace devboost.dronedelivery.felipe.DTO
{
    public sealed class StatusDroneDto
    {
        public int DroneId { get; set; }
        public bool Situacao { get; set; }
        public int PedidoId { get; set; }
        public int? ClienteId { get; set; }

    }
}
