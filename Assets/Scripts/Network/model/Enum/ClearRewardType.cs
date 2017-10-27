namespace MarigoldModel.Model
{
    // 이 순서는 클리어시 보상을 지급하는 순서이기때문에 이유없이 바뀌면 안된다.
    public enum ClearRewardType
    {
        CLEAR_RANKING_REWARD, // 빙고 완료시 랭킹에 따라 얻은 보상
        DOUBLE_GAME_MONEY,
        DOUBLE_EXPERIENCE,
        COIN, // COIN 상자 RandomRewardId를 가지며 게임이 끝날때 실제 보상이 결정된다.
        CHEST, // CHEST 상자 RandomRewardId를 가지며 게임이 끝날때 실제 보상이 결정된다.
        COIN_OPEN,
        CHEST_OPEN,
    }
}
