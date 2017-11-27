namespace MarigoldGame.Common
{
    // 빙고를 달성한 카드에 대한 정보를 담는다.
    public class CompleteCard
    {
        public int Ranking { get; set; }
        public Card Card { get; set; }
        // TODO: 이 클래스를 여러 게임 모드에서 같이 쓸 수 있을지 생각해보자.
        //public GamePlayer Player { get; set; } // 임시로 Player로 생성.
    }
}
