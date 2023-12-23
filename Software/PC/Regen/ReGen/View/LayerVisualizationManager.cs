using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ReGen
{
    /// <summary>
    /// Classe che gestisce i LayerVisualization
    /// </summary>
    public static class LayerVisualizationManager
    {
        public static float betweenSpace = 16.0F;
        public static List<LayerVisualization> listLayerVisualization = new List<LayerVisualization>();
        //TODO da rivedere
        /// <summary>
        /// Refresh della lista degli schemi
        /// </summary>
        /// <param name="panel">Panel panel</param>
        /// <param name="pos">PalletOnSystem pos</param>
        public static void refreshSchemeList(Panel panel, PalletOnSystem pos)
        {
            int lastSelected = -1;
            foreach (LayerVisualization lv in LayerVisualizationManager.listLayerVisualization)
                if (lv.selected)
                    lastSelected = lv.layer.getLayerNumber();
            listLayerVisualization.Clear();
            //panelWhereDraw.Height = 6 + (listPayload.Count * (int)(PayloadThumbnail.maxThumbSize + PayloadVisualizationManager.betweenSpace));
            //panelWhereDraw.Width = 6 + (int)(PayloadThumbnail.maxThumbSize + PayloadVisualizationManager.betweenSpace);
            //per ogni file nella cartella ordinati per nome
            for (int i = 0; i < pos.getLayerCount(); i++)
            {
                Layer p = pos.getLayerAtIndex(i);
                LayerVisualization pv = new LayerVisualization(p, pos.getLayerCount() - i - 1);
                //crea una nuova casellina nella tabella dovuta e aggiungi un panel
                //disegna sul panel la miniatura
                pv.selected = (i == lastSelected);
                listLayerVisualization.Add(pv);
            }
            if (panel != null)
            {
                //TODO fixed
                panel.Height = 171;// LayerThumbnail.defaultMaxThumbSize;// 3 + (int)Math.Floor(pos.getLayerCount() * (LayerThumbnail.defaultMaxThumbSize + LayerVisualizationManager.betweenSpace));
                panel.Width = 3 + (int)Math.Floor(pos.getLayerCount() * (LayerThumbnail.defaultMaxThumbSize + LayerVisualizationManager.betweenSpace));

                //Graphics gs = panel.CreateGraphics();
                //refreshVisualization(gs);

                panel.Refresh();
            }
        }



        /// <summary>
        /// Refresh della visualizzazione dei Layer
        /// </summary>
        /// <param name="gs">Graphics gs</param>
        public static void refreshVisualization(Graphics gs)
        {
            gs.Clear(Color.White);
            foreach (LayerVisualization s in listLayerVisualization)
            {
                if (s.interf)
                    gs.DrawLine(new Pen(new SolidBrush(Program.colorInterlayer), 4), new Point((int)s.occupiedSpace.X, (int)s.occupiedSpace.Y), new Point((int)s.occupiedSpace.X, (int)s.occupiedSpace.Y + (int)s.occupiedSpace.Height));
                s.thumb.drawThere(gs);
                float wString = gs.MeasureString(s.name, new Font("Tahoma", 8)).Width;
                gs.DrawString(s.name, new Font("Tahoma", 8), new SolidBrush(Color.Black), new Point((int)s.occupiedSpace.Left + (int)(s.occupiedSpace.Width - wString) / 2, (int)s.occupiedSpace.Bottom + 5));
            }
        }
        /// <summary>
        /// Deseleziona tutti i Layer
        /// </summary>
        public static void deselectAll()
        {
            foreach (LayerVisualization s in listLayerVisualization)
            {
                s.selected = false;
            }
        }
        /// <summary>
        /// Seleziona il LayerVisualization che contiene il punto
        /// </summary>
        /// <param name="point">Punto</param>
        /// <returns>Il LayerVisualization che contiene il punto oppure l'ultimo selezionato</returns>
        public static LayerVisualization selected(PointF point)
        {
            
            LayerVisualization found = null;
            LayerVisualization lastSelected = listLayerVisualization[0];
            foreach (LayerVisualization s in listLayerVisualization)
            {
                if (s.selected)
                    lastSelected = s;

                if (s.occupiedSpace.Contains(point))
                {
                    found = s;
                    s.selected = true;
                }
                else
                    s.selected = false;
            }
            if (found == null)
            {
                found= lastSelected;
                lastSelected.selected = true;
            }

            return found;
        }
    }
}
