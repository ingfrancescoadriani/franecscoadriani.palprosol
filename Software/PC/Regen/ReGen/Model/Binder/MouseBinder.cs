using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Sintec.Tool;

namespace ReGen
{
    /// <summary>
    /// Classe che gestisce il bind del mouse
    /// </summary>
    //TODO a che serve?
    public class MouseBinder:Binder
    {

        public MouseBinder(PayloadGroup pg, Point2F mousePositionInMeters)
            : base(pg.moved(mousePositionInMeters), MainForm.MousePosition, pg, null) 
        {}

        public override void bind()
        {}

        public override List<Figure> listFigureAdditional()
        {
            List<Figure> res = new List<Figure>();
            return res;
        }

        public override void changeReferredTo(object referredTo)
        {}
    }
}
