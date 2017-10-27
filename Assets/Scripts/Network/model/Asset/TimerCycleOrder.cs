using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 타이머의 RegenerateRate가 일정하지 않은 경우 순서대로 순환하며 RegenerateRate가 변하는 데이터를 기록하는 테이블
    [Table("TimerCycleOrder")]
    public partial class TimerCycleOrder : BaseModel
    {
        public int Order { get; set; }
        public int RegenerateRate { get; set; }

        // foreign key
        public Int64 TimerId { get; set; }
        public Timer Timer { get; set; }
    }
}
