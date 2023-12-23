using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReGen.Model
{
    /// <summary>
    /// Classe Ranker che da priorità ai PayloadGroup a Nord
    /// </summary>
    public class OnNordRanker: PayloadOnPalletRanker 
    {
        /// <summary>
        /// Torna la coordinata del lato destro del PayloadGroup
        /// </summary>
        /// <param name="pg">PayloadGroup di cui si cerca la posizione</param>
        /// <param name="pos">PalletOnSystem / non usato in questa strategy</param>
        /// <returns></returns>
        public override double getRank(PayloadGroup pg, PalletOnSystem pos)
        {
            return pg.placedMe().getBounds().right();
        }
    }
}
