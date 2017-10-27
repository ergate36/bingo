using MarigoldModel.Commands;

namespace MarigoldGame.Protocol
{
    public class BlitzEnterGameResponse : BaseResponse
    {
        public int Result { get; set; }
        public EnterGameCommand Command { get; set; }

        public BlitzEnterGameResponse() : base(MessageType.BlitzEnterGameResponse)
        {
        }
    }
}
