using MarigoldGame.Common;
using MarigoldModel.Commands;
using System.Collections.Generic;

namespace MarigoldGame.Protocol
{
    class MonsterOpponentStateAlarm : BaseProtocol
    {
        public Command Command { get; set; }

        public MonsterOpponentStateAlarm() : base(MessageType.MonsterOpponentStateAlarm)
        {
        }
    }
}
