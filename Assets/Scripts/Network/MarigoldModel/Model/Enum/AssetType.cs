using System;
using System.Collections.Generic;
using System.Text;

namespace MarigoldModel.Model
{
    // DB에 숫자가 들어가기때문에 순서 변경 금지
    public enum AssetType
    {
        NONE = 0,
        GAME_MONEY = 1,
        COLLECTION = 2,
        POWER_UP = 3,
        RANDOM_REWARD = 4, // 랜덤한 보상을 받는다.
        EXPERIENCE = 5, // 경험치 보상. AssetId는 항상 0이어야 한다.
        PAYMENT = 6, // 인앱 결제
        TREASURE_BOX = 7, // 보물상자. 랜덤한 보상을 유저에게 보여주고 싶을때 사용한다.
    }
}
