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
    }
}
