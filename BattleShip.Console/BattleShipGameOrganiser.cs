using Battleship.CommandHandler.Interfaces;
using Battleship.Contracts;
using Battleship.Contracts.Commands;
using Battleship.Domain;
using cons = System.Console;

namespace BattleShip.Console
{
    public class BattleShipGameOrganiser
    {
        // Add Ship Command Handler
        private readonly ICommandHandler<AddShipCommand> _addShipCommandHandler;

        // Attack Ship Command Handler
        private readonly ICommandHandler<AttackPositionCommand> _attackShipCommandHandler;

        // Players list, max 2 players
        public Player[] Players = new Player[1];

        public BattleShipGameOrganiser(ICommandHandler<AddShipCommand> addShipCommandHandler, ICommandHandler<AttackPositionCommand> attackShipCommandHandler)
        {
            _addShipCommandHandler = addShipCommandHandler;
            _attackShipCommandHandler = attackShipCommandHandler;
        }

        /// <summary>
        /// Set up the Game Instantiate Board, add ships at specific places
        /// </summary>
        /// <param name="player">player for the game</param>
        /// <param name="noShips">No. of ships to be added </param>
        public void Setup(Player player, int noShips)
        {
            Coordinate coordinate;
            int length = 0, direction = 0;

            // Players will be stored in the array index 0 or 1 based on first player or second
            switch (player.PlayerNo)
            {
                case Battleship.Contracts.Enums.Players.First:
                    Players[0] = player;
                    break;
                case Battleship.Contracts.Enums.Players.Second:
                    Players[1] = player;
                    break;
            }

            // Instantiate a new Board for player
            player.Board = new Board();

            // Add Ships to the Player
            for(var index=0; index < noShips; index++)
            {
                do
                {
                    // Read co-ordinate for placing ship
                    coordinate = InputHelper.Read_Coordinates();
                    // Read length of ship
                    length = InputHelper.Read_Length();

                    // Read placement direction how ship is placed horizontally or vertically on the board
                    direction = InputHelper.Read_PlacementDirection();
                    
                    // Add ship to the board and display result
                    var actionResponse = AddShip(player.Board, coordinate, length, direction);

                    cons.Clear();
                    cons.WriteLine($"Action Result   : {actionResponse} \n\r");
                } while (player.Board.Ships.Length <= index); // make sure ship is placed otherwise repeat input and add ship again
            }

            cons.WriteLine("Game Board Setup for Player");
        }

        /// <summary>
        /// Attack the specific co-ordinate on the board
        /// </summary>
        /// <param name="board">The board</param>
        /// <param name="coordinate">The co-ordinate</param>
        /// <returns></returns>
        internal string AttackShip(Board board, Coordinate coordinate)
        {
            // Attack ship handler is called
            return _attackShipCommandHandler.Execute(board, new AttackPositionCommand
            {
                Coordinate = coordinate
            });
        }


        // Call Add Ship Handler to add a ship on the board
        private string AddShip(Board board, Coordinate coordinate, int length, int direction)
        {
            return _addShipCommandHandler.Execute(board, new AddShipCommand
                                                        {
                                                            Coordinate = coordinate,
                                                            Length = length,
                                                            Direction = direction == 1 ? Battleship.Contracts.Enums.Direction.Horizontal : Battleship.Contracts.Enums.Direction.Vertical
                                            });
        }
    }
}
