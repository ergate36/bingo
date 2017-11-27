using MarigoldModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace MarigoldGame.Commands
{
    public class ChattingReceiveCommand : Command
    {
        public long UserAccountId { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
    }
}
