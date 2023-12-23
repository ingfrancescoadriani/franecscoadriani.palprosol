using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ReGen.View;
using Sintec.Tool;

namespace ReGen
{
    /// <summary>
    /// Rappresenta una figura generica
    /// </summary>
    public abstract class Figure
    {
        private int priority = 0;
        public static int lastId = 0;
        private int id;
        /// <summary>
        /// Costruttore per la classe Figure
        /// </summary>
        /// <param name="priority">Priorità della figura</param>
        public Figure(int priority)
        {
            lastId = (lastId + 1) % int.MaxValue;
            this.id = lastId;
            this.priority = priority;
        }
        /// <summary>
        /// Torna la priority della figura
        /// </summary>
        /// <returns></returns>
        public int getPriority()
        {
            return priority;
        }
        public abstract void renderMeThere(Graphics gs);
        
        /// <summary>
        /// Costruisce la figura di tipo FigureRanieriPayloadGroup
        /// </summary>
        /// <param name="pg">PayloadGroup da disegnare</param>
        /// <param name="ps">PlacingState del PayloadGroup</param>
        /// <param name="offset">Offset</param>
        /// <param name="selected">Booleano di selezione</param>
        /// <returns>Figura di tipo FigureRanieriPayloadGroup</returns>
        public static Figure getFigure(PayloadGroup pg, PlacingState ps, Point offset, bool selected){
            Figure res = new FigurePayloadGroup(pg, ps, offset, selected);
            return res;
        }
        /// <summary>
        /// Costruisce la figura del PayloadPlaced passato come parametro in base alla Strategy da applicare
        /// </summary>
        /// <param name="pp">PayloadPlaced da disegnare</param>
        /// <param name="ps">PlacingState del PayloadPlaced</param>
        /// <param name="offset">Offset</param>
        /// <param name="selected">Booleano di selezione</param>
        /// <returns>Figura corrispondente al PayloadPlaced controllando il tipo della strategy</returns>
        public static Figure getFigure(PayloadPlaced pp, PlacingState ps, Point offset, bool selected)
        {
            Figure res = null;
            if (pp.getPayloadStrategy().GetType() == typeof(BoxStrategy))
                res = new FigurePayload(pp, ps, offset, selected);
            else if (pp.getPayloadStrategy().GetType() == typeof(BoxWithRightSurplusStrategy))
                res = new FigurePayloadWithRightSurplus(pp, ps, offset, selected);
            else if (pp.getPayloadStrategy().GetType() == typeof(BoxWithLabelStrategy))
                res = new FigurePayloadWithLabel(pp, ps, offset, selected);
            return res;
        }
        
        //Point => coordinate a schermo
        //Point2F => coordinate in metri
        //torna il centro in px della figura piazzata in p (metri) con un offset in px
        /// <summary>
        /// Torna il centro in px della figura con un offset
        /// </summary>
        /// <param name="p">Punto in metri</param>
        /// <param name="offset">Offset in px</param>
        /// <returns>Torna il centro in px della figura piazzata in p (metri) con un offset in px</returns>
        public static Point getPointForRender(Point2F p, Point offset)
        {
          
            //offset che viene definito dal trascinamento del Payload al click del mouse
            if (offset.X == 0 && offset.Y == 0)
                offset = MainForm.pointMetersToPixels(new Point2F(MainForm.instance.work.extraPallet.Width / 2, MainForm.instance.work.extraPallet.Height / 2));
            Point2F offsetMeters2 = MainForm.pointPixelsToMeters(offset);
            offsetMeters2 = offsetMeters2.traslatedOf(new Point2F(MainForm.instance.work.extraPallet.Width / 2, MainForm.instance.work.extraPallet.Height / 2));

            Point centerPoint = MainForm.pointMetersToPixels(p.traslatedOf(offsetMeters2));
           
            return centerPoint;
        }
    }

}
