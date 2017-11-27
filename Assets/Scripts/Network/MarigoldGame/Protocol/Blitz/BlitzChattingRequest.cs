using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace MarigoldGame.Protocol
{
    public class BlitzChattingRequest : BaseProtocol
    {
        public string Message { get; set; } // 채팅 메시지

        public BlitzChattingRequest() : base(MessageType.BlitzChattingRequest)
        {
        }
    }
}
