using System;

namespace MarigoldGame.Protocol
{
    public class BlitzGambleRequest : BaseProtocol
    {
        public BlitzGambleRequest() : base(MessageType.BlitzGambleRequest)
        {
        }

        public Int64 StageGambleFeeId { get; set; } // 어떤 갬블을 하는 가에 대한 정보
    }
}
