using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 사용자의 시간 관련 정보를 담는 테이블
    [Table("UserTimer")]
    class UserTimer : BaseUserModel
    {
        public Int64 TimerId { get; set; }

        public Int64 Count { get; set; } // 현재 가진 갯수
        public Int64 RegenerateUnit { get; set; } // 회복시 늘어나는 갯수(게임 진행에 따라 늘어나는 갯수가 바뀔 수 있다)
        public Int64 RegenerateMax { get; set; } // 회복으로 늘어날 수 있는 최대치(게임 진행에 따라 최대치가 바뀔 수 있다)
        public Int64 CycleOrder { get; set; } // 타이머의 시간이 가변적일 경우 현재 순서
        public DateTime RegenerateTime { get; set; } // 다음 회복 시간
        public DateTime LastUsedTime { get; set; } // 마지막으로 Count가 감소한 시간(감소 쿨타임이 필요할 때 사용)
    }
}
