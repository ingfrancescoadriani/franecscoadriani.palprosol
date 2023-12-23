using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;
using System.Drawing;
using ReGen.View;
using ReGen.Model;
using ReGen.Model.AutoPopulate;

namespace ReGen
{
    /// <summary>
    /// Rappresenta il carico che fa parte/ da inserire in uno strato
    /// </summary>
    public class PayloadGroup
    {
        private Approaches approaches;
        private static long lastId = 0;
        protected long id;
        private Point3FR pCatching = new Point3FR(0,0,0,0);
        public virtual Point3FR pointCatching
        {
            get { return pCatching; }
        }
        public Queue<String> queueStringForDebug = new Queue<string>();
        private Point2F lastBoundCenter = new Point2F(0, 0);
        private Point2F _center = new Point2F(0, 0);
        public virtual Point2F center
        {
            get
            {
                return _center;
            }
            set
            {
                _center = value;
                this.lastBoundCenter = this._center;
            }
        }
        //rotation=quadrant*90
        private int quadrant;
        private List<PayloadPlaced> listPayloadPlaced;

        
        public List<ProblemOfPositioning> listProblem = new List<ProblemOfPositioning>();

        private PayloadOnPalletRanker pRanker = SolutionFacade.Instance.payloadOnPalletRanker;
        /// <summary>
        /// Aggiunge un PayloadGroup con l'accosto da un lato 
        /// </summary>
        /// <param name="pg">PayloadGroup da aggiungere</param>
        /// <param name="side">Lato da cui inserire il PayloadGroup</param>
        public void addApprochedPayload(PayloadGroup pg, SideOrPoint side)
        {
            this.approaches.addApproach(pg, side);
        }

        /// <summary>
        /// Costruttore del PayloadGroup
        /// </summary>
        public PayloadGroup()
        {
            listPayloadPlaced = new List<PayloadPlaced>();
            reNewId();
            this.approaches = new Approaches(this);
        }
        /// <summary>
        /// Costruttore del PayloadGroup
        /// </summary>
        /// <param name="pp">PayloadPlaced da aggiungere al PayloadGroup</param>
        public PayloadGroup(PayloadPlaced pp)
            : this()
        {
            this.addToPayloadPlacedList(pp);
            this.quadrant = pp.quadrant;
            pp.quadrant = 0;
        }
        //TODO: da implementare nel PayloadGroup specializzato
        public virtual PayloadGroup newPayloadGroup(PayloadPlaced pp)
        {
            return new PayloadGroup(pp.meWithAbsCenter());
        }
        /// <summary>
        /// Aggiornamento dell'Id
        /// </summary>
        private void reNewId()
        {
            lastId = (lastId + 1) % long.MaxValue;
            id = lastId;
        }
        /// <summary>
        /// Conto del numero dei Payload piazzati con la label su di un lato
        /// </summary>
        /// <param name="angle">Lato da controllare</param>
        /// <returns>Numero di Payload che hanno la label sul lato</returns>
        public int labelOnSideAt(double angle)
        {
            int res = 0;
            foreach(PayloadPlaced p in this.listPayloadPlaced)
                if (p.haveLabelOnSideAt(angle))
                    res++;
            return res;
        }

        /// <summary>
        /// La posizione da cui accostare il Payload (X, Y)
        /// </summary>
        /// <returns>La posizione da cui accostare il Payload (X, Y)</returns>
        public Point2F getApproachDirection()
        {
            return approaches.getApproachDirection().to2DLocation();
        }
        /// <summary>
        /// La posizione completa da cui accostare il Payload (X,Y,Z)
        /// </summary>
        /// <returns>La posizione completa da cui accostare il Payload (X,Y,Z)</returns>
        public Point3F getCompleteApproachDirection()
        {
            return new Point3F(approaches.getApproachDirection().X, approaches.getApproachDirection().Y, approaches.getApproachDirection().Z);
        }

