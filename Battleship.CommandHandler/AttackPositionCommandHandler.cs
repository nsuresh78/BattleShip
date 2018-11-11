using Battleship.CommandHandler.Interfaces;
using Battleship.Contracts.Commands;
using Battleship.Domain;

namespace Battleship.CommandHandler
{
    /// <summary>
    /// Attack Position Command Handler, Implement ICommandHandler interface with AttackPositionCommand
    /// </summary>
    public class AttackPositionCommandHandler : ICommandHandler<AttackPositionCommand>
    {
        // Implement Execute method of interfae
        public string Execute(Board board, AttackPositionCommand command)
        {
            // Validate whether board is valid and ships are already added
            if (board == null)
            {
                return "Board has to be Created before attacking";
            }
            else if(board.Ships.Length == 0)
            {
                return "At least one Battleship has to be added before attacking";
            }

            // Call attack position on board and return message based on result
            var attackResult = board.AttackPosition(command.Coordinate);

            // Build customised output message based on the result
            switch (attackResult)
            {
                case Contracts.Enums.AttackResult.Hit:
                    return "Attack has HIT Battleship";
                case Contracts.Enums.AttackResult.Miss:
                    return "Attack MISSED Battleship";
                case Contracts.Enums.AttackResult.LostGame:
                    return "GAME LOST !!!";
                case Contracts.Enums.AttackResult.None:
                    return "Attack Position is either Invalid or Duplicate Attack on same position";
                default:
                    return string.Empty;
            }
        }
    }
}
