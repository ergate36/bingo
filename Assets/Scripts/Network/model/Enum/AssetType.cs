using System;
using System.Collections.Generic;
using System.Text;

namespace MarigoldModel.Model
{
    public enum AssetType
    {
        GAME_MONEY = 1,
        COLLECTION,
        POWER_UP,
        RANDOM_REWARD, // 랜덤한 보상을 받는다.
        EXPERIENCE, // 경험치 보상 AssetId는 항상 0이어야 한다.
    }
}
