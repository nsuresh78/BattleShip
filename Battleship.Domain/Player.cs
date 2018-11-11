using Battleship.Contracts;
using Battleship.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Domain
{
    /// <summary>
    /// Player domain model representing player in the game
    /// </summary>
    public class Player
    {
        // Player has Board where the ships are placed 
        public Board Board { get; set; }

        // Player No. first or second since only 2 players are allowed max.
        public Players PlayerNo { get; }

        // Name of the player, Currently not getting input, we can get input when 2 players are involved
        public string Name { get; }

        // Constructor to instantiate player object with first or second and the name
        public Player(Players playerNo, string name)
        {
            PlayerNo = playerNo;
            Name = name;
        }
    }
}
