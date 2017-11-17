using MarigoldGame.Common;

namespace MarigoldGame.Protocol
{
    public class BlitzCheckNumberRequest : BaseProtocol
    {
        public int CardIndex { get; set; }
        public int Number { get; set; }

        public BlitzCheckNumberRequest() : base(MessageType.BlitzCheckNumberRequest)
        {
        }
    }
}
