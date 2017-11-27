using MarigoldModel.Model;

namespace MarigoldGame.Common
{
    public partial class Square
    {
        public int Number { get; private set; } // 빙고 숫자
        public bool Checked { get; private set; } // 빙고 숫자가 체크되었는지 보여준다.
        public PowerUp PowerUp { get; private set; } // 칸에 묶여 있는 아이템. 중복 될 수 없다.
        public int DustTime { get; set; }
        public int Index { get; set; }

        public Square(int value, bool check = false)
        {
            Index = -1; // 나중에 초기화 필요함.
            Number = value;
            Checked = check;
            PowerUp = null;
        }
    }
}
