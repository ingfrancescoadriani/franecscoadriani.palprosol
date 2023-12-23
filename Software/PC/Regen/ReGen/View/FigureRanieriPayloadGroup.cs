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
    /// Classe che rappresenta le figure del PayloadGroup e della pinza 
    /// </summary>
    public class FigureRanieriPayloadGroup:FigurePayloadGroup
    {
        /// <summary>
        /// Costruttore per la classe FigureRanieriPayloadGroup
        /// </summary>
        /// <param name="pp">PayloadGroup</param>
        /// <param name="ps">PlacingState</param>
        /// <param name="offset">Offset</param>
        /// <param name="selected">Selected</param>
        public FigureRanieriPayloadGroup(PayloadGroup pp, PlacingState ps, Point offset, bool selected)
            : base(pp, ps, offset, selected) { }

        /// <summary>
        /// Disegna il PayloadGroup e la pinza
        /// </summary>
        /// <param name="gs">Graphics</param>
        public override void renderMeThere(Graphics gs)
        {
            base.renderMeThere(gs);
            //TODO fixed
            /*if (ps.GetType() == typeof(PlacingState_onPlatform))
            {

                int lineSize = ((int)Math.Round(Program.widthFinger * MainForm.getRatioPixel_MetersPerZoomLevel()));
                int ppQuadrant = this.pg.getPayloadPlacedAt(0).quadrant;
                Point leftBottomPoint = Figure.getPointForRender(this.pg.getBounds().location, new Point(0, 0));

                Size sizeOnPanel = new System.Drawing.Size((int)Math.Round(this.pg.getBounds().size.X * MainForm.getRatioPixel_MetersPerZoomLevel()), (int)Math.Round(this.pg.getBounds().size.Y * MainForm.getRatioPixel_MetersPerZoomLevel()));
                   
                int widthF = (int)Math.Round(Program.widthFinger * MainForm.getRatioPixel_MetersPerZoomLevel());
                int heigthF = (int)Math.Round(Program.heigthFinger * MainForm.getRatioPixel_MetersPerZoomLevel());
                int startSeriesF = (int)Math.Round(Program.startSeriesFinger * MainForm.getRatioPixel_MetersPerZoomLevel());
                int numberF = Program.numberFinger;
                int distanceBetweenF = (int)Math.Round(Program.distanceBetweenFingers * MainForm.getRatioPixel_MetersPerZoomLevel());

                int x = 0;
                int y = 0;
                Point p1 = new Point(0, 0);
                Point p2 = new Point(0, 0);
                List<List<Point>> listPForFingers = new List<List<Point>>();

                if (ppQuadrant == 2)
                {
                    x = leftBottomPoint.X + sizeOnPanel.Width;
                    y = leftBottomPoint.Y - (lineSize / 2) - startSeriesF ;
                    List<Point> tmpL;
                    for (int i = 0; i < numberF; i++)
                    {
                        tmpL = new List<Point>();
                        tmpL.Add(new Point(x, y - (i * (distanceBetweenF + widthF))));
                        tmpL.Add(new Point(x + heigthF, y - (i * (distanceBetweenF + widthF))));
                        listPForFingers.Add(tmpL);
                    }
                }
                if (ppQuadrant == 1)
                {
                    y = leftBottomPoint.Y;
                    x = leftBottomPoint.X + (lineSize / 2) + startSeriesF;
                    List<Point> tmpL;
                    for (int i = 0; i < numberF; i++)
                    {
                        tmpL = new List<Point>();
                        tmpL.Add(new Point(x + (i * (distanceBetweenF + widthF)), y));
                        tmpL.Add(new Point(x + (i * (distanceBetweenF + widthF)), y + heigthF));
                        listPForFingers.Add(tmpL);
                    }
                }
                else if (ppQuadrant == 0)
                {
                    x = leftBottomPoint.X;
                    y = leftBottomPoint.Y + (lineSize / 2) + startSeriesF - sizeOnPanel.Height;
                    List<Point> tmpL;
                    for (int i = 0; i < numberF; i++)
                    {
                        tmpL = new List<Point>();
                        tmpL.Add(new Point(x, y + (i * (distanceBetweenF + widthF))));
                        tmpL.Add(new Point(x - heigthF, y + (i * (distanceBetweenF + widthF))));
                        listPForFingers.Add(tmpL);
                    }
                }
                else if (ppQuadrant == 3)
                {
                    y = leftBottomPoint.Y - sizeOnPanel.Height ;
                    x = leftBottomPoint.X + sizeOnPanel.Width - (lineSize / 2) - startSeriesF;
                    List<Point> tmpL;
                    for (int i = 0; i < numberF; i++)
                    {
                        tmpL = new List<Point>();
                        tmpL.Add(new Point(x - (i * (distanceBetweenF + widthF)), y));
                        tmpL.Add(new Point(x - (i * (distanceBetweenF + widthF)), y - heigthF));
                        listPForFingers.Add(tmpL);
                    }
                }
                
                Color c = ps.colorOf(this);
                foreach (List<Point> tmpL in listPForFingers)
                    gs.DrawLine(new Pen(new SolidBrush(c), lineSize), tmpL[0], tmpL[1]);
              
            }
            */
        }
    }
}
