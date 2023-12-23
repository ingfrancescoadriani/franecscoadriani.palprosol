using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VirtualConnectors;
using System.Windows.Forms;
using System.IO;
using Sintec.Tool;
using System.Globalization;
using System.Runtime.InteropServices;
using CSGL12;
using System.Drawing;
namespace ThreeDConnectorSpace
{
    public class ThreeDConnector : VirtualConnector
    {
        public override void refreshLanguage()
        {
            //throw new NotImplementedException();
        }

        InterfacciaImpostazioni interfaccia;
        public String workDataXmlString = "";
        private Mesh mMesh;
        private ShaderProgram mShaderProgramSelected;

        private Bitmap mHUDBitmap;
        private Texture mHUDTexture;
        private Font mFont1;
        private Font mFont2;
        private Font mFont3;
        private Font mFont4;
        private Pen mPen1;
        private Brush mBrush1;

        private double mViewDistance = 2000.0;
        private double mViewAzimuthDegrees = 0.0;
        private double mViewAltitudeDegrees = 0.0;

        private double mViewAzimuthDegreesVelocity = 9.0;
        private double mViewAltitudeDegreesVelocity = 1.0;

        private Point mMouseClientPositionStart;
        private double mViewAzimuthDegreesStart = 0.0;
        private double mViewAltitudeDegreesStart = 0.0;

        private MicroTimer timerForSimulation = new MicroTimer();
        private int stepOfSimulation = 0;
        private long stepOfSingleMovement = 0;
        List<Parallelepiped> payloadList = new List<Parallelepiped>();
        private int _velocity = 100;
        public int velocity
        {
            get
            { return _velocity / 10; }
            set
            { _velocity = 10 * Math.Max(0, Math.Min(value, 100)); }
        }
        private double[] atanPrecalculated = new double[1000];

        public ThreeDConnector()
        {
            int rapidity = 8;
            for (int i = 0; i < 1000; i++)
            {
                double factor = 1;
                factor = (i - 500.0) / (20.0 * rapidity);
                factor = Math.Atan(factor) / Math.PI * 2.0;
                factor = factor / (Math.Atan(25.0/rapidity) / Math.PI * 2.0);
                factor = (factor + 1.0) / 2.0;
                atanPrecalculated[i] = factor;
            }

            timerForSimulation.Enabled = false;
            timerForSimulation.Interval = 30000;
            timerForSimulation.MicroTimerElapsed +=new MicroTimer.MicroTimerElapsedEventHandler(timerForSimulation_MicroTimerElapsed);

            mMesh = new Mesh();

            mShaderProgramSelected = new ShaderProgram();

            mHUDBitmap = new Bitmap(512, 512, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //mHUDBitmap = new Bitmap(2048, 1024, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            mHUDTexture = new Texture();

            mFont1 = new Font("Verdana", 36.0f);
            mFont2 = new Font("Verdana", 24.0f);
            mFont3 = new Font("Courier New", 16.0f);
            mFont4 = new Font("Courier New", 10.0f);

            mPen1 = new Pen(Color.Red, 3.0f);

            mBrush1 = new SolidBrush(Color.FromArgb(64, 128, 128, 140));
        }

        public override string getPreview(string workDataXmlString)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            String resForAnteprima = translate("string_RecipeParams") + "\r\n"; //"Parametri della ricetta\r\n";
            if (isLicencedIdPcFromRegistry())
            {
                Dictionary<string, byte> dictPayloadNameCode = new Dictionary<string, byte>();

                double altezzaPallet = 0;
                double altezzaScatola = 0;

                //PALLET
                List<String> sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_size");
                Point3F data = Point3F.from(sTmp[0]);
                Point3F palletSize = data;
                resForAnteprima += translate("string_PalletDimension") + ": " + palletSize.ToString() + "\r\n";// "Dimensioni pallet: " + palletSize.ToString() + "\r\n";
                double altezzaPanel = 0;
                double altezzaInterfalda = 0;
                altezzaPallet = data.Z;
                sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_extraBorder");
                Point2F extraBorder = Point2F.from(sTmp[0]);
                resForAnteprima += translate("string_ExtraBorderPallet") + ": " + extraBorder.ToString() + "\r\n"; //"Extra bordi pallet: " + extraBorder.ToString() + "\r\n";

                //PANEL
                dictPayloadNameCode.Add("panelOverPallet", 5);
                sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_panelOverPalletThickness");
                altezzaPanel = double.Parse(sTmp[0], nfi);
                bool presenzaInterfaldaSuPallet = (bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_panelOverPallet")[0]));
                resForAnteprima += translate("string_panelPresenceOverPallet") + ": " + (presenzaInterfaldaSuPallet ? (translate("string_yesThickness") + " " + altezzaPanel.ToString("0.00")) : translate("string_no")) + "\r\n"; //"Presenza pannello sopra il pallet: " + (presenzaInterfaldaSuPallet ? ("si di spessore " + altezzaPanel.ToString("0.00")) : "no") + "\r\n";


                //INTERLAYER
                dictPayloadNameCode.Add("interLayer", 6);
                sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_interLayerThickness");
                altezzaInterfalda = double.Parse(sTmp[0], nfi);
                //payload code

                resForAnteprima += translate("string_interlayerPresenceOverPallet") + ": " + ((bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_interLayer")[0])) ? (translate("string_yesThickness") + " " + altezzaPanel.ToString("0.00")) : translate("string_no")) + "\r\n";
                                    //Presenza interfalda sopra il pallet: " + ((bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_interLayer")[0])) ? ("si di spessore " + altezzaPanel.ToString("0.00")) : "no") + "\r\n";

                for (int shift = 0; shift < 4; shift++)
                {
                    sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "payloadStrategy" + (shift).ToString() + "_name_id");
                    if (sTmp.Count > 0)
                    {
                        dictPayloadNameCode.Add(sTmp[0], (byte)(shift + 10));
                        //PAYLOAD X (shift)
                        sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "payloadStrategy" + (shift).ToString() + "_data");
                        Point3F size = Point3F.from(sTmp[0].Split(']')[1].Split('[')[0]);
                        altezzaScatola = size.Z;
                        //float rightSurplus = (float)Util.getDoubleFromString(sTmp[0].Split(']')[2]);
                        resForAnteprima += "Payload " + VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "payloadStrategy" + (shift).ToString() + "_name_id")[0] + ": " + size.ToString() + "\r\n";
                    }
                }

                //TOOLDATA
                sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "payloadStrategy0_toolData");

                //EXTRADATA
                sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "extraData");
                resForAnteprima += translate("lblCustomExtraData") + ": " + ((sTmp.Count > 0) ? sTmp[0] : "") + "\r\n"; //"Extra data: " + ((sTmp.Count > 0) ? sTmp[0] : "") + "\r\n";

                //OTHERINFO
                sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "otherInfo");
                resForAnteprima += translate("string_placementInfo") + ": " + ((sTmp.Count > 0) ? sTmp[0] : "") + "\r\n"; //"Info di piazzamento: " + ((sTmp.Count>0)?sTmp[0]:"") + "\r\n";

