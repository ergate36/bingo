using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 아마존 게임 세션을 관리하기 위한 테이블.
    // 게임 리프트에 검색을 할때 게임의 스테이지 등의 정보로 필터링할 수 없기때문에 직접 리스트를 관리한다.
    // 이 리스트도 빠른 검색을 위해 나중에 redis로 옮길 예정이다.(StageId를 키로 사용한다.)
    [Table("ControlGameSession")]
    public partial class ControlGameSession : BaseModel
    {
        public Int64 StageId { get; set; } // 현재 게임의 스테이지

        public string GameSessionId { get; set; } // 게임리프트 세션의 아이디. 게임리프트 쪽 API의 이름과 같게하기 위해 GameSessionId로 이름 지음.

        // 중복으로 쓸려고할 경우가 생길 수 있으므로 변수를 없앨 수 있으면 없애자.
        // redis를 쓸경우 INC를 쓸 수 있게 하자.
        // PlayerCount 이 값을 우리가 가지고 있고 동기화 하게 할려면 많은 
        public int PlayerCount { get; set; } // 세션에 접속한 인원 수
        public double Elapsed { get; set; } // 방이 만들어질때 걸린 시간(디버그용)
    }
}
