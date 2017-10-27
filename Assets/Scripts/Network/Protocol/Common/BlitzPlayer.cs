using MarigoldGame.Common;
using System.Collections.Generic;

namespace MarigoldGame.Game.Blitz
{
    public partial class BlitzPlayer
    {
        public int CardCount { get; set; } // 블리츠 모드로 게임을 시작할때 가질 카드의 개수
        public List<Card> CardList { get; set; } // 블리츠 모드 게임이 시작된 후 가지고 있는 카드 리스트

        public string Name { get; set; }
    }
}
