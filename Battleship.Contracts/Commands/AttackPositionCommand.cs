using Battleship.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Contracts.Commands
{
    // Attack Position Command with necessary input
    public class AttackPositionCommand : ICommand
    {
        public Coordinate Coordinate { get; set; }
    }
}
