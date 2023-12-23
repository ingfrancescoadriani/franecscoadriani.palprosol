
using System.Collections.Generic;
using ReGen;
using System;
using System.Linq;
using System.Text;
using Sintec.Tool;
using ReGen.Model.AutoPopulate;

namespace ReGen
{
    /// <summary>
    /// Classe astratta che genera le soluzioni candidate in base allo stato del layer
    /// </summary>
    public abstract class FindCandidateStrategy
    {
        /// <summary>
        /// Genera una lista di soluzioni candidate in base allo stato del Layer attuale
        /// </summary>
        /// <param name="cll">CandidateLayerListener per rilanciare eventuali recuperi di soluzioni</param>
        /// <param name="layer">Layer su cui si vogliono piazzare i PayloadGroup</param>
        /// <param name="possiblePayloadGroupList">PayloadGroup che si vuole piazzare</param>
        /// <param name="f">FindPointStrategy da applicare</param>
        /// <param name="a">ApproverStrategy</param>
        /// <returns>Lista di CandidateSolution in base allo stato del layer attuale</returns>
        public abstract List<CandidateSolution> getListCandidate(CandidateLayerListener cll, Layer layer, List<PayloadGroup> possiblePayloadGroupList, FindPointsStrategy f, ApproverStrategy a);
        // genera una lista di soluzioni candidate in base allo stato del layer attuale, ai payloadGroup che si vogliono agganciare e ai punti disponibili
        // associa un listener per rilanciare eventuali recuperi di soluzioni
        // le soluzioni candidate associano i payload in ogni rotazione applicando nel punto le varie disposizioni ruotate
        // qui i possibili filtri riguardo le rotazioni o le disposizioni dei payloadGroup (ma non degli accostamenti o ingombri)
    }

}