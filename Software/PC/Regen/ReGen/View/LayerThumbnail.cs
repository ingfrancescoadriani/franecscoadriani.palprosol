using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using Sintec.Tool;

namespace ReGen
{
    /// <summary>
    /// Classe che rappresenta il thumbnail del layer
    /// </summary>
    public class LayerThumbnail
    {
        public static int defaultMaxThumbSize = 150;
        private Bitmap bitmap;
        private LayerVisualization associatedLayer = null;
        private static float defaultRatioCompression = 1.0F;
        private float ratioCompression = defaultRatioCompression;
        /// <summary>
        /// Torna il bitmap
        /// </summary>
        /// <returns>Bitmap</returns>
        public Bitmap getBitmap()
        {
            return this.bitmap;
        }
        /// <summary>
        /// Costruttore per la classe LayerThumbnail
        /// </summary>
        /// <param name="b">Bitmap</param>
        /// <param name="associatedLayer">Layer associato al thumbnail</param>
        /// <param name="ratioCompression">Fattore di compressione</param>
        public LayerThumbnail(Bitmap b, LayerVisualization associatedLayer, float ratioCompression)
        {
            this.associatedLayer = associatedLayer;
            this.bitmap = b;
            this.ratioCompression = ratioCompression;
        }
        /// <summary>
        /// Costruttore per la classe LayerThumbnail
        /// </summary>
        /// <param name="b">Bitmap</param>
        /// <param name="associatedLayer">Layer associato al thumbnail</param>
        public LayerThumbnail(Bitmap b, LayerVisualization associatedLayer)
            : this(b, associatedLayer, LayerThumbnail.defaultRatioCompression)
        {}
        /// <summary>
        /// Carica il thumbnail associato al LayerVisualization
        /// </summary>
        /// <param name="s">LayerVisualization di cui si cerca il thumbnail</param>
        /// <returns>LayerThumbnail associato al LayerVisualization</returns>
        public static LayerThumbnail loadThumbnail(LayerVisualization s)
        {
            return loadThumbnail(Program.thumbPath, s.name + ".jpg", s, defaultRatioCompression, defaultMaxThumbSize);
        }

        /// <summary>
        /// Carica il thumbnail dalla directory specificata
        /// </summary>
        /// <param name="customPath">Path della directory del thumnail che si vuole caricare</param>
        /// <param name="filename">Nome del file da caricare</param>
        /// <param name="s">LayerVisualization associato</param>
        /// <param name="ratioCompression">Fattore di compressione</param>
        /// <param name="maxThumbSize">Dimensione massima del thumbnail</param>
        /// <returns>LayerThumbnail associato al file</returns>
        public static LayerThumbnail loadThumbnail(String customPath, String filename, LayerVisualization s, float ratioCompression, int maxThumbSize)
        {
            //cerca il file e se non esiste crea la thumb dalle strutture contenute nello scheme passato
            Bitmap bmp = null;
            //if (File.Exists(Program.thumbPath + "\\" + s.name + ".jpg"))
            //{
            //    bmp = new Bitmap(Program.thumbPath + "\\" + s.name + ".jpg");
            //}
            //else
            {
                bmp = thumbFromSchemeAndSaveIt(customPath,filename, s, ratioCompression, maxThumbSize);
            }
            //carica la thumb associata dal file
            LayerThumbnail bt = new LayerThumbnail(bmp, s);
            return bt;
        }

        /// <summary>
        /// Cancella il file thumbnail associato al layer
        /// </summary>
        /// <param name="s">Layer del quale si vuole cancellare l'immagine a lui associata</param>
        public static void deleteThumbnail(LayerVisualization s)
        {
            //cancella il file associato
            try
            {
                File.Delete(Program.thumbPath + "\\" + s.name + ".jpg");
            }
            catch { }
        }
        /// <summary>
        /// Disegna il thumbnail
        /// </summary>
        /// <param name="gs">Graphics gs</param>
        public void drawThere(Graphics gs)
        {
            if (this.associatedLayer.selected)
                gs.DrawRectangle(new Pen(new SolidBrush(Color.Orange), 3), new Rectangle((int)this.associatedLayer.occupiedSpace.X -2 +3, (int)this.associatedLayer.occupiedSpace.Y -2, (int)this.associatedLayer.occupiedSpace.Height +4 , (int)this.associatedLayer.occupiedSpace.Width +4));
            gs.DrawImage(this.bitmap, this.associatedLayer.occupiedSpace.X + 3, this.associatedLayer.occupiedSpace.Y, this.associatedLayer.occupiedSpace.Height, this.associatedLayer.occupiedSpace.Width);
        }

