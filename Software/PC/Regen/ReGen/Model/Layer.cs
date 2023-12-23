using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;
using System.Drawing;
using ReGen.Model.AutoPopulate;
using ReGen.View;

namespace ReGen
{
    /// <summary>
    /// Rappresenta uno strato di un Pallet piazzato
    /// </summary>
    public class Layer
    {
        /// <summary>
        /// Lista dei PayloadGroupPlaced nel Layer
        /// </summary>

        public bool interLayer = false;
        public double interLayerThickness
        {
            get
            {
                return pallet.interLayerThickness;
            }
        }
        public List<PayloadGroup> listPayloadGroupPlaced;
        public List<PayloadGroup> sortedListPayloadGroupPlaced
        {
            get
            {
                return listPayloadGroupPlaced.OrderBy(y => ReGen.Model.Sequencer.getProgressive(y.getId(), this)).ToList();
            }
        }

        //public List<PayloadGroup> listPayloadGroupPlaced;
        private PalletOnSystem pallet;
        public Point2F getPalletSize()
        {
            return new Point2F(pallet.size.toSize());
        }
        public Point2F getPalletSizeWithExtraBorder()
        {
            return pallet.getBundleOfFirstLayerWithBorder().size;
        }
        public virtual Point3F size
        {
            get
            {
                double maxH = 0;
                foreach (PayloadGroup pg in listPayloadGroupPlaced)
                    maxH = Math.Max(maxH, pg.height());
                maxH += interLayerThickness;
                return new Point3F(pallet.getSize().X,pallet.getSize().Y,maxH);
            }
        }
        /// <summary>
        /// Costruttore della classe Layer
        /// </summary>
        /// <param name="pallet">Pallet contenente gli strati</param>
        public Layer(PalletOnSystem pallet)
        {
            this.pallet = pallet;
            listPayloadGroupPlaced = new List<PayloadGroup>();
        }
        
        //public bool canCointainsPayloadGroupAtPoint(PayloadGroup pg, Point2FWithDirection p)
        //{
        //    return canCointainsPayloadGroup(pg.withCenter(p));
        //}

        /// <summary>
        /// Punto dove è possibile inserire il PayloadGroup
        /// </summary>
        /// <param name="pg">PayloadGroup da inserire</param>
        /// <param name="p">Punto di inserimento</param>
        /// <returns>
        /// Punto di inserimento con la direzione.
        /// Null se non è possibile applicare il payload secondo la Strategy.
        /// </returns>
        public Point2FWithDirection pointWhereCanAddPayloadGroup(PayloadGroup pg, Point2FWithDirection p)
        {
            return pallet.findGoodPayloadGroupToAddStrategy.canAddPayloadGroupToLayer(this, pg, p);
        }

        /// <summary>
        /// Test per controllare se è possibile aggiungere un PayloadGroup ad uno strato
        /// </summary>
        /// <param name="pg">PayloadGroup da voler inserire</param>
        /// <returns>True se è possibile aggiungere il PayloadGroup. False altrimenti.</returns>
        public bool canCointainsPayloadGroup(PayloadGroup pg)
        {
            //dice se non posso aggiungerlo a livello di ingombri (solo analizzando le sagome)
            bool res = true;
            //forma dei payload già piazzati
            Region rA = shapeOfPayloadGroups();
            //forma del payload che si vuole aggiungere
            BoundsF2D bPg = pg.getBounds();
            RectangleF rPg = bPg.toRectF();
            
            rA.Intersect(rPg);
            //il gruppo non interseca con gli altri già piazzati (intersezione nulla)
            res = res && rA.IsEmpty(FrmTmp.Instance.getTempGraphics());
            rA = new Region(rPg);
            
            Region rB = shapeOfPallet();
            rB.Union(rA);
            rB.Exclude(shapeOfPallet());
            //il gruppo è strettamente contenuto nel pallet (unendo le sagome interne del pallet e del payload e riescludendo quella del pallet si deve avere il vuoto)
            res = res && rB.IsEmpty(FrmTmp.Instance.getTempGraphics());

            return (res);
        }

