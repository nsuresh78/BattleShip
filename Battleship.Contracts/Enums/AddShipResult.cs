using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Contracts.Enums
{
    /// <summary>
    /// Add Ship Result with various add ship results
    /// </summary>
    public enum AddShipResult
    {
        InvalidSlotPosition,
        Overlap,
        NotEnoughSpace,
        Ok
    }
}
