namespace MarigoldGame.Protocol
{
    public class BlitzCallNumberAlarm : BaseProtocol
    {
        public BlitzCallNumberAlarm() : base(MessageType.BlitzCallNumberAlarm)
        {
        }

        public int Number { get; set; }
    }
}
