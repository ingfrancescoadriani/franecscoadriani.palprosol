using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Sintec.Tool;

namespace ReGen
{
    /// <summary>
    /// Classe astratta per gestire l'allineamento
    /// </summary>
    public abstract class Aligner:Binder
    {
        /// <summary>
        /// Costruttore per la classe Aligner
        /// </summary>
        /// <param name="payloadGroup">PayloadGroup aligner</param>
        /// <param name="rectReferredTo">Oggetto a cui si fa il riferimento</param>
        /// <param name="payloadGroupOriginal">PayloadGroup originale</param>
        public Aligner(PayloadGroup payloadGroup, object rectReferredTo, PayloadGroup payloadGroupOriginal)
            : base(payloadGroup, rectReferredTo, payloadGroupOriginal, null)
        {}
        /// <summary>
        /// Ricava l'offset per la distanza minima tra il payload aligner e un'altro rettangolo
        /// </summary>
        /// <returns>Offset per la distanza minima</returns>
        public abstract Point2F getOffset();
        /// <summary>
        /// Collega il payload aligner all'oggetto vicino
        /// </summary>
        public override void bind()
        {
            Point2F offset = getOffset();
            if (Util.absAmplitude(offset.toLocation())>0)
                this.move(offset);
        }
        /// <summary>
        /// Costruisce una lista di figure vuota
        /// </summary>
        /// <returns>Lista di figure</returns>
        public override List<Figure> listFigureAdditional()
        {
            List<Figure> res = new List<Figure>();
            return res;
        }
    }
    /// <summary>
    /// Classe che rappresenta l'aligner a un rettangolo
    /// </summary>
    public class AlignerOfRectangle : Aligner
    {
        RectangleF rectReferredTo;
        /// <summary>
        /// Costruttore per la classe AlignerOfRectangle
        /// </summary>
        /// <param name="payloadGroup">PayloadGroup aligner</param>
        /// <param name="rectReferredTo">Rettangolo a cui si fa riferimento</param>
        /// <param name="payloadGroupOriginal">PayloadGroup originale</param>
        public AlignerOfRectangle(PayloadGroup payloadGroup, RectangleF rectReferredTo, PayloadGroup payloadGroupOriginal)
            : base(payloadGroup, rectReferredTo, payloadGroupOriginal)
        { }
        /// <summary>
        /// Cambia il rettangolo a cui fa riferimento l'aligner
        /// </summary>
        /// <param name="referredTo">Rettangolo a cui si fa riferimento</param>
        public override void changeReferredTo(object referredTo)
        {
            this.rectReferredTo = (RectangleF)referredTo;
        }
        /// <summary>
        /// Ricava l'offset in base alla distanza minima che c'è tra il payload Aligner e un rettangolo dato
        /// </summary>
        /// <returns>Offset ricavato</returns>
        public override Point2F getOffset()
        {
            Point2F offset = new Point2F(0, 0);
            if (Math.Abs(this.left() - rectReferredTo.Left) < Program.deltaAligment)
            {
                offset.X = -(float)(this.left() - rectReferredTo.Left);
            }
            if (Math.Abs(this.right() - rectReferredTo.Right) < Program.deltaAligment)
            {
                offset.X = -(float)(this.right() - rectReferredTo.Right);
            }
            if (Math.Abs(this.top() - rectReferredTo.Top) < Program.deltaAligment)
            {
                offset.Y = -(float)(this.top() - rectReferredTo.Top);
            }
            if (Math.Abs(this.bottom() - rectReferredTo.Bottom) < Program.deltaAligment)
            {
                offset.Y = -(float)(this.bottom() - rectReferredTo.Bottom);
            }

            if (Math.Abs(this.left() - rectReferredTo.Right) < Program.deltaAligment)
            {
                offset.X = -(float)(this.left() - rectReferredTo.Right);
            }
            if (Math.Abs(this.right() - rectReferredTo.Left) < Program.deltaAligment)
            {
                offset.X = -(float)(this.right() - rectReferredTo.Left);
            }
            if (Math.Abs(this.bottom() - rectReferredTo.Top) < Program.deltaAligment)
            {
                offset.Y = -(float)(this.bottom() - rectReferredTo.Top);
            }
            if (Math.Abs(this.top() - rectReferredTo.Bottom) < Program.deltaAligment)
            {
                offset.Y = -(float)(this.top() - rectReferredTo.Bottom);
            }
            return offset;
        }
    }
    /// <summary>
    /// Classe che rappresenta l'aligner al PayloadGroup
    /// </summary>
    public class AlignerOfPayloadGroup: Aligner
    {
        PayloadGroup payloadPlacedReferredTo;
        /// <summary>
        /// Costruttore della classe AlignerOfPayloadGroup
        /// </summary>
        /// <param name="payloadGroup">PayloadGroup aligner</param>
        /// <param name="rectReferredTo">PayloadGroup a cui si fa riferimento</param>
        /// <param name="payloadGroupOriginal">PayloadGroup originale</param>
        public AlignerOfPayloadGroup(PayloadGroup payloadGroup, PayloadGroup rectReferredTo, PayloadGroup payloadGroupOriginal)
            : base(payloadGroup, rectReferredTo, payloadGroupOriginal)
        {}
        /// <summary>
        /// Cambia il riferimento dell'aligner
        /// </summary>
        /// <param name="referredTo">Nuovo PayloadGroup di riferimento</param>
        public override void changeReferredTo(object referredTo)
        {
            this.payloadPlacedReferredTo = (PayloadGroup)referredTo;
        }
        /// <summary>
        /// Ricava l'offset in base alla distanza minima che c'è tra il payload Aligner e un PayloadGroup dato
        /// </summary>
        /// <returns>Offset di distanza minima</returns>
        public override Point2F getOffset()
        {
            Point2F offset = new Point2F(0, 0);
            if (Math.Abs(this.left() - payloadPlacedReferredTo.left(new Point2F(0, 0))) < Program.deltaAligment)
            {
                offset.X = -(float)(this.left() - payloadPlacedReferredTo.left(new Point2F(0, 0)));
            }
            if (Math.Abs(this.right() - payloadPlacedReferredTo.right(new Point2F(0, 0))) < Program.deltaAligment)
            {
                offset.X = -(float)(this.right() - payloadPlacedReferredTo.right(new Point2F(0, 0)));
            }
            if (Math.Abs(this.top() - payloadPlacedReferredTo.top(new Point2F(0, 0))) < Program.deltaAligment)
            {
                offset.Y = -(float)(this.top() - payloadPlacedReferredTo.top(new Point2F(0, 0)));
            }
            if (Math.Abs(this.bottom() - payloadPlacedReferredTo.bottom(new Point2F(0, 0))) < Program.deltaAligment)
            {
                offset.Y = -(float)(this.bottom() - payloadPlacedReferredTo.bottom(new Point2F(0, 0)));
            }

            if (Math.Abs(this.left() - payloadPlacedReferredTo.right(new Point2F(0, 0))) < Program.deltaAligment)
            {
                offset.X = -(float)(this.left() - payloadPlacedReferredTo.right(new Point2F(0, 0)));
            }
            if (Math.Abs(this.right() - payloadPlacedReferredTo.left(new Point2F(0, 0))) < Program.deltaAligment)
            {
                offset.X = -(float)(this.right() - payloadPlacedReferredTo.left(new Point2F(0, 0)));
            }
            if (Math.Abs(this.bottom() - payloadPlacedReferredTo.top(new Point2F(0, 0))) < Program.deltaAligment)
            {
                offset.Y = -(float)(this.bottom() - payloadPlacedReferredTo.top(new Point2F(0, 0)));
            }
            if (Math.Abs(this.top() - payloadPlacedReferredTo.bottom(new Point2F(0, 0))) < Program.deltaAligment)
            {
                offset.Y = -(float)(this.top() - payloadPlacedReferredTo.bottom(new Point2F(0, 0)));
            }
            return offset;
        }
    }
}
