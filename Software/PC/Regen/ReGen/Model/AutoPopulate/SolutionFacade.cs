using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;

namespace ReGen.Model.AutoPopulate
{
    /// <summary>
    /// Facciata per l'utilizzo delle Strategy
    /// </summary>
    public class SolutionFacade
    {
        public double angleChoosen;
        public FindPointsStrategy findPointStrategy;
        public FindCandidateStrategy findCandidateStrategy;
        public FindGoodPayloadGroupToAddStrategy findGoodPayloadGroupToAddStrategy;
        public ApproverStrategy approverStrategy;
        public PayloadOnPalletRanker payloadOnPalletRanker;
        private static SolutionFacade _instance;
        public static SolutionFacade Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SolutionFacade();
                return _instance;
            }
            set
            {

            }
        }
        private DoubleBox payloadOnPalletRankerAngle;
        private DoubleBox findPointStrategyAngle;
        /// <summary>
        /// Inizializza le strategy
        /// </summary>
        private SolutionFacade()
        {
            refreshFindGoodPayloadGroupToAddStrategy(true, true, true, true, true, true, true, true);
            findCandidateStrategy = new AllPlacementStrategy();
            payloadOnPalletRankerAngle = new DoubleBox(315.0);
            findPointStrategyAngle = new DoubleBox(315.0);
            findPointStrategy = new PointByUserChoiceStrategy(findPointStrategyAngle);
            payloadOnPalletRanker = new RankerByDirection(payloadOnPalletRankerAngle);
        }
        /// <summary>
        /// Richiama il costruttore AddPayloadGroupWithApproachStrategy
        /// </summary>
        /// <param name="approachingAtEast">Booleano per l'approccio da est</param>
        /// <param name="approachingAtNorthEast">Booleano per l'approccio da nord-est</param>
        /// <param name="approachingAtNorth">Booleano per l'approccio da nord</param>
        /// <param name="approachingAtNorthWest">Booleano per l'approccio da nord-ovest</param>
        /// <param name="approachingAtWest">Booleano per l'approccio da ovest</param>
        /// <param name="approachingAtSouthWest">Booleano per l'approccio da sud-ovest</param>
        /// <param name="approachingAtSouth">Booleano per l'approccio da sud</param>
        /// <param name="approachingAtSouthEast">Booleano per l'approccio da sud-est</param>
        public void refreshFindGoodPayloadGroupToAddStrategy(bool approachingAtEast, bool approachingAtNorthEast, bool approachingAtNorth, bool approachingAtNorthWest, bool approachingAtWest, bool approachingAtSouthWest, bool approachingAtSouth, bool approachingAtSouthEast)
        {
            findGoodPayloadGroupToAddStrategy = new AddPayloadGroupWithApproachStrategy(approachingAtEast, approachingAtNorthEast, approachingAtNorth, approachingAtNorthWest, approachingAtWest, approachingAtSouthWest, approachingAtSouth, approachingAtSouthEast);
        }
        /// <summary>
        /// Richiama il costruttore ExternalLabelBasedFilterStrategy
        /// </summary>
        /// <param name="allPayloadGroupHaveExternalLabel">Booleano che rappresenta se tutto il PayloadGroup ha le Label all'estern</param>
        /// <param name="externalLabelOnEast">Booleano che rappresenta se la Label esterna è a est</param>
        /// <param name="externalLabelOnNorth">Booleano che rappresenta se la Label esterna è a nord</param>
        /// <param name="externalLabelOnWest">Booleano che rappresenta se la Label esterna è a ovest</param>
        /// <param name="externalLabelOnSouth">Booleano che rappresenta se la Label esterna è a sud</param>
        public void refreshApproverStrategy(bool allPayloadGroupHaveExternalLabel, bool externalLabelOnEast, bool externalLabelOnNorth, bool externalLabelOnWest, bool externalLabelOnSouth)
        {
            approverStrategy = new ExternalLabelBasedFilterStrategy(allPayloadGroupHaveExternalLabel,externalLabelOnEast, externalLabelOnNorth, externalLabelOnWest, externalLabelOnSouth);
        }
        /// <summary>
        /// Aggiorna il valore di payloadOnPalletRankerAngle
        /// </summary>
        /// <param name="angleChoosen">Nuovo angolo</param>
        public void refreshPayloadOnPalletRanker(double angleChoosen)
        {
            this.payloadOnPalletRankerAngle.setVal(angleChoosen);
            this.angleChoosen = angleChoosen;
        }
        /// <summary>
        /// Aggiorna il valore di findPointStrategyAngle
        /// </summary>
        /// <param name="angleChoosen">Nuovo angolo</param>
        public void refreshFindPointsStrategy(double angleChoosen)
        {
            this.findPointStrategyAngle.setVal(angleChoosen);
        }
    }
}