                //PLACEMENT SETTINGS
                sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "payloadsPlaced");
                int tmpI = (sTmp.Count);
                resForAnteprima += translate("string_payloadPlacedNumber") + ": " + tmpI + "\r\n";// "Numero di payload piazzati: " + tmpI + "\r\n";
                sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "payloadGroups");
                tmpI = (sTmp.Count);
                resForAnteprima += translate("string_numeroDiRaggruppamenti") + ": " + tmpI + "\r\n";// "Numero di raggruppamenti: " + tmpI + "\r\n";
                sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layers");
                tmpI = (sTmp.Count);
                resForAnteprima += translate("string_numeroDiStrati") + ": " + tmpI + "\r\n"; //"Numero di strati: " + tmpI + "\r\n";


                int placingNumber = 0;
                double altezzaInterfaldeEPannello = 0;
                if (bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_panelOverPallet")[0]))
                    altezzaInterfaldeEPannello += altezzaPanel;

                if (bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_interLayer")[0]))
                    altezzaInterfaldeEPannello += altezzaInterfalda;

                // "[" + (placingNumber + 1).ToString() + "; " + ptw.groupNumber.ToString() + "; " + ptw.layerNumber + "; " + ptw.onGroupOf + "; " + ptw.onLayerOf + "]: " 
                resForAnteprima += "\r\n" + translate("string_placementList") + "\r\n"; // += "\r\nElenco piazzamenti\r\n";

                Point3FR pgDataCatching;
                Point2F pgDataCenter;
                double pgDataQuadrant;
                double pgDataProgressive;
                Point3F pgDataApproach;
                int onLayerOf = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layers").Count;
                //per ogni layer
                for (int i = 0; i < onLayerOf; i++)
                {
                    //per ogni gruppo
                    SortedListWithDuplicatedKey<PayloadToWrite> sortedLp = new SortedListWithDuplicatedKey<PayloadToWrite>();
                    for (int j = 0; j < VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroups").Count; j++)
                    {
                        pgDataCatching = Point3FR.from(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_pointCatching")[0]);
                        pgDataCenter = Point2F.from(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_center")[0]);
                        pgDataQuadrant = double.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_quadrant")[0], nfi);
                        pgDataApproach = Point3F.from(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_approach")[0]);
                        pgDataProgressive = double.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_progressiveNumber")[0], nfi);
                        //per ogni scatola
                        PayloadToWrite tmpP;
                        int onGroupOf = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_payloadsPlaced").Count;
                        for (int k = 0; k < onGroupOf; k++)
                        {
                            tmpP = new PayloadToWrite();
                            tmpP.onLayerOf = onLayerOf;
                            tmpP.layerNumber = i + 1;
                            tmpP.onGroupOf = onGroupOf;
                            tmpP.groupNumber = j + 1;
                            Point2F ppDataCenter = Point2F.from(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_payload" + k + "_center")[0]);
                            double ppDataQuadrant = double.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_payload" + k + "_quadrant")[0], nfi);
                            tmpP.pgDataCenter = new Point3FR(pgDataCatching.X + pgDataCenter.X + ppDataCenter.X,
                                                            pgDataCatching.Y + pgDataCenter.Y + ppDataCenter.Y,
                                                            pgDataCatching.Z + (altezzaInterfaldeEPannello + altezzaPallet + ((i + 1) * altezzaScatola)),
                                                            (((pgDataCatching.R + (ppDataQuadrant * 90.0)) + 90) % 360.0) - 90.0);
                            tmpP.pgDataApproach = pgDataApproach;
                            tmpP.pgDataProgressive = pgDataProgressive;
                            tmpP.payloadCode = dictPayloadNameCode[VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_payload" + k + "_name_id")[0]];
                            tmpP.payloadName = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_payload" + k + "_name_id")[0];
                            sortedLp.Add(pgDataProgressive, tmpP);
                        }
                    }
                    altezzaInterfaldeEPannello += bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_interlayer")[0]) ? altezzaInterfalda : 0.0;
                    for (int j = 0; j < sortedLp.Count; j++)
                    {
                        PayloadToWrite ptw = sortedLp.Values[j];
                        resForAnteprima += translate("labelPayload") + " " + ptw.payloadName + "[" + (placingNumber + 1).ToString() + "; " + ptw.groupNumber.ToString() + "; " + ptw.layerNumber + "; " + ptw.onGroupOf + "; " + ptw.onLayerOf + "]: " + new Point3FR(ptw.pgDataApproach.X + ptw.pgDataCenter.X, ptw.pgDataApproach.Y + ptw.pgDataCenter.Y, ptw.pgDataApproach.Z + ptw.pgDataCenter.Z, ptw.pgDataCenter.R).ToString() + " -> " + ptw.pgDataCenter.ToString() + "\r\n";
                    }

                    if (VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i.ToString() + "_interlayer").Count > 0)
                        if (bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i.ToString() + "_interlayer")[0]))
                            resForAnteprima += translate("string_interlayer") + ": " + new Point3F(palletSize.X / 2.0, palletSize.Y / 2.0, (altezzaInterfaldeEPannello + altezzaPallet + ((i + 1) * altezzaScatola))).ToString() + "\r\n";
                                        //"Interfalda: " + new Point3F(palletSize.X / 2.0, palletSize.Y / 2.0, (altezzaInterfaldeEPannello + altezzaPallet + ((i + 1) * altezzaScatola))).ToString() + "\r\n";

                }
                return resForAnteprima;
            }
            else
                return translate("string_licenceNotFoundOrNotValidNoSimulation"); //"Licenza non trovata o non valida, impossibile mostrare la simulazione";
        }
        public override string getIstruction(string workDataXmlString)
        {
            String lStr = translate("string_doDragToChangePerspective"); //"Fare un trascina con il mouse per cambiare prospettiva. Effettuare un doppioclick per avviare la simulazione. Premere il pulsante destro per avviare il cambio di prospettiva automatico. Usare la rotella per avvicinarsi o allontanarsi dal pallet.\r\nE' possibile visualizzare l'estratto della ricetta nel pannello dell'anteprima. In una prima sezione sono contenuti i parametri della ricetta e nella seconda parte c'è l'elenco dei piazzamenti utilizzando il seguente formato:\r\n  Payload <nome payload>[<progressivo piazzamento>; <progressivo gruppo>; <progressivo strato>; <numero di payload nel gruppo>; <numero di payload nello strato>]: (<coordinate di accostamento>) -> (<coordinate di arrivo>)\r\n";
            return lStr;
        }

        public static int decimalNumberOfMeasure = 2;  // 3,0347 -> 3,03
        public override void send(string workDataXmlString)
        {
            if (isLicencedIdPcFromRegistry())
            {
                if (interfaccia != null)
                {
                    interfaccia.mCSGL12ExampleHandler.startSimulation(1000000);
                }
            }
        }

        public override void sendTag(string tag, string workDataXmlString)
        {
            throw new NotImplementedException();
        }

        public override void init(string workDataXmlString)
        {

        }

        public override Panel getSettingsPanel(string workDataXmlString)
        {
            if (isLicencedIdPcFromRegistry())
            {
                interfaccia = new InterfacciaImpostazioni(workDataXmlString);
                return interfaccia.getPanel();
            }
            else
            {
                return new Panel();
            }
        }


        // bool decodeWorkData(char* cipherTextIn, char** out_stringChar, char* id, char* hashId, char** out_checkSum)
        [DllImport("lApproachDll.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool checkIdPcLicence(StringBuilder cbcDate, StringBuilder id, StringBuilder hashId);

        Registry reg = new Registry();
        private bool isLicencedIdPcFromRegistry()
        {
            List<String> macAddress = Sintec.Tool.NetworkTool.GetMACAddress2();
            //macAddress = Sintec.Tool.NetworkTool.GetMACAddress1();
            return isLicencedMacFromRegistry(macAddress);
        }

        private bool isLicencedMacFromRegistry(List<String> macAddresses)
        {
            bool res = false;
            List<String> keys = new List<String>();
            keys.Add((String)reg.GetValue("key", "LicenceMac"));
            keys.Add((String)reg.GetValue("key", "LicenceMac2"));
            keys.Add((String)reg.GetValue("key", "LicenceMac3"));
            keys.Add((String)reg.GetValue("key", "LicenceMac4"));
            keys.Add((String)reg.GetValue("key", "LicenceMac5"));
            keys.Add((String)reg.GetValue("key", "LicenceMac6"));
            keys.Add((String)reg.GetValue("key", "LicenceMac7"));
            keys.Add((String)reg.GetValue("key", "LicenceMac8"));
            keys.Add((String)reg.GetValue("key", "LicenceMac9"));
            keys.Add((String)reg.GetValue("key", "LicenceMac10"));
            int noLicenceCode = 0;  // 0 = chiavi valide ma nessuna è per il plugin (chiave non presente)
                                    // 1 = E' stata riscontrata una data di sistema incoerente, si prega di reimpostarla
                                    // 2 = E' stata riscontrata una data di sistema incoerente, si prega di reimpostarla
                                    // 3 = La chiave di licenza è scaduta, per ottenere una nuova chiave contattare l'assistenza
                                    // 4 = La chiave di licenza non è valida, si prega di contattare l'assistenza
            foreach (String key in keys)
            {
                if (!String.IsNullOrEmpty(key))
                {
                    String hwCode = key.Split('-')[0];
                    String dateCode = key.Split('-')[1];
                    String date = HwProtection.Decrypt(dateCode);
                    DateTime startDate = DateTime.ParseExact(date.Substring(0, 8), "ddMMyyyy", CultureInfo.InvariantCulture);
                    DateTime endDate = DateTime.ParseExact(date.Substring(8, 8), "ddMMyyyy", CultureInfo.InvariantCulture);
                    int i = 0;
                    while (!res && i < macAddresses.Count)
                    {
                        String hwCode_calculated = HwProtection.Encrypt(macAddresses[i++] + "8rWh785IK3plugin 3dSimulator v1.0");
                        res = checkIdPcLicence(new StringBuilder(dateCode), new StringBuilder( hwCode_calculated ), new StringBuilder(hwCode));
                    }
                    if (res)
                    {
                        res = false;
                        if (reg.GetValue("key", "lastStartDate") != null && DateTime.Now < DateTime.ParseExact(HwProtection.Decrypt((String)reg.GetValue("key", "lastStartDate")), "ddMMyyyy", CultureInfo.InvariantCulture))
                            noLicenceCode = 1;
                        else if (DateTime.Now < startDate)
                            noLicenceCode = 2;
                        else if (DateTime.Now > endDate)
                            noLicenceCode = 3;
                        else if (HwProtection.getMD5(key.Split('-')[0] + key.Split('-')[1]) != key.Split('-')[2])
                            noLicenceCode = 4;
                        else
                            res = true;
                        break;
                    }
                }
            }
            if (noLicenceCode == 1)
                MessageBox.Show(translate("string_incorrectData"));// "E' stata riscontrata una data di sistema incoerente, si prega di reimpostarla");
            else if (noLicenceCode == 2)
                MessageBox.Show(translate("string_incorrectData"));// "E' stata riscontrata una data di sistema incoerente, si prega di reimpostarla");
            else if (noLicenceCode == 3)
                MessageBox.Show(translate("string_licenceOutdated"));// "La chiave di licenza è scaduta, per ottenere una nuova chiave contattare l'assistenza");
            else if (noLicenceCode == 4)
                MessageBox.Show(translate("string_licenceNotValid"));// "La chiave di licenza non è valida, si prega di contattare l'assistenza");
            return res;
        }
        private static Random r = new Random();
        private static Dictionary<string, Color> _colorPlacedPayload = new Dictionary<string, Color>();
        private static Color baseColorPlacedPayload = Color.DarkKhaki;
        public static Color colorPlacedPayload(String name)
        {
            if (!_colorPlacedPayload.ContainsKey(name))
            {
                HSLColor hslColor = new HSLColor(baseColorPlacedPayload);
                hslColor.Luminosity *= (0.5 + (1.0 * r.NextDouble())); // 0,5 to 1,5
                hslColor.Hue *= (0.5 + (1.0 * r.NextDouble())); // 0,5 to 1,5
                Color darkenColor = hslColor;
                _colorPlacedPayload.Add(name, darkenColor);
            }
            return _colorPlacedPayload[name];
        }

        public void startSimulation(int velocity)
        {
            payloadList = new List<Parallelepiped>();
            mMesh = new Mesh();
            stepOfSimulation = 0;
            stepOfSingleMovement = 0;
            double altezzaBase = 0;
            double altezzaPayloads = 0;
            List<Parallelepiped> payloadListTemp = getPayloadParallelepipedFromWorkDataXmlString(out altezzaPayloads);
            this.velocity = velocity;
            foreach (Parallelepiped p1 in getPalletParallelepipedFromWorkDataXmlString(out altezzaBase))
                mMesh.addParallelepiped(p1.size.X, p1.size.Y, p1.size.Z, p1.centerWithRotationOnZAxis.X, p1.centerWithRotationOnZAxis.Y, p1.centerWithRotationOnZAxis.Z  - (float)((altezzaBase + altezzaPayloads) / 2.0), 0.0f, p1.color, "pallet");
            foreach(Parallelepiped p in payloadListTemp)
            {
                p.centerWithRotationOnZAxis = new Point3FR(p.centerWithRotationOnZAxis.X, p.centerWithRotationOnZAxis.Y, p.centerWithRotationOnZAxis.Z + (float)altezzaBase - (float)((altezzaBase + altezzaPayloads) / 2.0), p.centerWithRotationOnZAxis.R);
                p.approachPoint = new Point3F(p.approachPoint.X, p.approachPoint.Y, p.approachPoint.Z + (float)altezzaBase - (float)((altezzaBase + altezzaPayloads) / 2.0));
                payloadList.Add(p);
            }
            timerForSimulation.Start();
        }

        public List<Parallelepiped> getPalletParallelepipedFromWorkDataXmlString(out double totalheight)
        {

            totalheight = 0;
            Color colorOfPallet = Color.Khaki;
            List<Parallelepiped> res = new List<Parallelepiped>();
            double altezzaPallet = 0;
            Point3F payloadSize = new Point3F();
            Point3F palletSize = new Point3F();
            Point2F extraBorder = new Point2F();
            //PALLET
            List<String> sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_size");
            palletSize = Point3F.from(sTmp[0]);
            altezzaPallet = palletSize.Z;


            sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_extraBorder");
            extraBorder = Point2F.from(sTmp[0]);

            for (int j = 0; j < 3; j++)
            {
                //res.Add(new Parallelepiped()
                //{
                //    centerWithRotationOnZAxis = new Point3FR(0, ((j - 1) * ((palletSize.Y - 150) / 2)), 0, 0),
                //    size = new Point3F(palletSize.X, 150, 15),
                //    color = colorOfPallet
                //});
                res.Add(new Parallelepiped()
                {
                    centerWithRotationOnZAxis = new Point3FR(((j - 1) * ((palletSize.X - 150) / 2)), 0, 0, 0),
                    size = new Point3F(150, palletSize.Y, 15),
                    color = colorOfPallet
                });
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    res.Add(new Parallelepiped()
                    {
                        centerWithRotationOnZAxis = new Point3FR(((i - 1) * ((palletSize.X - 150) / 2)), ((j - 1) * ((palletSize.Y - 150) / 2)), 15, 0),
                        size = new Point3F(150, 150, altezzaPallet - 45),
                        color = colorOfPallet
                    });
                }
            }
            for (int j = 0; j < 3; j++)
            {
                res.Add(new Parallelepiped()
                {
                    centerWithRotationOnZAxis = new Point3FR(0, ((j - 1) * ((palletSize.Y - 150) / 2)), altezzaPallet - 30, 0),
                    size = new Point3F(palletSize.X, 150, 15),
                    color = colorOfPallet
                });
                //res.Add(new Parallelepiped()
                //{
                //    centerWithRotationOnZAxis = new Point3FR(((j - 1) * ((palletSize.X - 150) / 2)), 0,  - altezzaPallet + 30, 0),
                //    size = new Point3F(150, palletSize.Y, 15),
                //    color = colorOfPallet
                //});
            }
            int numberoDiSteccheTrasversali = (int)(palletSize.X / 230) + 1;
            numberoDiSteccheTrasversali = ((numberoDiSteccheTrasversali / 2) * 2) + 1;
            for (int j = 0; j < numberoDiSteccheTrasversali; j++)
            {
                res.Add(new Parallelepiped()
                {
                    centerWithRotationOnZAxis = new Point3FR(((j - (int)(numberoDiSteccheTrasversali / 2)) * ((palletSize.X - 150) / (numberoDiSteccheTrasversali - 1))), 0, altezzaPallet - 15, 0),
                    size = new Point3F(150, palletSize.Y, 15),
                    color = colorOfPallet
                });
            }
            totalheight = altezzaPallet;
            return res;
        }

        public List<Parallelepiped> getPayloadParallelepipedFromWorkDataXmlString(out double totalheight)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            Color interfaldaColore = Color.FromArgb(127, Color.Brown.R, Color.Brown.G, Color.Brown.B);
            Color pannelloColore = Color.FromArgb(127, Color.CornflowerBlue.R, Color.CornflowerBlue.G, Color.CornflowerBlue.B);
            // lista dei payload e interfalde sopra a partire dalla prima scatola depositata (z = 0 cioè non considera pallet e pannello e falda sopra di esso)
            List<Parallelepiped> res = new List<Parallelepiped>();
            double altezzaPallet = 0;
            double altezzaScatola = 0;
            totalheight = 0;
            Dictionary<String,Point3F> payloadSize = new Dictionary<string,Point3F>();
            Point3F palletSize = new Point3F();
            Point2F extraBorder = new Point2F();
            //PALLET
            List<String> sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_size");
            palletSize = Point3F.from(sTmp[0]);
            altezzaPallet = palletSize.Z;


            sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_extraBorder");
            extraBorder = Point2F.from(sTmp[0]);
            for (int shift = 0; shift < 4; shift++)
            {
                //PAYLOAD 0
                sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "payloadStrategy" + (shift).ToString() + "_name_id");
                if (sTmp.Count > 0)
                {
                    String name = sTmp[0];
                    sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "payloadStrategy" + (shift).ToString() + "_data");
                    payloadSize.Add(name, Point3F.from(sTmp[0].Split(']')[1].Split('[')[0]));
                    altezzaScatola = payloadSize[name].Z;
                }
            }

            //PANEL
            sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_panelOverPalletThickness");
            double spessorePannello = double.Parse(sTmp[0], nfi);
            bool presenzaPannelloSuPallet = bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_panelOverPallet")[0]);

            //INTERLAYER
            sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_interLayerThickness");
            double spessoreInterfalda = double.Parse(sTmp[0], nfi);
            bool presenzaInterfaldaSuPallet = bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_interLayer")[0]);

            if (presenzaPannelloSuPallet)
                res.Add(new Parallelepiped() { centerWithRotationOnZAxis = new Point3FR(0, 0, 0.0, 0.0), name = "interlayer", approachPoint = new Point3F(0, 0, 0.0), size = new Point3F(palletSize.X, palletSize.Y, spessorePannello), color = pannelloColore });
            if (presenzaInterfaldaSuPallet)
                res.Add(new Parallelepiped() { centerWithRotationOnZAxis = new Point3FR(0, 0, 0.0 + (presenzaPannelloSuPallet ? spessorePannello : 0.0), 0.0), approachPoint = new Point3F(0, 0, 0.0 + (presenzaPannelloSuPallet ? spessorePannello : 0.0)), name = "interlayer", size = new Point3F(palletSize.X, palletSize.Y, spessoreInterfalda), color = interfaldaColore });

            double altezzaInterfaldeEPannello = (presenzaInterfaldaSuPallet ? spessoreInterfalda : 0.0) + (presenzaPannelloSuPallet ? spessorePannello : 0.0);

            //PLACEMENT SETTINGS
            sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "payloadsPlaced");
            int tmpI = (sTmp.Count);
                            
            sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "payloadGroups");
            tmpI = (sTmp.Count);

            sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layers");
            tmpI = (sTmp.Count);

            Point3FR pgDataCatching;
            Point2F pgDataCenter;
            double pgDataQuadrant;
            double pgDataProgressive;
            Point3F pgDataApproach;
            int onLayerOf = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layers").Count;
            //per ogni layer
            for (int i = 0; i < onLayerOf; i++)
            {
                //per ogni gruppo
                SortedListWithDuplicatedKey<PayloadToWrite> sortedLp = new SortedListWithDuplicatedKey<PayloadToWrite>();
                for (int j = 0; j < VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroups").Count; j++)
                {
                    pgDataCatching = Point3FR.from(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_pointCatching")[0]);
                    pgDataCenter = Point2F.from(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_center")[0]);
                    pgDataQuadrant = double.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_quadrant")[0], nfi);
                    pgDataApproach = Point3F.from(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_approach")[0]);
                    pgDataProgressive = double.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_progressiveNumber")[0], nfi);
                    //per ogni scatola
                    PayloadToWrite tmpP;
                    int onGroupOf = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_payloadsPlaced").Count;
                    for (int k = 0; k < onGroupOf; k++)
                    {
                        tmpP = new PayloadToWrite();
                        tmpP.onLayerOf = onLayerOf;
                        tmpP.layerNumber = i + 1;
                        tmpP.onGroupOf = onGroupOf;
                        tmpP.groupNumber = j + 1;
                        Point2F ppDataCenter = Point2F.from(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_payload" + k + "_center")[0]);
                        double ppDataQuadrant = double.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_payload" + k + "_quadrant")[0], nfi);
                        tmpP.pgDataCenter = new Point3FR(pgDataCatching.X + pgDataCenter.X + ppDataCenter.X,
                                                        pgDataCatching.Y + pgDataCenter.Y + ppDataCenter.Y,
                                                        pgDataCatching.Z + (altezzaInterfaldeEPannello + (i * altezzaScatola)),
                                                        (((pgDataCatching.R + (ppDataQuadrant * 90.0)) + 90) % 360.0) - 90.0);
                        tmpP.pgDataApproach = pgDataApproach;
                        tmpP.pgDataProgressive = pgDataProgressive;
                        tmpP.tag = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_payload" + k + "_name_id")[0];
                        sortedLp.Add(pgDataProgressive, tmpP);
                    }
                }
                for (int j = 0; j < sortedLp.Count; j++)
                {
                    PayloadToWrite ptw = sortedLp.Values[j];
                    res.Add(new Parallelepiped()
                    {
                        centerWithRotationOnZAxis = new Point3FR(0.5 + ptw.pgDataCenter.X - (palletSize.X / 2.0f),
                        0.5 + ptw.pgDataCenter.Y - (palletSize.Y / 2.0f),
                        0.5 + ptw.pgDataCenter.Z,
                        ptw.pgDataCenter.R),
                        size = new Point3F(payloadSize[ptw.tag].X - 1, payloadSize[ptw.tag].Y - 1, payloadSize[ptw.tag].Z - 1),
                        color = colorPlacedPayload(ptw.tag),
                        approachPoint = new Point3F(0.5 + ptw.pgDataCenter.X - (palletSize.X / 2.0f) - ptw.pgDataApproach.X,
                        0.5 + ptw.pgDataCenter.Y - (palletSize.Y / 2.0f) - ptw.pgDataApproach.Y,
                        0.5 + ptw.pgDataCenter.Z + ptw.pgDataApproach.Z),
                        name = ptw.tag
                    });
                }
                if (bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_interlayer")[0]))
                {
                    res.Add(new Parallelepiped() { centerWithRotationOnZAxis = new Point3FR(0, 0, (altezzaInterfaldeEPannello + ((i + 1) * altezzaScatola)), 0.0), name = "interlayer", approachPoint = new Point3F(0, 0, (altezzaInterfaldeEPannello + ((i + 1) * altezzaScatola))), size = new Point3F(palletSize.X, palletSize.Y, spessoreInterfalda), color = interfaldaColore });
                    altezzaInterfaldeEPannello += spessoreInterfalda;
                }
            }
            totalheight = altezzaInterfaldeEPannello + (onLayerOf * altezzaScatola) + (presenzaInterfaldaSuPallet ? spessoreInterfalda : 0.0) + (presenzaPannelloSuPallet ? spessorePannello : 0.0);
            return res;
        }

        public void OpenGLStarted( CSGL12Control csgl12Control )
        {
            GL gl = csgl12Control.GetGL();

            if (null == gl) { return; }

            // per l'illuminazione vedere :http://www.glprogramming.com/red/chapter05.html
            float[] mat_specular = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] mat_shininess = { 50.0f };
            float[] light_ambient = { 1.0f, 1.0f, 1.0f, 1.0f }; //intensity of the ambient light 
            float[] light_diffuse = { 0.0f, 1.0f, 0.0f, 1.0f }; //the color of a light
            float[] light_specular = { 0.0f, 1.0f, 0.0f, 1.0f }; //luce nella parte speculare dell'oggetto
            float[] light_position = { 100000.0f, 0.0f, 0.0f, 0.0f };

            gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);

            gl.glMaterialfv(GL.GL_FRONT, GL.GL_SPECULAR, mat_specular);
            gl.glMaterialfv(GL.GL_FRONT, GL.GL_SHININESS, mat_shininess);
            gl.glLightfv(GL.GL_LIGHT0, GL.GL_AMBIENT, light_ambient);
            gl.glLightfv(GL.GL_LIGHT0, GL.GL_DIFFUSE, light_diffuse);
            gl.glLightfv(GL.GL_LIGHT0, GL.GL_SPECULAR, light_specular);
            gl.glLightfv(GL.GL_LIGHT0,GL. GL_POSITION, light_position);
            //l.glLightf(GL.GL_LIGHT0, GL.GL_CONSTANT_ATTENUATION, 0.0f);     //attenuazioni della luce
            //gl.glLightf(GL.GL_LIGHT0, GL.GL_LINEAR_ATTENUATION, 0.4f);   //attenuazioni della luce
            //gl.glLightf(GL.GL_LIGHT0, GL.GL_QUADRATIC_ATTENUATION, 0.2f);    //attenuazioni della luce

            //gl.glEnable(GL.GL_LIGHTING);
            //gl.glEnable(GL.GL_LIGHT0);
            //gl.glEnable(GL.GL_DEPTH_TEST);

            double altezzaBase = 0;
            double altezzaPayloads = 0;
            mMesh = new Mesh();
            List <Parallelepiped> palletParallelepipedFromWorkDataXmlString = getPalletParallelepipedFromWorkDataXmlString(out altezzaBase);
            foreach (Parallelepiped p in getPayloadParallelepipedFromWorkDataXmlString(out altezzaPayloads))
                mMesh.addParallelepiped(p.size.X, p.size.Y, p.size.Z, p.centerWithRotationOnZAxis.X, p.centerWithRotationOnZAxis.Y, p.centerWithRotationOnZAxis.Z + (float)altezzaBase - (float)((altezzaBase + altezzaPayloads) / 2.0), p.centerWithRotationOnZAxis.R, p.color, p.name);
            foreach (Parallelepiped p in palletParallelepipedFromWorkDataXmlString)
                mMesh.addParallelepiped(p.size.X, p.size.Y, p.size.Z, p.centerWithRotationOnZAxis.X, p.centerWithRotationOnZAxis.Y, p.centerWithRotationOnZAxis.Z - (float)((altezzaBase + altezzaPayloads) / 2.0), 0.0f, p.color, "pallet");
            
            mHUDTexture.CreateTextureFromBitmap(gl, mHUDBitmap, false); // NOTE: MIP-MAPPING DISABLED FOR TEXURE UPDATE SPEED

            using (Graphics g = Graphics.FromImage(mHUDBitmap))
            {
                PointF carat = new PointF(0.0f, 0.0f);

                String text = "";

                text = "C# OpenGL (CSGL)";
                g.DrawString(text, mFont1, Brushes.Black, carat);
                carat.Y += mFont1.GetHeight();

                text = "Здравствуйте";
                g.DrawString(text, mFont2, Brushes.Black, carat);
                carat.Y += mFont2.GetHeight();

                text = "γεια σου";
                g.DrawString(text, mFont2, Brushes.Black, carat);
                carat.Y += mFont2.GetHeight();

                text = "مرحبا";
                g.DrawString(text, mFont2, Brushes.Black, carat);
                carat.Y += mFont2.GetHeight();
            }
            mHUDTexture.UpdateTextureWithBitmapData(gl, mHUDBitmap);

            if (true == gl.bwglSwapIntervalEXT)
            {
                // ALLOW TEARING
                gl.wglSwapIntervalEXT(0);
            }
        }

        float distanceFromPalletMM = 2000.0f;
        public void timerForSimulation_MicroTimerElapsed(object sender, EventArgs e)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            if (payloadList.Count > 0)
            {
                Parallelepiped p = payloadList[stepOfSimulation];
                if (stepOfSingleMovement == 0)
                {
                    mMesh.addParallelepiped(p.size.X, p.size.Y, p.size.Z, p.centerWithRotationOnZAxis.X, p.centerWithRotationOnZAxis.Y, p.centerWithRotationOnZAxis.Z, p.centerWithRotationOnZAxis.R, p.color, p.name);

                    //if ((p.centerWithRotationOnYZAxis.R + 360) % 360 == 90 || (p.centerWithRotationOnYZAxis.R + 360) % 360 == 270)
                    //    mMesh.addParallelepiped(p.size.Y, p.size.X, p.size.Z, p.centerWithRotationOnYZAxis.X, p.centerWithRotationOnYZAxis.Y, p.centerWithRotationOnYZAxis.Z, 0.0f, p.color);
                    //else
                    //    mMesh.addParallelepiped(p.size.X, p.size.Y, p.size.Z, p.centerWithRotationOnYZAxis.X, p.centerWithRotationOnYZAxis.Y, p.centerWithRotationOnYZAxis.Z, 0.0f, p.color);
                }
                //stepOfSingleMovement += this.velocity;
                int phase = (int)(stepOfSingleMovement / 10000);
                long step = stepOfSingleMovement % 10000;

                Point3FR actualPosition = new Point3FR();
                Point3FR startPosition = new Point3FR();
                Point3FR finalPosition = new Point3FR();
                if (phase < 4)
                {
                    if (phase == 0) //pick
                    {
                        stepOfSingleMovement += this._velocity * 2;
                        List<String> sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "payloadStrategy0_initialCenter");
                        Point2F startlocation = Point2F.from(sTmp[0]);
                        startPosition = new Point3FR(startlocation);
                        startPosition.Z = p.centerWithRotationOnZAxis.Z - 200;
                        startPosition.R = 0;
                        sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_angleOfRanker");
                        startPosition.X = startPosition.X + (float)(distanceFromPalletMM * Math.Cos(Math.PI + (double.Parse(sTmp[0], nfi) / 180.0 * Math.PI)));
                        startPosition.Y = startPosition.Y + (float)(distanceFromPalletMM * Math.Sin(Math.PI + (double.Parse(sTmp[0], nfi) / 180.0 * Math.PI)));
                        finalPosition = startPosition.clone();
                        finalPosition.Z = p.centerWithRotationOnZAxis.Z + p.size.Z + 100;
                    }
                    else if (phase == 1) //translate and rotate
                    {
                        stepOfSingleMovement += this._velocity;
                        List<String> sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "payloadStrategy0_initialCenter");
                        Point2F startlocation = Point2F.from(sTmp[0]);
                        startPosition = new Point3FR(startlocation);
                        startPosition.Z = p.centerWithRotationOnZAxis.Z + p.size.Z + 100;
                        startPosition.R = 0;
                        sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_angleOfRanker");
                        startPosition.X = startPosition.X + (float)(distanceFromPalletMM * Math.Cos(Math.PI + (double.Parse(sTmp[0], nfi) / 180.0 * Math.PI)));
                        startPosition.Y = startPosition.Y + (float)(distanceFromPalletMM * Math.Sin(Math.PI + (double.Parse(sTmp[0], nfi) / 180.0 * Math.PI)));
                        finalPosition = new Point3FR(p.approachPoint);
                        finalPosition.Z = p.centerWithRotationOnZAxis.Z + p.size.Z + 100;
                        finalPosition.R = p.centerWithRotationOnZAxis.R;
                    }
                    else if (phase == 2) // to approach point (go down)
                    {
                        stepOfSingleMovement += this._velocity * 2;
                        startPosition = new Point3FR(p.approachPoint);
                        startPosition.R = p.centerWithRotationOnZAxis.R;
                        startPosition.Z = p.centerWithRotationOnZAxis.Z + p.size.Z + 100;
                        finalPosition = new Point3FR(p.approachPoint);
                        finalPosition.R = p.centerWithRotationOnZAxis.R;
                    }
                    else if (phase == 3) // approach - place
                    {
                        stepOfSingleMovement += this._velocity * 2;
                        startPosition = new Point3FR(p.approachPoint);
                        startPosition.R = p.centerWithRotationOnZAxis.R;
                        finalPosition = p.centerWithRotationOnZAxis.clone();
                    }

                    double factor = step / 10000.0;
                    factor = atanPrecalculated[step / 10];
                    //factor = (step - 5000) /  1000;
                    //factor = Math.Atan(factor) / Math.PI * 2.0;
                    ////factor = factor * factor;
                    //factor = (factor + 1.0) / 2.0;
                    actualPosition = new Point3FR(startPosition.X + (finalPosition.X - startPosition.X) * factor, startPosition.Y + (finalPosition.Y - startPosition.Y) * factor, startPosition.Z + (finalPosition.Z - startPosition.Z) * factor, startPosition.R + (finalPosition.R - startPosition.R) * factor);
                    mMesh.substituteLastParallelepiped(p.size.X, p.size.Y, p.size.Z, actualPosition.X, actualPosition.Y, actualPosition.Z, actualPosition.R, p.color, p.name);

                }
                else
                {
                    actualPosition = p.centerWithRotationOnZAxis.clone();
                    mMesh.addParallelepiped(p.size.X, p.size.Y, p.size.Z, actualPosition.X, actualPosition.Y, actualPosition.Z, actualPosition.R, p.color, p.name);
                    stepOfSimulation++;
                    stepOfSingleMovement = 0;
                }

                if (stepOfSimulation == payloadList.Count)
                {
                    timerForSimulation.Stop();
                    stepOfSimulation = 0;
                }
            }
        }

        public void Paint(object sender, PaintEventArgs e)
        {
            if (null == sender) { return; }
            if (false == (sender is CSGL12Control)) { return; }

            CSGL12Control csgl12Control = (sender as CSGL12Control);
            GL gl = csgl12Control.GetGL();

            int clientWidth = csgl12Control.ClientRectangle.Width;
            int clientHeight = csgl12Control.ClientRectangle.Height;

            if (clientWidth <= 0)
            {
                clientWidth = 1;
            }

            if (clientHeight <= 0)
            {
                clientHeight = 1;
            }

            // Set the viewport 
            gl.glViewport(0, 0, clientWidth, clientHeight);

            // Clear the viewport
            gl.glClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            gl.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);

            // Basic drawing conditions
            gl.glEnable(GL.GL_DEPTH_TEST);
            gl.glDepthFunc(GL.GL_LEQUAL);
            gl.glEnable(GL.GL_CULL_FACE);
            gl.glCullFace(GL.GL_BACK);
            gl.glFrontFace(GL.GL_CCW);

            // PROJECTION matrix, typically for perspective correction or orthographic projection
            gl.glMatrixMode(GL.GL_PROJECTION);
            gl.glLoadIdentity();

            double aspectRatio = 1.0;

            if (0 != clientHeight)
            {
                aspectRatio = ((double)(clientWidth) / (double)(clientHeight));
            }

            double verticalFieldOfViewAngle = 60.0;

            gl.gluPerspective
            (
                verticalFieldOfViewAngle, // Field of view angle (Y angle; degrees)
                aspectRatio, // width/height
                0.1, // distance to near clipping plane
                64000.0 // distance to far clipping plane
            );

            // MODELVIEW matrix, typically used to transform individual models
            gl.glMatrixMode(GL.GL_MODELVIEW);
            gl.glLoadIdentity();

            // Preserve current matrix for the active matrix stack (in this case the MODELVIEW matrix)
            gl.glPushMatrix();

            if (mViewAltitudeDegreesVelocity != 0.0)
            {
                if (mViewAltitudeDegrees > 70.0)
                {
                    mViewAltitudeDegrees = 70.0;
                    mViewAltitudeDegreesVelocity *= -1.0;
                }
                else if (mViewAltitudeDegrees < -70.0)
                {
                    mViewAltitudeDegrees = -70.0;
                    mViewAltitudeDegreesVelocity *= -1.0;
                }

                mViewAzimuthDegrees += mViewAzimuthDegreesVelocity * csgl12Control.GetPreviousFrameDurationSeconds();
                mViewAltitudeDegrees += mViewAltitudeDegreesVelocity * csgl12Control.GetPreviousFrameDurationSeconds();
            }

            Vector3f from =
                new Vector3f
                (
                    (float)(mViewDistance * Math.Cos(mViewAltitudeDegrees * (Math.PI / 180.0)) * Math.Sin(mViewAzimuthDegrees * (Math.PI / 180.0))),
                    (float)(mViewDistance * Math.Sin(mViewAltitudeDegrees * (Math.PI / 180.0))),
                    (float)(mViewDistance * Math.Cos(mViewAltitudeDegrees * (Math.PI / 180.0)) * Math.Cos(mViewAzimuthDegrees * (Math.PI / 180.0)))
                );
            Vector3f to = new Vector3f(0.0f, 0.0f, 0.0f);
            Vector3f up = new Vector3f(0.0f, 1.0f, 0.0f);

            Matrix4x4f camera = Matrix4x4f.LookAt(from, to, up);

            float[] matrix = new float[16];

            matrix[0] = camera.m11;
            matrix[1] = camera.m21;
            matrix[2] = camera.m31;
            matrix[3] = 0.0f;

            matrix[4] = camera.m12;
            matrix[5] = camera.m22;
            matrix[6] = camera.m32;
            matrix[7] = 0.0f;

            matrix[8] = camera.m13;
            matrix[9] = camera.m23;
            matrix[10] = camera.m33;
            matrix[11] = 0.0f;

            matrix[12] = camera.m14;
            matrix[13] = camera.m24;
            matrix[14] = camera.m34;
            matrix[15] = 1.0f;

            gl.glMultMatrixf(matrix);

            if (mShaderProgramSelected != null)
            {
                mShaderProgramSelected.DemonstrateModificationOfVariables(gl, csgl12Control.GetPreviousFrameStartTimeSeconds(), csgl12Control.GetPreviousFrameDurationSeconds());
                mShaderProgramSelected.Select(gl);
            }

            // Draw model(s), using active texture or shader
            mMesh.Draw(gl);

            // If we used a shader, disable it now...
            if (true == gl.bglUseProgramObjectARB)
            {
                ShaderProgram.ShaderProgram_Select(gl, 0);
            }

            // Restore the previously-preserved matrix for the active matrix stack (in this case the MODELVIEW matrix)
            gl.glPopMatrix();

            // Demonstrate drawing text to a GDI+ Bitmap and then copying to
            // an OpenGL texture.
            DemonstrateDrawingTextToAGDIBitmapAndCopyingToAnOpenGLTexture(csgl12Control, gl);

            // Flush all the current rendering and flip the back buffer to the front.
            gl.wglSwapBuffers(csgl12Control.GetHDC());
        }

        public void DemonstrateDrawingTextToAGDIBitmapAndCopyingToAnOpenGLTexture(CSGL12Control csgl12Control, GL gl)
        {
            bool updateOverlayImage = false;

            // The following code only enables an update of the Bitmap
            // and OpenGL texture every 64 frames, thus avoiding the
            // slowdown of performing updates every single frame.
            // HOWEVER, updating the Bitmap and OpenGL texture can be
            // done EVERY frame with acceptable speed.
            // Updates should be limited to once per frame, but the
            // logic to trigger updates can be based on when the relevant
            // text changes.

            // 2012 April : Update the texture every frame; Will likely be fast for most people...
            if ((csgl12Control.GetTotalFramesDrawn() % 1) == 0)
            {
                updateOverlayImage = true;
            }

            bool showOverlayImage = true;

            if (true == updateOverlayImage)
            {
                using (Graphics g = Graphics.FromImage(mHUDBitmap))
                {
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    g.Clear(Color.FromArgb(0, Color.White));


                    //g.FillEllipse(mBrush1, new Rectangle(0, 0, 256, 256));
                    //g.FillEllipse(mBrush1, new Rectangle(256, 256, 256, 256));

                    PointF center = new PointF(0.5f * (256.0f + 0.0f), 0.5f * (256.0f + 0.0f));
                    PointF displacement = new PointF();
                    double fraction = csgl12Control.GetTotalElapsedTimeSeconds() * 0.1;
                    displacement.X = 128.0f * (float)Math.Cos(2.0 * Math.PI * fraction);
                    displacement.Y = 128.0f * (float)Math.Sin(2.0 * Math.PI * fraction);

                    //g.DrawLine(mPen1, new Point(0,0), new PointF(100.0f, 0.0f));
                    //g.DrawLine(mPen1, new Point(0,0), new PointF(0.0f, 100.0f));

                    PointF carat = new PointF(0.0f, 0.0f);

                    String text = "";

                    text = "C# OpenGL (CSGL)";
                    //g.DrawString(text, mFont1, Brushes.Black, carat);
                    carat.Y += mFont1.GetHeight();

                    text = "Здравствуйте";
                    //g.DrawString(text, mFont2, Brushes.Black, carat);
                    carat.Y += mFont2.GetHeight();

                    text = "γεια σου";
                    //g.DrawString(text, mFont2, Brushes.Black, carat);
                    carat.Y += mFont2.GetHeight();

                    text = "مرحبا";
                    //g.DrawString(text, mFont2, Brushes.Black, carat);
                    carat.Y += mFont2.GetHeight();

                    text = "שלום";
                    //g.DrawString(text, mFont2, Brushes.Black, carat);
                    carat.Y += mFont2.GetHeight();




                    carat.Y += 64.0f;

                    text = "Shift + 0: Save BMP,PNG,JPG,GIF";
                    //g.DrawString(text, mFont4, Brushes.Black, carat);
                    carat.Y += mFont4.GetHeight();

                    text = "1,2,3,4  : Switch shader program";
                    //g.DrawString(text, mFont4, Brushes.Black, carat);
                    carat.Y += mFont4.GetHeight();

                    carat.Y += 12.0f;

                    text = "Text : GDI+ on 512*512 Bitmap.";
                    //g.DrawString(text, mFont4, Brushes.Black, carat);
                    carat.Y += mFont4.GetHeight();

                    text = "Bitmap copied to OpenGL texture.";
                    //g.DrawString(text, mFont4, Brushes.Black, carat);
                    carat.Y += mFont4.GetHeight();

                    text = "Texture update happens every frame.";
                    //g.DrawString(text, mFont4, Brushes.Black, carat);
                    carat.Y += mFont4.GetHeight();

                    text = "(Modify code to update less often if too slow.)";
                    //g.DrawString(text, this.mFont4, Brushes.Black, carat);
                    carat.Y += this.mFont4.GetHeight();

                    carat.Y += 12.0f;

                    text = String.Format("Frame:{0}", csgl12Control.GetTotalFramesDrawn());
                    text += " ";
                    text += String.Format("Time:{0:f2}", csgl12Control.GetTotalElapsedTimeSeconds());

                    //double previousFrameDurationSeconds = 
                    //    csgl12Control.GetPreviousFrameDurationSeconds();

                    //if (previousFrameDurationSeconds > 1.0e-10)
                    //{
                    //    double framesPerSecondOverall =
                    //        1.0 / previousFrameDurationSeconds;

                    //    text += " ";
                    //    text += String.Format("FPS:{0:f2}", framesPerSecondOverall );
                    //}
                    //g.DrawString(text, mFont3, Brushes.Black, carat);

                    carat.Y += mFont3.GetHeight();

                    text = "GL_VERSION : " + gl.glGetString( GL.GL_VERSION );
                    //g.DrawString(text, mFont3, Brushes.Black, carat);
                    carat.Y += mFont3.GetHeight();

                    text = "GL_VENDOR : " + gl.glGetString(GL.GL_VENDOR);
                    //g.DrawString(text, mFont3, Brushes.Black, carat);
                    carat.Y += mFont3.GetHeight();

                    text = "GL_EXTENSIONS : " + gl.glGetString(GL.GL_EXTENSIONS);
                    //g.DrawString(text, mFont3, Brushes.Black, carat);
                    carat.Y += mFont3.GetHeight();


                    text = String.Format("Frame:{0}", csgl12Control.GetTotalFramesDrawn());
                    text += " ";
                    text += String.Format("Time:{0:f2}", csgl12Control.GetTotalElapsedTimeSeconds());

                    double previousFrameDurationSeconds =
                        csgl12Control.GetPreviousFrameDurationSeconds();

                    if (previousFrameDurationSeconds > 1.0e-10)
                    {
                        double framesPerSecondOverall =
                            1.0 / previousFrameDurationSeconds;

                        text += " ";
                        text += String.Format("FPS:{0:f2}", framesPerSecondOverall);
                    }
                    carat.Y = csgl12Control.Height - mFont3.GetHeight()-5;
                    //g.DrawString(text, mFont3, Brushes.Black, carat);

                }

                mHUDTexture.UpdateTextureWithBitmapData(gl, mHUDBitmap);
            }

            if (true == showOverlayImage)
            {
                CSGL12Support.SupportDrawTextureImageUnrotatedAndOrthographically
                (
                    gl,
                    csgl12Control.ClientSize.Width,
                    csgl12Control.ClientSize.Height,
                    mHUDTexture,
                    0,
                    0, // i.e., 0 == draw TOP of image at TOP of viewport, Y-axis points DOWN
                    mHUDTexture.GetWidth(),  // glControl.ClientSize.Width, // mHUDTexture.GetWidth(),
                    mHUDTexture.GetHeight() // glControl.ClientSize.Height // mHUDTexture.GetHeight()
                );
            }
        }

        public void KeyDown(object sender, KeyEventArgs e)
        {
            if (null == sender) { return; }
            if (false == (sender is CSGL12Control)) { return; }

            CSGL12Control csgl12Control = (sender as CSGL12Control);
            GL gl = csgl12Control.GetGL();

            if (e.KeyCode == Keys.A)
            {
            }

            if (e.KeyCode == Keys.Z)
            {
            }

            //if (e.KeyCode == Keys.D1)
            //{
            //    mShaderProgramSelected = mShaderProgram1;
            //}
            //if (e.KeyCode == Keys.D2)
            //{
            //    mShaderProgramSelected = mShaderProgram2;
            //}
            //if (e.KeyCode == Keys.D3)
            //{
            //    mShaderProgramSelected = mShaderProgram3;
            //}
            //if (e.KeyCode == Keys.D4)
            //{
            //    mShaderProgramSelected = mShaderProgram4;
            //}




            // NOTE: The only way for cursor key events (up,down,left,right)
            // to make it to this function is for the main form to implement 
            // the following:
            //
            //   protected override bool ProcessDialogKey ( Keys keyData )
            //
            // and explicitly invoke this KeyDown() method with the 
            // an appropriately formed KeyEventArgs instance.

            if (e.KeyCode == Keys.Up)
            {
                mViewDistance -= 10.0;
            }
            if (e.KeyCode == Keys.Down)
            {
                mViewDistance += 10.0;
            }
            if (e.KeyCode == Keys.Left)
            {
                mViewAzimuthDegrees += 1.0;
            }
            if (e.KeyCode == Keys.Right)
            {
                mViewAzimuthDegrees -= 1.0;
            }




            // Save an image of the viewport (press Shift-0 (zero)).  The following
            // code writes out the viewport in the following image formats: BMP, PNG, GIF, JPG.

            // If you only want a single format, comment out the other file write commands.
            // BMP has no compression artifacts, but the file can be quite large.
            // PNG looks good, and supports 8-bit transparancy (good for textures, etc).
            // GIF looks bad unless you build the color table intelligently (there is a 
            //    neural network color table builder for GIF, in C#/.NET, that you can
            //    find on the Internet; perhaps Paint.NET uses that code); but GIF files
            //    can be quite small, and supports animation.
            // JPG looks good under most circumstances, and the file size can be quite small,
            //    but transparency is not supported.
            // So, for pixel-perfect images, where file size is not important, BMP might be appropriate.
            // For textures with transparency, PNG might be appropriate.
            // For good-looking images, and small file size, and use in Web pages, JPG might be appropriate.
            // For some purposes, with small file sizes, and use in Web pages, GIF might be appropriate.

            if ((e.KeyCode == Keys.D0) && (e.Shift == true))
            {
                DateTime now = DateTime.Now;

                String dateTimeString = String.Format("{0:d4}{1:d2}{2:d2}{3:d2}{4:d2}{5:d2}{6:d3}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond);

                String frameIndexString = String.Format("{0:d6}", csgl12Control.GetTotalFramesDrawn());

                String fileNameWithoutExtension = "screen" + "_" + dateTimeString + "_" + frameIndexString;

                CSGL12Support.SupportWriteViewportToImageFile(gl, fileNameWithoutExtension + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                CSGL12Support.SupportWriteViewportToImageFile(gl, fileNameWithoutExtension + ".png", System.Drawing.Imaging.ImageFormat.Png);
                CSGL12Support.SupportWriteViewportToImageFile(gl, fileNameWithoutExtension + ".gif", System.Drawing.Imaging.ImageFormat.Gif);
                CSGL12Support.SupportWriteViewportToImageFile(gl, fileNameWithoutExtension + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        public void KeyUp(object sender, KeyEventArgs e)
        {
        }

        public void MouseDown(object sender, MouseEventArgs e)
        {
            if (null == sender) { return; }
            if (false == (sender is CSGL12Control)) { return; }

            CSGL12Control csgl12Control = (sender as CSGL12Control);

            mMouseClientPositionStart = csgl12Control.PointToClient(Cursor.Position);

            mViewAzimuthDegreesStart = mViewAzimuthDegrees;
            mViewAltitudeDegreesStart = mViewAltitudeDegrees;


            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                mViewAzimuthDegreesVelocity = 0.0;
                mViewAltitudeDegreesVelocity = 0.0;
            }

            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                mViewAzimuthDegreesVelocity = 9.0;
                mViewAltitudeDegreesVelocity = 5.0;
            }
        }

        public void MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
            }
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (null == sender) { return; }
            if (false == (sender is CSGL12Control)) { return; }

            CSGL12Control csgl12Control = (sender as CSGL12Control);

            Point mouseClientPositionCurrent = csgl12Control.PointToClient(Cursor.Position);

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                double azimuth =
                    mViewAzimuthDegreesStart
                    - (360.0 / (double)(csgl12Control.Width + 1))
                    * (double)(mouseClientPositionCurrent.X - mMouseClientPositionStart.X);

                double altitude =
                    mViewAltitudeDegreesStart
                    + (180.0 / (double)(csgl12Control.Height + 1))
                    * (double)(mouseClientPositionCurrent.Y - mMouseClientPositionStart.Y);

                double epsilon = 0.05;

                if (azimuth < (-180 + epsilon)) { azimuth = (-180 + epsilon); }
                if (azimuth > (180 - epsilon)) { azimuth = (180 - epsilon); }

                if (altitude < (-90 + epsilon)) { altitude = (-90 + epsilon); }
                if (altitude > (90 - epsilon)) { altitude = (90 - epsilon); }

                mViewAzimuthDegrees = azimuth;
                mViewAltitudeDegrees = altitude;
            }
        }

        public void MouseWheel(object sender, MouseEventArgs e)
        {
            mViewDistance -= 0.1 * (double)e.Delta;
        }
    }

}
