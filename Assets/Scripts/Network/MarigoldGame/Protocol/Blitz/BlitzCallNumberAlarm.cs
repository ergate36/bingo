namespace MarigoldGame.Protocol
{
    public class BlitzCallNumberAlarm : BaseProtocol
    {
        public int Number { get; set; }

        public BlitzCallNumberAlarm() : base(MessageType.BlitzCallNumberAlarm)
        {
        }
    }
}
