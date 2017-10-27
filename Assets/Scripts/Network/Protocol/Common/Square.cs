using MarigoldGame.Common.Commands;
using MarigoldGame.Game.Blitz;
//using MarigoldGame.Main;
using MarigoldModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarigoldGame.Common
{
    public class Square
    {
        public int Number { get; set; } // 빙고 숫자
        public bool Checked { get; set; } // 빙고 숫자가 체크되었는지 보여준다.
        public PowerUp PowerUp { get; set; } // 칸에 묶여 있는 아이템. 중복 될 수 없다.

        //public Square(int value, bool check = false)
        //{
        //    Number = value;
        //    Checked = check;
        //    PowerUp = null;
        //}

        //internal Command CheckSquare(BlitzPlayer player)
        //{
        //    if (Checked == true) { throw new Exception("Already checked"); }

        //    var command = new CheckSquareCommand();
        //    Checked = true;
        //    if (PowerUp != null)
        //    {
        //        command.SubCommandList.Add(PowerUpExecutor.UsePowerUp(player, PowerUp));
        //    }
        //    return command;
        //}

        //internal Command PlantSquare(BlitzPlayer player, int powerUpId)
        //{
        //    if (Checked == true) { throw new Exception("Already checked"); }
        //    if (PowerUp != null) { throw new Exception("Already planted"); }

        //    var powerUp = DatabaseCache.PowerUpCache.Where(w => w.Id == powerUpId).FirstOrDefault();
        //    PowerUp = powerUp ?? throw new Exception(String.Format("Cannot Find Power up. {0}", powerUpId));

        //    var command = new PlantSquareCommand();
        //    return command;
        //}
    }
}
