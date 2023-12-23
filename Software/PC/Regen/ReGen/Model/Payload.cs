using Sintec.Tool;
using ReGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ReGen
{
    /// <summary>
    /// Rappresenta il modello del Payload
    /// </summary>
    public class Payload
    {
        protected PayloadStrategy ps;
        /// <summary>
        /// Costruttore della classe Payload
        /// </summary>
        /// <param name="ps">Strategy da applicare</param>
        public Payload(PayloadStrategy ps)
        {
            resetStrategy(ps);
        }
        /// <summary>
        /// Torna la Strategy applicata al Payload
        /// </summary>
        /// <returns></returns>
        public PayloadStrategy getPayloadStrategy()
        {
            return this.ps;
        }
        /// <summary>
        /// Fornisce l'ingombro del Payload in base alla Strategy
        /// </summary>
        /// <returns>Fornisce l'ingombro del Payload in base alla Strategy</returns>
        public BoundsF2D getOriginalBounds()
        {
            return ps.get2DBounds();
        }
        /// <summary>
        /// Torna la dimensione del Payload
        /// </summary>
        /// <returns>Dimensione del Payload</returns>
        public Point3F getOriginalSize()
        {
            return ps.getSize();
        }
        /// <summary>
        /// Fornisce dati relativi al tool
        /// </summary>
        /// <returns>Dati relativi al tool</returns>
        public bool[] getToolData()
        {
            return ps.getToolData();
        }
        /// <summary>
        /// Setta la Strategy specificata
        /// </summary>
        /// <param name="ps">PayloadStrategy da applicare</param>
        public void resetStrategy(PayloadStrategy ps)
        {
            this.ps = ps;
        }
        /// <summary>
        /// Torna una stringa con le informazioni della strategy applicata al Payload
        /// </summary>
        /// <returns>Strina con le informazioni della Strategy applicata al Payload</returns>
        public override string ToString()
        {
            String res = ps.ToString();
            return res;
        }
    }
}
