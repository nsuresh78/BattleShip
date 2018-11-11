using Battleship.CommandHandler.Interfaces;
using Battleship.Contracts.Commands;
using Battleship.Domain;

namespace Battleship.CommandHandler
{
    /// <summary>
    /// Add Ship Command Handler, implements ICommandHandler interface with AddShipCommand 
    /// </summary>
    public class AddShipCommandHandler : ICommandHandler<AddShipCommand>
    {
        // Implement Execute method of the interface
        public string Execute(Board board, AddShipCommand command)
        {
            // Validate board is valid, otherwise return error message
            if (board == null)
            {
                return "Board has to be Created before adding Battleship";
            }

            // Add Ship on the board and return message based on result
            var addShipResult = board.AddShip(command.Coordinate, command.Length, command.Direction);

            // Generate customised message based on result
            switch (addShipResult)
            {
                case Contracts.Enums.AddShipResult.Ok:
                    return "Battleship Added";
                case Contracts.Enums.AddShipResult.Overlap:
                    return "Battleship can not be added since it's position overlaps with another ship";
                case Contracts.Enums.AddShipResult.NotEnoughSpace:
                    return "Battleship can not be added since there's not enough space in the board";
                case Contracts.Enums.AddShipResult.InvalidSlotPosition:
                    return "Battleship can not be added since the coordinates are invalid";
                default:
                    return string.Empty;
            }
        }
    }
}
