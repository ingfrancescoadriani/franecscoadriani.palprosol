using ReGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;

namespace ReGen
{
    //TODO classe non utilizzata
    /// <summary>
    /// Classe che rappresenta la Strategy per i sacchi
    /// </summary>
    public class SacStrategy : PayloadStrategy
    {
        private string name = "Sacco";
        Point3F size;
        /// <summary>
        /// Costruttore per la classe SacStrategy
        /// </summary>
        /// <param name="size">Dimensione</param>
        public SacStrategy(Point3F size)
        {
            this.size = size;
        }
        /// <summary>
        /// Trova il più piccolo rettangolo che contiene gli elementi nella Strategy
        /// </summary>
        /// <returns>Il più piccolo rettangolo che contiene gli elementi nella Strategy</returns>
        public override BoundsF2D get2DBounds()
        {
            return new BoundsF2D(new Point2F(0,0), getSize().to2DLocation());
        }
        public override Point3F getSize()
        {
            return size;
        }
        public override bool[] getToolData()
        {
            return null;
        }
        public override string ToString()
        {
            return name + "[" + size.X + ";" + size.Y + "]"; // this.GetType().Name;
        }

        public override Point2F getInitialCenter()
        {
            return new Point2F(0, 0);
        }

        public override string toDataString()
        {
            throw new NotImplementedException();
        }
        public override bool haveLabelOnSideAt(double angle)
        {
            return false;
        }
    }
}
 
