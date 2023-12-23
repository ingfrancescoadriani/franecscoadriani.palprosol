using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Drawing;
using Sintec.Tool;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using ReGen.Model.AutoPopulate;
using System.Globalization;

namespace ReGen.Model
{
    public class WorkData
    {
        public List<Payload> payloads = new List<Payload>();
        //public Payload box
        //{
        //    get
        //    {
        //        return payloads[0];
        //    }
        //    set
        //    {
        //        payloads[0] = value;
        //    }
        //}
        public String customPickData = "";
        public String customPlaceData = "";
        public PalletOnSystem palletOnSystem;
        private int indexLayerUsed = 0;
        public virtual List<PayloadGroup> listPayloadGroupPlaced
        {
            get
            {
                return palletOnSystem.getLayerAtIndex(indexLayerUsed).listPayloadGroupPlaced;
            }
        }
        /// <summary>
        /// Trova l'extraBorder del PalletOnSystem
        /// </summary>
        public virtual SizeF extraPallet
        {
            get
            {
                return palletOnSystem.getExtraBorder().toSize();
            }
        }
        /// <summary>
        /// Dimensione della WorkArea (pallet+extraBorder)
        /// </summary>
        public virtual Point2F sizeWorkArea
        {
            get
            {
                return palletOnSystem.getSizeWithBorder();
            }
        }
        /// <summary>
        /// Costruttore per la classse WorkData
        /// </summary>
        public WorkData() {
            payloads.Add(new Payload(new BoxStrategy(new Point3F(0, 0, 0), "Scatola")));
            indexLayerUsed = 0;
            palletOnSystem = new PalletOnSystem(new Point3FR(0, 0, 0, 0), 0, new Point3F(1200, 800, 150), new Point2F(100, 100), 999999.0F, SolutionFacade.Instance.findGoodPayloadGroupToAddStrategy);
            palletOnSystem.addAnEmptyLayer();
        }
        public WorkData(StringBuilder stringData)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            XmlDocument xmlDoc = new XmlDocument(); //* create an xml document object.
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(stringData.ToString());
            writer.Flush();
            stream.Position = 0;
            xmlDoc.Load(stream); //* load the XML document from the specified file.

