using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    [Table("UserStageCompleteCollection")]
    public partial class UserStageCompleteCollection : BaseUserModel
    {
        public long StageId { get; set; }

        public bool RewardReceived { get; set; }
    }
}
