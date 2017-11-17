using MarigoldModel.Commands;

namespace MarigoldGame.Protocol
{
    public class BlitzRefreshPowerUpResponse : BaseProtocol
    {
        public Command Command { get; set; }

        public BlitzRefreshPowerUpResponse() : base(MessageType.BlitzRefreshPowerUpResponse)
        {
        }
    }
}
