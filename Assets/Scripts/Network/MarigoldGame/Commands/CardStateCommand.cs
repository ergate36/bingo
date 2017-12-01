using MarigoldModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace MarigoldGame.Commands
{
    public class CardStateCommand : Command
    {
        public int Index { get; set; }
        public List<int> CheckedIndexList { get; set; }
        
        public CardStateCommand()
        {
            CheckedIndexList = new List<int>();
        }
    }
}
