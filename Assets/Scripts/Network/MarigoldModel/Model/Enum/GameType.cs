using System;
using System.Collections.Generic;
using System.Text;

namespace MarigoldModel.Model
{
    public enum GameType
    {
        NONE,
        BLITZ = 1,          // 메인 스테이지. 빙고 블리츠와 유사한 게임 모드
        MONSTER = 2,        // 서브 스테이지. 빙고 몬스터의 아이템을 가져온 모드
    }
}