        /// <summary>
        /// Calcola il modo dell'accostamento
        /// </summary>
        /// <returns>Codice dell'accostamento</returns>
        public int getApproachCode()
        {
            int res = 0;
            Point2F p = this.getApproachDirection();
            if (Util.absAmplitude(p.toLocation()) > 0)
            {
                double angle = Util.angleOf(new PointF(0, 0), p.toLocation());
                res = 1 + (int)Math.Round(((angle + (3*Math.PI)) % (2*Math.PI)) / (Math.PI / 4));
            }
            return res;
        }
        /// <summary>
        /// Torna il modo dell'accostamento applicato
        /// </summary>
        /// <returns>Modo dell'accostamento applicato</returns>
        public Approaches getApproaches()
        {
            return this.approaches;
        }
        /// <summary>
        /// Resetta l'accostamento
        /// </summary>
        public void resetApproaches()
        {
            this.approaches = new Approaches(this);
        }
        /// <summary>
        /// Cambia il modo dell'accostamento
        /// </summary>
        /// <param name="a">Nuovo accostamento</param>
        public void importApproaches(Approaches a)
        {
            this.approaches = a;
        }

        /// <summary>
        /// Calcola qual è il lato o il punto vicino a un dato PayloadGroup
        /// </summary>
        /// <param name="nearTo">PayloadGroup di cui si vuole vedere la vicinanza</param>
        /// <returns>Il lato o il punto vicino, Null se non sono vicini i PayloadGroup</returns>
        public SideOrPoint nearSideOrPoint(PayloadGroup nearTo)
        {
            SideOrPoint res = null;
            double epsilon = 0.01;
            bool isInX = false;
            bool isInY = false;
            bool l = false;
            bool r = false;
            //controllo se il Payload è alla destra di quello piazzato
            if ((Math.Abs(left() - nearTo.right()) < epsilon))
            {
                res = new LeftSide();
                l = true;
                isInX=true;
            }
            //controllo se il Payload è alla sinistra di quello piazzato
            if ((Math.Abs(right() - nearTo.left()) < epsilon))
            {
                res = new RightSide();
                r = true;
                isInX=true;
            }
            //controllo se il Payload è sotto a quello piazzato
            if ((Math.Abs(top() - nearTo.bottom()) < epsilon))
            {
                //controllo se il Payload è anche sulla sinistra o sulla destra
                if (l)
                    res = new UpLeftPoint();
                else if (r)
                    res = new UpRightPoint();
                else
                    res = new UpSide();
                isInY=true;
            }
            //controllo se il Payload è sopra a quello piazzato
            if ((Math.Abs(bottom() - nearTo.top()) < epsilon))
            {
                //controllo se il Payload è anche sulla sinistra o sulla destra
                if (l)
                    res = new DownLeftPoint();
                else if (r)
                    res = new DownRightPoint();
                else
                    res = new DownSide();
                isInY=true;
            }

            if (isInY)
            {
                double minRange = nearTo.left() - this.getSize().X;
                double maxRange = nearTo.right();

                if (!(left() >= minRange && left() <= maxRange))
                     res=null;
            }
            if (isInX)
            {
                double minRange = nearTo.bottom() - this.getSize().Y;
                double maxRange = nearTo.top();

                if (!(bottom() >= minRange && bottom() <= nearTo.top()))
                    res = null;
            }

            return res;
        }
        /// <summary>
        /// Torna l'id del PayloadGroup
        /// </summary>
        /// <returns>Id del PayloadGroup</returns>
        public virtual long getId()
        {
            return id;
        }
        /// <summary>
        /// Torna il rank del PayloadGroup in un dato pallet
        /// </summary>
        /// <param name="pos">PalletOnSystem</param>
        /// <returns>Rank del PayloadGroup dentro il PalletOnSystem</returns>
        public double getRank(PalletOnSystem pos)
        {
            return pRanker.getRank(this, pos);
        }