        /// <summary>
        /// PayloadGroup che sono al bordo specificato
        /// </summary>
        /// <param name="angle">
        /// 0 a dx
        /// 90 in alto
        /// 180 a sx
        /// 270 in basso
        /// </param>
        /// <returns>Lista di PayloadGroup che distano in modulo meno di Program.deltaAligment dal bordo specificato</returns>
        public List<PayloadGroup> payloadGroupOnSideOnAngle(double angle)
        {
            List<PayloadGroup> res = new List<PayloadGroup>();
            switch ((int)Math.Abs(angle))
            {
                case 0:
                    foreach (PayloadGroup pg in listPayloadGroupPlaced)
                    {
                        if (Math.Abs(pg.right() - this.pallet.getBundleOfFirstLayerWithBorder().right()) < Program.deltaAligment)
                        {
                            res.Add(pg);
                        }
                    }
                    break;
                case 90:
                    foreach (PayloadGroup pg in listPayloadGroupPlaced)
                    {
                        if (Math.Abs(pg.top() - this.pallet.getBundleOfFirstLayerWithBorder().top()) < Program.deltaAligment)
                        {
                            res.Add(pg);
                        }
                    }
                    break;
                case 180:
                    foreach (PayloadGroup pg in listPayloadGroupPlaced)
                    {
                        if (Math.Abs(pg.left() - this.pallet.getBundleOfFirstLayerWithBorder().left()) < Program.deltaAligment)
                        {
                            res.Add(pg);
                        }
                    }
                    break;
                case 270:
                    foreach (PayloadGroup pg in listPayloadGroupPlaced)
                    {
                        if (Math.Abs(pg.bottom() - this.pallet.getBundleOfFirstLayerWithBorder().bottom()) < Program.deltaAligment)
                        {
                            res.Add(pg);
                        }
                    }
                    break;
            }     
            return res;
        }

        /// <summary>
        /// Forma complessiva dei Payload piazzati in uno strato
        /// </summary>
        /// <returns>La forma complessiva dei Payload</returns>
        public Region shapeOfPayloadGroups(){
            Region rA = new Region(new RectangleF(0, 0, 0, 0));
            foreach (PayloadGroup pgTmp in this.listPayloadGroupPlaced)
            {
                BoundsF2D b = pgTmp.getBounds();
                rA.Union(b.toRectF());
            }
            return rA;
        }
        /// <summary>
        /// Forma del Pallet (X, Y)
        /// </summary>
        /// <returns>Forma del Pallet (X,Y)</returns>
        public Region shapeOfPallet()
        {
            RectangleF rPg = new RectangleF(new PointF(0, 0), this.pallet.getSize().toSize());
            Region rB = new Region(rPg);
            return rB;
        }
        /// <summary>
        /// Clona uno strato
        /// </summary>
        /// <returns>Lo strato clonato</returns>
        public Layer clone()
        {
            Layer l = new Layer(this.pallet);
            l.interLayer = this.interLayer;
            foreach (PayloadGroup pg in this.listPayloadGroupPlaced)
                l.listPayloadGroupPlaced.Add(pg.clone());
            return l;
        }
        /// <summary>
        /// Copia il Payload con lo stesso riferimento per i Payload
        /// </summary>
        /// <returns>Layer copiato</returns>
        public Layer cloneWithSamePayloadGroup()
        {
            Layer l = new Layer(this.pallet);
            l.interLayer = this.interLayer;
            foreach (PayloadGroup pg in this.listPayloadGroupPlaced)
                l.listPayloadGroupPlaced.Add(pg);
            return l;
        }
        /// <summary>
        /// Calcola l'area occupata in percentuale dai PayloadGroup sullo strato
        /// </summary>
        /// <returns>Percentuale dell'area occupata dai PayloadGroup</returns>
        public double occupation()
        {
            float occupiedArea=0;
            foreach (PayloadGroup pgTmp in this.listPayloadGroupPlaced)
            {
                occupiedArea = occupiedArea + (pgTmp.getBounds().size.X * pgTmp.getBounds().size.Y);
            }
            return (occupiedArea / (this.pallet.getSize().X * this.pallet.getSize().Y) * 100);
        }
        /// <summary>
        /// Informazioni sul Pallet
        /// </summary>
        /// <returns>Informazioni sul Pallet</returns>
        public string info()
        {
            return Program.translate("string_strato") + " " + (getLayerNumber() + 1).ToString() + "\n\r " + this.pallet.getSize().toSize().ToString() + "\n\r " + Program.translate("string_con") + " " + this.listPayloadGroupPlaced.Count + " " + Program.translate("string_gruppi") + " \n\r" + Program.translate("string_occupazione") + "= " + this.occupation() + "%";
        }
        /// <summary>
        /// Torna il numero del Layer all'interno del Pallet
        /// </summary>
        /// <returns>Numero del Layer nel Pallet</returns>
        public int getLayerNumber()
        {
            return this.pallet.getPositionOfLayer(this);
        }
        /// <summary>
        /// Torna le informazioni del Pallet
        /// </summary>
        /// <returns>Informazioni del Pallet</returns>
        public override string ToString()
        {
            return info();
        }

