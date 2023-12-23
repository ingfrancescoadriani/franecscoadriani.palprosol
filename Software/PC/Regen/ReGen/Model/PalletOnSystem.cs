using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;
using System.Drawing;
using ReGen.Model.AutoPopulate;

namespace ReGen
{
    /// <summary>
    ///  Pallet piazzato nel sistema
    /// </summary>
    public class PalletOnSystem : Pallet
    {
        /// <summary>
        /// posizione del punto 0 del pallet (dove poggia) e non del primo strato
        /// </summary>
        /// 
        private Point3FR positionOfZero;
        public new Point3F size
        {
            get
            {
                Point3F adding = new Point3F(0,0,0);
                if (panelOverPallet)
                    adding.Z += (float)panelOverPalletThickness;
                if (interLayer)
                    adding.Z += (float)interLayerThickness;
                return new Point3F(base.size.X + adding.X, base.size.Y + adding.Y, base.size.Z + adding.Z);
            }
        }
        public bool panelOverPallet = false;
        public double panelOverPalletThickness = 0.0;
        public bool interLayer = false;
        public double interLayerThickness = 0.0;
        private List<Layer> layerList;
        public bool panelUsed = false;
        private float maxHeight;
        public double approachWidth
        {
            get
            {
                return ReGen.Model.Approaches.approachLenght;
            }
            set
            {
                ReGen.Model.Approaches.approachLenght = value;
            }
        }
        public Point3F approachFixedDirection
        {
            get
            {
                return ReGen.Model.Approaches.approachFixed ;
            }
            set
            {
                ReGen.Model.Approaches.approachFixed = value;
            }
        }
        public FindGoodPayloadGroupToAddStrategy findGoodPayloadGroupToAddStrategy;


        public List<Layer> getLayerList()
        {
            return layerList;
        }

        /// <summary>
        /// Costruttore della classe PayloadOnSystem
        /// </summary>
        /// <param name="positionOfZero">Posizione del punto 0 del pallet</param>
        /// <param name="layerNumber">Numeri di strati da inserire</param>
        /// <param name="size">Dimensione dello strato</param>
        /// <param name="extraBorder">Dimensione del bordo extra</param>
        /// <param name="maxHeight">Altezza massima</param>
        /// <param name="fgptas">Strategy da applicare</param>
        public PalletOnSystem(Point3FR positionOfZero, int layerNumber, Point3F size, Point2F extraBorder, float maxHeight, FindGoodPayloadGroupToAddStrategy fgptas)
            : base(size, extraBorder)
        {
            this.findGoodPayloadGroupToAddStrategy = fgptas;
            setMaxHeight(maxHeight);
            this.positionOfZero = positionOfZero;
            this.layerList = new List<Layer>();
            for (int i = 0; i < layerNumber; i++)
                addAnEmptyLayer();
            //this.size = size;
            //this.extraBorder = extraBorder;
        }
        /// <summary>
        /// Costruttore per la classe PalletOnSystem
        /// </summary>
        /// <param name="positionOfZero">Posizione del punto 0 del pallet</param>
        /// <param name="layerNumber">Numeri di layer da inserire</param>
        /// <param name="p">Pallet dal quale prendere le dimensioni</param>
        /// <param name="maxHeight">Altezza massima</param>
        /// <param name="fgptas">Strategy da applicare</param>
        public PalletOnSystem(Point3FR positionOfZero, int layerNumber, Pallet p, float maxHeight, FindGoodPayloadGroupToAddStrategy fgptas)
            : this(positionOfZero, layerNumber, p.getSize(), p.getExtraBorder(), maxHeight, fgptas)
        { }
        /// <summary>
        /// Setta l'altezza massima del Pallet
        /// </summary>
        /// <param name="maxHeight">Valore dell'altezza massima</param>
        public void setMaxHeight(float maxHeight)
        {
            this.maxHeight = maxHeight;
        }
        /// <summary>
        /// Torna l'altezza massima del pallet
        /// </summary>
        /// <returns>Altezza massima del pallet</returns>
        public float getMaxHeight()
        {
            return this.maxHeight;
        }
        /// <summary>
        /// Posizione dello strato richiesto nel pallet
        /// </summary>
        /// <param name="l"> Strato di cui si cerca la posizione</param>
        /// <returns>
        /// i: Posizione dello strato
        /// -1: Se lo strato non è presente
        /// </returns>
        public int getPositionOfLayer(Layer l)
        {
            for (int i = 0; i < layerList.Count; i++)
                if (layerList[i] == l)
                    return i;
            return -1;
        }
        /// <summary>
        /// Conta il numero di Payload piazzati
        /// </summary>
        /// <returns>res: numero dei Payload</returns>
        public int getNumberOfPayloadPlaced()
        {
            int res = 0;
            foreach (Layer l in layerList)
            {
                foreach (PayloadGroup pg in l.listPayloadGroupPlaced)
                {
                    res += pg.countListPayloadPlaced();
                }
            }
            return res;
        }
        /// <summary>
        /// Torna la posizione dello zero del pallet
        /// </summary>
        /// <returns>Possizione dello zero del pallet</returns>
        public Point3FR getPositionOfZero()
        {
            return this.positionOfZero;
        }
        /// <summary>
        /// Il più piccolo rettangolo che contiene lo strato con il bordo extra
        /// </summary>
        /// <returns>Rettangolo con posizione e bordo extra</returns>
        public BoundsF3DR getBundleOfFirstLayerWithBorder()
        {
            Point3FR positionOfFirstLayer = new Point3FR(positionOfZero.X, positionOfZero.Y, positionOfZero.Z + this.size.Z, positionOfZero.R);
            return new BoundsF3DR(positionOfFirstLayer, this.getSizeWithBorder());
        }
        /// <summary>
        /// Aggiunge uno strato vuoto
        /// </summary>
        /// <returns>l: Nuovo strato vuoto</returns>
        public Layer addAnEmptyLayer()
        {
            Layer l = new Layer(this);
            this.layerList.Add(l);
            return l;
        }

