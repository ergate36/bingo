using MarigoldModel.Commands;
using MarigoldModel.Model;
using System.Collections.Generic;

namespace MarigoldGame.Commands
{
    public class CalledNumberListCommand : Command
    {
        public List<int> CalledNumberList { get; set; } // 불려진 빙고 넘버 리스트

        public CalledNumberListCommand() : base(CommandType.CALLED_NUMBER_LIST)
        {

        }
    }
}
