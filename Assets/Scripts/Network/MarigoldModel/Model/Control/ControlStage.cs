using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MarigoldModel.Model
{
    // 현재 스테이지의 정보. PlayerCount 이외에 필요한 정보가 없는지 확인이 필요.
    [Table("ControlStage")]
    public partial class ControlStage : BaseModel
    {
        public long StageId { get; set; } // 현재 게임의 스테이지

        // 중복으로 쓸려고할 경우가 생길 수 있으므로 변수를 없앨 수 있으면 없애자.
        // Redis로 옮긴다고 생각할 경우 INC 명령이 있기때문에 동기화 걱정없이 관리할 수 있다.
        public int PlayerCount { get; set; } // 스테이지에서 플레이하는 사람의 수.(각 세션별 인원 수의 합과 같아야 한다.)
    }
}
