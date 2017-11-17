using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    [Table("UserPowerUp")]
    public partial class UserPowerUp : BaseUserModel
    {
        public Int64 PowerUpId { get; set; }

        public Int64 Count { get; set; }
    }
}
