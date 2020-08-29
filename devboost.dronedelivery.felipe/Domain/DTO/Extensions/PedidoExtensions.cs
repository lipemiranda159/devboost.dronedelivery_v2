using devboost.dronedelivery.felipe.DTO.Models;

namespace devboost.dronedelivery.felipe.DTO.Extensions
{
    public static class PedidoExtensions
    {
        public static Point GetPoint(this Pedido pedido)
        {
            return new Point(pedido.Cliente.Latitude, pedido.Cliente.Longitude);
        }
    }
}
