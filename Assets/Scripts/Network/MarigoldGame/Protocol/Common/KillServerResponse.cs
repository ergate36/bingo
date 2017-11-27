using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace MarigoldGame.Protocol.Common
{
    class KillServerResponse : BaseResponse
    {
        public KillServerResponse() : base(MessageType.KillServerResponse)
        {
        }
    }
}
