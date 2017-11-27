using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MarigoldModel.Model
{
    [Table("UserHistory")]
    public partial class UserHistory : BaseUserModel
    {
        public long HistoryId { get; set; } // 해당 히스토리의 Id

        public DateTime StartTime { get; set; } // 히스토리가 시작된 시간. 데이터 확인을 쉽게하기 위하여 기록.
        public DateTime EndTime { get; set; } // 히스토리가 집계가 끝나는 시간

        public long Count { get; set; } // 히스토리 누적 개수
    }
}
