using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 재화 리스트 테이블
    [Table("GameMoney")]
    public partial class GameMoney : BaseModel
    {
        [Column(TypeName = "mediumtext")]
        public string Comment { get; set; } // 설명을 기록한다.

        public Int64 InitialValue { get; set; } // 생성될때의 초기값
        public bool CanRemove { get; set; } // 개수가 줄어들 수 있는지 여부
    }
}
