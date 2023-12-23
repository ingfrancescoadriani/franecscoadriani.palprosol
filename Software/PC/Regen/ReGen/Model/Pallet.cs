using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;
using System.Drawing;

namespace ReGen
{
    /// <summary>
    /// Modello di Pallet
    /// </summary>
    public abstract class Pallet
    {
        protected Point3F size;
        protected Point2F extraBorder;

        /// <summary>
        /// Costruttore della classe Pallet
        /// </summary>
        /// <param name="size">Dimensioni del Pallet</param>
        /// <param name="extraBorder">Dimensioni del bordo extra</param>
        public Pallet(Point3F size, Point2F extraBorder)
        {
            this.setSize(size);
            this.setExtraBorder(extraBorder);
        }
        /// <summary>
        /// Dimensione totale del pallet (size + extraBorder)
        /// </summary>
        /// <returns>Dimensione totale</returns>
        public Point2F getSizeWithBorder()
        {
            return (new Point2F(this.size.X+(extraBorder.X),this.size.Y+(extraBorder.Y)));
        }
        /// <summary>
        /// Dimensione del Pallet
        /// </summary>
        /// <returns>Dimensione del Pallet</returns>
        public Point3F getSize()
        {
            return size;
        }
        /// <summary>
        /// Setta la dimensione del Pallet
        /// </summary>
        /// <param name="size">Dimensione da assegnare al Pallet</param>
        public void setSize(Point3F size)
        {
            this.size = size;
        }

        /// <summary>
        /// Setta la dimensione Z del pallet
        /// </summary>
        /// <param name="sizeZ">Nuova dimensione Z del pallet</param>
        public void setSizeZ(float sizeZ)
        {
            this.size.Z = sizeZ;
        }
        /// <summary>
        /// Setta la dimensione Y del pallet
        /// </summary>
        /// <param name="sizeY">Nuova dimensione Y del pallet</param>
        public void setSizeY(float sizeY)
        {
            this.size.Y = sizeY;
        }
        /// <summary>
        /// Setta la dimensione X del pallet
        /// </summary>
        /// <param name="sizeX">Nuova dimensione X del pallet</param>
        public void setSizeX(float sizeX)
        {
            this.size.X = sizeX;
        }
        /// <summary>
        /// Trova l'extraBorder del Pallet
        /// </summary>
        /// <returns>ExtraBorder del Pallet</returns>
        public Point2F getExtraBorder()
        {
            return extraBorder;
        }
        /// <summary>
        /// Setta l'extraBorder
        /// </summary>
        /// <param name="extraBorder">Valore da dare all'extraBorder</param>
        public void setExtraBorder(Point2F extraBorder)
        {
            this.extraBorder = extraBorder;
        }
        /// <summary>
        /// Setta il valore X dell'extraborder
        /// </summary>
        /// <param name="extraBorderX">Nuovo valore di X dell'extraborder</param>
        public void setExtraBorderX(float extraBorderX)
        {
            this.extraBorder.X = extraBorderX;
        }
        /// <summary>
        /// Setta il valore Y dell'extraborder
        /// </summary>
        /// <param name="extraBorderY">Nuovo valore di Y dell'extraborder</param>
        public void setExtraBorderY(float extraBorderY)
        {
            this.extraBorder.Y = extraBorderY;
        }
    }

}