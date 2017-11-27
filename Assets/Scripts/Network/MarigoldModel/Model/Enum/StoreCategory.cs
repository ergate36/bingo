using System;
using System.Collections.Generic;
using System.Text;

namespace MarigoldModel.Model.Enum
{
    public enum StoreCategory
    {
        NONE,
        COIN_STORE,
        CREDIT_STORE,
        POWER_UP_BLITZ_STORE, // 메인 스테이지 아이템 상점
        POWER_UP_MONSTER_STORE, // 서브 스테이지 아이템 상점
        SPECIAL, // 스페셜 상점. StoreProductSpecial에 추가 정보가 들어있다.
    }
}