        /// <summary>
        /// Ruota i PayloadGroup dello strato
        /// </summary>
        /// <param name="quadrant">Quadrante per rotazione</param>
        /// <returns>Lo strato con i PayloadGroup ruotati (la rotazione è (quadrant*90)°)</returns>
        public Layer rotated(int quadrant)
        {
            //TODO fixed rotazione/ moltiplicava i payload a schermo
            //Layer l = this.clone();
            Layer l = this;
            foreach (PayloadGroup pg in l.listPayloadGroupPlaced)
            {
                double radiantAngle = quadrant * Math.PI / 2.0;
                pg.setQuadrant((pg.getQuadrant() + quadrant + 4) % 4);
                pg.recentering(new Point2F(Util.rotatedOf(new PointF((float)this.size.X / 2.0F, (float)this.size.Y / 2.0F), pg.center.toLocation(), radiantAngle)));
            }
            return l;
        }
        /// <summary>
        /// Ribalta i PayloadGroup dello strato rispetto a Y
        /// </summary>
        /// <returns>Lo strato con i PayloadGroup ribaltati rispetto a Y</returns>
        public Layer horizontalFlipped()
        {
            Layer l = this.clone();
            double centerLine = this.size.Y / 2.0F;
            foreach (PayloadGroup pg in l.listPayloadGroupPlaced)
            {
                pg.horizontalFlip(centerLine);
            }
            return l;
        }
        /// <summary>
        /// Ribalta i PayloadGroup dello strato rispetto a X
        /// </summary>
        /// <returns>Lo strato con i PayloadGroup ribaltati rispetto a Y</returns>
        public Layer verticalFlipped()
        {
            Layer l = this.clone();
            double centerLine = this.size.X / 2.0F;
            foreach (PayloadGroup pg in l.listPayloadGroupPlaced)
            {
                pg.verticalFlip(centerLine);
            }
            return l;
        }

        public void resetApproach()
        {
            foreach (PayloadGroup pp in listPayloadGroupPlaced)
            {
                pp.resetApproaches();

                foreach (PayloadGroup pp2 in listPayloadGroupPlaced)
                    if (!pp2.isTheSame(pp))
                        new Approacher(pp, pp2, pp, this);
                    //{
                    //    Approacher a = (new Approacher(pp, pp2, pp, this));
                    //    if (pp.getApproaches().getApproachDirection().X > 0 && pp.getApproaches().getApproachDirection().Y>0)
                    //    {
                    //        int pippo = 0;
                    //    }
                    //}
            }
        }
    }


}