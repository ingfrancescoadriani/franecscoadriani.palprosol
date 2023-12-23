using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;

namespace ReGen
{
    /// <summary>
    /// Classe astratta per rappresentare la Strategy
    /// </summary>
    public abstract class PayloadStrategy
    {
        private String name = "";
        public PayloadStrategy() { }
        public PayloadStrategy(String name) { this.name = name; }
        public String getName()
        {
            return name;
        }
        //fornisce l'ingombro
        /// <summary>
        /// Fornisce l'ingombro
        /// </summary>
        /// <returns>Fornisce l'ingombro</returns>
        public abstract BoundsF2D get2DBounds();
        //fornisce le misure originali senza ingombro
        public abstract Point3F getSize();
        //fornisce dati relativi al tool
        /// <summary>
        /// Fornisce dati relativi al tool
        /// </summary>
        /// <returns>Dati relativi al tool</returns>
        public abstract bool[] getToolData();
        //fornisce il centro all'inizio
        public abstract Point2F getInitialCenter();
        //fornisce una stringa per il salvataggio
        public abstract String toDataString();
        //restituisce se c'è un'etichetta nel lato indicato dall'angolo
        public abstract bool haveLabelOnSideAt(double angle);
    }
}
