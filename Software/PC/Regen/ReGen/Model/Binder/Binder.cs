using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Sintec.Tool;
using ReGen.View;

namespace ReGen
{
    /// <summary>
    /// Classe astratta che gestisce il binding tra le figure
    /// </summary>
    public abstract class Binder: PayloadGroup
    {
        protected Layer l;
        protected PayloadGroup payloadGroupOriginal;
        Object referredTo;
        /// <summary>
        /// Costruttore per la classe Binder
        /// </summary>
        /// <param name="payloadGroup">PayloadGroup</param>
        /// <param name="referredTo">Oggetto di riferimento</param>
        /// <param name="payloadGroupOriginal">PayloadGroup originale</param>
        public Binder(PayloadGroup payloadGroup, Object referredTo, PayloadGroup payloadGroupOriginal, Layer l)
            : base()
        {
            this.l = l;
            this.id = payloadGroup.getId();
            while (payloadGroup.queueStringForDebug.Count > 0)
                this.queueStringForDebug.Enqueue(payloadGroup.queueStringForDebug.Dequeue());
            this.importApproaches(payloadGroup.getApproaches());
            this.payloadGroupOriginal = payloadGroupOriginal;
            this.referredTo = referredTo;
            changeReferredTo(referredTo);
            changePayloadPlaced(payloadGroup);
        }
        /// <summary>
        /// Cambia il PayloadGroup piazzato con il nuovo PayloadGroup
        /// </summary>
        /// <param name="pg">Nuovo PayloadGroup</param>
        private void changePayloadPlaced(PayloadGroup pg)
        {
            PayloadGroup.cloneFromTo(this, pg);
            callBindAction();
        }
        /// <summary>
        /// Costruisce una lista di figure da renderizzare con i payloadPlaced e PayloadGroup
        /// </summary>
        /// <param name="ps">PlacingState</param>
        /// <returns>Lista di figure da renderizzare</returns>
        public List<Figure> getListFigureToRender(PlacingState ps)
        {
            List<Figure> res = new List<Figure>();
            foreach (Figure fg in (listFigureAdditional()))
                res.Add(fg);
            foreach (Figure fg in (base.getListFigureToRender(ps, this.center, true)))
                res.Add(fg);
            return res;
        }
        /// <summary>
        /// Attiva il binding
        /// </summary>
        private void callBindAction()
        {
            bind();
            payloadGroupOriginal.setLastBoundPosition(new Point2F(center.X, center.Y));
            queueStringForDebug.Enqueue(this.GetType().ToString() + " " + center.ToString());
        }
        /// <summary>
        /// Aggiunge un problema di posizionamento
        /// </summary>
        /// <param name="pop">ProblemOfPositioning</param>
        public void addProblem(ProblemOfPositioning pop)
        {
            payloadGroupOriginal.listProblem.Add(pop);
        }
        /// <summary>
        /// Effettua il binding
        /// </summary>
        public abstract void bind();
        /// <summary>
        /// Cambia l'oggettto a cui si fa riferimento
        /// </summary>
        /// <param name="referredTo">Nuovo oggetto a cui si fa riferimento</param>
        public abstract void changeReferredTo(Object referredTo);
        /// <summary>
        /// Costruisce una lista di figure in più
        /// </summary>
        /// <returns>Lista di figure</returns>
        public abstract List<Figure> listFigureAdditional();
        /// <summary>
        /// Torna l'Id del payloadGroup originale
        /// </summary>
        /// <returns>Id del PayloadGroup originale</returns>
        public override long getId(){
            return this.payloadGroupOriginal.getId();
        }
    }
}
