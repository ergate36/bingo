using MarigoldGame.Common.Commands;

namespace MarigoldGame.Protocol
{
    public class BlitzRefreshPowerUpResponse : BaseProtocol
    {
        public BlitzRefreshPowerUpResponse() : base(MessageType.BlitzRefreshPowerUpResponse)
        {
        }

        public Command Command { get; set; }
    }
}
