using Sintec.Tool;
using ReGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReGen
{
    /// <summary>
    /// Rappresenta la Strategy per le scatole con ingombro
    /// </summary>
    public class BoxWithRightSurplusStrategy : BoxStrategy
    {
        Point3F size;
        float surplus;
        float tollerance;
        private string name = "Scatola con ingombro";
        /// <summary>
        /// Costruttore per la classe BoxWithRightSurplusStrategy
        /// </summary>
        /// <param name="size">Dimensione</param>
        /// <param name="surplus">Valore dell'ingombro</param>
        /// <param name="tollerance">Tolleranza</param>
        public BoxWithRightSurplusStrategy(Point3F size, float surplus, float tollerance, String name)
            : base(size, name)
        {
            this.size = size;
            this.surplus = surplus;
            this.tollerance = tollerance;
        }
        /// <summary>
        /// Costruttore per la classe BoxWithRightSurplusStrategy data una stringa
        /// </summary>
        /// <param name="initString">Stringa per l'inizializzazione</param>
        public BoxWithRightSurplusStrategy(String initString)
            : base(initString, "")
        {
            this.size = Point3F.from(initString.Split(']')[1].Split('[')[0]);
            this.surplus = (float)Util.getDoubleFromString(initString.Split(']')[2].Split('[')[0]);
            this.tollerance = (float)Util.getDoubleFromString(initString.Split(']')[3].Split('[')[0]);
        }
        /// <summary>
        /// Trova il più piccolo rettangolo che contiene gli elementi nella Strategy
        /// </summary>
        /// <returns>Il più piccolo rettangolo che contiene gli elementi nella Strategy</returns>
        public override BoundsF2D get2DBounds()
        {
            return new BoundsF2D(new Point2F(-getSize().X / 2.0F, -getSize().Y / 2.0F), new Point2F(getSize().X + surplus, getSize().Y));
        }
        //TODO metodo inutilizzato
        public BoundsF2D get2DSurplusBounds()
        {
            //non sono corretti i valori di posizione
            return new BoundsF2D(new Point2F(surplus, 0), new Point2F(surplus, getSize().Y));
        }
        /// <summary>
        /// Trova la dimensione della Strategy
        /// </summary>
        /// <returns>Dimensione della Strategy</returns>
        public override Point3F getSize()
        {
            return size;
        }
        public override bool[] getToolData()
        {
            return new bool[0];
        }
        public override string ToString()
        {
            return name + "[" + size.X + " + " + this.surplus + ";" + size.Y + "]"; // this.GetType().Name;
        }
        public override Point2F getInitialCenter()
        {
            return new Point2F(0, 0);
        }
        public override string toDataString()
        {
            return "[size]" + getSize() + "[surplus]" + surplus + "[tollerance]" + tollerance;
        }
        public override bool haveLabelOnSideAt(double angle)
        {
            return false;
        }
    }
}