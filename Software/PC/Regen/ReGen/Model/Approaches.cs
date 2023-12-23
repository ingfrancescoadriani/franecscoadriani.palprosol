using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;

namespace ReGen.Model
{
    /// <summary>
    /// Classe che rappresenta il modo per l'accostamento
    /// </summary>
    public class Approaches
    {
        public static double approachLenght = 50.0;
        public static Point3F approachFixed = new Point3F(20, 20, 30);
        List<ApproachTo> approachesTo = new List<ApproachTo>();
        PayloadGroup referredTo;
        /// <summary>
        /// Costruttore di Appcroaches
        /// </summary>
        /// <param name="referredTo">PayloadGroup a cui si riferisce</param>
        public Approaches(PayloadGroup referredTo)
        {
            this.referredTo = referredTo;
        }
        /// <summary>
        /// Aggiunge un elemento alla lista degli accostamenti
        /// </summary>
        /// <param name="to">PayloadGroup a cui si vuole accostare</param>
        /// <param name="meSide">Lato da cui si accosta</param>
        public void addApproach(PayloadGroup to, SideOrPoint meSide)
        {
            approachesTo.Add(new ApproachTo(to, meSide));
        }
        /// <summary>
        /// Calcola la direzione dell'accostamento
        /// </summary>
        /// <returns>Punto da cui far partire l'accostamento</returns>
        public Point3F getApproachDirection()
        {
            Point3F res = new Point3F(0, 0, approachFixed.Z);
            double sumX = 0;
            double sumY = 0;
            double averageWidthSum = 0;
            if (approachesTo.Count > 0)
            {
                foreach (ApproachTo a in approachesTo)
                {
                    sumX += Math.Cos(a.getDirection()) * a.getAverageWidth();
                    sumY += Math.Sin(a.getDirection()) * a.getAverageWidth();
                    averageWidthSum += a.getAverageWidth();
                }

                sumX = (sumX) / averageWidthSum;
                sumY = (sumY) / averageWidthSum;

                if ((Math.Abs(sumX) + Math.Abs(sumY)) > 0)
                {
                    double angle = Math.Atan2(sumY, sumX);
                    res = new Point3F(Math.Cos(angle) * approachLenght, Math.Sin(angle) * approachLenght, approachFixed.Z);
                    if (Math.Abs(res.X) > (approachLenght / 20.0))
                        res.X = approachFixed.X * Math.Sign(res.X);
                    else
                        res.X = 0.0F;
                    if (Math.Abs(res.Y) > (approachLenght / 20.0))
                        res.Y = approachFixed.Y * Math.Sign(res.Y);
                    else
                        res.Y = 0.0F;
                }
            }
            return res;
        }
    }
}
