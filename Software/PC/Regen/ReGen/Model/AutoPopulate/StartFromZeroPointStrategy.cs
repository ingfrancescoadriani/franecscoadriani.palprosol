using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;
using System.Drawing;

namespace ReGen
{
    /// <summary>
    /// Strategy che trova una lista di punti partendo dallo zero
    /// </summary>
    public class StartFromZeroPointStrategy : FindPointsStrategy
    {
        /// <summary>
        /// Trova una lista di punti di applicabilità dei PayloadGroup
        /// </summary>
        /// <param name="layer">Layer su cui applicare la Strategy</param>
        /// <param name="possiblePayloadGroupList">Lista di possibili PayloadGroup da posizionare</param>
        /// <returns>Lista di punti di applicabilità dei PayloadGroup</returns>
        public override List<Point2FWithDirection> getListPoint2FToPlacePayloadGroup(Layer layer, List<PayloadGroup> possiblePayloadGroupList)
        {
            List<Point2FWithDirection> res = new List<Point2FWithDirection>();
            res.Add(new Point2FWithDirection(0, 0, 315));
            foreach (PayloadGroup pg in layer.listPayloadGroupPlaced)
            {
                foreach (Point2FWithDirection p in pg.getListPoint(0))
                {
                    res.Add(p);
                }
            }
            return res;
        }
    }
}
