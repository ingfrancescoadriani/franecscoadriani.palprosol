    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing;
    using Sintec.Tool;
using ReGen.Model;

namespace ReGen
{
    /// <summary>
    /// Classe che gestisce l'accostamento
    /// </summary>
    public class Approacher : Binder
    {
        List<Figure> additionalFigure = new List<Figure>();
        PayloadGroup payloadPlacedReferredTo;
        /// <summary>
        /// Costruttore per la classe Approacher
        /// </summary>
        /// <param name="payload">PayloadGroup</param>
        /// <param name="payloadPlacedReferredTo">PayloadGroup a cui si fa riferimento</param>
        /// <param name="payloadOriginal">PayloadGroup originale</param>
        public Approacher(PayloadGroup payload, PayloadGroup payloadPlacedReferredTo, PayloadGroup payloadOriginal, Layer l)
            : base(payload, payloadPlacedReferredTo, payloadOriginal, l)
        { }
        /// <summary>
        /// Aggiunge il PayloadGroup di riferimento alla lista degli approcci del payloadGroup originale
        /// </summary>
        public override void bind()
        {
            //TODO a che serve?
            if (Sequencer.getProgressive(payloadPlacedReferredTo.getId(), l) < Sequencer.getProgressive(getId(), l))
            {
                SideOrPoint s = nearSideOrPoint(payloadPlacedReferredTo);
                if (s != null)
                {
                    addApprochedPayload(payloadPlacedReferredTo, s);
                    this.payloadGroupOriginal.addApprochedPayload(payloadPlacedReferredTo, s);
                }
            }
        }
        /// <summary>
        /// Torna la lista delle figure addizionali
        /// </summary>
        /// <returns>Lista di figure addizzionali</returns>
        public override List<Figure> listFigureAdditional()
        {
            return additionalFigure;
        }
        /// <summary>
        /// Cambia il payloadGroup di riferimento
        /// </summary>
        /// <param name="referredTo">PayloadGroup a cui si fa riferimento</param>
        public override void changeReferredTo(object referredTo)
        {
            this.payloadPlacedReferredTo = (PayloadGroup)referredTo;
        }
    }
}
