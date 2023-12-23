using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace ReGen
{
    /// <summary>
    /// Classe che rappresenta il thumbnail per il Payload
    /// </summary>
    public class PayloadThumbnail
    {
        public static int maxThumbSize = 118;
        private Bitmap bitmap;
        private PayloadVisualization associatedPayload=null;
        private static float defaultRatioCompression= 1.0F;
        private float ratioCompression = defaultRatioCompression;

        /// <summary>
        /// Torna il bitmap
        /// </summary>
        /// <returns>Torna il bitmap</returns>
        public Bitmap getBitmap()
        {
            return this.bitmap;
        }

        /// <summary>
        /// Costruttore per la classe PayloadThumbnail
        /// </summary>
        /// <param name="b">Bitmap</param>
        /// <param name="associatedPayload">Payload associato</param>
        /// <param name="ratioCompression">Fattore di compressione</param>
        public PayloadThumbnail(Bitmap b,PayloadVisualization associatedPayload, float ratioCompression)
        {
            this.associatedPayload = associatedPayload;
            this.bitmap = b;
            this.ratioCompression = ratioCompression;
        }
        /// <summary>
        /// Costruttore per la classe PayloadThumbnail con fattore di compressione di default
        /// </summary>
        /// <param name="b">Bitmap</param>
        /// <param name="associatedPayload">Payload associato</param>
        public PayloadThumbnail(Bitmap b, PayloadVisualization associatedPayload)
            : this(b, associatedPayload, PayloadThumbnail.defaultRatioCompression)
        {}

        /// <summary>
        /// Carica la thumbnail associata al PayloadVisualization
        /// </summary>
        /// <param name="s">Payload associato</param>
        /// <returns>Thumbnail associato al Payload</returns>
        public static PayloadThumbnail loadThumbnail(PayloadVisualization s)
        {
            //cerca il file e se non esiste crea la thumb dalle strutture contenute nello scheme passato
            Bitmap bmp = null;
            if (File.Exists(Program.thumbPath + "\\" + s.name + ".jpg"))
            {
                bmp = new Bitmap(Program.thumbPath + "\\" + s.name + ".jpg");
            }
            else
            {
                bmp = thumbFromSchemeAndSaveIt(s.name, s);
            }
            //carica la thumb associata dal file
            PayloadThumbnail bt = new PayloadThumbnail(bmp, s);
            return bt;
        }

        /// <summary>
        /// Cancella il file associato al Payload
        /// </summary>
        /// <param name="s">Payload associato</param>
        public static void deleteThumbnail(PayloadVisualization s)
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
            if (this.associatedPayload.selected)
                gs.DrawRectangle(new Pen(new SolidBrush(Color.Orange), 3), new Rectangle((int)this.associatedPayload.occupiedSpace.X-2,(int)this.associatedPayload.occupiedSpace.Y-2,(int)this.associatedPayload.occupiedSpace.Height+4,(int)this.associatedPayload.occupiedSpace.Width+4));
            gs.DrawImage(this.bitmap, this.associatedPayload.occupiedSpace.X, this.associatedPayload.occupiedSpace.Y, this.associatedPayload.occupiedSpace.Height, this.associatedPayload.occupiedSpace.Width);
        }
        //TODO metodo non utilizzato
        public void drawThereWithOccupiedSpace(Graphics gs)
        {
            gs.DrawImage(this.bitmap, this.associatedPayload.occupiedSpace.X, this.associatedPayload.occupiedSpace.Y, this.associatedPayload.occupiedSpace.Width, this.associatedPayload.occupiedSpace.Height);
        }

        /// <summary>
        /// Disegna il thumbnail del Payload associato
        /// </summary>
        /// <param name="s">Payload associato</param>
        /// <param name="ratioCompression">Fattore di comppressione</param>
        /// <returns>Bitmap con il thumbnail disegnato</returns>
        public static Bitmap thumbFrom(PayloadVisualization s, float ratioCompression)
        {
            Bitmap objBitmap;
            Graphics gBmp;

            objBitmap = new Bitmap((int)maxThumbSize, (int)maxThumbSize);
            gBmp = Graphics.FromImage(objBitmap);

            List<PointF> tmpLp = new List<PointF>();

            tmpLp.Add(new PointF(0, 0));
            tmpLp.Add(new PointF(maxThumbSize, 0));
            tmpLp.Add(new PointF(maxThumbSize, maxThumbSize));
            tmpLp.Add(new PointF(0, maxThumbSize));
            gBmp.FillPolygon(new SolidBrush(Color.White), tmpLp.ToArray<PointF>());

            tmpLp.Clear();
            if (s.payload.getOriginalBounds().size.X > s.payload.getOriginalBounds().size.Y)
            {
                double ratio = maxThumbSize / s.payload.getOriginalBounds().size.X;
                tmpLp.Add(new PointF(0, (maxThumbSize - (int)(ratio * s.payload.getOriginalBounds().size.Y)) / 2.0F));
                tmpLp.Add(new PointF(maxThumbSize - 1, (maxThumbSize - (int)(ratio * s.payload.getOriginalBounds().size.Y)) / 2.0F));
                tmpLp.Add(new PointF(maxThumbSize - 1, maxThumbSize - 1 - (maxThumbSize - (int)(ratio * s.payload.getOriginalBounds().size.Y)) / 2.0F));
                tmpLp.Add(new PointF(0, maxThumbSize - 1 - (maxThumbSize - (int)(ratio * s.payload.getOriginalBounds().size.Y)) / 2.0F));
            }
            else
            {
                double ratio = maxThumbSize / s.payload.getOriginalBounds().size.Y;
                tmpLp.Add(new PointF((maxThumbSize - (int)(ratio * s.payload.getOriginalBounds().size.X)) / 2.0F, 0));
                tmpLp.Add(new PointF(maxThumbSize - 1 - (maxThumbSize - (int)(ratio * s.payload.getOriginalBounds().size.X)) / 2.0F, 0));
                tmpLp.Add(new PointF(maxThumbSize - 1 - (maxThumbSize - (int)(ratio * s.payload.getOriginalBounds().size.X)) / 2.0F, maxThumbSize - 1));
                tmpLp.Add(new PointF((maxThumbSize - (int)(ratio * s.payload.getOriginalBounds().size.X)) / 2.0F, maxThumbSize - 1));
            }

            gBmp.FillPolygon(new SolidBrush(Color.White), tmpLp.ToArray<PointF>());
            gBmp.DrawPolygon(new Pen(Color.Black, 1), tmpLp.ToArray<PointF>());

            objBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            objBitmap = new Bitmap(Resize(objBitmap, 1.0F / ratioCompression));
            gBmp.Dispose();
            return (objBitmap);
        }
        /// <summary>
        /// Salva il thumbnail associato al Payload nel path predefinito con il fattore di compressione predefinito e ritorna il bitmap
        /// </summary>
        /// <param name="filename">Nome del file da salvare</param>
        /// <param name="s">Payload associato</param>
        /// <returns>Bitmap del thumbnail</returns>
        private static Bitmap thumbFromSchemeAndSaveIt(String filename, PayloadVisualization s)
        {
            return PayloadThumbnail.thumbFromSchemeAndSaveIt(filename, s, PayloadThumbnail.defaultRatioCompression);
        }
        /// <summary>
        /// Salva il thumbnail associato al Payload nel path predefinito e ritorna il bitmap
        /// </summary>
        /// <param name="filename">Nome del file da salvare</param>
        /// <param name="s">Payload associato</param>
        /// <param name="ratioCompression">Fattore di compressione</param>
        /// <returns>Bitmap del thumbnail</returns>
        private static Bitmap thumbFromSchemeAndSaveIt(String filename, PayloadVisualization s, float ratioCompression)
        {
            Bitmap objBitmap = PayloadThumbnail.thumbFrom(s, ratioCompression);
            objBitmap.Save(Program.thumbPath + "\\" + s.name + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            return (objBitmap);
        }

        /// <summary>
        /// Ridimensiona l'immagine in base al fattore di scala
        /// </summary>
        /// <param name="img">Immagine da scalare</param>
        /// <param name="percentage">Fattore di scala</param>
        /// <returns>Immagina ridimensionata</returns>
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
