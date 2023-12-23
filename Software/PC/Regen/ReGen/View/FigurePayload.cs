using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Sintec.Tool;
using ReGen.View;
using System.Drawing.Drawing2D;
using ReGen.Model;

namespace ReGen
{
    /// <summary>
    /// Classe che rappresenta la figura del Payload
    /// </summary>
    public class FigurePayload:Figure
    {
        protected Point offset;
        /// <summary>
        /// PayloadPlaced
        /// </summary>
        public PayloadPlaced pp;
        protected PlacingState ps;
        public bool selected;
        /// <summary>
        /// Costruttore per FigurePayload
        /// </summary>
        /// <param name="pp">PayloadPlaced</param>
        /// <param name="ps">PlacingState</param>
        /// <param name="selected">Selected</param>
        public FigurePayload(PayloadPlaced pp, PlacingState ps, bool selected)
            : this(pp, ps, new Point(0, 0), selected)
        { }
        /// <summary>
        /// Costruttore per FigurePayload
        /// </summary>
        /// <param name="pp">PayloadPlaced</param>
        /// <param name="ps">PlacingState</param>
        /// <param name="selected">Selected</param>
        public FigurePayload(PayloadPlaced pp, PlacingState ps, Point offset, bool selected)
            : base(1)
        {
            this.offset = offset;
            this.pp = pp;
            this.ps = ps;
            this.selected = selected;
        }
        /// <summary>
        /// Disegna il PayloadPlaced
        /// </summary>
        /// <param name="gs">Graphics gs</param>
        public override void renderMeThere(Graphics gs)
        {
            //Centro del PayloadPlaced
            Point2F posMeters = pp.getRelCenter();
            //Dimensione del PayloadPlaced
            SizeF size = pp.getBounds().size.toSize();
            
            if (offset.X == 0 && offset.Y == 0)
                offset = MainForm.pointMetersToPixels(new Point2F(MainForm.instance.work.extraPallet.Width / 2, MainForm.instance.work.extraPallet.Height / 2));
            Point2F offsetMeters = MainForm.pointPixelsToMeters(offset);
            offsetMeters = offsetMeters.traslatedOf(new Point2F(MainForm.instance.work.extraPallet.Width / 2, MainForm.instance.work.extraPallet.Height / 2));
            
            Point2F basePointMeters = posMeters.traslatedOf(offsetMeters);
            Point basePoint = MainForm.pointMetersToPixels(basePointMeters);

            Point leftBottomPoint = MainForm.pointMetersToPixels(pp.getBounds().location.traslatedOf(offsetMeters));

            Size sizeOnPanel = new System.Drawing.Size((int)Math.Round(size.Width * MainForm.getRatioPixel_MetersPerZoomLevel()), (int)Math.Round(size.Height * MainForm.getRatioPixel_MetersPerZoomLevel()));
            Rectangle rect = new Rectangle(leftBottomPoint.X, leftBottomPoint.Y - sizeOnPanel.Height, sizeOnPanel.Width, sizeOnPanel.Height);
            Color c = ps.colorOf(this);
            
            Rectangle rectOfBorder = rect;

            gs.FillRectangle(new SolidBrush(c), rect);
            gs.DrawRectangle(new Pen(Color.Black, 1), rect);
            //cerchio che indica il centro dei payload
            double rawCenter = (2.0 * MainForm.getRatioPixel_MetersPerZoomLevel());
            rect = new Rectangle((int)Math.Round(basePoint.X - (rawCenter)), (int)Math.Round(basePoint.Y - (rawCenter)), (int)rawCenter*2, (int)rawCenter*2);
            if (rect.Height*rect.Width>0)
                gs.DrawArc(new Pen(Color.Black, 2), rect, 0, 360);
            
            Pen p = new Pen(Color.Black, 1);
            p.StartCap = LineCap.Round;
            p.EndCap = LineCap.ArrowAnchor;
            //disegno degli assi sul payload
            Point p0 = Figure.getPointForRender(pp.getOriginalBounds().location.traslatedOf(new Point2F(10, 10)).rotated((float)(pp.quadrant * Math.PI / 2.0)).traslatedOf(pp.getRelCenter()), offset);
            Point p1 = Figure.getPointForRender(pp.getOriginalBounds().location.traslatedOf(new Point2F(10, 50)).rotated((float)(pp.quadrant * Math.PI / 2.0)).traslatedOf(pp.getRelCenter()), offset);
            Point p2 = Figure.getPointForRender(pp.getOriginalBounds().location.traslatedOf(new Point2F(50, 10)).rotated((float)(pp.quadrant * Math.PI / 2.0)).traslatedOf(pp.getRelCenter()), offset);
            gs.DrawLine(p,p0,p1);
            gs.DrawLine(p,p0,p2);
            p.Dispose();

            Font fff = new Font(FontFamily.GenericSansSerif, 8);
            gs.DrawString(this.pp.getPayloadStrategy().getName(), fff, Brushes.Black, rectOfBorder.Left + 5, rectOfBorder.Bottom - gs.MeasureString("A", fff).Height - 5);

            Point centerPoint = Figure.getPointForRender(pp.getRelCenter(), offset);
            //scrive informazioni sopra al payload
            if (Program.infoRequesting)
            {
                String tmp = pp.ToString().Replace(" - ", "\r\n");
                Font f = new Font(FontFamily.GenericSansSerif, 10);
                gs.DrawString(tmp, f, Brushes.Black, centerPoint.X - (int)Math.Round(gs.MeasureString(tmp, f).Width / 2.0), centerPoint.Y - (int)Math.Round(gs.MeasureString("A", f).Height * 3));
                tmp = "[q=" + pp.quadrant.ToString() + "]";
                gs.DrawString(tmp, f, Brushes.Black, centerPoint.X - (int)Math.Round(gs.MeasureString(tmp, f).Width / 2.0), centerPoint.Y - (int)Math.Round(gs.MeasureString("A", f).Height * 4));
            }
        }
    }
}
