using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Sintec.Tool;
using ReGen.View;

namespace ReGen
{
    /// <summary>
    /// Classe che rappresenta i Payload con un surplus sul lato destro
    /// </summary>
    public class FigurePayloadWithRightSurplus : FigurePayload
    {
        /// <summary>
        /// Costruttore per la classe FigurePayloadWithRightSurplus
        /// </summary>
        /// <param name="pp">PayloadPlaced</param>
        /// <param name="ps">Placingstate</param>
        /// <param name="selected">Selected</param>
        public FigurePayloadWithRightSurplus(PayloadPlaced pp, PlacingState ps, bool selected)
            : base(pp, ps, selected)
        { }
        /// <summary>
        /// Costruttore per la classe FigurePayloadWithRightSurplus
        /// </summary>
        /// <param name="pp">PayloadPlaced</param>
        /// <param name="ps">PlacingState</param>
        /// <param name="offset">Offset</param>
        /// <param name="selected">Selected</param>
        public FigurePayloadWithRightSurplus(PayloadPlaced pp, PlacingState ps, Point offset, bool selected)
            : base(pp, ps, offset, selected)
        { }
        /// <summary>
        /// Disegna il PayloadWithRightSurplus
        /// </summary>
        /// <param name="gs">Graphics gs</param>
        public override void renderMeThere(Graphics gs)
        {
            base.renderMeThere(gs);

            //stampa il rettangolo del payload
            int lineSize = MainForm.pointMetersToPixels(new Point2F(pp.getOriginalBounds().size.X - pp.getOriginalSize().X,0)).X;
            Point2F posMeters = pp.getRelCenter();
            SizeF size = pp.getBounds().size.toSize();
            
            if (offset.X == 0 && offset.Y == 0)
                offset = MainForm.pointMetersToPixels(new Point2F(MainForm.instance.work.extraPallet.Width / 2, MainForm.instance.work.extraPallet.Height / 2));
            Point2F offsetMeters = MainForm.pointPixelsToMeters(offset);
            offsetMeters = offsetMeters.traslatedOf(new Point2F(MainForm.instance.work.extraPallet.Width / 2, MainForm.instance.work.extraPallet.Height / 2));
            Point2F basePointMeters = posMeters.traslatedOf(offsetMeters);
           
            Point basePoint = MainForm.pointMetersToPixels(basePointMeters);

            Point leftBottomPoint = MainForm.pointMetersToPixels(pp.getBounds().location.traslatedOf(offsetMeters));

            Size sizeOnPanel = new System.Drawing.Size((int)Math.Round(size.Width * MainForm.getRatioPixel_MetersPerZoomLevel()), (int)Math.Round(size.Height * MainForm.getRatioPixel_MetersPerZoomLevel()));
            int x = leftBottomPoint.X + sizeOnPanel.Width - (lineSize / 2) ;
            Point p1 = new Point(x, leftBottomPoint.Y - sizeOnPanel.Height);
            Point p2 = new Point(x, leftBottomPoint.Y);
            if (pp.quadrant == 3)
            {
                int y = leftBottomPoint.Y - (lineSize / 2) ;
                p1 = new Point(leftBottomPoint.X + sizeOnPanel.Width, y);
                p2 = new Point(leftBottomPoint.X, y);
            }
            else if (pp.quadrant == 2)
            {
                x = leftBottomPoint.X + (lineSize / 2) ;
                p1 = new Point(x, leftBottomPoint.Y - sizeOnPanel.Height);
                p2 = new Point(x, leftBottomPoint.Y);
            }
            else if (pp.quadrant == 1)
            {
                int y = leftBottomPoint.Y - sizeOnPanel.Height + (lineSize / 2) ;
                p1 = new Point(leftBottomPoint.X + sizeOnPanel.Width, y);
                p2 = new Point(leftBottomPoint.X, y);
            }

            Color c = ps.colorOf(this);
            gs.DrawLine(new Pen(new SolidBrush(c), lineSize), p1, p2);
        }
    }
}