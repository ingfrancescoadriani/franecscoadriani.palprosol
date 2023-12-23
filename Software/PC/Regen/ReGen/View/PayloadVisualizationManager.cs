using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ReGen
{
    public static class PayloadVisualizationManager
    {
        public static float betweenSpace = 16.0F;
        public static List<PayloadVisualization> listPayloadVisualization = new List<PayloadVisualization>();
        /// <summary>
        /// Refresh della visualizzazione dei Payload
        /// </summary>
        /// <param name="gs">Graphics gs</param>
        /// <param name="listPayload">Lista di Payload</param>
        public static void refreshSchemeList(Graphics gs, List<Payload> listPayload)
        {
            listPayloadVisualization.Clear();
            //panelWhereDraw.Height = 6 + (listPayload.Count * (int)(PayloadThumbnail.maxThumbSize + PayloadVisualizationManager.betweenSpace));
            //panelWhereDraw.Width = 6 + (int)(PayloadThumbnail.maxThumbSize + PayloadVisualizationManager.betweenSpace);
            //per ogni file nella cartella ordinati per nome
            int i = 0;
            foreach (Payload p in listPayload)
            {
                PayloadVisualization pv = new PayloadVisualization(p, i++);
                //crea una nuova casellina nella tabella dovuta e aggiungi un panel
                //disegna sul panel la miniatura
                listPayloadVisualization.Add(pv);
            }
            if (gs!=null) refreshVisualization(gs);
        }
        


        /// <summary>
        /// Refresha la visualizzazione dei Payload
        /// </summary>
        /// <param name="gs">Graphics gs</param>
        public static void refreshVisualization(Graphics gs)
        {
            gs.Clear(Color.White);
            foreach (PayloadVisualization s in listPayloadVisualization)
            {
                s.thumb.drawThere(gs);
                gs.DrawString(s.name, new Font("Tahoma", 8), new SolidBrush(Color.Black), new Point((int)s.occupiedSpace.Left + 3, (int)s.occupiedSpace.Bottom + 5));
                
            }
        }
        /// <summary>
        /// Seleziona il PayloadVisualization che contiene il punto
        /// </summary>
        /// <param name="point">Punto</param>
        /// <returns>PayloadVisualization che contiene il punto</returns>
        public static PayloadVisualization selected(PointF point)
        {
            PayloadVisualization found = null;
            foreach (PayloadVisualization s in listPayloadVisualization)
            {
                if (s.occupiedSpace.Contains(point))
                {
                    found = s;
                    s.selected = true;
                }
                else
                    s.selected = false;
            }
            return found;
        }
    }
}
