
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using Sintec.Tool;
using ReGen.Model.AutoPopulate;

namespace ReGen
{
    public class CandidateLayer
    {
        public static List<PayloadGroup> payloadGroupPossibleList = new List<PayloadGroup>();
        private FindCandidateStrategy fcs;
        private FindPointsStrategy fps;
        private ApproverStrategy aps;
        private bool haveASolution = false;
        private Layer layer;
        private CandidateLayerListener candidateLayerListener;
        /// <summary>
        /// Costruttore per la classe CandidateLayer
        /// </summary>
        /// <param name="cll">CandidateLayerListener</param>
        /// <param name="fcs">FindCandidateStrategy</param>
        /// <param name="fps">FindPointsStrategy</param>
        /// <param name="aps">ApproverStrategy</param>
        private CandidateLayer(CandidateLayerListener cll, FindCandidateStrategy fcs, FindPointsStrategy fps, ApproverStrategy aps)
        {
            this.candidateLayerListener = cll;
            this.fcs = fcs;
            this.fps = fps;
            this.aps = aps;
        }
        /// <summary>
        /// Costruttore per la calsse CandidateLayer
        /// </summary>
        /// <param name="cll">CandidateLayerListener</param>
        /// <param name="l">Layer</param>
        /// <param name="fcs">FindCandidateStrategy</param>
        /// <param name="fps">FindPointsStrategy</param>
        /// <param name="aps">ApproverStrategy</param>
        public CandidateLayer(CandidateLayerListener cll, Layer l, FindCandidateStrategy fcs, FindPointsStrategy fps, ApproverStrategy aps)
            : this(cll, fcs, fps, aps)
        {
            this.layer = l;
        }
        /// <summary>
        /// Clona il Layer candidato
        /// </summary>
        /// <returns>Clone del Layer candidato</returns>
        public Layer getLayerClone(){
            return this.layer.clone();
        }
        /// <summary>
        /// Torna il Layer
        /// </summary>
        /// <returns>Torna il Layer</returns>
        public Layer getLayer()
        {
            return this.layer;
        }
        /// <summary>
        /// Estende il CandidateLayer aggiungendo il PayloadGroup alla lista dei PayloadGroupPlaced
        /// </summary>
        /// <param name="p">PayloadGroup da aggiungere</param>
        /// <returns>CandidateLayer esteso con il PayloadGroup</returns>
        public CandidateLayer extendedWith(PayloadGroup p)
        {
            Layer tmp = this.layer.cloneWithSamePayloadGroup();// clone();
            CandidateLayer res = new CandidateLayer(candidateLayerListener, tmp, this.fcs, this.fps, this.aps);
            res.layer.listPayloadGroupPlaced.Add(p);
            return res;
        }
        /// <summary>
        /// Setta il campo haveASolution a true
        /// </summary>
        public void haveAsSolution()
        {
            this.haveASolution = true;
        }
        //TODO come?/ generazione automatica
        public List<Layer> findSolution(CandidateLayer father, Point2FWithDirection p, PayloadGroup pg, int level, BoolBox finishing)
        {
            // cicla ricorsivamente generando un albero di soluzioni in cui l'unico controllo effettuato è se il payload ha spazio per entrare nel layer
            bool rootOfSolutionTree = (pg == null);
            List<Layer> res = new List<Layer>();
            bool go = false;
            bool notASolution = false;
            //se pg non è null
            if (!rootOfSolutionTree)
            {
                go = (layer.pointWhereCanAddPayloadGroup(pg, p) != null);  //layer.canCointainsPayloadGroupAtPoint(pg, p);
                if (go && aps.doWhileCreate)     // se l'approvatore verifica soluzioni intermedie
                {
                    notASolution = !aps.approveSolution(layer); // verifica che l'approvatore approvi la soluzione intermedia
                }
            }
            if ((rootOfSolutionTree || go) && !notASolution)  // posso aggiungermi al layer
            {
                father.haveAsSolution();
                CandidateLayer tmpCL = null;
                if (!rootOfSolutionTree)
                {
                    PayloadGroup tmpPg = pg.withCenter(p);
                    tmpCL = this.extendedWith(tmpPg); // layer a cui si aggiunge me stesso (gruppo)
                    ReGen.Model.Sequencer.addPayloadGroupAtFixedRankList(tmpPg.getId(), level);
                }
                else
                {
                    tmpCL = this; //prima volta
                    ReGen.Model.Sequencer.clearFixedRankList();
                }
                List<CandidateSolution> candidateSolutionList = fcs.getListCandidate(candidateLayerListener, tmpCL.layer, CandidateLayer.payloadGroupPossibleList, this.fps, this.aps);
                foreach(CandidateSolution cs in candidateSolutionList )
                {
                    if (!finishing.getVal())
                    {
                        // della lista dei possibili candidati controllo le soluzione nei figli e itero sui figli
                        candidateLayerListener.anotherCicle();
                        List<Layer> llSol = cs.candidateLayer.findSolution(tmpCL, cs.point2FWithDirection, cs.payloadGroup, level + 1, finishing);
                        foreach (Layer lSol in llSol)
                            res.Add(lSol);
                    }
                }
                // tornando al padre se non ha soluzioni (cioè tutti i figli (gruppi non stanno nel layer) allora aggiungo la soluzione
                // non posso riempire di più
                if (!finishing.getVal())
                {
                    if (!tmpCL.haveASolution)
                    {
                        if (aps.approveSolution(tmpCL.layer))
                        {
                            candidateLayerListener.anotherSolution(tmpCL);
                            res.Add(tmpCL.layer);
                        }
                    }
                }
            }
            else
            {
                // non dico al padre che non sono una soluzione, lui controllerò se c'è almeno una soluzione valida
                //father.haveAsSolution();
                //candidateLayerListener.anotherSolution(father);
            }
            if (Program.withDoEvents) 
                System.Windows.Forms.Application.DoEvents();
            return res;
        }
        public static int worstCase(int groupNumber, int maxPayload)
        {
            //return (maxPayload * 4 * Factorial.Calc(maxPayload) * (int)Math.Pow(groupNumber,maxPayload));
            //return ((int)Math.Pow(4 * (1 + maxPayload), maxPayload));
            return 1;
        }
    }
    //TODO a cosa serve? /generazione automatica
    public interface  CandidateLayerListener
    {
        void anotherSolution(CandidateLayer cl);
        /// <summary>
        /// Incrementa il numero di cicli
        /// </summary>
        void anotherCicle();
    }
}