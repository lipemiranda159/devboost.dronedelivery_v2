using devboost.dronedelivery.felipe.DTO.Enums;

namespace devboost.dronedelivery.felipe.DTO.Extensions
{
    public static class StatusEnvioExtensions
    {
        public static bool IsEmTransito(this StatusEnvio status)
        {
            return status == StatusEnvio.EM_TRANSITO;
        }
    }
}
