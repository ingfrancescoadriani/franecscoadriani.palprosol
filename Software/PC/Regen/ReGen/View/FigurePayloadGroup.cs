using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Sintec.Tool;
using ReGen.View;
using ReGen.Model;
using System.Drawing.Drawing2D;

namespace ReGen
{
    /// <summary>
    /// Classe che rappresenta la figura del PayloadGroup
    /// </summary>
    public class FigurePayloadGroup : Figure
    {
        Point offset;
        public PayloadGroup pg;
        protected PlacingState ps;
        public bool selected;
        /// <summary>
        /// Costruttore per la classe FigurePayloadGroup
        /// </summary>
        /// <param name="pp">PayloadGroup</param>
        /// <param name="ps">PlacingState</param>
        /// <param name="selected">Selected</param>
        public FigurePayloadGroup(PayloadGroup pp, PlacingState ps, bool selected)
            : this(pp, ps, new Point(0, 0), selected)
        { }
        /// <summary>
        /// Costruttore per la classe FigurePayloadGroup
        /// </summary>
        /// <param name="pp">PayloadPlaced</param>
        /// <param name="ps">PlacingState</param>
        /// <param name="selected">Selected</param>
        public FigurePayloadGroup(PayloadPlaced pp, PlacingState ps, bool selected)
            : this(pp, ps, new Point(0, 0), selected)
        { }
        /// <summary>
        /// Costruttore per la classe FigurePayloadGroup
        /// </summary>
        /// <param name="pp">PayloadPlaced</param>
        /// <param name="ps">PlacingState</param>
        /// <param name="offset">Offset</param>
        /// <param name="selected">Selected</param>
        public FigurePayloadGroup(PayloadPlaced pp, PlacingState ps, Point offset, bool selected)
            : base(3)
        {
            this.offset = offset;
            this.pg = new PayloadGroup(pp);
            this.ps = ps;
            this.selected = selected;
        }
        /// <summary>
        /// Costruttore per la classe FigurePayloadGroup
        /// </summary>
        /// <param name="pp">PayloadGroup</param>
        /// <param name="ps">PlacingState</param>
        /// <param name="offset">Offset</param>
        /// <param name="selected">Selected</param>
        public FigurePayloadGroup(PayloadGroup pp, PlacingState ps, Point offset, bool selected)
            : base(3)
        {
            this.offset = offset;
            this.pg = pp;
            this.ps = ps;
            this.selected = selected;
        }
        /// <summary>
        /// Disegna il PayloadGroup
        /// </summary>
        /// <param name="gs">Graphics</param>
        public override void renderMeThere(Graphics gs)
        {
            int distSegment = 22;
            distSegment = (int)(distSegment * MainForm.getRatioPixel_MetersPerZoomLevel());
            int rawCircle = 5;
            rawCircle = (int)(rawCircle * MainForm.getRatioPixel_MetersPerZoomLevel());

            gs.DrawRectangle(Pens.Olive, pg.getBounds().toRect());

            //disegna le ancore ai lati tangenti dei propri PayloadPLaced
            Color c = ps.colorOf(this);
            //cerchi e linee che collegano i payload raggruppati
            foreach (Grip g in this.pg.getListGrip())
            {
                Point posPixel = offset;
                if (offset.X == 0 && offset.Y == 0)
                    offset = MainForm.pointMetersToPixels(new Point2F(MainForm.instance.work.extraPallet.Width / 2, MainForm.instance.work.extraPallet.Height / 2));
                Point2F offsetMeters = MainForm.pointPixelsToMeters(offset);
                offsetMeters = offsetMeters.traslatedOf(new Point2F(MainForm.instance.work.extraPallet.Width / 4, MainForm.instance.work.extraPallet.Height / 4));

                Point2F basePointMeters = g.getStartPoint(distSegment).traslatedOf(offsetMeters);
                Point pa = MainForm.pointMetersToPixels(basePointMeters.traslatedOf(offsetMeters));

                basePointMeters = g.getEndPoint(distSegment).traslatedOf(offsetMeters);
                Point pb = MainForm.pointMetersToPixels(basePointMeters.traslatedOf(offsetMeters));

                gs.DrawLine(new Pen(new SolidBrush(c), 2), pa, pb);
                Rectangle r = new Rectangle(new Point2F(pa).traslatedOf(new Point2F(-rawCircle, -rawCircle)).toIntLocation(), new Size(2 * rawCircle, 2 * rawCircle));
                if (r.Width * r.Height > 0)
                    gs.DrawArc(new Pen(new SolidBrush(c), 2), r, 0.0F, (float)(360.0F));
                r = new Rectangle(new Point2F(pb).traslatedOf(new Point2F(-rawCircle, -rawCircle)).toIntLocation(), new Size(2 * rawCircle, 2 * rawCircle));
                if (r.Width * r.Height > 0)
                    gs.DrawArc(new Pen(new SolidBrush(c), 2), r, 0.0F, (float)(360.0F));
            }

            Point centerPoint2 = Figure.getPointForRender(pg.center, offset);

            String tmp2 = Sequencer.getProgressive(pg.getId(), MainForm.instance.work.getLayerUsed()).ToString();
            if (tmp2 == "0")
            {
                try
                {
                    tmp2 = Sequencer.getRankOfPayloadGroup(pg.getId()).ToString();
                    if (tmp2 == "0")
                        tmp2 = "";
                }
                catch
                {
                    //tmp2 = "";
                }
            }
            Font f2 = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
            gs.DrawString(tmp2, f2, Brushes.White, centerPoint2.X - (int)Math.Round(gs.MeasureString(tmp2, f2).Width / 2.0) - 20, (float)(centerPoint2.Y - gs.MeasureString("A", f2).Height / 2.0));

            Point centerPoint = Figure.getPointForRender(pg.center, offset);
            double rawCenter = (5.0 * MainForm.getRatioPixel_MetersPerZoomLevel());

            Rectangle rect = new Rectangle((int)Math.Round(centerPoint.X - (rawCenter)), (int)Math.Round(centerPoint.Y - (rawCenter)), (int)rawCenter * 2, (int)rawCenter * 2);
            //gs.DrawArc(new Pen(Color.White, 2), rect, 0, 360);

            gs.DrawRectangle(new Pen(Color.White, 2), rect);

            Pen p = new Pen(Program.colorOfArrow, 3);
            p.StartCap = LineCap.Round;
            p.EndCap = LineCap.ArrowAnchor;
            Point p0 = centerPoint;
            Point p1 = Figure.getPointForRender(pg.center.traslatedOf(pg.getApproachDirection()), offset);
            gs.DrawLine(p, p0, p1);
            p.Dispose();


            if (Program.infoRequesting)
            {
                Font f = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
                gs.DrawString(pg.getRank(MainForm.instance.work.palletOnSystem).ToString("0.00") + Sequencer.getProgressive(pg.getId(), MainForm.instance.work.getLayerUsed()).ToString(), f, Brushes.White, centerPoint.X + 5, centerPoint.Y - (int)Math.Round(gs.MeasureString("A", f).Height * 0.5));
                gs.DrawString(pg.center.ToString(), f, Brushes.Black, centerPoint.X - (int)Math.Round(gs.MeasureString(pg.center.ToString(), f).Width / 2.0), centerPoint.Y + (int)Math.Round(gs.MeasureString("A", f).Height * 0.5));
                gs.DrawString("[q=" + pg.getQuadrant() + "]", f, Brushes.Black, centerPoint.X - (int)Math.Round(gs.MeasureString("[q=" + pg.getQuadrant() + "]", f).Width / 2.0), centerPoint.Y + (int)Math.Round(gs.MeasureString("A", f).Height * 3.5));
                gs.DrawString(pg.getBounds().ToString().Replace(" - ", "\r\n"), f, Brushes.Black, centerPoint.X - (int)Math.Round(gs.MeasureString(pg.getBounds().ToString().Replace(" - ", "\r\n"), f).Width / 2.0), centerPoint.Y + (int)Math.Round(gs.MeasureString("A", f).Height * 1.5));

                if (Program.debugState)
                {
                    int i = 1;
                    while (pg.queueStringForDebug.Count > 0)
                    {
                        String s = pg.queueStringForDebug.Dequeue();
                        gs.DrawString(s, f, Brushes.Black, centerPoint.X - (int)Math.Round(gs.MeasureString(s, f).Width / 2.0), centerPoint.Y + (int)Math.Round(gs.MeasureString("A", f).Height * (i + 1.5)));
                        i++;
                    }
                }
            }
        }
    }
}
