using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using MarigoldGame.Commands;

namespace MarigoldGame.Protocol
{
    class BlitzChattingAlarm : BaseProtocol
    {
        public BlitzChattingAlarm() : base(MessageType.BlitzChattingAlarm)
        {
        }

        public ChattingReceiveCommand Command { get; internal set; }
    }
}