        //TODO metodo non utilizzato
        public void drawThereWithOccupiedSpace(Graphics gs)
        {
            gs.DrawImage(this.bitmap, this.associatedLayer.occupiedSpace.X, this.associatedLayer.occupiedSpace.Y, this.associatedLayer.occupiedSpace.Width, this.associatedLayer.occupiedSpace.Height);
        }
        //TODO metodo non utilizzato
        public static Bitmap thumbFrom(LayerVisualization s, float ratioCompression)
        {
            return thumbFrom(s, ratioCompression, defaultMaxThumbSize);
        }

        /// <summary>
        /// Disegna il thumbnail del layer associato 
        /// </summary>
        /// <param name="s">LayerVizualization associato</param>
        /// <param name="ratioCompression">Fattore di compressione</param>
        /// <param name="maxThumbSize">Dimensione massima del thumbnail</param>
        /// <returns>Bitmap rappresentante il thumbnail</returns>
        public static Bitmap thumbFrom(LayerVisualization s, float ratioCompression, int maxThumbSize)
        {
            double widthArea = s.layer.getPalletSizeWithExtraBorder().X;
            double heightArea = s.layer.getPalletSizeWithExtraBorder().Y;

            Bitmap objBitmap;
            Graphics gBmp;

            objBitmap = new Bitmap((int)maxThumbSize+4, (int)maxThumbSize+4);
            gBmp = Graphics.FromImage(objBitmap);

            List<PointF> tmpLp = new List<PointF>();
            //disegna un quadrato grande quanto la massima dimensione del thumbnail
            tmpLp.Add(new PointF(0, 0));
            tmpLp.Add(new PointF(maxThumbSize, 0));
            tmpLp.Add(new PointF(maxThumbSize, maxThumbSize));
            tmpLp.Add(new PointF(0, maxThumbSize));
            gBmp.FillPolygon(new SolidBrush(Color.White), tmpLp.ToArray<PointF>());

            double ratio = 1;
            double l = 0; double b = 0;
            tmpLp.Clear();
            if (widthArea > heightArea)
            {
              
                ratio = maxThumbSize / widthArea;
                l = 0;
                b= (maxThumbSize - (int)(ratio * heightArea)) / 2.0F;
                tmpLp.Add(new PointF(0, (maxThumbSize - (int)(ratio * heightArea)) / 2.0F));
                tmpLp.Add(new PointF(maxThumbSize - 1, (maxThumbSize - (int)(ratio * heightArea)) / 2.0F));
                tmpLp.Add(new PointF(maxThumbSize - 1, maxThumbSize - 1 - (maxThumbSize - (int)(ratio * heightArea)) / 2.0F));
                tmpLp.Add(new PointF(0, maxThumbSize - 1 - (maxThumbSize - (int)(ratio * heightArea)) / 2.0F));
            }
            else
            {
                
                ratio = maxThumbSize / heightArea;
                l = (maxThumbSize - (int)(ratio * widthArea)) / 2.0F;
                b = 0;
                tmpLp.Add(new PointF((maxThumbSize - (int)(ratio * widthArea)) / 2.0F, 0));
                tmpLp.Add(new PointF(maxThumbSize - 1 - (maxThumbSize - (int)(ratio * widthArea)) / 2.0F, 0));
                tmpLp.Add(new PointF(maxThumbSize - 1 - (maxThumbSize - (int)(ratio * widthArea)) / 2.0F, maxThumbSize - 1));
                tmpLp.Add(new PointF((maxThumbSize - (int)(ratio * widthArea)) / 2.0F, maxThumbSize - 1));
            }
            //TODO ?
            //disegna il rettangolo bianco con bordo nero rappresentante il pancale 
            gBmp.FillPolygon(new SolidBrush(Color.White), tmpLp.ToArray<PointF>());
            float[] dashValues = { 2, 1, 2 };
            Pen blackPen = new Pen(Color.Black, 2);
            blackPen.DashPattern = dashValues;
            gBmp.DrawPolygon(blackPen, tmpLp.ToArray<PointF>());

            tmpLp.Clear();

            double w = s.layer.getPalletSize().X;
            double h = s.layer.getPalletSize().Y;
            double lp = l + ratio * (widthArea - w) / 2.0;
            double bp = b + ratio * (heightArea - h) / 2.0;
            tmpLp.Add(new Point2F(l + ratio * (widthArea - w) / 2.0, b + ratio * (heightArea - h) / 2.0).toLocation());
            tmpLp.Add(new Point2F(l + ratio * (widthArea - w) / 2.0, b + ratio * (((heightArea - h) / 2.0) + h)).toLocation());
            tmpLp.Add(new Point2F(l + ratio * (((widthArea - w) / 2.0) + w), b + ratio * (((heightArea - h) / 2.0) + h)).toLocation());
            tmpLp.Add(new Point2F(l + ratio * (((widthArea - w) / 2.0) + w), b + ratio * (heightArea - h) / 2.0).toLocation());
            gBmp.FillPolygon(new SolidBrush(Program.colorOfPallet), tmpLp.ToArray<PointF>());
            blackPen = new Pen(Color.Black, 1);
            gBmp.DrawPolygon(blackPen, tmpLp.ToArray<PointF>());

            //disegna il payloadgroup
            foreach (PayloadGroup pg in s.layer.listPayloadGroupPlaced)
            {
                PayloadGroup absPg = pg.placedMe();
                for (int i = 0; i < absPg.countListPayloadPlaced(); i++)
                {
                    PayloadPlaced pp = absPg.getPayloadPlacedAt(i).meWithAbsCenter();
                    tmpLp = pp.getBounds().resized(ratio).traslated(new Point2F(lp, bp)).getPList();
                    gBmp.FillPolygon(new SolidBrush(Program.colorPlacedPayload(pp.getPayloadStrategy().getName())), tmpLp.ToArray<PointF>());
                    gBmp.DrawPolygon(blackPen, tmpLp.ToArray<PointF>());
                }
            }

            objBitmap = new Bitmap(Resize(objBitmap, 1.0F / ratioCompression));
            objBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            gBmp.Dispose();

            return (objBitmap);
        }

