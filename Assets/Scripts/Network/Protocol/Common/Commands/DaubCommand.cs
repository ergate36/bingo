using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace MarigoldGame.Common.Commands
{
    // 칸이 칠해진 액션을 클라이언트에 전달한다.
    class DaubCommand : Command
    {
        public int CardIndex { get; set; }
        public int Number { get; set; }
    }
}
