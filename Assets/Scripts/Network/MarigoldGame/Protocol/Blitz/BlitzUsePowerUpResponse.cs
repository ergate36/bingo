using MarigoldModel.Commands;

namespace MarigoldGame.Protocol
{
    public class BlitzUsePowerUpResponse : BaseResponse
    {
        public Command Command { get; set; }

        public BlitzUsePowerUpResponse() : base(MessageType.BlitzUsePowerUpResponse)
        {
        }
    }
}
