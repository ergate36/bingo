using MarigoldGame.Common;
using System.Collections.Generic;

namespace MarigoldGame.Protocol
{
    public class BlitzStartGameAlarm : BaseProtocol
    {
        public List<Card> CardList { get; set; }
        public List<string> UserNameList { get; set; }

        public BlitzStartGameAlarm() : base(MessageType.BlitzStartGameAlarm)
        {
        }
    }
}
