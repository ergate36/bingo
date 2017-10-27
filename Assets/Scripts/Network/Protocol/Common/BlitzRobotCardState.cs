using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace MarigoldGame.Game.Blitz
{
    public class BlitzRobotCardState
    {
        public enum State
        {
            WAIT,
            CAN_CALL,
            FINISHED,
        };

        public State CurrentState { get; set; }
        public int CallWaitSecond { get; set; } // 빙고 완성 후 실제 빙고를 외칠때까지 기달리는 시간
    }
}
