using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Sintec.Tool;

namespace ReGen
{
    public class Repellers:Binder
    {
        RectangleF rectReferredTo;
        public Repellers(PayloadGroup payload, RectangleF rectReferredTo, PayloadGroup payloadOriginal)
            : base(payload, rectReferredTo, payloadOriginal, null) 
        {}

        public override void bind()
        {
            Point2F offset = new Point2F(0, 0);
            Point2F posCorrect = new Point2F((float)this.left(),(float)this.bottom());
            posCorrect = new Point2F((float)Math.Max(posCorrect.X, rectReferredTo.Left),
                (float)Math.Max(posCorrect.Y, rectReferredTo.Bottom));
            posCorrect = new Point2F((float)Math.Min(rectReferredTo.Right - this.getBounds().size.X, posCorrect.X),
                (float)Math.Min(rectReferredTo.Top - this.getBounds().size.Y, posCorrect.Y));
            offset = new Point2F(posCorrect.X - (float)this.left(), posCorrect.Y - (float)this.bottom());
            this.move(offset);
        }

        public override List<Figure> listFigureAdditional()
        {
            List<Figure> res = new List<Figure>();
            return res;
        }

        public override void changeReferredTo(object referredTo)
        {
            this.rectReferredTo = (RectangleF)referredTo;
        }
    }
}