        /// <summary>
        /// Aggiunge il layer dato al pallet
        /// </summary>
        /// <param name="l">Layer da inserire nel pallet</param>
        public void addLayer(Layer l)
        {
            this.layerList.Add(l);

        }
        /// <summary>
        /// Rimuove l'ultimo strato
        /// </summary>
        public void removeLastLayer()
        {
            this.layerList.RemoveAt(this.layerList.Count-1);
        }
        /// <summary>
        /// Rimuove uno strato specifico
        /// </summary>
        /// <param name="index">Indice dello strato da eliminare</param>
        public void removeLayerAtIndex(int index)
        {
            if (index>=0 && index<this.getLayerCount())
                this.layerList.RemoveAt(index);
        }
        /// <summary>
        /// Strato ad un indice specificato
        /// </summary>
        /// <param name="i">Indice dello strato</param>
        /// <returns>Strato all'indice specificato</returns>
        public Layer getLayerAtIndex(int i)
        {
            return this.layerList.ElementAt<Layer>(i);
        }
        /// <summary>
        /// Posizione Z dello strato specificato
        /// </summary>
        /// <param name="indexOfLayer">Indice dello strato</param>
        /// <returns>Posizione Z dello strato</returns>
        public double getPositionOfLayer(int indexOfLayer)
        {
            return this.getPositionOfZero().Z + (indexOfLayer * getLayerAtIndex(indexOfLayer).size.Z);
        }
        /// <summary>
        /// Numero di strati presenti
        /// </summary>
        /// <returns>Numero di strati</returns>
        public int getLayerCount()
        {
            return this.layerList.Count;
        }
        /// <summary>
        /// Sposta lo strato in basso di una posizione
        /// </summary>
        /// <param name="index">Indice dello strato da spostare verso il basso</param>
        public void moveDownLayerAt(int index)
        {
            Layer l =layerList.ElementAt(index);
            layerList.RemoveAt(index);
            layerList.Insert(index-1,l);
        }

