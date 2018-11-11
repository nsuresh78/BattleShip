using Battleship.Contracts.Interfaces;
using Battleship.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.CommandHandler.Interfaces
{
    // Interface for various command handler with Execute method
    public interface ICommandHandler<T> where T : ICommand
    {
        string Execute(Board board, T command);
    }
}
