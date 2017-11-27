using MarigoldModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace MarigoldGame.Commands
{
    public class ChattingSendCommand : Command
    {
        public bool Success { get; set; }
        public Exception Exception { get; set; } // 예외 발생시 기록
    }
}
