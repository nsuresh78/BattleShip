using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Contracts
{
    // Coordinate to represent a position with X & Y 
    public struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