        /// <summary>
        /// Sposta lo strato in alto di una posizione
        /// </summary>
        /// <param name="index">Indice dello strato da spostare verso l'alto</param>
        public void moveUpLayerAt(int index)
        {
            Layer l = layerList.ElementAt(index);
            layerList.RemoveAt(index);
            layerList.Insert(index+1, l);
        }
        /// <summary>
        /// Copia lo strato
        /// </summary>
        /// <param name="index">Indice dello strato da copiare</param>
        public void copyLayerAt(int index)
        {
            copyLayerFromTo(index, index);
            //layerList.Insert(index, layerList[index].clone());
        }
        /// <summary>
        /// Ricopia uno strato 
        /// </summary>
        /// <param name="indexFrom">Indice dello strato da copiare</param>
        /// <param name="indexTo">Posizione in cui inserire la copia</param>
        public void copyLayerFromTo(int indexFrom, int indexTo)
        {
            layerList.Insert(indexTo, layerList[indexFrom].clone());
        }
        /// <summary>
        /// Aggiunge una copia ruotata di uno strato
        /// </summary>
        /// <param name="indexOf">Indice dello strato da copiare</param>
        /// <param name="indexTo">Indice destinazione della copia ruotata</param>
        public void addRotatedCopyOfTo(int indexOf, int indexTo)
        {
            copyLayerFromTo(indexOf, indexTo);
            rotateLayer(indexTo, 2);
            //horizontalFlip(indexTo);
            //verticalFlip(indexTo);
        }
        /// <summary>
        /// Rotazione di uno strato
        /// </summary>
        /// <param name="indexOfLayer">Indice dello strato da ruotare</param>
        /// <param name="quadrant">Quadrante di rotazione</param>
        public void rotateLayer(int indexOfLayer, int quadrant)
        {
            layerList[indexOfLayer] = layerList[indexOfLayer].rotated(quadrant);
        }
        /// <summary>
        /// Ribalta uno strato rispetto a Y
        /// </summary>
        /// <param name="index">Indice dello strato</param>
        public void horizontalFlip(int index)
        {
            layerList[index] = layerList[index].horizontalFlipped();
        }
        /// <summary>
        /// Ribalta uno strato rispetto a X
        /// </summary>
        /// <param name="index">Indice dello strato</param>
        public void verticalFlip(int index)
        {
            layerList[index] = layerList[index].verticalFlipped();
        }
        /// <summary>
        /// Test per controllare se è possibile inserire un nuovo strato
        /// </summary>
        /// <param name="newLayer">Strato che si vuole inserire</param>
        /// <returns>
        /// True se l'altezza totale del pallet (con lo strato da aggiungere) non supera l'altezza massima.
        /// False altrimenti.
        /// </returns>
        public bool canAddALayer(Layer newLayer)
        {
            float heigthLayerSum = 0;
            foreach (Layer l in layerList)
                heigthLayerSum += l.size.Z;
            return ((newLayer.size.Z + heigthLayerSum) < this.maxHeight) ;
        }

        /// <summary>
        /// Test per controllare se è possibile inserire un nuovo strato data la sua altezza
        /// </summary>
        /// <param name="assumedHeight">Altezza dello strato da inserire</param>
        /// <returns>
        /// True se l'altezza massima del pallet (con l'altezza passata come parametro) non supera l'altezza massima.
        /// False altrimenti.
        /// </returns>
        public bool canAddALayer(float assumedHeight)
        {
            float heigthLayerSum = 0;
            foreach (Layer l in layerList)
                heigthLayerSum += l.size.Z;
            return ((assumedHeight + heigthLayerSum) <= this.maxHeight);
        }

        /// <summary>
        /// Test per controllare se è possibile aggiungere uno strato clone
        /// </summary>
        /// <param name="indexOfClone">Indice dello strato da clonare</param>
        /// <returns>
        /// True se l'altezza massima del pallet (con l'altezza dello strato clonato) non supera l'altezza massima.
        /// False altrimenti.
        /// </returns>
        public bool canAddALayer(int indexOfClone)
        {
            return canAddALayer(layerList[indexOfClone]);
        }
        /// <summary>
        /// Costruisce un rettangolo
        /// </summary>
        /// <param name="factor">Fattore di scala</param>
        /// <returns>Rettangolo scalato</returns>
        public RectangleF getRectangleF(double factor)
        {
            return new RectangleF(this.getPositionOfZero().toLocation(factor), this.getSize().toSize(factor));
        }
    }
}
