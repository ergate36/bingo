using System;
using System.Collections.Generic;
using System.Text;

namespace MarigoldModel.Model
{
    public enum HistoryType
    {
        ACHIEVEMENT = 1, // 업적 달성시 증가
        COLLECTION, // 새로운 콜렉션을 획득시 증가
        TOTAL_BINGO, // 빙고를 할때 마다 증가
        CARD_PLAYED, // 게임 종료시 플레이한 카드 개수만큼 증가
        ACTIVE_FRIEND, // 현재 내 친구의 수(증가 아님)
        STAGE_COMPLETE, // 컬렉션을 12개 다 모으면 증가
    }
}
