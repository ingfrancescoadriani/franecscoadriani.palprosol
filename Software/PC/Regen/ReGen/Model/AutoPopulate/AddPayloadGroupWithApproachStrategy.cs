using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;

namespace ReGen.Model.AutoPopulate
{
    /// <summary>
    /// Classe Strategy che controlla se è possibile aggiungere un PayloadGroup ad un Layer con accostamento
    /// </summary>
    public class AddPayloadGroupWithApproachStrategy : FindGoodPayloadGroupToAddStrategy
    {
        List<double> listAngleAllowed = new List<double>();
        private bool approachingAtEast;
        private bool approachingAtNorthEast;
        private bool approachingAtNorth;
        private bool approachingAtNorthWest;
        private bool approachingAtWest;
        private bool approachingAtSouthWest;
        private bool approachingAtSouth;
        private bool approachingAtSouthEast;
        /// <summary>
        /// Costruttore per la classe AddPayloadGroupWithApporoachStrategy che dati gli angoli di approccio setta anche il valore dell'angolo per l'accostamento
        /// </summary>
        /// <param name="approachingAtEast">Bool per l'approccio da est</param>
        /// <param name="approachingAtNorthEast">Bool per l'approccio da nord-est</param>
        /// <param name="approachingAtNorth">Bool per l'approccio da nord</param>
        /// <param name="approachingAtNorthWest">Bool per l'approccio da nord-ovest</param>
        /// <param name="approachingAtWest">Bool per l'approccio da ovest</param>
        /// <param name="approachingAtSouthWest">Bool per l'approccio da sud-ovest</param>
        /// <param name="approachingAtSouth">Bool per l'approccio da sud</param>
        /// <param name="approachingAtSouthEast">Bool per l'approccio da sud-est</param>
        public AddPayloadGroupWithApproachStrategy(bool approachingAtEast, bool approachingAtNorthEast, bool approachingAtNorth, bool approachingAtNorthWest, bool approachingAtWest, bool approachingAtSouthWest, bool approachingAtSouth, bool approachingAtSouthEast)
        {
            this.approachingAtEast = approachingAtEast;
            if (approachingAtEast) listAngleAllowed.Add(0);
            this.approachingAtNorthEast = approachingAtNorthEast;
            if (approachingAtNorthEast) listAngleAllowed.Add(45);
            this.approachingAtNorth = approachingAtNorth;
            if (approachingAtNorth) listAngleAllowed.Add(90);
            this.approachingAtNorthWest = approachingAtNorthWest;
            if (approachingAtNorthWest) listAngleAllowed.Add(135);
            this.approachingAtWest = approachingAtWest;
            if (approachingAtWest) listAngleAllowed.Add(180);
            this.approachingAtSouthWest = approachingAtSouthWest;
            if (approachingAtSouthWest) listAngleAllowed.Add(225);
            this.approachingAtSouth = approachingAtSouth;
            if (approachingAtSouth) listAngleAllowed.Add(270);
            this.approachingAtSouthEast = approachingAtSouthEast;
            if (approachingAtSouthEast) listAngleAllowed.Add(315);
        }
        /// <summary>
        /// Test per controllare se è possibile aggiungere un PayloadGroup ad un Layer
        /// </summary>
        /// <param name="layer">Layer al quale si vuole aggiungere il PayloadGroup</param>
        /// <param name="possiblePayloadGroup">PayloadGroup che si vuole aggiungere</param>
        /// <param name="possiblePointOfApplication">Punto di applicazione del PayloadGroup</param>
        /// <returns>True se è possibile aggiungere il PayloadGroup, False altrimenti</returns>
        public override Point2FWithDirection canAddPayloadGroupToLayer(Layer layer, PayloadGroup possiblePayloadGroup, Point2FWithDirection possiblePointOfApplication)
        {
            Point2FWithDirection res = null;
            PayloadGroup p = possiblePayloadGroup.withCenter(possiblePointOfApplication);
            bool canAdd = layer.canCointainsPayloadGroup(p.withCenter(possiblePointOfApplication));
            if (canAdd)
            {
                foreach (PayloadGroup pg in layer.listPayloadGroupPlaced)
                {
                    bool allowed = true;   
                    SideOrPoint s = p.nearSideOrPoint(pg);
                    if (s != null)
                    {
                        allowed = false;
                        double angle;

                        //s = pg.nearSide(p);
                        //pg.resetApproaches();
                        //pg.addApprochedPayload(p, s);
                        //angle = pg.getApproachDirection().angle(); //angolo in pigrechi
                        //pg.resetApproaches();
                        //pg.addApprochedPayload(possiblePayloadGroup, s);

                        s = p.nearSideOrPoint(pg);
                        p.resetApproaches();
                        p.addApprochedPayload(pg, s);
                        angle = p.getApproachDirection().angle(); //angolo in pigrechi
                        p.resetApproaches();
                        //p.addApprochedPayload(pg, s);

                        foreach (double a in listAngleAllowed)
                            allowed = allowed || (Math.Abs(Program.getRobotRotation(angle / Math.PI * 180) - a) <= 22.5);
                    }
                    canAdd = canAdd && allowed;
                }
            }
            if (canAdd)
                res = possiblePointOfApplication;
            return res;
        }
    }
}
