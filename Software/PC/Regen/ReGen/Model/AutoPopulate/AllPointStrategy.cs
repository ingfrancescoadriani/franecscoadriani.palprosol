using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;
using System.Drawing;

namespace ReGen
{
    /// <summary>
    /// Strategy che trova tutti i vertici del Layer e dei Payload
    /// </summary>
    public class AllPointStrategy : FindPointsStrategy
    {
        /// <summary>
        /// Trova la lista di tutti i punti dei PayloadGroup in un Layer
        /// </summary>
        /// <param name="layer">Layer del quale si cerca la lista</param>
        /// <param name="possiblePayloadGroupList">PayloadGroup del quale cercare i punti</param>
        /// <returns>Lista dei punti dei Payload del PayloadGroup</returns>
        public override List<Point2FWithDirection> getListPoint2FToPlacePayloadGroup(Layer layer, List<PayloadGroup> possiblePayloadGroupList)
        {
            List<Point2FWithDirection> res = new List<Point2FWithDirection>();
            res.Add(new Point2FWithDirection(0, 0, 315));
            res.Add(new Point2FWithDirection(0, layer.size.Y, 45));
            res.Add(new Point2FWithDirection(layer.size.X, layer.size.Y, 135));
            res.Add(new Point2FWithDirection(layer.size.X, 0, 225));
            foreach (PayloadGroup pp in layer.listPayloadGroupPlaced)
            {
                foreach (Point2FWithDirection p in pp.getListPoint(0))
                {
                    res.Add(p);
                }
            }
            return res;
        }
    }
}
