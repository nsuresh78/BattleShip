using Battleship.Contracts.Enums;
using Battleship.Contracts.Interfaces;
using System;

namespace Battleship.Contracts.Commands
{
    // Add Ship Command Contract with necessary input
    public class AddShipCommand : ICommand
    {
        public Coordinate Coordinate { get; set; }

        public int Length { get; set; }

        public Direction Direction { get; set; }
    }
}

