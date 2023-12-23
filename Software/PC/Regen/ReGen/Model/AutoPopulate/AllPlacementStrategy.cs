using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;
using ReGen.Model.AutoPopulate;

namespace ReGen
{
    /// <summary>
    /// Strategy che trova tutti i possibili piazzamenti dei PayloadGroup
    /// </summary>
    public class AllPlacementStrategy : FindCandidateStrategy
    {
        //TODO cosa?/ generazione automatica 
        public override List<CandidateSolution> getListCandidate(CandidateLayerListener cll, Layer layer, List<PayloadGroup> possiblePayloadGroupList, FindPointsStrategy f, ApproverStrategy a)
        {
            List<CandidateSolution> res = new List<CandidateSolution>();
            foreach (Point2FWithDirection p in f.getListPoint2FToPlacePayloadGroup(layer, possiblePayloadGroupList))
            {
                foreach (PayloadGroup pg in possiblePayloadGroupList)
                {
                    for (int j = 0; j < 4; j++) // ruota ogni payloadgroup attorno a se stesso
                    {
                        Point2F offset; // tocca su tutti i punti in ogni direzione

                        CandidateSolution cs = new CandidateSolution();
                        cs.payloadGroup = pg.clone();
                        cs.payloadGroup.setQuadrant(j);
                        offset = new Point2F(cs.payloadGroup.getBounds().left(), cs.payloadGroup.getBounds().top());
                        cs.point2FWithDirection = p.traslatedOf(offset).toPoint2FWithDirection(135);
                        cs.candidateLayer = new CandidateLayer(cll, layer, this, f, a);
                        res.Add(cs);

                        cs = new CandidateSolution();
                        cs.payloadGroup = pg.clone();
                        cs.payloadGroup.setQuadrant(j);
                        offset = new Point2F(cs.payloadGroup.getBounds().left(), cs.payloadGroup.getBounds().bottom());
                        cs.point2FWithDirection = p.traslatedOf(offset).toPoint2FWithDirection(225);
                        cs.candidateLayer = new CandidateLayer(cll, layer, this, f, a);
                        res.Add(cs);

                        cs = new CandidateSolution();
                        cs.payloadGroup = pg.clone();
                        cs.payloadGroup.setQuadrant(j);
                        offset = new Point2F(cs.payloadGroup.getBounds().right(), cs.payloadGroup.getBounds().top());
                        cs.point2FWithDirection = p.traslatedOf(offset).toPoint2FWithDirection(45);
                        cs.candidateLayer = new CandidateLayer(cll, layer, this, f, a);
                        res.Add(cs);

                        cs = new CandidateSolution();
                        cs.payloadGroup = pg.clone();
                        cs.payloadGroup.setQuadrant(j);
                        offset = new Point2F(cs.payloadGroup.getBounds().right(), cs.payloadGroup.getBounds().bottom());
                        cs.point2FWithDirection = p.traslatedOf(offset).toPoint2FWithDirection(315);
                        cs.candidateLayer = new CandidateLayer(cll, layer, this, f, a);
                        res.Add(cs);
                    }
                }
            }
            return res;
        }
    }
}
