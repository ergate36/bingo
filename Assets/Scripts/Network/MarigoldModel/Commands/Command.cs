using MarigoldModel.Model;
using System.Collections.Generic;

namespace MarigoldModel.Commands
{
    // 서버에서 실행한 명령에 대해서 클라이언트에 자세한 정보를 전달하기 위한 클래스.
    public class Command
    {
        public CommandType Type { get; set; }
        public List<Command> SubCommandList { get; set; }

        public Command()
        {
            Type = CommandType.NONE;
        }

        public Command(CommandType type)
        {
            Type = type;
        }

        public void AddSubCommand(Command subCommand)
        {
            if (subCommand != null)
            {
                if (SubCommandList == null)
                {
                    SubCommandList = new List<Command>(); // 필요할때만 만든다.
                }
                SubCommandList.Add(subCommand);
            }
        }
    }
}
