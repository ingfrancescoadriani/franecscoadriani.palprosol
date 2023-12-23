using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReGen.Model
{
    /// <summary>
    /// Classe che rappresenta l'accostamento
    /// </summary>
    class ApproachTo
    {
        PayloadGroup to;
        SideOrPoint meSide;
        public ApproachTo(PayloadGroup to, SideOrPoint meSide)
        {
            this.meSide = meSide;
            this.to = to;
        }
        /// <summary>
        /// Torna la direzione del lato o punto del PayloadGroup che si vuole accostare
        /// </summary>
        /// <returns>Direzione del lato o punto del PayloadGroup che si vuole accostare</returns>
        public double getDirection(){
            return meSide.getDirection();
        }
        /// <summary>
        /// Torna la larghezza media del PayloadGroup che si vuole accostare
        /// </summary>
        /// <returns>Larghezza media del PayloadGroup che si vuole accostare</returns>
        public double getAverageWidth()
        {
            return meSide.getAverageWidth();
        }
    }
    /// <summary>
    /// Classe astratta che rappresenta un lato o un punto
    /// </summary>
    public abstract class SideOrPoint
    {
        /// <summary>
        /// Angolo tra i baricentri
        /// </summary>
        /// <returns>Angolo tra i baricentri</returns>
        public abstract double getDirection();
        /// <summary>
        /// Peso del lato/punto (viene dato un valore più alto ai lati rispetto ai punti per fare una media pesata)
        /// </summary>
        /// <returns>Peso del lato/punto</returns>
        public abstract double getAverageWidth();   // serve a pesare i lati rispetto ai punti (se ho sia punti che lati vicini ciò che contato sono i lati e non i punti = poco peso)
                                                    // se ci sono solo lati allora i pesi sono uguali, lo stesso se ci sono solo punti
    }
    /// <summary>
    /// Classe che rappresenta il lato destro
    /// </summary>
    public class RightSide : SideOrPoint
    {
        /// <summary>
        /// Angolo tra i baricentri
        /// </summary>
        /// <returns>Angolo tra i baricentri: 0</returns>
        public override double getDirection()
        {
            return 0;
        }
        /// <summary>
        /// Peso del lato (viene dato un valore più alto ai lati rispetto ai punti per fare una media pesata)
        /// </summary>
        /// <returns>Peso del lato: 100</returns>
        public override double getAverageWidth()
        {
            return 100;
        }
    }
    /// <summary>
    /// Classe che rappresenta il punto in alto a destra
    /// </summary>
    public class UpRightPoint : SideOrPoint
    {
        /// <summary>
        /// Angolo tra i baricentri
        /// </summary>
        /// <returns>Angolo tra i baricentri: PI/4</returns>
        public override double getDirection()
        {
            return Math.PI / 4.0;
        }
        /// <summary>
        /// Peso del punto (viene dato un valore più alto ai lati rispetto ai punti per fare una media pesata)
        /// </summary>
        /// <returns>Peso del punto: 0.001</returns>
        public override double getAverageWidth()
        {
            return 0.001;
        }
    }

    /// <summary>
    /// Classe che rappresenta il lato superiore
    /// </summary>
    public class UpSide : SideOrPoint
    {
        /// <summary>
        /// Angolo tra i baricentri
        /// </summary>
        /// <returns>Angolo tra i baricentri: PI/2</returns>
        public override double getDirection()
        {
            return Math.PI/2.0;
        }
        /// <summary>
        /// Peso del lato (viene dato un valore più alto ai lati rispetto ai punti per fare una media pesata)
        /// </summary>
        /// <returns>Peso del lato: 100</returns>
        public override double getAverageWidth()
        {
            return 100;
        }
    }
    /// <summary>
    /// Classe che rappresenta il punto in alto a sinistra
    /// </summary>
    public class UpLeftPoint : SideOrPoint
    {
        /// <summary>
        /// Angolo tra i baricentri
        /// </summary>
        /// <returns>Angolo tra i baricentri: (PI*3)/4</returns>
        public override double getDirection()
        {
            return Math.PI / 4.0 * 3.0;
        }
        /// <summary>
        /// Peso del punto (viene dato un valore più alto ai lati rispetto ai punti per fare una media pesata)
        /// </summary>
        /// <returns>Peso del lato: 0.001</returns>
        public override double getAverageWidth()
        {
            return 0.001;
        }
    }
    /// <summary>
    /// Classe che rappresenta il lato sinistro
    /// </summary>
    public class LeftSide : SideOrPoint
    {
        /// <summary>
        /// Angolo tra i baricentri
        /// </summary>
        /// <returns>Angolo tra i baricentri: PI</returns>
        public override double getDirection()
        {
            return Math.PI;
        }
        /// <summary>
        /// Peso del lato (viene dato un valore più alto ai lati rispetto ai punti per fare una media pesata)
        /// </summary>
        /// <returns>Peso del lato: 100</returns>
        public override double getAverageWidth()
        {
            return 100;
        }
    }
    /// <summary>
    /// Classe che rappresenta il punto in basso a sinistra
    /// </summary>
    public class DownLeftPoint : SideOrPoint
    {
        /// <summary>
        /// Angolo tra i baricentri
        /// </summary>
        /// <returns>Angolo tra i baricentri: (PI*5)/4</returns>
        public override double getDirection()
        {
            return Math.PI / 4.0 * 5.0;
        }
        /// <summary>
        /// Peso del punto (viene dato un valore più alto ai lati rispetto ai punti per fare una media pesata)
        /// </summary>
        /// <returns>Peso del punto: 0.001</returns>
        public override double getAverageWidth()
        {
            return 0.001;
        }
    }
    /// <summary>
    /// Classe che rappresenta il lato inferiore
    /// </summary>
    public class DownSide : SideOrPoint
    {
        /// <summary>
        /// Angolo tra i baricentri
        /// </summary>
        /// <returns>Angolo tra i baricentri: (PI*3)/2</returns>
        public override double getDirection()
        {
            return (Math.PI/2.0)*3.0;
        }
        /// <summary>
        /// Peso del lato (viene dato un valore più alto ai lati rispetto ai punti per fare una media pesata)
        /// </summary>
        /// <returns>Peso del lato: 100</returns>
        public override double getAverageWidth()
        {
            return 100;
        }
    }
    /// <summary>
    /// Classe che rappresenta il punto in basso a destra
    /// </summary>
    public class DownRightPoint : SideOrPoint
    {
        /// <summary>
        /// Angolo tra i baricentri
        /// </summary>
        /// <returns>Angolo tra i baricentri: (PI*7)/4</returns>
        public override double getDirection()
        {
            return Math.PI / 4.0 * 7.0;
        }
        /// <summary>
        /// Peso del punto (viene dato un valore più alto ai lati rispetto ai punti per fare una media pesata)
        /// </summary>
        /// <returns>Peso del punto: 0.001</returns>
        public override double getAverageWidth()
        {
            return 0.001;
        }
    }
}
