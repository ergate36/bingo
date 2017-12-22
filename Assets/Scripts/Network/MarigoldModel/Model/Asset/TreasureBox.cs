using System;
using System.Collections.Generic;
using System.Text;

namespace MarigoldModel.Model
{
    public partial class TreasureBox : BaseModel
    {
        public string Name { get; set; }

        public long RandomRewardId { get; set; }

        public string Image { get; set; }
    }
}
