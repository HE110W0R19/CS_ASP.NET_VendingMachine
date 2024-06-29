using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.DataAccess.Entities
{
    /// <summary>
    /// Entity coin object
    /// </summary>
    public class CoinEntity
    {
        public Guid Id { get; set; }
        public int Amount { get; set; } = 0;
        public int Denomination { get; set; }
        public bool isAvailable { get; set; }
    }
}
