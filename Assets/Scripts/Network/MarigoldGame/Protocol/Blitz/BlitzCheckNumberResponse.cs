using MarigoldModel.Commands;

namespace MarigoldGame.Protocol
{
    public class BlitzCheckNumberResponse : BaseResponse
    {
        public Command Command { get; set; }

        public BlitzCheckNumberResponse() : base(MessageType.BlitzCheckNumberResponse)
        {
        }
    }
}
