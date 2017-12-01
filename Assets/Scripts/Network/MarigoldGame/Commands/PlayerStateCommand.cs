using MarigoldModel.Commands;
using System;

namespace MarigoldGame.Commands
{
    public class PlayerStateCommand : Command
    {
        public Guid GamePlayerId { get; set; }
        public string Name { get; set; }
    }
}
