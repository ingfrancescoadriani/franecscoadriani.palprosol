using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;
using System.Drawing;
using ReGen.View;

namespace ReGen
{
    public class PayloadPlaced : Payload
    {
        private Point2F lastBoundCenter;
        private Point2F center;
        private int _quadrant = 0; //rotation = quadrant * 90
        /// <summary>
        /// Quadrante del piano
        /// </summary>
        public virtual int quadrant
        {
            get
            {
                return _quadrant;
            }
            set
            {
                _quadrant = (value + 8) % 4;
            }
        }
        public PayloadGroup group;
        /// <summary>
        /// Costruttore per la classe PayloadPlaced
        /// </summary>
        /// <param name="ps">PayloadStrategy da applicare</param>
        public PayloadPlaced(PayloadStrategy ps)
            : base(ps)
        {
            center = ps.getInitialCenter();
            lastBoundCenter = center;
        }
        /// <summary>
        /// Costruttore per la classe PayloadPlaced
        /// </summary>
        /// <param name="from">Payload sorgente da cui si estrae la Strategy</param>
        public PayloadPlaced(Payload from) : this(from.getPayloadStrategy()) { }
        /// <summary>
        /// Calcola il più piccolo rettangolo contenete il PayloadPlaced
        /// </summary>
        /// <returns>Il più piccolo rettangolo che contiene il PayloadPlaced</returns>
        public BoundsF2D getBounds()
        {
            //è possibile che in futuro sia necessario integrare il calcolo del Bounds ruotato all'interno 
            //della strategia se si usano forme diverse da rettangoli
            BoundsF2D res = ps.get2DBounds().rotated(quadrant);
            res.location = this.center.traslatedOf(res.location);
            return res;
        }
        /// <summary>
        /// Trova il centro assoluto del PayloadPlaced (centro del PayloadPlaced traslato quanto il centro del PayloadGroup)
        /// </summary>
        /// <returns>Centro assoluto del PayloadPlaced</returns>
        public Point2F getAbsCenter()
        {
            return center.traslatedOf(group.center);
        }
        /// <summary>
        /// Trova il centro relativo del PayloadPlaced
        /// </summary>
        /// <returns>Centro del PayloadPlaced</returns>
        public Point2F getRelCenter()
        {
            return new Point2F(center.X, center.Y);
        }
        /// <summary>
        /// Test per controlloare se il PayloadPlaced ha la Label sul lato indicato
        /// </summary>
        /// <param name="angle">Angolo su cui cercare la label</param>
        /// <returns>True se il PayloadPlaced ha la Label sul lato dato, False altrimenti</returns>
        public bool haveLabelOnSideAt(double angle)
        {
            return this.getPayloadStrategy().haveLabelOnSideAt((angle + (quadrant*90)) % 360);
        }

