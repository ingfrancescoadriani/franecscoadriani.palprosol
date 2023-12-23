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
    /// Classe che rappresenta la figura del PayloadWithLabel
    /// </summary>
    public class FigurePayloadWithLabel : FigurePayload
    {
        /// <summary>
        /// Costruttore per la classe FigurePayloadWithLabel
        /// </summary>
        /// <param name="pp">PayloadPlaced</param>
        /// <param name="ps">PlacingState</param>
        /// <param name="selected">Selected</param>
        public FigurePayloadWithLabel(PayloadPlaced pp, PlacingState ps, bool selected)
            : base(pp, ps, selected)
        { }
        /// <summary>
        /// Costruttore per la classe FigurePayloadWithLabel
        /// </summary>
        /// <param name="pp">PayloadPlaced</param>
        /// <param name="ps">PlacingState</param>
        /// <param name="offset">Offset</param>
        /// <param name="selected">Selected</param>
        public FigurePayloadWithLabel(PayloadPlaced pp, PlacingState ps, Point offset, bool selected)
            : base(pp, ps, offset, selected)
        { }
        /// <summary>
        /// Disegna il Payload con la label
        /// </summary>
        /// <param name="gs">Graphics</param>
        public override void renderMeThere(Graphics gs)
        {
            base.renderMeThere(gs);

            //stampa il rettangolo del payload
            int lineSize = 4;// MainForm.pointMetersToPixels(new Point2F(pp.getOriginalBounds().size.X - pp.getOriginalSize().X, 0)).X;
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
            
            // stampa della label
            BoxWithLabelStrategy ps = (BoxWithLabelStrategy)pp.getPayloadStrategy();
            int[] lVect = new int[4];
            for (int i = 0; i < 4; i++)
                lVect[i] = 0;
            foreach (LabelOnBox l in ps.labels)
                lVect[(int)Math.Abs((l.sideAngleWhereIs) / 90)] = 1;
            Point p1;
            Point p2;
            if (lVect[pp.quadrant] == 1)
            {
                int x = leftBottomPoint.X + sizeOnPanel.Width - (lineSize / 2);
                p1 = new Point(x, leftBottomPoint.Y - sizeOnPanel.Height);
                p2 = new Point(x, leftBottomPoint.Y);
                Color c = Color.LightSeaGreen;// ps.colorOf(this);
                gs.DrawLine(new Pen(new SolidBrush(c), lineSize), p1, p2);
            }
            if (lVect[(pp.quadrant + 3) % 4] == 1)
            {
                int y = leftBottomPoint.Y - (lineSize / 2) ;
                p1 = new Point(leftBottomPoint.X + sizeOnPanel.Width, y);
                p2 = new Point(leftBottomPoint.X, y);
                Color c = Color.LightSeaGreen;// ps.colorOf(this);
                gs.DrawLine(new Pen(new SolidBrush(c), lineSize), p1, p2);
            }
            if (lVect[(pp.quadrant + 2) % 4] == 1)
            {
                int x = leftBottomPoint.X + (lineSize / 2);
                p1 = new Point(x, leftBottomPoint.Y - sizeOnPanel.Height);
                p2 = new Point(x, leftBottomPoint.Y);
                Color c = Color.LightSeaGreen;// ps.colorOf(this);
                gs.DrawLine(new Pen(new SolidBrush(c), lineSize), p1, p2);
            }
            if (lVect[(pp.quadrant + 1) % 4] == 1)
            {
                int y = leftBottomPoint.Y - sizeOnPanel.Height + (lineSize / 2) ;
                p1 = new Point(leftBottomPoint.X + sizeOnPanel.Width, y);
                p2 = new Point(leftBottomPoint.X, y);
                Color c = Color.LightSeaGreen;// ps.colorOf(this);
                gs.DrawLine(new Pen(new SolidBrush(c), lineSize), p1, p2);
            }

            Rectangle rect = new Rectangle(leftBottomPoint.X, leftBottomPoint.Y - sizeOnPanel.Height, sizeOnPanel.Width, sizeOnPanel.Height);
            //gs.FillRectangle(new SolidBrush(this.ps.colorOf(this)), rect);
            gs.DrawRectangle(new Pen(Color.Black, 1), rect);
        }
    }
}