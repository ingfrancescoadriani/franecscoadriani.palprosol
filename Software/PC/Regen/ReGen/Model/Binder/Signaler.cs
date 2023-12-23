    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing;
    using Sintec.Tool;

namespace ReGen
{
    public class Signaler : Binder
    {
        List<Figure> additionalFigure = new List<Figure>();
        PayloadGroup payloadPlacedReferredTo;
        public Signaler(PayloadGroup payload, PayloadGroup payloadPlacedReferredTo, PayloadGroup payloadOriginal)
            : base(payload, payloadPlacedReferredTo, payloadOriginal, null)
        { }

        public override void bind()
        {
            if (this.collides(payloadPlacedReferredTo))
            
            {
                addProblem(new Collision(payloadPlacedReferredTo));
            }
        }
        
        public override List<Figure> listFigureAdditional()
        {
            return additionalFigure;
        }

        public override void changeReferredTo(object referredTo)
        {
            this.payloadPlacedReferredTo = (PayloadGroup)referredTo;
        }
    }
}
