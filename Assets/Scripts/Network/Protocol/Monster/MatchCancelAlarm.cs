using System;

namespace MarigoldGame.Protocol
{
    public class MatchCancelAlarm : BaseProtocol
	{
        public Int64 UserAccountId { get; set; }

        public MatchCancelAlarm() : base(MessageType.MatchCancelAlarm)
		{
		}
	}

}
