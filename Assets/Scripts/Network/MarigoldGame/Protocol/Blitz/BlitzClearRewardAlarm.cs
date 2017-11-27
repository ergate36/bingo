using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using MarigoldModel.Commands;
using MarigoldGame.Commands;

namespace MarigoldGame.Protocol
{
    class BlitzClearRewardAlarm : BaseProtocol
    {
        public ClearRewardCommand Command { get; internal set; }

        public BlitzClearRewardAlarm() : base(MessageType.BlitzClearRewardAlarm)
        {
        }
    }
}
