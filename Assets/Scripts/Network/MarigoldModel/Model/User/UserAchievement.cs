using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    [Table("UserAchievement")]
    public partial class UserAchievement : BaseUserModel
    {
        public long AchievementId { get; set; }

        public bool RewardReceived { get; set; }
    }
}
