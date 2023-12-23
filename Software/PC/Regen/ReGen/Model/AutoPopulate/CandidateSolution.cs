using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;

namespace ReGen
{
    /// <summary>
    /// Classe che rappresenta la soluzione candidata
    /// </summary>
    public class CandidateSolution
    {
        /// <summary>
        /// Layer per la soluzione candidata
        /// </summary>
        public CandidateLayer candidateLayer;
        /// <summary>
        /// Punto con direzione della soluzione candidata
        /// </summary>
        public Point2FWithDirection point2FWithDirection;
        /// <summary>
        /// PayloadGroup della soluzione candidata
        /// </summary>
        public PayloadGroup payloadGroup;

    }
}
