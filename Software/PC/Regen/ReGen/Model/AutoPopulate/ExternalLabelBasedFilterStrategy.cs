using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReGen.Model.AutoPopulate
{
    /// <summary>
    /// Classe che rappresenta la Strategy che tiene conto della posizione delle Label
    /// </summary>
    public class ExternalLabelBasedFilterStrategy: ApproverStrategy
    {
        bool allPayloadGroupHaveExternalLabelOnEast;
        bool externalLabelOnEast;
        bool allPayloadGroupHaveExternalLabelOnNorth;
        bool externalLabelOnNorth;
        bool allPayloadGroupHaveExternalLabelOnWest;
        bool externalLabelOnWest;
        bool allPayloadGroupHaveExternalLabelOnSouth;
        bool externalLabelOnSouth;

        /// <summary>
        /// Costruttore per la classe ExternalLabelBasedFilterStrategy
        /// </summary>
        /// <param name="allPayloadGroupHaveExternalLabelOnEast">Booleano che rappresenta se tutto il PayloadGroup ha le Label esterne a est</param>
        /// <param name="externalLabelOnEast">Booleano che rappresenta se la Label esterna è a est</param>
        /// <param name="allPayloadGroupHaveExternalLabelOnNorth">Booleano che rappresenta se tutto il PayloadGroup ha le Label esterne a nord</param>
        /// <param name="externalLabelOnNorth">Booleano che rappresenta se la Label esterna è a nord</param>
        /// <param name="allPayloadGroupHaveExternalLabelOnWest">Booleano che rappresenta se tutto il PayloadGroup ha le Label esterne a ovest</param>
        /// <param name="externalLabelOnWest">Booleano che rappresenta se la Label esterna è a ovest</param>
        /// <param name="allPayloadGroupHaveExternalLabelOnSouth">Booleano che rappresenta se tutto il PayloadGroup ha le Label esterne a sud</param>
        /// <param name="externalLabelOnSouth">Booleano che rappresenta se la Label esterna è a sud</param>
        public ExternalLabelBasedFilterStrategy(bool allPayloadGroupHaveExternalLabelOnEast, bool externalLabelOnEast,
                                                bool allPayloadGroupHaveExternalLabelOnNorth, bool externalLabelOnNorth,
                                                bool allPayloadGroupHaveExternalLabelOnWest, bool externalLabelOnWest,
                                                bool allPayloadGroupHaveExternalLabelOnSouth, bool externalLabelOnSouth)
        {
            this.allPayloadGroupHaveExternalLabelOnEast = allPayloadGroupHaveExternalLabelOnEast;
            this.externalLabelOnEast = externalLabelOnEast;
            this.allPayloadGroupHaveExternalLabelOnNorth = allPayloadGroupHaveExternalLabelOnNorth;
            this.externalLabelOnNorth = externalLabelOnNorth;
            this.allPayloadGroupHaveExternalLabelOnWest = allPayloadGroupHaveExternalLabelOnWest;
            this.externalLabelOnWest = externalLabelOnWest;
            this.allPayloadGroupHaveExternalLabelOnSouth = allPayloadGroupHaveExternalLabelOnSouth;
            this.externalLabelOnSouth = externalLabelOnSouth;
            this.doWhileCreate = (allPayloadGroupHaveExternalLabelOnEast || allPayloadGroupHaveExternalLabelOnNorth || allPayloadGroupHaveExternalLabelOnWest || allPayloadGroupHaveExternalLabelOnSouth);
        }
        /// <summary>
        /// Costruttore per la classe ExternalLabelBasedFilterStrategy
        /// </summary>
        /// <param name="allPayloadGroupHaveExternalLabel">Booleano che rappresenta se tutto il PayloadGroup ha le Label all'esterno</param>
        /// <param name="externalLabelOnEast">Booleano che rappresenta se la Label esterna è a est</param>
        /// <param name="externalLabelOnNorth">Booleano che rappresenta se la Label esterna è a nord</param>
        /// <param name="externalLabelOnWest">Booleano che rappresenta se la Label esterna è a ovest</param>
        /// <param name="externalLabelOnSouth">Booleano che rappresenta se la Label esterna è sud</param>
        public ExternalLabelBasedFilterStrategy(bool allPayloadGroupHaveExternalLabel, 
                                                bool externalLabelOnEast, 
                                                bool externalLabelOnNorth, 
                                                bool externalLabelOnWest, 
                                                bool externalLabelOnSouth)
            : this(allPayloadGroupHaveExternalLabel, externalLabelOnEast, 
                                                 allPayloadGroupHaveExternalLabel,externalLabelOnNorth, 
                                                 allPayloadGroupHaveExternalLabel,externalLabelOnWest, 
                                                 allPayloadGroupHaveExternalLabel,externalLabelOnSouth)
        {
        }
        /// <summary>
        /// Test per l'approvazione della soluzione generata in funzione di varie specifiche
        /// </summary>
        /// <param name="layer">Layer su cui fare il test della soluzione</param>
        /// <returns>True/False se la soluzione è approvata o meno</returns>
        public override bool approveSolution(Layer layer)
        {
            bool res = true;
            double angle = 0;
            if (externalLabelOnEast)
            {
                if (allPayloadGroupHaveExternalLabelOnEast)
                {
                    bool tmpRes = (layer.listPayloadGroupPlaced.Count == 0) || ((layer.listPayloadGroupPlaced.Count > 0) && (layer.payloadGroupOnSideOnAngle(angle).Count > 0));
                    foreach (PayloadGroup p in layer.payloadGroupOnSideOnAngle(angle))
                        if (p.labelOnSideAt(angle) != p.countListPayloadPlaced()) tmpRes = false;
                    res = res && tmpRes;
                }
                else
                {
                    bool tmpRes = false;
                    foreach (PayloadGroup p in layer.payloadGroupOnSideOnAngle(angle))
                        if (p.labelOnSideAt(angle) > 0) tmpRes = true;
                    res = res && tmpRes;
                }
            }

            angle = 90;
            if (externalLabelOnNorth)
            {
                if (allPayloadGroupHaveExternalLabelOnNorth)
                {
                    bool tmpRes = (layer.listPayloadGroupPlaced.Count == 0) || ((layer.listPayloadGroupPlaced.Count > 0) && (layer.payloadGroupOnSideOnAngle(angle).Count > 0));
                    foreach (PayloadGroup p in layer.payloadGroupOnSideOnAngle(angle))
                        if (p.labelOnSideAt(angle) != p.countListPayloadPlaced()) tmpRes = false;
                    res = res && tmpRes;
                }
                else
                {
                    bool tmpRes = false;
                    foreach (PayloadGroup p in layer.payloadGroupOnSideOnAngle(angle))
                        if (p.labelOnSideAt(angle) > 0) tmpRes = true;
                    res = res && tmpRes;
                }
            }

            angle = 180;
            if (externalLabelOnWest)
            {
                if (allPayloadGroupHaveExternalLabelOnWest)
                {
                    bool tmpRes = (layer.listPayloadGroupPlaced.Count == 0) || ((layer.listPayloadGroupPlaced.Count > 0) && (layer.payloadGroupOnSideOnAngle(angle).Count > 0));
                    foreach (PayloadGroup p in layer.payloadGroupOnSideOnAngle(angle))
                        if (p.labelOnSideAt(angle) != p.countListPayloadPlaced()) tmpRes = false;
                    res = res && tmpRes;
                }
                else
                {
                    bool tmpRes = false;
                    foreach (PayloadGroup p in layer.payloadGroupOnSideOnAngle(angle))
                        if (p.labelOnSideAt(angle) > 0) tmpRes = true;
                    res = res && tmpRes;
                }
            }

            angle = 270;
            if (externalLabelOnSouth)
            {
                if (allPayloadGroupHaveExternalLabelOnSouth)
                {
                    bool tmpRes = (layer.listPayloadGroupPlaced.Count == 0) || ((layer.listPayloadGroupPlaced.Count > 0) && (layer.payloadGroupOnSideOnAngle(angle).Count > 0));
                    foreach (PayloadGroup p in layer.payloadGroupOnSideOnAngle(angle))
                        if (p.labelOnSideAt(angle) != p.countListPayloadPlaced()) tmpRes = false;
                    res = res && tmpRes;
                }
                else
                {
                    bool tmpRes = false;
                    foreach (PayloadGroup p in layer.payloadGroupOnSideOnAngle(angle))
                        if (p.labelOnSideAt(angle) > 0) tmpRes = true;
                    res = res && tmpRes;
                }
            }

            return res;
        }
    }
}
