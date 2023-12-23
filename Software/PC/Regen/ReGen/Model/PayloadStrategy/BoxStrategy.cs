using Sintec.Tool;
using ReGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReGen
{
    /// <summary>
    /// Strategy per le scatole
    /// </summary>
    public class BoxStrategy : PayloadStrategy
    {
        Point3F size;
        /// <summary>
        /// Costruttore per BoxStrategy
        /// </summary>
        /// <param name="size">Dimensione del box</param>
        public BoxStrategy(Point3F size, String name)
            :base(name)
        {
            this.size = size;
        }
        /// <summary>
        /// Costruttore per BoxStrategy
        /// </summary>
        /// <param name="initString">Stringa di inizializzazione per la dimensione del box</param>
        public BoxStrategy(String initString, String name):base(name)
        {
            this.size = Point3F.from(initString.Split(']')[1].Split('[')[0]);
        }
        /// <summary>
        /// Ricava l'ingombro
        /// </summary>
        /// <returns>Ingombro del box</returns>
        public override BoundsF2D get2DBounds()
        {
            return new BoundsF2D(new Point2F(-getSize().X / 2.0F, -getSize().Y / 2.0F), new Point2F(getSize().X, getSize().Y));
        }
        /// <summary>
        /// Torna la dimensione
        /// </summary>
        /// <returns>Dimensione</returns>
        public override Point3F getSize()
        {
            return size;
        }
        /// <summary>
        /// Fornisce dati relativi al tool / torna null per questa strategy
        /// </summary>
        /// <returns>Null per questa strategy</returns>
        public override bool[] getToolData()
        {
            return new bool[0];
        }
        /// <summary>
        /// Informazione sul nome e dimensione del box
        /// </summary>
        /// <returns>Stringa contenente il nome e la dimensione</returns>
        public override string ToString()
        {
            return this.getName() + "[" + size.X + ";" + size.Y + "]"; // this.GetType().Name;
        }
        /// <summary>
        /// Torna il punto Point2F (0, 0)
        /// </summary>
        /// <returns>Point2F (0, 0)</returns>
        public override Point2F getInitialCenter()
        {
            return new Point2F(0, 0);
        }
        /// <summary>
        /// Torna le informazioni della dimensione come Stringa
        /// </summary>
        /// <returns>Stringa che contiene le informazioni sulla dimensione</returns>
        public override string toDataString()
        {
            return "[size]" + getSize();
        }
        /// <summary>
        /// Trova se la Label è sull'angolo dato / sempre null per questa strategy
        /// </summary>
        /// <param name="angle">Angolo su cui vedere se è presente la Label</param>
        /// <returns>Sempre False per questa strategy</returns>
        public override bool haveLabelOnSideAt(double angle)
        {
            return false;
        }
    }

}