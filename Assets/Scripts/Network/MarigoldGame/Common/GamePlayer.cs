using Newtonsoft.Json;
using System;
using System.Collections.Generic;
//using MarigoldGame.Network;
using System.Linq;
//using MarigoldGame.Utility;
using MarigoldGame.Game;
//using MarigoldGame.Main;
using MarigoldGame.Protocol;

namespace MarigoldGame.Common
{
    public partial class GamePlayer
    {
        [JsonIgnore]
        protected Object CardListLock = new Object();

        public int CardCount { get; set; } // 게임을 시작할때 가지는 카드의 개수
        public int CollectionCardCount { get; set; } // 컬렉션을 포함한 카드의 개수
        public List<Card> CardList { get; set; } // 블리츠 모드 게임이 시작된 후 가지고 있는 카드 리스트

        public string Name { get; set; }
        public int ShieldTime { get; set; }
        public int BlindTime { get; set; }

        //public void CreateCardList()
        //{
        //    lock (CardListLock)
        //    {
        //        CardList = new List<Card>();
        //        for (int i = 0; i < CardCount; ++i)
        //        {
        //            CardList.Add(new Card(i));
        //        }
        //    }
        //}

        //// 로봇의 경우 서버에서 갱신이 필요하므로 Update 함수가 필요하다.
        //public virtual void Update(GameServer server)
        //{
        //}

        //internal Card FindRandomUnfinishedCard()
        //{
        //    return CardList.Where(w => w.Finished == false).Shuffle().FirstOrDefault();
        //}

        //internal Card FindRandomUnfinishedAndNoFogCard()
        //{
        //    return CardList.Where(w => w.Finished == false && w.FogTime == 0).Shuffle().FirstOrDefault();
        //}

        //internal Card FindRandomUnfinishedAndNoFreezeCard()
        //{
        //    return CardList.Where(w => w.Finished == false && w.FreezeTime == 0).Shuffle().FirstOrDefault();
        //}
    }
}
