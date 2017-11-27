using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace MarigoldGame.Protocol.Common
{
    public class KillServerRequest : BaseProtocol
    {
        public KillServerRequest() : base(MessageType.KillServerRequest)
        {
        }
    }
}