        /// <summary>
        /// posizione fissa eventualmente fissata dall'utente
        /// </summary>
        public long fixedProgressivePosition { get; set; } 

        /// <summary>
        /// Test per vedere se questo PayloadGroup è uguale a quello passato come parametro
        /// </summary>
        /// <param name="pg">PayloadGroup su cui fare il test</param>
        /// <returns>True se i due PayloadGroup sono uguali, False altrimenti</returns>
        public bool isTheSame(PayloadGroup pg)
        {
            return (this.id == pg.id);
        }
        /// <summary>
        /// Torna il pCatching del PayloadGroup
        /// </summary>
        /// <returns>pCatching del PayloadGroup</returns>
        public Point3FR getPointCatching()
        {
            return pCatching;
        }
        //TODO: metodo non utilizzato
        public Point3FR getPoint3FRReleasing(int floor)
        {
            return new Point3FR(center.X, center.Y, floor * this.getMaxDeep(), Program.getRobotRotation(this.quadrant * 90.0 - this.getPointCatching().R));
        }

        //TODO: parametro floor non utilizzato
        /// <summary>
        /// Lista contenente i 4 vertici del rettangolo pià piccolo che copre il gruppo con associato
        /// un angolo che va dal vertice verso il centro
        /// </summary>
        /// <param name="floor">Non viene utilizzato</param>
        /// <returns>Lista contenente i 4 punti con l'angolo associato</returns>
        public List<Point2FWithDirection> getListPoint(int floor)
        {
            List<Point2FWithDirection> res = new List<Point2FWithDirection>();
            res.Add(getBounds().location.toPoint2FWithDirection(135));
            res.Add(getBounds().location.traslatedOf(getBounds().size).toPoint2FWithDirection(315));
            res.Add(getBounds().location.traslatedOf(new Point2F(getBounds().size.X, 0)).toPoint2FWithDirection(45));
            res.Add(getBounds().location.traslatedOf(new Point2F(0, getBounds().size.Y)).toPoint2FWithDirection(225));
            return res;
        }

        /// <summary>
        /// Trova l'altezza massima tra i Payload piazzati
        /// </summary>
        /// <returns>Altezza massima</returns>
        public double getMaxDeep()
        {
            double res = 0;
            foreach (PayloadPlaced pp in this.listPayloadPlaced)
            {
                res = Math.Max(res, pp.getOriginalSize().Z);
            }
            return res;
        }

        /// <summary>
        /// Setta il pCatching
        /// </summary>
        /// <param name="p">Valore del nuovo pCatching</param>
        public void setPointCatching(Point3FR p)
        {
            this.pCatching = p.clone();
        }

        /// <summary>
        /// Aggiunge il Payload nella lista dei PayloadPlaced
        /// </summary>
        /// <param name="pp">PayloadPlaced da aggiungere in lista</param>
        public void addToPayloadPlacedList(PayloadPlaced pp)
        {
            //si presume che abbia coordinate già assolute dato che non appartiene a nessun gruppo
            Point2F cooSum = pp.getRelCenter();
            double xSum = cooSum.X + this.center.X * this.listPayloadPlaced.Count;
            double ySum = cooSum.Y + this.center.Y * this.listPayloadPlaced.Count;
            xSum = xSum / (this.listPayloadPlaced.Count + 1);
            ySum = ySum / (this.listPayloadPlaced.Count + 1);
            foreach (PayloadPlaced ppTmp in listPayloadPlaced)
                ppTmp.move(new Point2F(center.X - xSum, center.Y - ySum));
            this.center = new Point2F(xSum, ySum);
            PayloadPlaced ppMoved = pp.moved(new Point2F(-this.center.X, -this.center.Y));
            this.listPayloadPlaced.Add(ppMoved);
            ppMoved.group = this;
        }
        ///// <summary>
        ///// Torna la Strategy del primo Payload nella lista dei PayloadPlaced
        ///// </summary>
        ///// <returns>Strategy del primo Payload nella lista dei PayloadPlaced</returns>
        //public PayloadStrategy getPayloadStrategy()
        //{
        //    return listPayloadPlaced[0].getPayloadStrategy();
        //}
        /// <summary>
        /// Trova l'unione dei più piccoli rettangoli che coprono i vari Payload piazzati
        /// </summary>
        /// <returns>La forma finale delle varie unioni</returns>
        public BoundsF2D getBounds()
        {
            //RectangleF r = getRectBounds();  //in questo modo vengono approssimati tutti i valori agli interi NON VA BENE
            //return new BoundsF2D(r);
            BoundsF2D res = new BoundsF2D(listPayloadPlaced[0].rectOfMeAbsolute());
            foreach (PayloadPlaced pp in listPayloadPlaced)
            {
                res.union(pp.rectOfMeAbsolute());
            }
            return res;
        }