            XmlNodeList appInfo = xmlDoc.GetElementsByTagName("applicationInfo");
            foreach (XmlElement e in appInfo[0].ChildNodes)
            {
                if (e.Name == "Regen")
                {
                    foreach (XmlAttribute a in e.Attributes)
                    {
                        if (a.Name == "version")
                        {
                            if (a.Value != Util.getVersion().Major.ToString() + "." + Util.getVersion().Minor.ToString() + "." + Util.getVersion().Build.ToString())
                            {
                                //MessageBox.Show("La versione che si sta utilizzando è diversa da quella con cui è stato creto il file, potrebbero esserci problemi nel caricamento dei dati");
                            }
                        }

                    }
                }
            }
            XmlNodeList workData = xmlDoc.GetElementsByTagName("workData");
            foreach (XmlElement e in workData[0].ChildNodes)
            {
                if (e.Name == "payloadStrategy")
                {
                    PayloadStrategy ps = null;
                    String strategyData = "";
                    String strategyName = "";
                    String strategyId = "";
                    foreach (XmlElement f in e.ChildNodes)
                    {
                        if (f.Name == "data")
                            strategyData = f.InnerText;
                        if (f.Name == "name")
                        {
                            foreach (XmlAttribute a in f.Attributes)
                                if (a.Name == "id")
                                    strategyId = a.Value;
                            strategyName = f.InnerText;
                        }
                    }
                    switch (strategyName)
                    {
                        case "BoxStrategy":
                            ps = new BoxStrategy(strategyData, strategyId);
                            break;
                        case "BoxWithLabelStrategy":
                            ps = new BoxWithLabelStrategy(strategyData);
                            break;
                        case "BoxWithRightSurplusStrategy":
                            ps = new BoxWithRightSurplusStrategy(strategyData);
                            break;
                    }
                    payloads.Add(new Payload(ps));
                    //box = new Payload(ps);
                }
                if (e.Name == "extraData")
                {
                    customPickData = e.InnerText;
                }
                if (e.Name == "palletOnSystem")
                {
                    this.palletOnSystem = null;
                    Point3FR poz = null;
                    Point3F pSize = null;
                    float maxH = float.MaxValue;
                    Point2F pExtraBorder = null;
                    double angleOfRanker = 135.0;
                    bool panelOverPallet = false;
                    double panelOverPalletThickness = 0.0;
                    bool interLayer = false;
                    double interLayerThickness = 0.0;
                    foreach (XmlAttribute attr in e.Attributes)
                    {
                        if (attr.Name == "panelOverPallet")
                        {
                            if (attr.InnerText == true.ToString())
                                panelOverPallet = true;
                            else
                                panelOverPallet = false;
                        }
                        if (attr.Name == "panelOverPalletThickness")
                        {
                            double d;
                            if (double.TryParse(attr.InnerText, NumberStyles.Number, nfi, out d))
                                panelOverPalletThickness = (float)d;
                            else
                                panelOverPalletThickness = 0.0F;
                        }
                        if (attr.Name == "interLayer")
                        {
                            if (attr.InnerText == true.ToString())
                                interLayer = true;
                            else
                                interLayer = false;
                        }
                        if (attr.Name == "interLayerThickness")
                        {
                            double d;
                            if (double.TryParse(attr.InnerText, NumberStyles.Number, nfi, out d))
                                interLayerThickness = (float)d;
                            else
                                interLayerThickness = 0.0F;
                        }
                    }
                    foreach (XmlElement f in e.ChildNodes)
                    {
                        if (f.Name == "otherInfo")
                        {
                            customPlaceData = (f.InnerText);
                        }
                        if (f.Name == "angleOfRanker")
                        {
                            angleOfRanker = (float)Util.getDoubleFromString(f.InnerText);
                        }
                        if (f.Name == "approachLenght")
                        {
                            Approaches.approachLenght = (double)Util.getDoubleFromString(f.InnerText);
                        }
                        if (f.Name == "approachX")
                        {
                            Approaches.approachFixed.X = (float)Util.getDoubleFromString(f.InnerText);
                        }
                        if (f.Name == "approachY")
                        {
                            Approaches.approachFixed.Y = (float)Util.getDoubleFromString(f.InnerText);
                        }
                        if (f.Name == "approachZ")
                        {
                            Approaches.approachFixed.Z = (float)Util.getDoubleFromString(f.InnerText);
                        }
                        if (f.Name == "positionOfZero")
                        {
                            poz = Point3FR.from(f.InnerText);
                        }
                        if (f.Name == "extraBorder")
                        {
                            pExtraBorder = Point2F.from(f.InnerText);
                        }
                        if (f.Name == "size")
                        {
                            pSize = Point3F.from(f.InnerText);
                        }
                        if (f.Name == "maxHeight")
                        {
                            maxH = (float)Util.getDoubleFromString(f.InnerText);
                        }
                        if (f.Name == "layer")
                        {
                            if (this.palletOnSystem == null)
                            {
                                this.palletOnSystem = new PalletOnSystem(poz, 0, pSize, pExtraBorder, maxH, SolutionFacade.Instance.findGoodPayloadGroupToAddStrategy);
                                this.palletOnSystem.panelOverPallet = panelOverPallet;
                                this.palletOnSystem.interLayer = interLayer;
                                this.palletOnSystem.panelOverPalletThickness = panelOverPalletThickness;
                                this.palletOnSystem.interLayerThickness = interLayerThickness;
                            }
                            SolutionFacade.Instance.refreshPayloadOnPalletRanker(angleOfRanker);
                            Layer l = this.palletOnSystem.addAnEmptyLayer();

                            foreach (XmlAttribute attr in f.Attributes)
                            {
                                if (attr.Name == "interLayer")
                                {
                                    if (attr.InnerText == true.ToString())
                                        l.interLayer = true;
                                    else
                                        l.interLayer = false;
                                }
                                if (attr.Name == "interLayerThickness")
                                {
                                    //double d;
                                    //if (double.TryParse(attr.InnerText, out d))
                                    //    l.interLayerThickness = (float)d;
                                    //else
                                    //    l.interLayerThickness = 0.0F;
                                }
                            }

                            foreach (XmlElement ch4 in f.ChildNodes)
                            {

                                if (ch4.Name == "payloadGroups")
                                {
                                    foreach (XmlElement child in ch4.ChildNodes)
                                    {
                                        if (child.Name == "payloadGroup")
                                        {
                                            PayloadGroup pg = new PayloadGroup();
                                            Point2F groupCenter = new Point2F();
                                            l.listPayloadGroupPlaced.Add(pg);
                                            foreach (XmlElement ch in child.ChildNodes)
                                            {
                                                //if (ch.Name == "approach")
                                                //    pg.?
                                                //if (ch.Name == "progressiveNumber")
                                                //    pg.?

                                                if (ch.Name == "pointCatching")
                                                    pg.setPointCatching(Point3FR.from(ch.InnerText));
                                                if (ch.Name == "center")
                                                {
                                                    groupCenter = (Point2F.from(ch.InnerText));
                                                    pg.center = new Point2F(0, 0);
                                                }
                                                if (ch.Name == "quadrant")
                                                    pg.setQuadrant((int)Util.getDoubleFromString(ch.InnerText));
                                                if (ch.Name == "fixedProgressivePosition")
                                                    pg.fixedProgressivePosition = ((int)Util.getDoubleFromString(ch.InnerText));

                                                if (ch.Name == "payloadsPlaced")
                                                {
                                                    foreach (XmlElement ch2 in ch.ChildNodes)
                                                    {
                                                        Point2F center = new Point2F(float.NaN, float.NaN);
                                                        int quadrant = int.MinValue;
                                                        String payloadStrategy = "";
                                                        String idPayload = "";
                                                        if (ch2.Name == "payloadPlaced")
                                                        {
                                                            foreach (XmlAttribute att in ch2.Attributes)
                                                            {
                                                                if (att.Name == "strategy")
                                                                    payloadStrategy = att.InnerText;
                                                                if (att.Name == "id")
                                                                    idPayload = att.InnerText;
                                                            }
                                                            foreach (XmlElement chi in ch2.ChildNodes)
                                                            {
                                                                if (chi.Name == "center")
                                                                    center = (Point2F.from(chi.InnerText));
                                                                if (chi.Name == "quadrant")
                                                                    quadrant = ((int)Util.getDoubleFromString(chi.InnerText));
                                                            }
                                                        }
                                                        PayloadPlaced pp = null;
                                                        foreach (Payload p in payloads)
                                                        {
                                                            if (p.getPayloadStrategy().getName() == idPayload && payloadStrategy.Contains(p.getPayloadStrategy().GetType().Name))
                                                            {
                                                                pp = new PayloadPlaced(p);
                                                                break;
                                                            }
                                                        }
                                                        if (!double.IsNaN(center.X) && quadrant != int.MinValue && pp!=null)
                                                        {
                                                            pp.recentering(center);
                                                            pp.quadrant = quadrant;
                                                            pg.addToPayloadPlacedList(pp);
                                                        }
                                                    }
                                                    pg.center = groupCenter;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            calculateOrderOnLayer();
        }

        public bool getXmlStream(StringBuilder xmlString)
        {
            return getXmlStream(xmlString, 7);
        }

        public bool getXmlStream(StringBuilder xmlString, int decimalPosition)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            bool res = false;
            try
            {
                XmlWriterSettings settings = new XmlWriterSettings { Indent = true, NewLineOnAttributes = false, OmitXmlDeclaration = true, IndentChars = "\t" };
                XmlWriter xmlWriter = XmlWriter.Create(xmlString, settings);

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("begin");

                xmlWriter.WriteStartElement("applicationInfo");
                xmlWriter.WriteStartElement("Regen");
                xmlWriter.WriteAttributeString("version", Util.getVersion().Major.ToString() + "." + Util.getVersion().Minor.ToString() + "." + Util.getVersion().Build.ToString());
                xmlWriter.WriteAttributeString("productName", Application.ProductName);
                xmlWriter.WriteAttributeString("companyName", Application.CompanyName);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("workData");
                foreach (Payload p in payloads)
                {
                    xmlWriter.WriteStartElement("payloadStrategy");
                    xmlWriter.WriteStartElement("name");
                    xmlWriter.WriteAttributeString("id", p.getPayloadStrategy().getName());
                    xmlWriter.WriteString(p.getPayloadStrategy().GetType().Name);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteString(p.getPayloadStrategy().toDataString());
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("size");
                    xmlWriter.WriteStartElement("width");
                    xmlWriter.WriteString(p.getPayloadStrategy().getSize().X.ToString(nfi));
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("length");
                    xmlWriter.WriteString(p.getPayloadStrategy().getSize().Y.ToString(nfi));
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("height");
                    xmlWriter.WriteString(p.getPayloadStrategy().getSize().Z.ToString(nfi));
                    xmlWriter.WriteEndElement();
                    //xmlWriter.WriteStartElement("tollerance");
                    //xmlWriter.WriteString(box.getPayloadStrategy().ToString());
                    //xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("toolData");
                    String tmpS = "";
                    for (int k = 0; k < p.getPayloadStrategy().getToolData().Length; k++)
                        tmpS = tmpS + p.getPayloadStrategy().getToolData()[k].ToString();
                    xmlWriter.WriteString(tmpS);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("initialCenter");
                    xmlWriter.WriteString(p.getPayloadStrategy().getInitialCenter().ToString());
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteStartElement("extraData");
                xmlWriter.WriteString(customPickData);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("palletOnSystem");
                xmlWriter.WriteAttributeString("interLayer", palletOnSystem.interLayer.ToString());
                xmlWriter.WriteAttributeString("interLayerThickness", palletOnSystem.interLayerThickness.ToString("0.00", nfi));
                xmlWriter.WriteAttributeString("panelOverPallet", palletOnSystem.panelOverPallet.ToString(nfi));
                xmlWriter.WriteAttributeString("panelOverPalletThickness", palletOnSystem.panelOverPalletThickness.ToString("0.00", nfi));
                xmlWriter.WriteStartElement("otherInfo");
                xmlWriter.WriteString(customPlaceData);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("angleOfRanker");
                xmlWriter.WriteString(SolutionFacade.Instance.angleChoosen.ToString("0.00", nfi));
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("approachLenght");
                xmlWriter.WriteString(Approaches.approachLenght.ToString("0.00", nfi));
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("approachX");
                xmlWriter.WriteString(Approaches.approachFixed.X.ToString("0.00", nfi));
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("approachY");
                xmlWriter.WriteString(Approaches.approachFixed.Y.ToString("0.00", nfi));
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("approachZ");
                xmlWriter.WriteString(Approaches.approachFixed.Z.ToString("0.00", nfi));
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("positionOfZero");
                xmlWriter.WriteString(palletOnSystem.getPositionOfZero().ToString());
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("extraBorder");
                xmlWriter.WriteString(palletOnSystem.getExtraBorder().ToString());
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("size");
                xmlWriter.WriteString(palletOnSystem.getSize().ToString());
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("maxHeight");
                xmlWriter.WriteString(palletOnSystem.getMaxHeight().ToString(nfi));
                xmlWriter.WriteEndElement();


                for (int i = 0; i < this.palletOnSystem.getLayerCount(); i++)
                {
                    setIndexLayerUsed(i);
                    Layer l = palletOnSystem.getLayerAtIndex(i);
                    xmlWriter.WriteStartElement("layer");
                    xmlWriter.WriteAttributeString("interLayer", l.interLayer.ToString());
                    xmlWriter.WriteAttributeString("interLayerThickness", l.interLayerThickness.ToString("0.00", nfi));
                    xmlWriter.WriteStartElement("payloadGroups");
                    xmlWriter.WriteAttributeString("layer", i.ToString());
                    xmlWriter.WriteAttributeString("number", l.listPayloadGroupPlaced.Count.ToString());
                    int k = 0;

                    foreach (PayloadGroup pg in l.sortedListPayloadGroupPlaced)
                    {
                        xmlWriter.WriteStartElement("payloadGroup");
                        xmlWriter.WriteStartElement("pointCatching");
                        xmlWriter.WriteString(pg.pointCatching.ToString(decimalPosition));
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("center");
                        xmlWriter.WriteString(pg.center.ToString(decimalPosition));
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("quadrant");
                        xmlWriter.WriteString(pg.getQuadrant().ToString());
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("fixedProgressivePosition");
                        xmlWriter.WriteString(pg.fixedProgressivePosition.ToString("0"));
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("progressiveNumber");
                        xmlWriter.WriteString(Sequencer.getProgressive(pg.getId(), l).ToString());
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("approach");
                        xmlWriter.WriteString(pg.getCompleteApproachDirection().ToString(decimalPosition));
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("payloadsPlaced");
                        xmlWriter.WriteAttributeString("layer", i.ToString());
                        xmlWriter.WriteAttributeString("payloadGroup", k.ToString());
                        xmlWriter.WriteAttributeString("number", pg.countListPayloadPlaced().ToString());

                        k++;
                        for (int j = 0; j < pg.countListPayloadPlaced(); j++)
                        {
                            PayloadPlaced pp = pg.getPayloadPlacedAt(j);
                            xmlWriter.WriteStartElement("payloadPlaced");
                            xmlWriter.WriteAttributeString("strategy", pp.getPayloadStrategy().GetType().ToString());
                            xmlWriter.WriteAttributeString("id", pp.getPayloadStrategy().getName());
                            xmlWriter.WriteStartElement("center");
                            xmlWriter.WriteString(pp.getRelCenter().ToString(decimalPosition));
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteStartElement("quadrant");
                            xmlWriter.WriteString(pp.quadrant.ToString());
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteEndElement();
                        }

                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();

                xmlWriter.Close();

                res = true;
            }
            catch
            {
                res = false;
            }
            return res;
        }

        /// <summary>
        /// Salva il file xml con le informazioni
        /// </summary>
        /// <param name="xmlFile">Nome del file xml</param>
        /// <returns>True se il salvataggio viene effettuato, False altrimenti</returns>
        public bool saveStreamToFile(String xmlFile, StringBuilder xmlData)
        {
            bool res = false;
            try
            {
                StreamWriter st = new StreamWriter(xmlFile, false, Encoding.Unicode);
                st.Write(xmlData.ToString());
                st.Close();
            }
            catch
            {
                res = false;
            }
            return res;
        }
        /// <summary>
        /// Salva il file xml con le informazioni
        /// </summary>
        /// <param name="xmlFile">Nome del file xml</param>
        /// <returns>True se il salvataggio viene effettuato, False altrimenti</returns>
        public bool saveToFile(String xmlFile)
        {
            bool res = false;
            StringBuilder s = new StringBuilder();
            res = getXmlStream(s);
            saveStreamToFile(xmlFile, s);
            return res;
        }

        public void calculateOrderOnLayer()
        {
            for (int i = 0; i < palletOnSystem.getLayerCount(); i++)
            {
                //setIndexLayerUsed(i);
                palletOnSystem.getLayerAtIndex(i).resetApproach();
            }
        }

        /// <summary>
        /// Setta l'indice del layerUsed al valore passato come argomento
        /// </summary>
        /// <param name="i">Indice da attribuire al layerUsed</param>
        public void setIndexLayerUsed(int i)
        {
            if (i >= this.palletOnSystem.getLayerCount())
                i = palletOnSystem.getLayerCount() - 1;
            if (i < 0)
                i = 0;
            indexLayerUsed = i;
        }
        /// <summary>
        /// Torna l'indice del LayerUsed
        /// </summary>
        /// <returns>Indice del LayerUsed</returns>
        public int getIndexLayerUsed()
        {
            return indexLayerUsed;
        }


        public Layer getLayerUsed()
        {
            return palletOnSystem.getLayerAtIndex(indexLayerUsed);
        }

        /// <summary>
        /// Cancella il layer puntato come usato
        /// </summary>
        public void deleteUsedLayer()
        {
            palletOnSystem.removeLayerAtIndex(indexLayerUsed);
            if (palletOnSystem.getLayerCount() == 0)
            {
                palletOnSystem.addAnEmptyLayer();
            }
            indexLayerUsed--;
        }        
        /// <summary>
        /// Cancella tutti i layer
        /// </summary>
        public void deleteAllLayers()
        {
            while (palletOnSystem.getLayerCount() > 0)
            {
                palletOnSystem.removeLastLayer();
            }
            palletOnSystem.addAnEmptyLayer();
            indexLayerUsed = 0;
        }
        /// <summary>
        /// Diminuisce l'indice del LayerUsed di 1
        /// </summary>
        public void moveDownSelectedLayer()
        {
            if (this.indexLayerUsed > 0)
            {
                palletOnSystem.moveDownLayerAt(indexLayerUsed);
                indexLayerUsed--;
            }
        }
        /// <summary>
        /// Incrementa l'indice del LayerUsed di 1
        /// </summary>
        public void moveUpSelectedLayer()
        {
            if (this.indexLayerUsed < palletOnSystem.getLayerCount()-1)
            {
                palletOnSystem.moveUpLayerAt(indexLayerUsed);
                indexLayerUsed++;
            }
        }
        /// <summary>
        /// Copia il Layer selezionato nell'indice dell'indexLayerUsed
        /// </summary>
        /// <returns>True se viene copiato il Layer, False altrimenti</returns>
        public bool copySelectedLayer()
        {
            if (palletOnSystem.canAddALayer((int)indexLayerUsed))
                palletOnSystem.copyLayerAt(indexLayerUsed);
            else
                return false;
            return true;
        }
        /// <summary>
        /// Copia tutti gli strati
        /// </summary>
        /// <returns>True se viengono copiati i layer, false altrimenti</returns>
        public bool copyAllLayer()
        {
            float h = 0;
            for (int i = palletOnSystem.getLayerCount() - 1; i >= 0; i--)
                h += palletOnSystem.getLayerAtIndex(i).size.Z;
            if (palletOnSystem.canAddALayer(h))
                for (int i = palletOnSystem.getLayerCount() - 1; i >= 0; i--)
                    palletOnSystem.copyLayerAt(i);
            else
                return false;
            return true;
        }
        /// <summary>
        /// Test per controllare (se true ricopia i primi layer) se è possibile inserire tante volte quanto specificato i primi layer del pallet 
        /// </summary>
        /// <param name="number">Numero di volte che si vuole copiare il Layer</param>
        /// <returns>True se è possibile copiare gli strati, false altrimenti</returns>
        public bool createPalletFromFirstsTwoLayer(int number)
        {
            float h = palletOnSystem.getLayerAtIndex(0).size.Z * number;
            if (palletOnSystem.canAddALayer(h))
                for (int i = 0; i < number; i++)
                {
                    if (i % 2 == 1)
                        palletOnSystem.copyLayerFromTo(0, i + 1);
                    else
                        palletOnSystem.copyLayerFromTo(1, i + 1);
                }
            else
                return false;
            return true;
        }
        /// <summary>
        /// Test per controllare (se true ricopia i layer) se è possibile copiare i primi due layer 
        /// </summary>
        /// <returns>True se è possibile copiare i layer, False altrimenti</returns>
        public bool copyFirstsLayer()
        {
            float h = palletOnSystem.getLayerAtIndex(1).size.Z + palletOnSystem.getLayerAtIndex(0).size.Z;
            if (palletOnSystem.canAddALayer(h))
            {
                palletOnSystem.copyLayerAt(1);
                palletOnSystem.copyLayerFromTo(0, 2);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Ruota il layer selezionato
        /// </summary>
        /// <param name="quadrant">Quadrante per la rotazione</param>
        public void rotateSelectedLayer(int quadrant)
        {
            palletOnSystem.rotateLayer(indexLayerUsed, quadrant);
        }
        /// <summary>
        /// Ribalta lo strato selezionato rispetto a Y
        /// </summary>
        public void horizontalFlipOfSelectedLayer()
        {
            palletOnSystem.horizontalFlip(indexLayerUsed);
        }
        /// <summary>
        /// Ribalta lo strato selezionato rispetto a X
        /// </summary>
        public void verticalFlipOfSelectedLayer()
        {
            palletOnSystem.verticalFlip(indexLayerUsed);
        }
        /// <summary>
        /// Torna una stringa contenente le informazioni nel file xml che viene creato
        /// </summary>
        /// <returns>Stringa contenente le informazioni nel file xml</returns>
        public override String ToString()
        {
            //saveToFile("workData.xml");
            //string text = System.IO.File.ReadAllText("workData.xml");
            StringBuilder text = new StringBuilder();
            getXmlStream(text);
            return text.ToString();
        }
        /// <summary>
        /// Torna una stringa contenente le informazioni nel file xml che viene creato
        /// </summary>
        /// <returns>Stringa contenente le informazioni nel file xml</returns>
        public String ToString(int decimalPosition)
        {
            //saveToFile("workData.xml");
            //string text = System.IO.File.ReadAllText("workData.xml");
            StringBuilder text = new StringBuilder();
            getXmlStream(text, decimalPosition);
            return text.ToString();
        }
    }
}



