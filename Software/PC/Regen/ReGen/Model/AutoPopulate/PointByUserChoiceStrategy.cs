using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;
using System.Drawing;
using ReGen.Model.AutoPopulate;

namespace ReGen
{
    /// <summary>
    /// Classe Strategy che permette di fare la scelta dei punti all'utente
    /// </summary>
    public class PointByUserChoiceStrategy : FindPointsStrategy
    {
        DoubleBox angleChosen;  // in sessagesimali
        /// <summary>
        /// Setta l'angolo
        /// </summary>
        /// <param name="angleChosen">Valore dell'angolo</param>
        public PointByUserChoiceStrategy(DoubleBox angleChosen)
        {
            this.angleChosen = angleChosen;
        }
        /// <summary>
        /// Ricava la lista dei punti in base all'angolo scelto 
        /// </summary>
        /// <param name="layer">Strato del quale si cercano i punti</param>
        /// <param name="possiblePayloadGroupList">PayloadGroup del quale si cercano i punti</param>
        /// <returns>Lista di punti</returns>
        public override List<Point2FWithDirection> getListPoint2FToPlacePayloadGroup(Layer layer, List<PayloadGroup> possiblePayloadGroupList)
        {
            List<Point2FWithDirection> res = new List<Point2FWithDirection>();
            //in base all'angolo aggiunge i punti corrispondenti dei vertici
            switch ((int)angleChosen.getVal())
            {
                case 0:
                    res.Add(new Point2FWithDirection(layer.size.X, layer.size.Y, 135));
                    res.Add(new Point2FWithDirection(layer.size.X, 0, 225));
                    break;
                case 45:
                    res.Add(new Point2FWithDirection(layer.size.X, layer.size.Y, 135));
                    break;
                case 90:
                    res.Add(new Point2FWithDirection(0, layer.size.Y, 45));
                    res.Add(new Point2FWithDirection(layer.size.X, layer.size.Y, 135));
                    break;
                case 135:
                    res.Add(new Point2FWithDirection(0, layer.size.Y, 45));
                    break;
                case 180:
                    res.Add(new Point2FWithDirection(0, 0, 315));
                    res.Add(new Point2FWithDirection(0, layer.size.Y, 45));
                    break;
                case 225:
                    res.Add(new Point2FWithDirection(0, 0, 315));
                    break;
                case 270:
                    res.Add(new Point2FWithDirection(0, 0, 315));
                    res.Add(new Point2FWithDirection(layer.size.X, 0, 225));
                    break;
                case 315:
                    res.Add(new Point2FWithDirection(layer.size.X, 0, 225));
                    break;
            }
            //aggiunge tutti i punti dei payload nella lista
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