        //privato
        private RectangleF getRectBounds()
        {
            RectangleF res = new RectangleF();
            Region rA = new Region(new RectangleF(0, 0, 0, 0));
            foreach (PayloadPlaced pp in this.listPayloadPlaced)
            {
                rA.Union(pp.rectOfMeAbsolute());
            }
            res = rA.GetBounds(FrmTmp.Instance.getTempGraphics());
            return res;
        }

        //TODO dentro si usa un metodo da implementare newPayloadGroup(pp)
        public List<PayloadGroup> disperse()
        {
            List<PayloadGroup> listPayloadGroup = new List<PayloadGroup>();
            foreach (PayloadPlaced pp in this.listPayloadPlaced)
            {
                //Payload pTmp = pp.meWithAbsCenter();
                PayloadGroup p = newPayloadGroup(pp);
                p.setPointCatching(this.getPointCatching());
                listPayloadGroup.Add(p);
            }
            return listPayloadGroup;
        }
        /// <summary>
        /// Unisce un PayloadGroup
        /// </summary>
        /// <param name="pg">PayloadGroup da unire</param>
        public void mergeWith(PayloadGroup pg){
            foreach (PayloadPlaced pp in pg.listPayloadPlaced)
                this.addToPayloadPlacedList(pp.meWithAbsCenter());
        }
        /// <summary>
        /// Torna la dimensione del PayloadGroup
        /// </summary>
        /// <returns>Dimensione del PayloadGroup</returns>
        private Point2F getSize()
        {
            return new Point2F(this.getRectBounds().Size.ToPointF());
        }
        /// <summary>
        /// Calcola l'altezza del PayloadGroup, cioè l'altezza massima tra i Payload piazzati
        /// </summary>
        /// <returns>Altezza del PayloadGroup</returns>
        public float height()
        {
            float maxH = 0;
            foreach (Payload p in listPayloadPlaced)
                maxH = Math.Max(maxH, p.getOriginalSize().Z);
            return maxH;
        }

