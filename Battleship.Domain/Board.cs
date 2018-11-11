using Battleship.Contracts;
using Battleship.Contracts.Enums;
using System;
using System.Collections.Generic;

namespace Battleship.Domain
{
    /// <summary>
    /// The Board Domain Model to represent a battle game board 
    /// </summary>
    public class Board
    {
        // Define constant for board slots 10 X 10
        private const int X_SLOTS = 10;
        private const int Y_SLOTS = 10;

        // List of ships on the board
        private List<Ship> _ships;

        // Keep track of fired slots. This would be used to check if attack position is already hit.
        private HashSet<Coordinate> _firedSlots;

        public Ship[] Ships
        {
            get
            {
                return _ships.ToArray();
            }
        }

        // Property to check Game is already lost
        public bool IsGameLost
        {
            get
            {
                return _ships.TrueForAll(ship => ship.HasSunk);
            }
        }

        // Constructor, Instantiate list of ships and firedslots to be 
        public Board()
        {
            _ships = new List<Ship>();
            _firedSlots = new HashSet<Coordinate>();
        }

        /// <summary>
        ///  Method to Add Ship on the coordinate, length of the ship and direction
        /// </summary>
        /// <param name="coordinate">'The start coordinate where ship is placed</param>
        /// <param name="length">Length of ship </param>
        /// <param name="direction">Direction horizontal or vertical</param>
        /// <returns></returns>
        public AddShipResult AddShip(Coordinate coordinate, int length, Direction direction)
        {
            // Check whether the coordinate is valid, otherwise return error result. For example 11 X 5 is invalid since board is 10 X 10
            if (!IsCoordinateValid(coordinate))
                return AddShipResult.InvalidSlotPosition;

            // Check whether placement location and direction results within the boundaries of the board, otherwis return error result
            // Sometimes the coordinates are fine but the ship length might result the ship to go out of board
            if (IsPlacementOutOfBoard(coordinate, length, direction))
            {
                return AddShipResult.NotEnoughSpace;
            }

            // For each ship already added to the board make sure the new ship to be placed doesn't overlap any other ship, otherwise return error
            foreach (var ship in _ships)
            {
                if (IsPlacementOverlaps(ship.SlotPositions, coordinate, length, direction))
                {
                    return AddShipResult.Overlap;
                }

            }

            // If the position coordinate and everything is valid, Create ship object and add to the ship collection
            var newShip = Ship.CreateShip(coordinate, length, direction);
            _ships.Add(newShip);
            return AddShipResult.Ok;
        }

        /// <summary>
        /// Attack a position with X & Y Coordinates for all ships whether they occupy the position in the board.
        /// </summary>
        /// <param name="coordinate">The X & Y coordinates</param>
        /// <returns>Returns the attack result which could be Hit or Miss orelse Lost Game if the hit resulted in sinking the ship</returns>
        public AttackResult AttackPosition(Coordinate coordinate)
        {
            AttackResult attackResult = AttackResult.None;

            // Is this coordinate on the board? 
            // Or Is the same Slot already fired? then just ignore the shot
            if (!IsCoordinateValid(coordinate) || _firedSlots.Contains(coordinate))
            {
                return attackResult;
            }

            // Set Default attack result as Miss if it's not hit
            attackResult = AttackResult.Miss;

            // Check whether the shot hits any of the ships, so loop through all the ships on the board
            foreach (var ship in _ships)
            {
                // If the shot resulted in hit, set result as a hit
                if (ship.FireSlot(coordinate))
                {
                    attackResult = AttackResult.Hit;
                    break;
                }
            }

            // The hit could have resulted in sinking the last ship if it has hit the last slot for the last ship making it sunk
            if (attackResult == AttackResult.Hit)
            {
                // Check if all ships are sunk then return result as Lost Game
                if (_ships.TrueForAll(ship => ship.HasSunk))
                {
                    attackResult = AttackResult.LostGame;
                }
            }

            return attackResult;
        }

        /// <summary>
        /// Check if the coordinate is valid based on the max. X & Y Slots for the board. The coordinate should be greater than 1 & 10 X 10
        /// </summary>
        /// <param name="coordinate">The X & Y Coordinates</param>
        /// <returns></returns>
        private bool IsCoordinateValid(Coordinate coordinate)
        {
            return coordinate.X >= 1 && coordinate.X <= X_SLOTS &&
            coordinate.Y >= 1 && coordinate.Y <= Y_SLOTS;
        }


        /// <summary>
        /// The ship might go out of board because of length of ship even though the coordinates are valid
        /// Check the placement is not out of board
        /// </summary>
        /// <param name="coordinate">The coordinate of the start position</param>
        /// <param name="length">Length of the ship</param>
        /// <param name="direction">Direction of placement horizontal or vertical</param>
        /// <returns></returns>
        private bool IsPlacementOutOfBoard(Coordinate coordinate, int length, Direction direction)
        {
            // Check the end slot position is not out of board horizontally
            if (direction == Direction.Horizontal)
            {
                return (coordinate.X + length) > X_SLOTS;
            }

            // Check the end slot position is not out of board vertically if placement is vertical
            if (direction == Direction.Vertical)
            {
                return (coordinate.Y + length) > X_SLOTS;
            }

            return false;
        }

        /// <summary>
        /// Check whether hte placement doesn't overlap with other ships placed already
        /// </summary>
        /// <param name="slotPositions">The slot positions of the ships</param>
        /// <param name="coordinate">Start Coordinate of the Ship</param>
        /// <param name="length">Length of the ship</param>
        /// <param name="direction">Direction horizonttal or vertical</param>
        /// <returns></returns>
        private bool IsPlacementOverlaps(HashSet<Coordinate> slotPositions, Coordinate coordinate, int length, Direction direction)
        {
            if (direction == Direction.Horizontal)
            {
                // Check each slot position of the ship is not occupied
                for (var index = 0; index <= length; index++)
                {
                    // if overlaps return true
                    if (slotPositions.Contains(new Coordinate(coordinate.X + index, coordinate.Y)))
                        return true;
                }
            }
            else
            {
                // Check each slot position of the ship is not occupied
                for (var index = 0; index <= length; index++)
                {
                    // if overlaps return true
                    if (slotPositions.Contains(new Coordinate(coordinate.X, coordinate.Y + index)))
                        return true;
                }
            }

            return false;
        }
    }
}
