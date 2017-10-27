using MarigoldGame.Common.Commands;

namespace MarigoldGame.Protocol
{
    public class BlitzUsePowerUpResponse : BaseResponse
    {
        public BlitzUsePowerUpResponse() : base(MessageType.BlitzUsePowerUpResponse)
        {
        }

        public Command Command { get; set; }
    }
}