        /// <summary>
        /// Clona il PayloadGroup
        /// </summary>
        /// <returns>PayloadGroup clone</returns>
        public PayloadGroup clone()
        {
            PayloadGroup pg = (PayloadGroup) this.MemberwiseClone();
            PayloadGroup.cloneFromTo(pg, this);
            pg.reNewId();
            return pg;
        }
        /// <summary>
        /// Clona un PayloadGroup sorgente su un PayloadGroup destinazione
        /// </summary>
        /// <param name="pTo">PayloadGroup destinazione</param>
        /// <param name="pFrom">PayloadGroup sorgente</param>
        public static void cloneFromTo(PayloadGroup pTo, PayloadGroup pFrom)
        {
            pTo.quadrant = pFrom.quadrant;
            pTo.setPointCatching(pFrom.getPointCatching());
            pTo.listPayloadPlaced = new List<PayloadPlaced>();
            foreach (PayloadPlaced pp in pFrom.listPayloadPlaced)
                pTo.addToPayloadPlacedList(pp.moved(pFrom.center));
            pTo.listProblem = new List<ProblemOfPositioning>();
            foreach (ProblemOfPositioning pp in pFrom.listProblem)
                pTo.listProblem.Add(pp);
            pTo.center = pFrom.center.clone();
            pTo.lastBoundCenter = pFrom.lastBoundCenter.clone();
        }
        /// <summary>
        /// Clona il PayloadGroup e gli cambia il centro
        /// </summary>
        /// <param name="p">Nuove centro del PayloadGroup</param>
        /// <returns>PayloadGroup clone con il nuovo centro</returns>
        public PayloadGroup withCenter(Point2F p)
        {
            PayloadGroup pgCloned = this.clone();
            pgCloned.center = p;
            return pgCloned;
        }
        /// <summary>
        /// Ritorna il PayloadPlaced in posizione nella lista dei PayloadPlaced
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PayloadPlaced getPayloadPlacedAt(int index)
        {
            return listPayloadPlaced[index];
        }
        /// <summary>
        /// Trova il numero di PayloadPlaced nella lista
        /// </summary>
        /// <returns>Numero di PayloadPlaced nella lista</returns>
        public int countListPayloadPlaced()
        {
            return listPayloadPlaced.Count;
        }
        public PayloadPlaced firstOfListPayloadPlaced()
        {
            return listPayloadPlaced[0];
        }
        //TODO da capire
        //restituisce per la grafica i grip tra i payload
        //ancoraggi tra coppie di payload
        /// <summary>
        /// Restituisce una lista di Grip tra i Payload nella lista dei PayloadPlaced
        /// </summary>
        /// <returns>Lista di Grip tra i Payload nella lista dei PayloadPlaced</returns>
        public List<Grip> getListGrip()
        {
            List<Grip> lg = new List<Grip>();
            foreach (PayloadPlaced pp1 in listPayloadPlaced)
            {
                foreach (PayloadPlaced pp2 in listPayloadPlaced)
                {
                    if (!pp1.Equals(pp2) && pp1.touching(pp2))
                    {
                        bool thereIs = false;
                        foreach (Grip g in lg)
                        {
                            if ((g.payload1==pp1 && g.payload2==pp2) || (g.payload2==pp1 && g.payload1==pp2))
                            {
                                thereIs = true;
                                break;
                            }
                        }
                        if (!thereIs)
                        {
                            Grip tmpG = new Grip();
                            tmpG.payload1 = pp1;
                            tmpG.payload2 = pp2;
                            tmpG.center = new Point2F((pp1.getAbsCenter().X + pp2.getAbsCenter().X) / 2.0F, (pp1.getAbsCenter().Y + pp2.getAbsCenter().Y) / 2.0F);
                            tmpG.angle = (float)Util.angleFromToPointF(pp1.getAbsCenter().toLocation(), pp2.getAbsCenter().toLocation());
                            tmpG.center = new Point2F(Util.rotatedOf(tmpG.center.toLocation(), tmpG.getStartPoint(40).toLocation(), Math.PI / 2.0));
                            lg.Add(tmpG);

                            tmpG = new Grip();
                            tmpG.payload1 = pp1;
                            tmpG.payload2 = pp2;
                            tmpG.center = new Point2F((pp1.getAbsCenter().X + pp2.getAbsCenter().X) / 2.0F, (pp1.getAbsCenter().Y + pp2.getAbsCenter().Y) / 2.0F);
                            tmpG.angle = (float)Util.angleFromToPointF(pp1.getAbsCenter().toLocation(), pp2.getAbsCenter().toLocation());
                            tmpG.center = new Point2F(Util.rotatedOf(tmpG.center.toLocation(), tmpG.getStartPoint(40).toLocation(), Math.PI / 2.0 * 3.0));
                            lg.Add(tmpG);
                        }
                    }
                }
            }
            return lg;
        }
        /// <summary>
        /// Cambia il centro del PayloadGroup
        /// </summary>
        /// <param name="newPos">Nuovo centro</param>
        public void recentering(Point2F newPos)
        {
            this.center = new Point2F(newPos.X, newPos.Y);
            this.lastBoundCenter = this.center;
        }
        /// <summary>
        /// Muove il PayloadGroup
        /// </summary>
        /// <param name="offset">Offset per lo spostamento</param>
        public void move(Point2F offset)
        {
            this.recentering(new Point2F(center.X + offset.X, center.Y + offset.Y));
        }
        /// <summary>
        /// Clona il PayloadGroup e lo sposta
        /// </summary>
        /// <param name="offset">Offset per lo spostamento</param>
        /// <returns>Il PayloadGroup spostato</returns>
        public PayloadGroup moved(Point2F offset)
        {
            PayloadGroup res = clone();
            res.move(offset);
            return res;
        }
        /// <summary>
        /// Setta il lastBoundCenter del PayloadGroup 
        /// </summary>
        /// <param name="newPos">Nuovo punto lastBoundCenter</param>
        public void setLastBoundPosition(Point2F newPos)
        {
            this.lastBoundCenter = newPos;
        }
        /// <summary>
        /// Coordinata X del lato sinistro
        /// </summary>
        /// <returns>Posizione del lato sinistro</returns>
        public double left()
        {
            return this.getBounds().left();
        }
        /// <summary>
        /// Coordinata Y del lato inferiore
        /// </summary>
        /// <returns>Posizione del lato inferiore</returns>
        public double bottom()
        {
            return this.getBounds().bottom();
        }
        /// <summary>
        /// Coordinata X del lato destro
        /// </summary>
        /// <returns>Posizione del lato destro</returns>
        public double right()
        {
            return this.getBounds().right();
        }
        /// <summary>
        /// Coordinata Y del lato superiore
        /// </summary>
        /// <returns>Posizione del lato superiore</returns>
        public double top()
        {
            return this.getBounds().top();
        }
        /// <summary>
        /// Coordinata X del lato sinistro più un offset
        /// </summary>
        /// <param name="offset">Offset per la traslazione</param>
        /// <returns>Somma del lato sinistro e l'offset </returns>
        public double left(Point2F offset)
        {
            return this.left() + offset.X;
        }
        /// <summary>
        /// Coordinata Y del lato inferiore più un offset
        /// </summary>
        /// <param name="offset">Offset per la traslazione</param>
        /// <returns>Somma del lato inferiore e l'offset</returns>
        public double bottom(Point2F offset)
        {
            return this.bottom() + offset.Y;
        }
        /// <summary>
        /// Coordinata X del lato destro più un offset
        /// </summary>
        /// <param name="offset">Offset per la traslazione</param>
        /// <returns>Somma del lato destro e l'offset</returns>
        public double right(Point2F offset)
        {
            return this.right() + offset.X;
        }
        /// <summary>
        /// Coordinata Y del lato superiore più un offset
        /// </summary>
        /// <param name="offset">Offset per la traslazione</param>
        /// <returns>Somma del lato superiore e l'offset</returns>
        public double top(Point2F offset)
        {
            return this.top() + offset.Y;
        }
        /// <summary>
        /// Torna il quadrante per la rotazione
        /// </summary>
        /// <returns>Quadrante per la rotazione</returns>
        public int getQuadrant()
        {
            return quadrant;
        }
        /// <summary>
        /// Torna il quadrante per la rotazione del primo Payload della lista dei PayloadPlaced
        /// </summary>
        /// <returns>Quadrante per la rotazione del primo Payload della lista dei PayloadPlaced</returns>
        public int getQuadrantOfFirstBox()
        {
            return listPayloadPlaced[0].quadrant;
        }
        /// <summary>
        /// Setta il quadrante di tutti Payload piazzati nel PayloadGroup
        /// </summary>
        /// <param name="newQuadrant">Nuovo quadrante</param>
        public void setQuadrant(int newQuadrant)
        {
            int deltaQuadrant = (newQuadrant - quadrant + 8) % 4;
            this.quadrant = newQuadrant;

            double radiantAngle = deltaQuadrant * Math.PI / 2.0;
            foreach (PayloadPlaced pp in listPayloadPlaced)
            {
                Point2F pRes = new Point2F(Util.rotatedOf(new PointF(0, 0), pp.getRelCenter().toLocation(), radiantAngle));
                pp.recentering(pRes);
                pp.quadrant = pp.quadrant + deltaQuadrant % 4;
            }
        }
        /// <summary>
        /// Test per controllare se il PayloadGroup traslato di un offset è contenuto in un rettangolo dato
        /// </summary>
        /// <param name="offset">Offset di spostamento</param>
        /// <param name="rect">Rettangolo che deve contenere il PayloadGroup</param>
        /// <returns>True se il PayloadGroup è contenuto nel rettangolo, False altrimenti</returns>
        public bool isContainedInRect(PointF offset, RectangleF rect)
        {
            return ((this.left(new Point2F(offset)) >= rect.Left ) &&
                (this.right(new Point2F(offset)) <= rect.Right) &&
                (this.bottom(new Point2F(offset)) >= rect.Bottom) &&
                (this.top(new Point2F(offset)) <= rect.Top));
        }
        //TODO da capire
        /// <summary>
        /// Costruisce una lista di figure da renderizzare con i PayloadPlaced e il PayloadGroup
        /// </summary>
        /// <param name="ps">PlacingState</param>
        /// <param name="basePointF">basePointF</param>
        /// <param name="selected">Selected</param>
        /// <returns>Lista di figure da renderizzare con i PayloadPlaced e il PayloadGroup</returns>
        public virtual List<Figure> getListFigureToRender(PlacingState ps, Point2F basePointF, bool selected)
        {
            List<Figure> res = new List<Figure>();
            Point basePoint = MainForm.pointMetersToPixels(basePointF);
            foreach (PayloadPlaced pp in listPayloadPlaced)
            {
                pp.getBounds();
                Figure fp = Figure.getFigure(pp.meWithAbsCenter(), ps, new Point(0,0), selected);
                res.Add(fp);
            }
            Figure f = Figure.getFigure(this, ps, new Point(0, 0), selected);
            res.Add(f);
            return res;
        }
        /// <summary>
        /// Aggiorna il centro del PayloadGroup settandolo a lastBoundCenter
        /// </summary>
        /// <returns>PayloadGroup con il nuovo centro</returns>
        public PayloadGroup placeMe()
        {
            this.center = lastBoundCenter;
            return this;
        }
        /// <summary>
        /// Clona il PayloadGroup e cambia il suo centro settandolo a lastBoundCenter
        /// </summary>
        /// <returns>PayloadGroup clone con il nuovo centro</returns>
        public PayloadGroup placedMe()
        {
            PayloadGroup res = this.clone();
            res.placeMe();
            return res;
        }
        /// <summary>
        /// Test per controllare se un punto è contenuto all'interno del PayloadGroup
        /// </summary>
        /// <param name="p">Punto da controllare</param>
        /// <returns>True se il punto è contenuto nello spazio del PayloadGroup</returns>
        public bool contains(Point2F p)
        {
            bool res = false;
            foreach (PayloadPlaced pp in listPayloadPlaced)
                res = res | (pp.contains(p));
            return (res);
        }
        /// <summary>
        /// Test per controllare se i PayloadPlaced di un PayloadGroup collidono tra di loro
        /// </summary>
        /// <param name="p">PayloadGroup da analizzare</param>
        /// <returns>True se almeno due PayloadPlaced del PayloadGroup collidono tra di loro, False altrimenti</returns>
        public bool collides(PayloadGroup p)
        {
            bool res = false;
            foreach (PayloadPlaced pp in p.listPayloadPlaced)
                res = res | collides(pp);
            return res;
        }
        /// <summary>
        /// Test per controllare se il PayloadPlaced collide con un altro
        /// </summary>
        /// <param name="p">PayloadPlaced da analizzare</param>
        /// <returns>True se il PayloadPlaced collide con un altro, False altrimenti</returns>
        public bool collides(PayloadPlaced p)
        {
            return collides(p.regionOfMe());
        }
        /// <summary>
        /// Test per controllare se un rettangolo collide con un altro
        /// </summary>
        /// <param name="r">Rettangolo da analizzare</param>
        /// <returns>True se il rettangolo collide con un altro, False altrimenti</returns>
        public bool collides(RectangleF r)
        {
            return collides(new Region(r));
        }

