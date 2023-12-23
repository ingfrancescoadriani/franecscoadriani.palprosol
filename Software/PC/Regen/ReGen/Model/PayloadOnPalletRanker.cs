using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReGen.Model
{
    /// <summary>
    /// Classe astratta per il Ranker
    /// </summary>
    public abstract class PayloadOnPalletRanker
    {
        public abstract double getRank(PayloadGroup pg, PalletOnSystem pos);
    }
}
