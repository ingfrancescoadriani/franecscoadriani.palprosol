using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ReGen
{
    /// <summary>
    /// Classe che rappresenta il Layer, il nome e il suo thumbnail associato
    /// </summary>
    public class LayerVisualization
    {
        //TODO test interfalda
        public bool interf = false;


        int id;
        public Layer layer;
        public virtual String name
        {
            get
            {
                return Program.translate("string_layer") + " " + (layer.getLayerNumber() + 1).ToString(); //"strato " + (layer.getLayerNumber() + 1).ToString();
            }
        }
        private LayerThumbnail _thumb = null;
        public virtual LayerThumbnail thumb
        {
            get
            {
                if (_thumb == null)
                {
                    _thumb = LayerThumbnail.loadThumbnail(this);
                }
                return _thumb;
            }
            set
            {
                _thumb = value;
            }
        }
        public RectangleF occupiedSpace;
        public bool selected;
        /// <summary>
        /// Costruttore del LayerVisualization
        /// </summary>
        /// <param name="p">Layer</param>
        /// <param name="pos">Id del LayerVisualization</param>
        public LayerVisualization(Layer p, int pos)
        {
            this.layer = p;
            id = pos;
            if (p.interLayer)
                interf = p.interLayer;
            //TODO fix
            //occupiedSpace = new RectangleF(3, 3 + (id * (LayerThumbnail.defaultMaxThumbSize + LayerVisualizationManager.betweenSpace)), LayerThumbnail.defaultMaxThumbSize, LayerThumbnail.defaultMaxThumbSize);
            occupiedSpace = new RectangleF(3 + (id * (LayerThumbnail.defaultMaxThumbSize + LayerVisualizationManager.betweenSpace)), 3, LayerThumbnail.defaultMaxThumbSize, LayerThumbnail.defaultMaxThumbSize);
        }
    }
}
