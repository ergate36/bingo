using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 플레이어가 레벨업 하기 위해 필요한 경험치
    // PlayerLevel이 되기 위해 필요한 경험치를 나타낸다.
    [Table("PlayerLevelRequirement")]
    public partial class PlayerLevelRequirement : BaseModel
    {
        public int PlayerLevel { get; set; } //플레이어 레벨

        public int RequireExperience { get; set; } // 필요 경험치
    }
}