        /// <summary>
        /// Test per controllare se i PayloadPlaced del PayloadGroup collidono con la Region data
        /// </summary>
        /// <param name="r">Region da analizzare</param>
        /// <returns>True se almeno un PayloadPlaced collide con la Region data, False altrimenti</returns>
        public bool collides(System.Drawing.Region r)
        {
            bool res = false;
            foreach (PayloadPlaced pp in listPayloadPlaced)
                res = res | (pp.collides(r));
            return (res);
        }
        /// <summary>
        /// Calcola la Region unendo le Region dei PayloadPlaced del PayloadGroup
        /// </summary>
        /// <returns>La region del PayloadGroup</returns>
        public Region regionOfMe()
        {
            Region r = listPayloadPlaced[0].regionOfMe();
            foreach (PayloadPlaced pp in listPayloadPlaced)
                r.Union(pp.regionOfMe());
            return r;
        }
        /// <summary>
        /// Test per verificare se i PayloadPlaced di un PayloadGroup sono messi correttamente
        /// </summary>
        /// <returns>True se non ci sono errori di piazzamento, False altrimenti</returns>
        public bool isCorrectlyPlaced()
        {
            return (listProblem.Count == 0);
        }
        /// <summary>
        /// Informazioni sul PayloadGroup
        /// </summary>
        /// <returns>Stringa contenente le informazioni</returns>
        public String extendedInfo()
        {
            String res = this.ToString() + " r " + (this.quadrant * 90).ToString() + "\n\r";
            foreach (PayloadPlaced p in this.listPayloadPlaced)
            {
                res += p.ToString() + "\n\r";
            }
            return res;
        }
        /// <summary>
        /// Torna una stringa con le informazioni sul centro e sulla dimensione
        /// </summary>
        /// <returns>Stringa con le informazioni sul centro e sulla dimensione</returns>
        public override String ToString()
        {
            return "c " + this.center.toLocation().ToString() + " - " + this.getBounds().ToString();
        }

