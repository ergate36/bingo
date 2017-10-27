using System.Collections.Generic;

namespace MarigoldGame.Common.Commands
{
    public class Command
    {
        public List<Command> SubCommandList;

        public Command()
        {
            SubCommandList = new List<Command>();
        }
    }
}
