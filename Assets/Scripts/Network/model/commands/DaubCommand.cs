namespace MarigoldModel.Commands
{
    // 칸이 칠해진 액션을 클라이언트에 전달한다.
    public class DaubCommand : Command
    {
        public int CardIndex { get; set; }
        public int Number { get; set; }
    }
}
