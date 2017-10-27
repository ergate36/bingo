using MarigoldGame.Common.Commands;

namespace MarigoldGame.Protocol
{
    public class BlitzCheckNumberResponse : BaseResponse
    {
        public BlitzCheckNumberResponse() : base(MessageType.BlitzCheckNumberResponse)
        {
        }

        public Command Command { get; set; }
    }
}
