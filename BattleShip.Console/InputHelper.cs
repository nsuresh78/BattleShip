using Battleship.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using cons = System.Console;

namespace BattleShip.Console
{
    /// <summary>
    /// This class is a helper to get input from user 
    /// </summary>
    internal static class InputHelper
    {
        // Read no. of ships from console, make sure the input is valid and within range
        // Assume no. of ships to be max. 10
        internal static int Read_NoShips()
        {
            int noShips;
            cons.WriteLine("Enter No. of the Ships");

            do
            {
                // Read input
                noShips = Read_Number();

                // If input is not within range then again read a new value until input is valid
                if (noShips < 1 || noShips > 10)
                {
                    cons.WriteLine("No. of Ships Entered is Invalid");
                }
                else
                {
                    return noShips;
                }
            } while (noShips < 1 || noShips > 10);

            return noShips;
        }

        // Read length of the ship from console, make sure the input is valid and within range
        // Assume length to be max. 10 since board is 10x10
        internal static int Read_Length()
        {
            int length;
            cons.WriteLine("Enter Length of the Ship");

            do
            {
                length = Read_Number();

                // If input is not within range then again read a new value until input is valid
                if (length < 1 || length > 10)
                {
                    cons.WriteLine("Ship Length Entered is Invalid");
                }
                else
                {
                    return length;
                }
            } while (length < 1 || length > 10);

            return length;
        }

        /// <summary>
        /// Read Placement Direction 1 - horizontal or 2 - vertical
        /// </summary>
        /// <returns></returns>
        internal static int Read_PlacementDirection()
        {
            int direction;
            cons.WriteLine("Enter Placement Direction");
            cons.WriteLine("1. Horizontal");
            cons.WriteLine("2. Vertical");

            do
            {
                // Read number
                direction = Read_Number();

                // Make sure the direction is either 1 or 2
                if (direction < 1 || direction > 2)
                {
                    cons.WriteLine("Placement Direction entered is invalid");
                }
                else
                {
                    return direction;
                }
            } while (direction < 1 || direction > 2);

            return direction;
        }

        /// <summary>
        /// Read Co-ordinates X & Y to be valid numbers
        /// </summary>
        /// <returns></returns>
        internal static Coordinate Read_Coordinates()
        {
            var coordinate = new Coordinate();

            cons.WriteLine("Enter X Position");
            coordinate.X = Read_Number();
            cons.WriteLine("Enter Y Position");
            coordinate.Y = Read_Number();

            return coordinate;
        }

        // Read number and make sure it's valid number
        private static int Read_Number()
        {
            do
            {
                // Read input
                var number = cons.ReadLine();

                // Check whether it's valid number, otherwise read again
                if (Int32.TryParse(number, out int value))
                {
                    return value;
                }

                cons.WriteLine("Input is not a valid number");
            } while (true);
        }
    }
}
