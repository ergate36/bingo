//using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarigoldModel.Model
{
    public partial class UserRetryCollection : BaseUserModel
    {
        public Int64 StageId { get; set; } // 해당 컬렉션이 저장된 스테이지의 아이디

        public Int64 CollectionId { get; set; } // 재시도할 Collection의 아이디

        public bool Used { get; set; } // 사용했는지 기록
        public DateTime UsedAt { get; set; } // 사용한 시간을 기록한다.
    }
}
