
using ReGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;

namespace ReGen
{
    /// <summary>
    /// Strategy che trova i punti di applicabilit� dei Payload
    /// </summary>
    public abstract class FindPointsStrategy
    {
        /// <summary>
        /// Genera i punti di applicabilit� dei Payload
        /// </summary>
        /// <param name="layer">Layer sul quale si vogliono piazzare i Payload</param>
        /// <param name="possiblePayloadGroupList">PayloadGroup che si vuole applicare</param>
        /// <returns>Lista di punti di applicabilit� dei Payload</returns>
        public abstract List<Point2FWithDirection> getListPoint2FToPlacePayloadGroup(Layer layer, List<PayloadGroup> possiblePayloadGroupList);
        // genera i punti di applicabilit� dei payload senza tener conto dei payload
        // qui i filtri sui punti in cui non � possibile applicare payoadGroup ?? non saprei dire quali
    }

}