        /// <summary>
        /// Salva il thumbnail nella directory predefinita e torna il bitmap associato
        /// </summary>
        /// <param name="filename">Nome del file</param>
        /// <param name="s">LayerVisualization associato</param>
        /// <returns>Salva il thumbnail e torna il bitmap associato</returns>
        private static Bitmap thumbFromSchemeAndSaveIt(String filename, LayerVisualization s)
        {
            return LayerThumbnail.thumbFromSchemeAndSaveIt(Program.thumbPath, filename, s, LayerThumbnail.defaultRatioCompression, defaultMaxThumbSize);
        }
        /// <summary>
        /// Salva il thumbnail nel path specificato e ritorna il bitmap
        /// </summary>
        /// <param name="customPath">Path della directory dove si vuole salvare il file</param>
        /// <param name="filename">Nome del file</param>
        /// <param name="s">LayerVisualization associato</param>
        /// <param name="ratioCompression">Fattore di compressione</param>
        /// <param name="maxThumbSize">Dimensione massima del thumbnail</param>
        /// <returns>Salva il thumbnail e torna il bitmap associato</returns>
        private static Bitmap thumbFromSchemeAndSaveIt(String customPath, String filename, LayerVisualization s, float ratioCompression, int maxThumbSize)
        {
            Bitmap objBitmap = LayerThumbnail.thumbFrom(s, ratioCompression, maxThumbSize);
            objBitmap.Save(customPath + "\\" + filename, System.Drawing.Imaging.ImageFormat.Jpeg);
            return (objBitmap);
        }
        /// <summary>
        /// Ridimensiona l'immagine in base al fattore di scala
        /// </summary>
        /// <param name="img">Immagine da ridimensionare</param>
        /// <param name="percentage">Fattore di scala</param>
        /// <returns>Immagine ridimensionata</returns>
        public static Image Resize(Image img, double percentage)
        {
            //get the height and width of the image
            int originalW = img.Width;
            int originalH = img.Height;

            //get the new size based on the percentage change
            int resizedW = Math.Max(1,(int)(originalW * percentage));
            int resizedH = Math.Max(1,(int)(originalH * percentage));

            //create a new Bitmap the size of the new image
            Bitmap bmp = new Bitmap(resizedW, resizedH);
            //create a new graphic from the Bitmap
            Graphics graphic = Graphics.FromImage((Image)bmp);
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //draw the newly resized image
            graphic.DrawImage(img, 0, 0, resizedW, resizedH);
            //dispose and free up the resources
            graphic.Dispose();
            //return the image
            return (Image)bmp;
        }
    }
}
