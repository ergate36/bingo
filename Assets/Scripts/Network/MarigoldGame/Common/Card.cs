using System;
using System.Collections.Generic;

namespace MarigoldGame.Common
{
    public partial class Card
    {
        // 하나의 빙고카드에 있는 숫자를 나타낸다. 순서는 다음과 같다.
        //  0   5   10  15  20
        //  1   6   11  16  21
        //  2   7   12  17  22
        //  3   8   13  18  23
        //  4   9   14  19  24
        public List<Square> SquareList { get; set; }
        // 이 카드로 빙고를 했을때 얻을 수 있는 콜렉션의 아이디
        public Int64 CollectionId { get; set; }
        public int FogTime { get; internal set; }
        public int Index { get; set; } // 이 카드의 순서. 서버와 클라이언트간에 비교를 위한 ID로 사용한다. 0번 부터 시작한다.
        public int FreezeTime { get; internal set; }
    }
}
