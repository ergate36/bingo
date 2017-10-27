namespace MarigoldGame.Protocol
{
    public class BlitzWaitRoomStatusAlarm : BaseProtocol
    {
        public BlitzWaitRoomStatusAlarm() : base(MessageType.BlitzWaitRoomStatusAlarm)
        {
            RemainSecond = 0;
            RemainBingo = 0;
        }

        public int RemainSecond { get; set; } // 게임 시작까지 남은 시간(초, 게임중이 아닐때)
        public int RemainBingo { get; set; } // 게임 끝날때까지 남은 빙고의 수(게임중일때)
    }
}