        public virtual void horizontalFlip(double yLineParam)
        {
        }
        public virtual void verticalFlip(double yLineParam)
        {
        }
    }
    /// <summary>
    /// Rappresenta gli ancoraggi
    /// </summary>
    public class Grip
    {
        public Point2F center;
        public float angle;
        public PayloadPlaced payload1;
        public PayloadPlaced payload2;
        /// <summary>
        /// Trova il punto da cui far partire l'ancoraggio
        /// </summary>
        /// <param name="lenghtSegment">Lunghezza del segmento per l'ancoraggio</param>
        /// <returns>Punto di partenza per l'ancoraggio</returns>
        public Point2F getStartPoint(float lenghtSegment)
        {
            return new Point2F(center.X + (lenghtSegment * Math.Cos(angle + Math.PI)), center.Y + (lenghtSegment * Math.Sin(angle + Math.PI)));
        }
        /// <summary>
        /// Trova il punto in cui finisce l'angoraggio
        /// </summary>
        /// <param name="lenghtSegment">Lunghezza del segmento per l'ancoraggio</param>
        /// <returns>Punto di fine per l'ancoraggio</returns>
        public Point2F getEndPoint(float lenghtSegment)
        {
            return new Point2F(center.X + (lenghtSegment * Math.Cos(angle)), center.Y + (lenghtSegment * Math.Sin(angle)));
        }
    }

}