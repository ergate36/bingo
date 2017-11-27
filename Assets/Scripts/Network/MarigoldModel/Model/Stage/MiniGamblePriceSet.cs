using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MarigoldModel.Model
{
    [Table("MiniGamblePriceSet")]
    public partial class MiniGamblePriceSet : BaseModel
    {
        public int Multiplier { get; set; }

        // foreign key
        public Int64 MiniGamblePriceId { get; set; }
        public MiniGamblePrice MiniGamblePrice { get; set; }
    }
}
