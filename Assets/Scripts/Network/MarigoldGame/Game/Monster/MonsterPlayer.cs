using MarigoldGame.Common;
//using MarigoldModel.Context;
using MarigoldModel.Model;
using System;
//using MarigoldGame.Network;
using System.Collections.Generic;
using MarigoldGame.Protocol;
//using MarigoldGame.Main;

namespace MarigoldGame.Game.Monster
{
    public class MonsterPlayer : GamePlayer
    {
        // monster mode 관련 변수
        public Int64 MonsterRoomId { get; set; } // 현재 플레이어가 소속된 게임룸의 번호

        //// AI 플레이어 용 생성자
        //protected MonsterPlayer() : base()
        //{

        //}

        //public MonsterPlayer(MarigoldContext dbContext, UserAccount userAccount) : base(dbContext, userAccount)
        //{

        //}
    }
}
