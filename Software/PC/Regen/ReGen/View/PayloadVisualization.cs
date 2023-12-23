using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ReGen
{
    /// <summary>
    /// Classe che rappresenta il Payload, il suo nome e il thumbnail associato
    /// </summary>
    public class PayloadVisualization
    {
        int id;
        public Payload payload;
        public virtual String name
        {
            get
            {
                return payload.ToString();
            }
        }
        private PayloadThumbnail _thumb = null;
        public virtual PayloadThumbnail thumb
        {
            get
            {
                if (_thumb == null)
                {
                    _thumb = PayloadThumbnail.loadThumbnail(this);
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
        /// Costruttore della classe PayloadVisualization
        /// </summary>
        /// <param name="p">Payload</param>
        /// <param name="pos">Id del Payload</param>
        public PayloadVisualization(Payload p, int pos)
        {
            this.payload = p;
            id = pos;
            occupiedSpace = new RectangleF(3, 3 + (id * (PayloadThumbnail.maxThumbSize + PayloadVisualizationManager.betweenSpace)), PayloadThumbnail.maxThumbSize, PayloadThumbnail.maxThumbSize);
        }
    }
}
