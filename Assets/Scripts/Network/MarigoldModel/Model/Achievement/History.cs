using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    [Table("History")]
    public partial class History : BaseModel
    {
        public HistoryType Type { get; set; }
        public HistoryResetType ResetType { get; set; }
    }
}
