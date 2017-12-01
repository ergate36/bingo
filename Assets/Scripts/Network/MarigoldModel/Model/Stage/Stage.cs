using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 스테이지 정보
    [Table("Stage")]
    public partial class Stage : BaseModel
    {
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } // 스테이지 이름

        public int LevelRequirement { get; set; } // 입장 가능 레벨

        public GameType GameType { get; set; } // 게임 타입

        public long MiniGamblePriceId { get; set; } // 해당 스테이지에서 게임시 플레이할 수 있는 미니갬블에 대한 정보

        public int Order { get; set; } // 스테이지 표시 순서

        // TODO: 나중에 스테이지가 열리는 조건이 바뀔 수 있다.

        // TODO: 카테고리가 필요해질 수 있다.

        // 그룹 내의 콜렉션 리스트
        [JsonIgnore]
        public List<Collection> CollectionList { get; set; }

        [JsonIgnore]
        public List<StageClearRankingReward> StageClearRankingRewardList { get; set; }

        [JsonIgnore]
        public List<StageCompleteCollectionReward> StageCompleteCollectionRewardList { get; set; }

        [JsonIgnore]
        public List<StageFee> StageFeeList { get; set; }
    }
}
