using MarigoldGame.Common;

namespace MarigoldGame.Protocol
{
    public class BlitzCheckNumberRequest : BaseProtocol
    {
        public BlitzCheckNumberRequest() : base(MessageType.BlitzCheckNumberRequest)
        {
        }

        public int CardIndex { get; set; }
        public int Number { get; set; }
    }
}
