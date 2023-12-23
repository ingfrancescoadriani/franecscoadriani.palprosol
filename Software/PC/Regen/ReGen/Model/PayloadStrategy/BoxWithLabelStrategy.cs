using Sintec.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReGen
{
    /// <summary>
    /// Rappresenta la strategy per le scatole con l'etichetta
    /// </summary>
    public class BoxWithLabelStrategy : BoxStrategy
    {
        Point3F size;
        public List<LabelOnBox> labels;
        float tollerance;
        private string name = "Scatola con etichette";
        /// <summary>
        /// Costruttore per la strategy per le scatole con l'etichetta
        /// </summary>
        /// <param name="size">Dimensioni della scatola</param>
        /// <param name="tollerance">Tolleranza</param>
        /// <param name="labels">Lista di label</param>
        public BoxWithLabelStrategy(Point3F size, float tollerance, List<LabelOnBox> labels, String name)
            : base(size, name)
        {
            this.size = size;
            this.labels = labels;
            this.tollerance = tollerance;
        }
        /// <summary>
        /// Costruttore per la strategy per le scatole con l'etichetta
        /// </summary>
        /// <param name="initString">Stringa per l'inizializzazione</param>
        public BoxWithLabelStrategy(String initString)
            : base(initString, "")
        {
            this.size = Point3F.from(initString.Split(']')[1].Split('[')[0]);
            this.tollerance = (float)Util.getDoubleFromString(initString.Split(']')[3].Split('[')[0]);
            //this.labels = new LabelOnBox(initString.Split(']')[3].Split('[')[0]);
        }
        public override BoundsF2D get2DBounds()
        {
            return new BoundsF2D(new Point2F(-getSize().X / 2.0F, -getSize().Y / 2.0F), new Point2F(getSize().X, getSize().Y));
        }
        public override Point3F getSize()
        {
            return size;
        }
        public override bool[] getToolData()
        {
            return new bool[0];
        }
        public override string ToString()
        {
            return name + "[" + size.X + ";" + size.Y + "]"; // this.GetType().Name;
        }
        public override Point2F getInitialCenter()
        {
            return new Point2F(0, 0);
        }
        public override string toDataString()
        {
            return "[size]" + getSize() + "[tollerance]" + tollerance + "[LabelOnBox]";// +labels.toDataString();
        }
        /// <summary>
        /// Test per vedere se c'è almeno una scatola con la label sul lato dato
        /// </summary>
        /// <param name="angle">Lato su cui fare il test</param>
        /// <returns>True se almeno una label è presente sul lato. False altrimenti</returns>
        public override bool haveLabelOnSideAt(double angle)
        {
            bool res = false;
            foreach (LabelOnBox l in this.labels)
                res = res || (Math.Abs(l.sideAngleWhereIs - angle) < Program.deltaAligment);
            return res;
        }
    }

    /// <summary>
    /// Rappresenta la Label della scatola
    /// </summary>
    public class LabelOnBox
    {
        String label;
        Point2F size;
        Point2F location;
        public double sideAngleWhereIs;
        /// <summary>
        /// Costruttore per la Label della scatola
        /// </summary>
        /// <param name="label">Label</param>
        /// <param name="size">Dimensione Label</param>
        /// <param name="location">Locazione</param>
        /// <param name="sideAngleWhereIs">Lato su cui è presente la label</param>
        public LabelOnBox(String label, Point2F size, Point2F location, double sideAngleWhereIs)
        {
            this.label = label;
            this.size = size;
            this.location = location;
            this.sideAngleWhereIs = sideAngleWhereIs;
        }
        public string toDataString()
        {
            return "[label]" + label + "[size]" + size + "[location]" + location + "[sideAngleWhereIs]" + sideAngleWhereIs;
        }
        //public int getQuadrantWhereIs()
        //{
        //    return (int)Math.Abs((sideAngleWhereIs + 90) / 90);
        //}

        /// <summary>
        /// Costruttore per la Label della scatola
        /// </summary>
        /// <param name="initString">Stringa per l'inizializzazione</param>
        public LabelOnBox(String initString)
        {
            this.label = initString.Split(']')[1].Split('[')[0];
            this.size = Point2F.from(initString.Split(']')[2].Split('[')[0]);
            this.location = Point2F.from(initString.Split(']')[3].Split('[')[0]);
            this.sideAngleWhereIs = Util.getDoubleFromString(initString.Split(']')[4].Split('[')[0]);
        }
    }
}