        /// <summary>
        /// Test per controllare se il PayloadPlaced è vicino di epsilon (1.0F) ad un altro PayloadPlaced
        /// </summary>
        /// <param name="other">PayloadPlace su cui si fa il test di vicinanza</param>
        /// <returns>True se la distanza tra i PayloadPlaced è minore di epsilon, False altrimenti</returns>
        public bool touching(PayloadPlaced other)
        {
            float epsilon = 1.0F;
            bool res = false;
            res = res || (Math.Abs((this.getBounds().location.X + this.getBounds().size.X) - other.getBounds().location.X) < epsilon);
            res = res || (Math.Abs((other.getBounds().location.X + other.getBounds().size.X) - this.getBounds().location.X) < epsilon);
            res = res || (Math.Abs((this.getBounds().location.Y + this.getBounds().size.Y) - other.getBounds().location.Y) < epsilon);
            res = res || (Math.Abs((other.getBounds().location.Y + other.getBounds().size.Y) - this.getBounds().location.Y) < epsilon); 
            return res;
        }
        /// <summary>
        /// Cambia il centro del PayloadPlaced
        /// </summary>
        /// <param name="newPos">Nuovo centro</param>
        public void recentering(Point2F newPos)
        {
            this.center = new Point2F(newPos.X, newPos.Y);
            this.setLastBoundCenter(this.center);
        }
        /// <summary>
        /// Sposta il PayloadPlaced (centro più offset)
        /// </summary>
        /// <param name="offset">Offset per lo spostamento</param>
        public void move(Point2F offset)
        {
            this.recentering(new Point2F(center.X + offset.X, center.Y + offset.Y));
        }
        /// <summary>
        /// Clona il PayloadPlaced e lo sposta
        /// </summary>
        /// <param name="offset">Offset per lo spostamento</param>
        /// <returns>PayloadPlaced spostato</returns>
        public PayloadPlaced moved(Point2F offset)
        {
            PayloadPlaced res = clone();
            res.move(offset);
            return res;
        }
        /// <summary>
        /// Setta il lastBoundCenter
        /// </summary>
        /// <param name="newPos">Nuova posizione</param>
        public void setLastBoundCenter(Point2F newPos)
        {
            this.lastBoundCenter = newPos;
        }
        public override string ToString()
        {
            return "c " + this.center.toLocation().ToString() + " - " + this.getBounds().ToString();
        }
        /// <summary>
        /// La coordinata X del lato sinistro del più piccolo rettangolo contenente il PayloadPlaced
        /// </summary>
        /// <returns>Coordinata X del lato sinistro del PayloadPlaced</returns>
        public double left()
        {
            return getBounds().left();
        }
        /// <summary>
        /// La coordinata Y del lato inferiore del più piccolo rettangolo contenente il PayloadPlaced
        /// </summary>
        /// <returns>Coordinata Y del lato inferiore del PayloadPlaced</returns>
        public double bottom()
        {
            return getBounds().bottom();
        }
        /// <summary>
        /// La coordinata X del lato destro del più piccolo rettangolo contenente il PayloadPlaced
        /// </summary>
        /// <returns>Coordinata X del lato destro del PayloadPlaced</returns>
        public double right()
        {
            return getBounds().right();
        }
        /// <summary>
        /// La coordinata Y del lato superiore del più piccolo rettangolo contenente il PayloadPlaced
        /// </summary>
        /// <returns>Coordinata Y del lato superiore del PayloadPlaced</returns>
        public double top()
        {
            return getBounds().top();
        }
        /// <summary>
        /// La coordinata X del lato sinistro del più piccolo rettangolo contenente il PayloadPlaced più uno spostamento
        /// </summary>
        /// <param name="offset">Offset per lo spostamento</param>
        /// <returns>Coordinata X del lato sinistro del PayloadPlaced più la coordinata X dell'offset</returns>
        public double left(Point2F offset)
        {
            return this.left() + offset.X;
        }
        /// <summary>
        /// La coordinata Y del lato inferiore del più piccolo rettangolo contenente il PayloadPlaced più uno spostamento
        /// </summary>
        /// <param name="offset">Offset per lo spostamento</param>
        /// <returns>Coordinata Y del lato inferiore del PayloadPlaced più la coordinata Y dell'offset</returns>
        public double bottom(Point2F offset)
        {
            return this.bottom() + offset.Y;
        }
        /// <summary>
        /// La coordinata X del lato destro del più piccolo rettangolo contenente il PayloadPlaced più uno spostamento
        /// </summary>
        /// <param name="offset">Offset per lo spostamento</param>
        /// <returns>Coordinata X del lato destro del PayloadPlaced più la coordinata X dell'offset</returns>
        public double right(Point2F offset)
        {
            return this.right() + offset.X;
        }
        /// <summary>
        /// La coordinata Y del lato superiore del più piccolo rettangolo contenente il PayloadPlaced più uno spostamento
        /// </summary>
        /// <param name="offset">Offset per lo spostamento</param>
        /// <returns>Coordinata Y del lato superiore del PayloadPlaced più la coordinata Y dell'offset</returns>
        public double top(Point2F offset)
        {
            return this.top() + offset.Y;
        }

