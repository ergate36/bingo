using MarigoldGame.Common;
using System.Collections.Generic;

namespace MarigoldGame.Protocol
{
    public class MonsterStartGameAlarm : BaseProtocol
    {
        public List<Card> CardList { get; set; }
        public List<string> UserNameList { get; set; }

        public MonsterStartGameAlarm() : base(MessageType.MonsterStartGameAlarm)
        {
        }
    }
}
