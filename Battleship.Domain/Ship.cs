using Battleship.Contracts;
using Battleship.Contracts.Enums;
using Battleship.Contracts.Interfaces;
using System.Collections.Generic;

namespace Battleship.Domain
{
    /// <summary>
    /// Ship Domain Model represents battle ship and related operations such as to create ship and fire a slot
    /// </summary>
    public class Ship
    {
        // Count of positions fired is stored whenever the shot hits the ship
        private int _positionsFired;

        // Slot positions storing the location where the ship is placed
        public HashSet<Coordinate> SlotPositions { get; }

        // Read-only property to check whether the all the slots have been fired and the ship has sunk
        public bool HasSunk
        {
            get
            {
                return SlotPositions.Count == _positionsFired;
            }
        }

        // Private constructor so the ship is not created from outside, we need to use Create Ship static method to create the ship instance
        // Instantiates the hash set with coordinates with required number of slot positions based on the length of the ship
        private Ship(int length)
        {
            SlotPositions = new HashSet<Coordinate>(length);
        }

        /// <summary>
        /// Ship Creator to create a ship on the coordinate and with ship length specified
        /// Ship can be placed in horizontal or vertical direction the slot positions are generated and stored in SlotPositoins hash set 
        /// This will make it easy to compare whether the ship is hit or miss when a shot is fired at some coordinate
        /// </summary>
        /// <param name="coordinate">The Coordinate</param>
        /// <param name="length">The length of the ship</param>
        /// <param name="placeDirection">Direction horizontal or vertical</param>
        /// <returns></returns>
        public static Ship CreateShip(Coordinate coordinate, int length, Direction placeDirection)
        {
            // Construct the ship object 
            var ship = new Ship(length);

            // Generate the slot positions based on the X & Y Coordinates of the inital position and subsequent positions based on length of the ship
            for(var index = 0; index < length; index++)
            {
                ship.SlotPositions.Add(new Coordinate { X = coordinate.X, Y = coordinate.Y });
                
                // if it's placed horizontally, the position Y will be incremented for next positions otherwise X coordinate will be incremented
                if(placeDirection == Direction.Horizontal)
                {
                    coordinate.Y++;
                }
                else if(placeDirection == Direction.Vertical)
                {
                    coordinate.X++;
                }
            }

            return ship;
        }

        /// <summary>
        /// Fire Slot when a slot is fired
        /// </summary>
        /// <param name="coordinate">The Coordinate where shot is fired</param>
        /// <returns></returns>
        public bool FireSlot(Coordinate coordinate)
        {
            // First check whether the ship occupies the slot then increment position fired
            // return true if it's shot or false otherwise
            if (SlotPositions.Contains(coordinate))
            {
                _positionsFired++;
                return true;
            }

            return false;
        }
    }
}