        //TODO metodo non utilizzato
        public bool isContainedInRect(PointF offset, RectangleF rect)
        {
            return ((offset.X - (this.getBounds().size.X / 2) >= rect.Left ) &&
                (offset.X + (this.getBounds().size.X / 2) <= rect.Right) &&
                (offset.Y - (this.getBounds().size.Y / 2) >= rect.Bottom) &&
                (offset.Y + (this.getBounds().size.Y / 2) <= rect.Top));
        }
        //TODO metodo non utilizzato
        protected virtual List<Figure> getListFigureToRender(PlacingState ps, Point2F basePointF, bool selected)
        {
            List<Figure> res = new List<Figure>();
            Point basePoint = MainForm.pointMetersToPixels(basePointF);
            Figure fp = Figure.getFigure(this, ps, new Point(0,0), selected);
            res.Add(fp);
            return res;
        }
        /// <summary>
        /// Setta il centro del PayloadPlaced con il lastBoundCenter
        /// </summary>
        /// <returns>PayloadPlaced con il nuovo centro</returns>
        public PayloadPlaced placeMe()
        {
            this.center = lastBoundCenter;
            return this;
        }
        /// <summary>
        /// Clona il PayloadPlaced e gli setta il centro con il lastBoundCenter
        /// </summary>
        /// <returns>PayloadPlaced clonato con il nuovo centro</returns>
        public PayloadPlaced placedMe()
        {
            PayloadPlaced res = this.clone();
            res.center = lastBoundCenter;
            return res;
        }
        /// <summary>
        /// Clona un PayloadPlaced su una destinazione specificata
        /// </summary>
        /// <param name="pTo">PayloadPlaced destinazione</param>
        /// <param name="pFrom">PayloadPlaced da clonare</param>
        public static void cloneFromTo(PayloadPlaced pTo,PayloadPlaced pFrom){
            pTo.center = new Point2F(pFrom.center.X, pFrom.center.Y);
            pTo.quadrant = pFrom.quadrant;
            pTo.ps = pFrom.getPayloadStrategy();
            pTo.group = pFrom.group;
            pTo.setLastBoundCenter(pFrom.lastBoundCenter);
            //TODO test deb
        }
        /// <summary>
        /// Clona il PaylaodPlaced
        /// </summary>
        /// <returns>PayloadPlaced clonato</returns>
        public PayloadPlaced clone()
        {
            PayloadPlaced pp = (PayloadPlaced)this.MemberwiseClone();
            pp.resetStrategy(this.ps);
            PayloadPlaced.cloneFromTo(pp, this);
            return pp;
        }
        /// <summary>
        /// Test per contrallare se un punto è contenuto all'interno del PayloadPlaced
        /// </summary>
        /// <param name="p">Punto da controllare</param>
        /// <returns>True se il punto è contenuto all'interno del PayloadPlaced, False altrimenti</returns>
        public bool contains(Point2F p)
        {
            RectangleF r = this.getBounds().toRectF();
            r.Offset(this.group.center.toLocation());
            return (r.Contains(p.toLocation()));
        }
        /// <summary>
        /// Test per controllare se un altro PayloadPlaced collide con questo PayloadPlaced
        /// </summary>
        /// <param name="p">PayloadPlaced da controllare</param>
        /// <returns>True se i PayloadPlaced collidono, False altrimenti</returns>
        public bool collides(PayloadPlaced p)
        {
            return collides(p.regionOfMe());
        }

        //TODO fix/ non utilizzato
        //public bool collides(PayloadGroup pg)
        //{
        //    return pg.collides(pg, this);
        //}
        //TODO metodo non utilizzato
        public bool collides(RectangleF r)
        {
            return collides(new Region(r));
        }
        /// <summary>
        /// Test per controllare se una Region collide con il PayloadPlaced
        /// </summary>
        /// <param name="r">Region da analizzare</param>
        /// <returns>True se la Region collide con il PayloadPlaced, False altrimenti</returns>
        public bool collides(System.Drawing.Region r)
        {
            Region rMe = regionOfMe();
            rMe.Intersect(r);
            return !rMe.IsEmpty(Program.g1);
        }
        /// <summary>
        /// Trova il più piccolo rettangolo che contiene il PayloadPlaced
        /// </summary>
        /// <returns>Rettangolo che contiene il PayloadPlaced</returns>
        public RectangleF rectOfMeAbsolute()
        {
            RectangleF r  = this.getBounds().toRectF();
            r.Offset(this.group.center.toLocation());
            return r;
        }
        /// <summary>
        /// Trova la Region del PayloadPlaced
        /// </summary>
        /// <returns>Region del PayloadPlaced</returns>
        public Region regionOfMe()
        {
            Region r = new Region(this.rectOfMeAbsolute());
            return r;
        }
        /// <summary>
        /// Clona il PayloadPlaced e lo sposta al centro
        /// </summary>
        /// <returns>PayloadPlaced clonato e centrato</returns>
        public PayloadPlaced meWithAbsCenter()
        {
            return this.clone().moved(group.center);
        }
    }
}
