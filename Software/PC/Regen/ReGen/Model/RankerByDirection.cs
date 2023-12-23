using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;

namespace ReGen.Model
{
    public class RankerByDirection: PayloadOnPalletRanker 
    {
        DoubleBox angleChosen;  // in sessagesimali
        /// <summary>
        /// Costruttore per UserChoosingRanker
        /// </summary>
        /// <param name="angleChosen">Angolo di priorità per il ranker</param>
        public RankerByDirection(DoubleBox angleChosen)
        {
            this.angleChosen = angleChosen;
        }
        /// <summary>
        /// Torna il rank in base all'angolo di priorità dato
        /// </summary>
        /// <param name="pg">PayloadGroup di cui si cerca il rank</param>
        /// <param name="pos">PalletOnSystem / non usato in questo rank</param>
        /// <returns>Rank del PayloadGroup</returns>
        public override double getRank(PayloadGroup pg, PalletOnSystem pos)
        {
            double rank = 0;
            switch ((int)angleChosen.getVal())
            {
                case 0:
                    rank = rank - (pg.placedMe().getBounds().top() * (Math.Pow(1.45, Program.xWeightPower)));
                    rank = rank + (pg.placedMe().getBounds().right());
                    break;
                case 45:
                    rank = rank + (pg.placedMe().getBounds().top());
                    rank = rank + (pg.placedMe().getBounds().right() * (Math.Pow(1.45, Program.xWeightPower)));
                    break;
                case 90:
                    rank = rank + (pg.placedMe().getBounds().top());
                    rank = rank - (pg.placedMe().getBounds().right() * (Math.Pow(1.45, Program.xWeightPower)));
                    break;
                case 135:
                    rank = rank + (pg.placedMe().getBounds().top());
                    rank = rank - (pg.placedMe().getBounds().left() * (Math.Pow(1.45, Program.xWeightPower)));
                    break;
                case 180:
                    rank = rank - (pg.placedMe().getBounds().top() * (Math.Pow(1.45, Program.xWeightPower)));
                    rank = rank - (pg.placedMe().getBounds().left());
                    break;
                case 225:
                    rank = rank - (pg.placedMe().getBounds().bottom());
                    rank = rank - (pg.placedMe().getBounds().left() * (Math.Pow(1.45, Program.xWeightPower)));
                    break;
                case 270:
                    rank = rank - (pg.placedMe().getBounds().bottom());
                    rank = rank - (pg.placedMe().getBounds().right() * (Math.Pow(1.45, Program.xWeightPower)));
                    break;
                case 315:
                    rank = rank - (pg.placedMe().getBounds().bottom() * (Math.Pow(1.45, Program.xWeightPower)));
                    rank = rank + (pg.placedMe().getBounds().right());
                    break;
            }
            return rank;
            //return -pg.placedMe().getBounds().bottom() + (pg.placedMe().getBounds().right() * (Math.Pow(1.45, Program.xWeightPower)));// pg.placedMe().getBounds().right();
        }
    }
}
