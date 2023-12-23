using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReGen.Model.AutoPopulate
{
    /// <summary>
    /// Classe che serve per approvare la soluzione della strategy
    /// </summary>
    public abstract class ApproverStrategy
    {
        public bool doWhileCreate; // or doOnEnd
        /// <summary>
        /// Test per l'approvazione della soluzione generata in funzione di varie specifiche
        /// </summary>
        /// <param name="layer">Layer su cui fare il test della soluzione</param>
        /// <returns>True/False se la soluzione è approvata o meno</returns>
        public abstract bool approveSolution(Layer layer);
        // genera vero/falso riguardo l'approvazione della soluzione generata in funzione di varie specifche
    }
}
