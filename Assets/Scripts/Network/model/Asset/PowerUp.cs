using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 파워업(아이템) 리스트 테이블
    [Table("PowerUp")]
    public partial class PowerUp : BaseModel
    {
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } // 아이템의 이름 (다국어 지원시 테이블을 만들어서 분리할 필요가 있다.)

        public PowerUpType Type { get; set; }

        public int UseRate { get; set; } // 아이템을 사용할 확률

        // 아이템의 기능에 대한 정보를 나눠서 처리를 쉽게 하도록 한다.
        public string Method { get; set; }
        public Int64 IntParam { get; set; }
        public string StringParam { get; set; }

        public bool UseOnce { get; set; } // 게임 중에 한번만 사용할 수 있는 아이템인지 알려준다.
    }
}
