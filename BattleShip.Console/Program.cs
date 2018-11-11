using Battleship.CommandHandler;
using Battleship.CommandHandler.Interfaces;
using Battleship.Contracts.Commands;
using Battleship.Domain;
using Microsoft.Extensions.DependencyInjection;
using cons = System.Console;

namespace BattleShip.Console
{
    class Program
    {
        private static ServiceProvider _serviceProvider;
        private static BattleShipGameOrganiser battleShipGameOrganiser;

        static void Main(string[] args)
        {
            Build_Services();
            RunBattleShipGame();                        
        }

        /// <summary>
        /// Setup DI for all command handlers and Game Organiser
        /// </summary>
        private static void Build_Services()
        {
            _serviceProvider = new ServiceCollection()
                                        .AddSingleton<ICommandHandler<AddShipCommand>, AddShipCommandHandler>()
                                        .AddSingleton<ICommandHandler<AttackPositionCommand>, AttackPositionCommandHandler>()
                                        .AddScoped<BattleShipGameOrganiser>()
                                        .BuildServiceProvider();            
        }

        /// <summary>
        /// Main method to run the Battle Ship Game
        /// </summary>
        private static void RunBattleShipGame()
        {
            // Get instance of the battle ship game organiser from DI container
            battleShipGameOrganiser = _serviceProvider.GetService<BattleShipGameOrganiser>();

            // Read no. of ships first
            var noShips = InputHelper.Read_NoShips();

            // Create a new player for the game
            var player = new Player(Battleship.Contracts.Enums.Players.First, "Default");

            // Setup Board and ships to the player
            battleShipGameOrganiser.Setup(player, noShips);
            cons.WriteLine("Start Attack Ships");


            do
            {
                //Attack ship position
                AttackShip(player.Board);
            } while (!player.Board.IsGameLost);

            cons.WriteLine("Press any key to Exit Game");
            cons.ReadKey();
        }

        /// <summary>
        /// Call Attack ship for the board
        /// </summary>
        /// <param name="board"></param>
        private static void AttackShip(Board board)
        {
            // Read co-ordinates for attack
            var coordinate = InputHelper.Read_Coordinates();

            // Attack ship and display the result in console
            var actionResponse = battleShipGameOrganiser.AttackShip(board, coordinate);
            cons.Clear();
            cons.WriteLine($"Action Result   : {actionResponse} \n\r");
        }
    }
}
