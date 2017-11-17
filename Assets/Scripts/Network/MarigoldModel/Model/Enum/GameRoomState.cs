using System;
using System.Collections.Generic;
using System.Text;

namespace MarigoldModel.Model
{
    public enum GameRoomState
    {
        WAIT,
        GAME,
        STARTING_GAME, // 게임 시작 준비중
        START_END, // 게임 종료 준비중
        END,
    }
}
