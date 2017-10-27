using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 시간 관리 변수 리스트
    [Table("Timer")]
    public partial class Timer : BaseModel
    {
        [Column(TypeName = "mediumtext")]
        public string Comment { get; set; } // 타이머에 대한 설명을 기록한다.

        public int RegenerateType { get; set; } // 타이머가 작동하는 방식
        public Int64 RegenerateUnit { get; set; } // 회복시 늘어나는 갯수의 초기값
        public Int64 RegenerateMin { get; set; } // 회복으로 변할 수 있는 최소값
        public Int64 RegenerateMax { get; set; } // 회복으로 변할 수 있는 최대값

        public int RegenerateBegin { get; set; } // 타이머가 시작하는 시간(초)
        public DateTime RegenerateBeginDate { get; set; } // 타이머가 시작하는 시간
        public int RegenerateRate { get; set; } // 타이머의 주기(초)

        public Int64 ValueMin { get; set; } // 모든 상황에서 변할 수 있는 최소값
        public Int64 ValueMax { get; set; } // 모든 상황에서 변할 수 있는 최대값

        public string CreateExcept { get; set; } // 타이머 생성시 예외상황을 기록(코드상에서 체크한다)
        public string UpdateExcept { get; set; } // 타이머 변경시 예외상황을 기록(코드상에서 체크한다)
        public int CoolTime { get; set; } // 타이머가 사용된 이후 다시 사용할 수 있는 시간(초)
        internal IEnumerable<TimerCycleOrder> TimerCycleOrderList { get; set; }
    }
}
