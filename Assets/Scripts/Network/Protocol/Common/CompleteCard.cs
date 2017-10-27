using MarigoldGame.Game.Blitz;

namespace MarigoldGame.Common
{
    // 빙고를 달성한 카드에 대한 정보를 담는다.
    class CompleteCard
    {
        public int Rank { get; set; }
        public Card Card { get; set; }
        // TODO: 이 클래스를 여러 게임 모드에서 같이 쓸 수 있을지 생각해보자.
        public BlitzPlayer Player { get; set; } // 임시로 BlitzPlayer로 생성.
    }
}
