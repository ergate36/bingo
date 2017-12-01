using MarigoldGame.Commands;
using MarigoldGame.Common;
using MarigoldModel.Commands;
using System.Collections.Generic;

namespace MarigoldGame.Protocol
{
    public class MonsterStartGameAlarm : BaseProtocol
    {
        public List<Card> CardList { get; set; }

        public Command Command { get; set; }

        public MonsterStartGameAlarm() : base(MessageType.MonsterStartGameAlarm)
        {
        }
    }
}
