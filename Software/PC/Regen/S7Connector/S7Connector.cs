using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sintec.Tool;
using System.Globalization;
using System.Runtime.InteropServices;
using Snap7;

namespace S7Connector
{
    public class S7Connector : VirtualConnectors.VirtualConnector
    {
        Registry reg = new Registry();
        String res = "";
        public static int decimalNumberOfMeasure = 2;  // 3,0347 -> 3,03
        panelForConnector p;

        public override void refreshLanguage()
        {
            p.lblIpCpu.Text = translate("lblIpCpu");
            p.lblRack.Text = translate("lblRack");
            p.lblSlot.Text = translate("lblSlot");
            p.lblDbNumber.Text = translate("lblDbNumber");
            p.lblPayloadMaxPayload.Text = translate("lblPayloadMaxPayload");
            p.lblCustomExtraData.Text = translate("lblCustomExtraData");
            p.lblCustomOtherInfo.Text = translate("lblCustomOtherInfo");
            p.chkAddInterlayerToPayload.Text = translate("chkAddInterlayerToPayload");
            p.lblDbOrganization.Text = translate("lblDbOrganization");
        }

        public override string getPreview(string workDataXmlString)
        {
            res = "";
            foreach (String s in VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "*"))
                res = res + s + "\n\r\n\r";
            return res;
        }
        public override string getIstruction(string workDataXmlString)
        {
            return translate("string_connectionParams");// "Definire i parametri di connessione nella sezione impostazioni e premere invia.";
        }
        public override void send(string workDataXmlString)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            Dictionary<string, byte> dictPayloadNameCode = new Dictionary<string, byte>();
            int res = 0;
            GetMacAddressFromIPAddress g = new GetMacAddressFromIPAddress();
            int timeToken = 5;
            while (!Sintec.Tool.NetworkTool.PingHost(p.getIp()) && timeToken > 0)
                timeToken--;
            String mac = g.GetMACAddressFromARP(p.getIp());
            if (String.IsNullOrEmpty(mac))
                MessageBox.Show(translate("string_ipNotConnected"));//"Ip non connesso al pc");
            else
            {
                if (isLicencedMacFromRegistry(new List<String>() { mac.ToUpper() }))
                {        
                    int rack = p.getRack();
                    int slot = p.getSlot();
                    String ipAddress = p.getIp();
                    String dbNumber = p.getDb();

                    //libnodave.daveOSserialType fds;
                    //libnodave.daveInterface di;
                    //libnodave.daveConnection dc;
                    //fds.rfd = libnodave.openSocket(102, ipAddress);
                    //fds.wfd = fds.rfd;

                    S7Client Client = new S7Client();
                    int Result = Client.ConnectTo(ipAddress, rack, slot);

                    if (Result == 0)
                    //if (fds.rfd > 0)
                    {
                        //di = new libnodave.daveInterface(fds, dbNumber, 0, libnodave.daveProtoISOTCP, libnodave.daveSpeed187k);
                        //di.setTimeout(1000000);
                        //dc = new libnodave.daveConnection(di, 0, rack, slot);
                        //if (0 == dc.connectPLC())
                        {
                            byte[] bytes = new byte[500];
                            bytes[0] = byte.Parse("0");
                            bytes[1] = byte.Parse("1");

                            double altezzaPallet = 0;
                            double altezzaScatola = 0;

                            //PALLET
                            List<String> sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_size");
                            Point3F data = Point3F.from(sTmp[0]);
                            Point3F palletSize = data;
                            double altezzaPanel = 0;
                            double altezzaInterfalda = 0;
                            altezzaPallet = data.Z;
                            sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_extraBorder");
                            Point2F extraBorder = Point2F.from(sTmp[0]);
                            byte[] dTmp = BitConverter.GetBytes((float)Math.Round(data.X, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[2] = dTmp[0];
                            bytes[3] = dTmp[1];
                            bytes[4] = dTmp[2];
                            bytes[5] = dTmp[3];
                            dTmp = BitConverter.GetBytes((float)Math.Round(data.Y, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[6] = dTmp[0];
                            bytes[7] = dTmp[1];
                            bytes[8] = dTmp[2];
                            bytes[9] = dTmp[3];
                            dTmp = BitConverter.GetBytes((float)Math.Round(data.Z, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[10] = dTmp[0];
                            bytes[11] = dTmp[1];
                            bytes[12] = dTmp[2];
                            bytes[13] = dTmp[3];
                            dTmp = BitConverter.GetBytes((float)Math.Round(extraBorder.X, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[14] = dTmp[0];
                            bytes[15] = dTmp[1];
                            bytes[16] = dTmp[2];
                            bytes[17] = dTmp[3];
                            dTmp = BitConverter.GetBytes((float)Math.Round(extraBorder.Y, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[18] = dTmp[0];
                            bytes[19] = dTmp[1];
                            bytes[20] = dTmp[2];
                            bytes[21] = dTmp[3];
                            //dTmp = BitConverter.GetBytes(size.X).Reverse().ToArray();
                            bytes[22] = 0;
                            bytes[23] = 0;
                            bytes[24] = 0;
                            bytes[25] = 0;
                            res = Client.DBWrite(int.Parse(dbNumber.ToLower().Replace("db", "")), 0, 26, bytes);
                            //res = Client.DBWrite(int.Parse(dbNumber.ToLower().Replace("db", "")), 0, 26, bytes);
                            if (res != 0)
                            {
                                MessageBox.Show(translate("string_dataTrasferError") + ": " + res.ToString("0")); ;//"Problemi nella trasmissione dei dati: " + res.ToString("0"));
                                //p.startTimer();
                                Client.Disconnect(); //dc.disconnectPLC();
                                //libnodave.closeSocket(fds.rfd);
                                return;
                            }

                            //PANEL
                            dictPayloadNameCode.Add("panelOverPallet", 5);
                            sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_panelOverPalletThickness");
                            altezzaPanel = double.Parse(sTmp[0], nfi);
                            //payload code
                            bytes[0] = 0;
                            bytes[1] = 5;
                            dTmp = BitConverter.GetBytes((float)Math.Round(data.X, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[2] = dTmp[0];
                            bytes[3] = dTmp[1];
                            bytes[4] = dTmp[2];
                            bytes[5] = dTmp[3];
                            dTmp = BitConverter.GetBytes((float)Math.Round(data.Y, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[6] = dTmp[0];
                            bytes[7] = dTmp[1];
                            bytes[8] = dTmp[2];
                            bytes[9] = dTmp[3];
                            dTmp = BitConverter.GetBytes((float)Math.Round(altezzaPanel, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[10] = dTmp[0];
                            bytes[11] = dTmp[1];
                            bytes[12] = dTmp[2];
                            bytes[13] = dTmp[3];
                            dTmp = BitConverter.GetBytes((float)Math.Round(0.0F, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[14] = dTmp[0];
                            bytes[15] = dTmp[1];
                            bytes[16] = dTmp[2];
                            bytes[17] = dTmp[3];
                            dTmp = BitConverter.GetBytes((float)Math.Round(0.0F, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[18] = dTmp[0];
                            bytes[19] = dTmp[1];
                            bytes[20] = dTmp[2];
                            bytes[21] = dTmp[3];
                            //dTmp = BitConverter.GetBytes(size.X).Reverse().ToArray();
                            bytes[22] = 0;
                            bytes[23] = 0;
                            bytes[24] = 0;
                            bytes[25] = 0;
                            bytes[26] = 0;
                            bytes[27] = 0;
                            bytes[28] = 0;
                            bytes[29] = 0;
                            bytes[30] = bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_panelOverPallet")[0]) ? (byte)1 : (byte)0;
                            bytes[31] = 0;
                            bytes[32] = 0;
                            bytes[33] = 0;
                            res = Client.DBWrite(int.Parse(dbNumber.ToLower().Replace("db", "")), 162, 34, bytes);
                            if (res != 0)
                            {
                                MessageBox.Show(translate("string_dataTrasferError") + ": " + res.ToString("0")); ;//"Problemi nella trasmissione dei dati: " + res.ToString("0"));
                                //p.startTimer();
                                Client.Disconnect(); //dc.disconnectPLC();
                                //libnodave.closeSocket(fds.rfd);
                                return;
                            }


                            //INTERLAYER
                            dictPayloadNameCode.Add("interLayer", 6);
                            sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_interLayerThickness");
                            altezzaInterfalda = double.Parse(sTmp[0], nfi);
                            //payload code
                            bytes[0] = 0;
                            bytes[1] = 6;
                            dTmp = BitConverter.GetBytes((float)Math.Round(data.X, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[2] = dTmp[0];
                            bytes[3] = dTmp[1];
                            bytes[4] = dTmp[2];
                            bytes[5] = dTmp[3];
                            dTmp = BitConverter.GetBytes((float)Math.Round(data.Y, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[6] = dTmp[0];
                            bytes[7] = dTmp[1];
                            bytes[8] = dTmp[2];
                            bytes[9] = dTmp[3];
                            dTmp = BitConverter.GetBytes((float)Math.Round(altezzaInterfalda, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[10] = dTmp[0];
                            bytes[11] = dTmp[1];
                            bytes[12] = dTmp[2];
                            bytes[13] = dTmp[3];
                            dTmp = BitConverter.GetBytes((float)Math.Round(0.0F, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[14] = dTmp[0];
                            bytes[15] = dTmp[1];
                            bytes[16] = dTmp[2];
                            bytes[17] = dTmp[3];
                            dTmp = BitConverter.GetBytes((float)Math.Round(0.0F, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[18] = dTmp[0];
                            bytes[19] = dTmp[1];
                            bytes[20] = dTmp[2];
                            bytes[21] = dTmp[3];
                            //dTmp = BitConverter.GetBytes(size.X).Reverse().ToArray();
                            bytes[22] = 0;
                            bytes[23] = 0;
                            bytes[24] = 0;
                            bytes[25] = 0;
                            bytes[26] = 0;
                            bytes[27] = 0;
                            bytes[28] = 0;
                            bytes[29] = 0;
                            byte b = 0;
                            b = bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_interLayer")[0]) ? (byte)1 : (byte)0;
                            for (int i = 0; i < 7; i++)
                                if (VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i.ToString() + "_interlayer").Count>0)
                                    b = (byte)(b + (int)(Math.Pow(2.0, (i + 1)) * (bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i.ToString() + "_interlayer")[0]) ? 1 : 0)));
                            bytes[30] = b;
                            b = 0;
                            for (int i = 0; i < 8; i++)
                                if (VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + (i + 7).ToString() + "_interlayer").Count > 0)
                                    b = (byte)(b + (int)(Math.Pow(2.0, (i)) * (bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + (i+7).ToString() + "_interlayer")[0]) ? 1 : 0)));
                            bytes[31] = b;
                            b = 0;
                            for (int i = 0; i < 8; i++)
                                if (VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + (i + 15).ToString() + "_interlayer").Count > 0)
                                    b = (byte)(b + (int)(Math.Pow(2.0, (i)) * (bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + (i + 15).ToString() + "_interlayer")[0]) ? 1 : 0)));
                            bytes[32] = b;
                            b = 0;
                            for (int i = 0; i < 8; i++)
                                if (VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + (i + 23).ToString() + "_interlayer").Count > 0)
                                    b = (byte)(b + (int)(Math.Pow(2.0, (i)) * (bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + (i + 23).ToString() + "_interlayer")[0]) ? 1 : 0)));
                            bytes[33] = b;
                            res = Client.DBWrite(int.Parse(dbNumber.ToLower().Replace("db", "")), 196, 34, bytes);
                            if (res != 0)
                            {
                                MessageBox.Show(translate("string_dataTrasferError") + ": " + res.ToString("0")); ;//"Problemi nella trasmissione dei dati: " + res.ToString("0"));
                                //p.startTimer();
                                Client.Disconnect(); //dc.disconnectPLC();
                                //libnodave.closeSocket(fds.rfd);
                                return;
                            }


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

                                    //payload code
                                    bytes[0] = 0;
                                    bytes[1] = (byte)(shift + 10);
                                    dTmp = BitConverter.GetBytes((float)Math.Round(size.X, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[2] = dTmp[0];
                                    bytes[3] = dTmp[1];
                                    bytes[4] = dTmp[2];
                                    bytes[5] = dTmp[3];
                                    dTmp = BitConverter.GetBytes((float)Math.Round(size.Y, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[6] = dTmp[0];
                                    bytes[7] = dTmp[1];
                                    bytes[8] = dTmp[2];
                                    bytes[9] = dTmp[3];
                                    dTmp = BitConverter.GetBytes((float)Math.Round(size.Z, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[10] = dTmp[0];
                                    bytes[11] = dTmp[1];
                                    bytes[12] = dTmp[2];
                                    bytes[13] = dTmp[3];
                                    bytes[14] = 0;
                                    bytes[15] = 0;
                                    bytes[16] = 0;
                                    bytes[17] = 0;
                                    //dTmp = BitConverter.GetBytes((float)Math.Round(rightSurplus, decimalNumberOfMeasure)).Reverse().ToArray();
                                    //bytes[14] = dTmp[0];
                                    //bytes[15] = dTmp[1];
                                    //bytes[16] = dTmp[2];
                                    //bytes[17] = dTmp[3];
                                    bytes[18] = 0;
                                    bytes[19] = 0;
                                    bytes[20] = 0;
                                    bytes[21] = 0;
                                    bytes[22] = 0;
                                    bytes[23] = 0;
                                    bytes[24] = 0;
                                    bytes[25] = 0;
                                    bytes[26] = 0;
                                    bytes[27] = 0;
                                    bytes[28] = 0;
                                    bytes[29] = 0;
                                    bytes[30] = 0;
                                    bytes[31] = 0;
                                    bytes[32] = 0;
                                    bytes[33] = 0;
                                    res = Client.DBWrite(int.Parse(dbNumber.ToLower().Replace("db", "")), (shift * 34) + 26, 34, bytes);
                                    if (res != 0)
                                    {
                                        MessageBox.Show(translate("string_dataTrasferError") + ": " + res.ToString("0")); ;//"Problemi nella trasmissione dei dati: " + res.ToString("0"));
                                        p.startTimer();
                                        Client.Disconnect(); //dc.disconnectPLC();
                                        //libnodave.closeSocket(fds.rfd);
                                        return;
                                    }
                                }
                            }

                            //TOOLDATA
                            sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "payloadStrategy0_toolData");
                            //String tooldata = (sTmp[0]);
                            //dTmp[0] = byte.Parse(sTmp[0]);
                            bytes[0] = 0;
                            bytes[1] = 0;
                            res = Client.DBWrite(int.Parse(dbNumber.ToLower().Replace("db", "")), 230, 1, bytes);
                            if (res != 0)
                            {
                                MessageBox.Show(translate("string_dataTrasferError") + ": " + res.ToString("0")); ;//"Problemi nella trasmissione dei dati: " + res.ToString("0"));
                                p.startTimer();
                                Client.Disconnect(); //dc.disconnectPLC();
                                //libnodave.closeSocket(fds.rfd); 
                                return;
                            }

                            //EXTRADATA
                            Snap7.S7.SetStringAt(bytes, 0, 36, p.getExtraData());
                            res = Client.DBWrite(int.Parse(dbNumber.ToLower().Replace("db", "")), 232, 38, bytes);
                            if (res != 0)
                            {
                                MessageBox.Show(translate("string_dataTrasferError") + ": " + res.ToString("0")); ;//"Problemi nella trasmissione dei dati: " + res.ToString("0"));
                                p.startTimer();
                                Client.Disconnect(); //dc.disconnectPLC();
                                //libnodave.closeSocket(fds.rfd); 
                                return;
                            }

                            //OTHERINFO
                            Snap7.S7.SetStringAt(bytes, 0, 36, p.getOtherInfo());
                            res = Client.DBWrite(int.Parse(dbNumber.ToLower().Replace("db", "")), 278, 38, bytes);
                            if (res != 0)
                            {
                                MessageBox.Show(translate("string_dataTrasferError") + ": " + res.ToString("0")); ;//"Problemi nella trasmissione dei dati: " + res.ToString("0"));
                                p.startTimer();
                                Client.Disconnect(); //dc.disconnectPLC();
                                //libnodave.closeSocket(fds.rfd);
                                return;
                            }

                            //PLACEMENT SETTINGS
                            sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "payloadsPlaced");
                            int tmpI = (sTmp.Count);
                            dTmp = BitConverter.GetBytes(tmpI).ToArray();
                            bytes[0] = dTmp[1];
                            bytes[1] = dTmp[0];
                            sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "payloadGroups");
                            tmpI = (sTmp.Count);
                            dTmp = BitConverter.GetBytes(tmpI).ToArray();
                            bytes[2] = dTmp[1];
                            bytes[3] = dTmp[0];
                            sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layers");
                            tmpI = (sTmp.Count);
                            dTmp = BitConverter.GetBytes(tmpI).ToArray();
                            bytes[4] = dTmp[1];
                            bytes[5] = dTmp[0];
                            res = Client.DBWrite(int.Parse(dbNumber.ToLower().Replace("db", "")), 272, 6, bytes);
                            if (res != 0)
                            {
                                MessageBox.Show(translate("string_dataTrasferError") + ": " + res.ToString("0")); ;//"Problemi nella trasmissione dei dati: " + res.ToString("0"));
                                p.startTimer();
                                Client.Disconnect(); //dc.disconnectPLC();
                                //libnodave.closeSocket(fds.rfd);
                                return;
                            }


                            int placingNumber = 0;
                            double altezzaInterfaldeEPannello = 0;
                            if (bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_panelOverPallet")[0]))
                            {
                                altezzaInterfaldeEPannello += altezzaPanel;
                                if (p.getAddInterlayerAndPanelOnPayloadsList())
                                {
                                    //payload code
                                    bytes[0] = 0;
                                    bytes[1] = dictPayloadNameCode["panelOverPallet"];
                                    //center position
                                    dTmp = BitConverter.GetBytes((float)Math.Round(palletSize.X / 2.0, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[2] = dTmp[0];
                                    bytes[3] = dTmp[1];
                                    bytes[4] = dTmp[2];
                                    bytes[5] = dTmp[3];
                                    dTmp = BitConverter.GetBytes((float)Math.Round(palletSize.Y / 2.0, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[6] = dTmp[0];
                                    bytes[7] = dTmp[1];
                                    bytes[8] = dTmp[2];
                                    bytes[9] = dTmp[3];
                                    dTmp = BitConverter.GetBytes((float)Math.Round(palletSize.Z + altezzaInterfaldeEPannello, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[10] = dTmp[0];
                                    bytes[11] = dTmp[1];
                                    bytes[12] = dTmp[2];
                                    bytes[13] = dTmp[3];
                                    dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[14] = dTmp[0];
                                    bytes[15] = dTmp[1];
                                    bytes[16] = dTmp[2];
                                    bytes[17] = dTmp[3];
                                    //Approach
                                    dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[18] = dTmp[0];
                                    bytes[19] = dTmp[1];
                                    bytes[20] = dTmp[2];
                                    bytes[21] = dTmp[3];
                                    dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[22] = dTmp[0];
                                    bytes[23] = dTmp[1];
                                    bytes[24] = dTmp[2];
                                    bytes[25] = dTmp[3];
                                    dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[26] = dTmp[0];
                                    bytes[27] = dTmp[1];
                                    bytes[28] = dTmp[2];
                                    bytes[29] = dTmp[3];
                                    //place number
                                    bytes[30] = (byte)0;
                                    bytes[31] = (byte)0;
                                    //group number
                                    bytes[32] = (byte)0;
                                    bytes[33] = (byte)0;
                                    //layer number
                                    bytes[34] = (byte)0;
                                    bytes[35] = (byte)0;
                                    //on a group of
                                    bytes[36] = (byte)0;
                                    bytes[37] = (byte)0;
                                    //on a layer of
                                    bytes[38] = (byte)0;
                                    bytes[39] = (byte)0;

                                    res = Client.DBWrite(int.Parse(dbNumber.ToLower().Replace("db", "")), (placingNumber++ * 40) + 318, 40, bytes);
                                    if (res != 0)
                                    {
                                        MessageBox.Show(translate("string_dataTrasferError") + ": " + res.ToString("0")); ;//"Problemi nella trasmissione dei dati: " + res.ToString("0"));
                                        p.startTimer();
                                        Client.Disconnect(); //dc.disconnectPLC();
                                        //libnodave.closeSocket(fds.rfd);
                                        return;
                                    }
                                }
                            }

                            if (bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_interLayer")[0]))
                            {
                                altezzaInterfaldeEPannello += altezzaInterfalda;
                                if (p.getAddInterlayerAndPanelOnPayloadsList())
                                {
                                    //payload code
                                    bytes[0] = 0;
                                    bytes[1] = dictPayloadNameCode["interLayer"];
                                    //center position
                                    dTmp = BitConverter.GetBytes((float)Math.Round(palletSize.X / 2.0, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[2] = dTmp[0];
                                    bytes[3] = dTmp[1];
                                    bytes[4] = dTmp[2];
                                    bytes[5] = dTmp[3];
                                    dTmp = BitConverter.GetBytes((float)Math.Round(palletSize.Y / 2.0, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[6] = dTmp[0];
                                    bytes[7] = dTmp[1];
                                    bytes[8] = dTmp[2];
                                    bytes[9] = dTmp[3];
                                    dTmp = BitConverter.GetBytes((float)Math.Round(palletSize.Z + altezzaInterfaldeEPannello, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[10] = dTmp[0];
                                    bytes[11] = dTmp[1];
                                    bytes[12] = dTmp[2];
                                    bytes[13] = dTmp[3];
                                    dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[14] = dTmp[0];
                                    bytes[15] = dTmp[1];
                                    bytes[16] = dTmp[2];
                                    bytes[17] = dTmp[3];
                                    //Approach
                                    dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[18] = dTmp[0];
                                    bytes[19] = dTmp[1];
                                    bytes[20] = dTmp[2];
                                    bytes[21] = dTmp[3];
                                    dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[22] = dTmp[0];
                                    bytes[23] = dTmp[1];
                                    bytes[24] = dTmp[2];
                                    bytes[25] = dTmp[3];
                                    dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[26] = dTmp[0];
                                    bytes[27] = dTmp[1];
                                    bytes[28] = dTmp[2];
                                    bytes[29] = dTmp[3];
                                    //place number
                                    bytes[30] = (byte)0;
                                    bytes[31] = (byte)0;
                                    //group number
                                    bytes[32] = (byte)0;
                                    bytes[33] = (byte)0;
                                    //layer number
                                    bytes[34] = (byte)0;
                                    bytes[35] = (byte)0;
                                    //on a group of
                                    bytes[36] = (byte)0;
                                    bytes[37] = (byte)0;
                                    //on a layer of
                                    bytes[38] = (byte)0;
                                    bytes[39] = (byte)0;

                                    res = Client.DBWrite(int.Parse(dbNumber.ToLower().Replace("db", "")), (placingNumber++ * 40) + 318, 40, bytes);
                                    if (res != 0)
                                    {
                                        MessageBox.Show(translate("string_dataTrasferError") + ": " + res.ToString("0")); ;//"Problemi nella trasmissione dei dati: " + res.ToString("0"));
                                        p.startTimer();
                                        Client.Disconnect(); //dc.disconnectPLC();
                                        //libnodave.closeSocket(fds.rfd);
                                        return;
                                    }
                                }
                            }

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
                                                                        pgDataCatching.Z + (altezzaInterfaldeEPannello + altezzaPallet + ((i+1) * altezzaScatola)),
                                                                        (((pgDataCatching.R + (ppDataQuadrant * 90.0)) + 90) % 360.0) - 90.0);
                                        tmpP.pgDataApproach = pgDataApproach;
                                        tmpP.pgDataProgressive = pgDataProgressive;
                                        tmpP.payloadCode = dictPayloadNameCode[VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_payloadGroup" + j + "_payload" + k + "_name_id")[0]];
                                        sortedLp.Add(pgDataProgressive, tmpP);
                                    }
                                }
                                altezzaInterfaldeEPannello += bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i + "_interlayer")[0]) ? altezzaInterfalda : 0.0;
                                if (sortedLp.Count > p.getMaxPayloadPlacedNumber())
                                    MessageBox.Show(translate("string_placeblePayload") + " " + sortedLp.Count + " " + translate("string_butCanTrasferOnly") + " " + p.getMaxPayloadPlacedNumber()); //"I payload piazzabili sono " + sortedLp.Count + " ma ne verranno trasferiti solo " + p.getMaxPayloadPlacedNumber());
                                for (int j = 0; j < Math.Min(sortedLp.Count, p.getMaxPayloadPlacedNumber()); j++)
                                {
                                    PayloadToWrite ptw = sortedLp.Values[j];
                                    //foreach (PayloadToWrite ptw in sortedLp.Values)
                                    //{
                                    //PLACEMENTS
                                    //payload code
                                    bytes[0] = 0;
                                    bytes[1] = (byte)ptw.payloadCode;
                                    //center position
                                    dTmp = BitConverter.GetBytes((float)Math.Round(ptw.pgDataCenter.X, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[2] = dTmp[0];
                                    bytes[3] = dTmp[1];
                                    bytes[4] = dTmp[2];
                                    bytes[5] = dTmp[3];
                                    dTmp = BitConverter.GetBytes((float)Math.Round(ptw.pgDataCenter.Y, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[6] = dTmp[0];
                                    bytes[7] = dTmp[1];
                                    bytes[8] = dTmp[2];
                                    bytes[9] = dTmp[3];
                                    dTmp = BitConverter.GetBytes((float)Math.Round(ptw.pgDataCenter.Z, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[10] = dTmp[0];
                                    bytes[11] = dTmp[1];
                                    bytes[12] = dTmp[2];
                                    bytes[13] = dTmp[3];
                                    dTmp = BitConverter.GetBytes((float)Math.Round(ptw.pgDataCenter.R, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[14] = dTmp[0];
                                    bytes[15] = dTmp[1];
                                    bytes[16] = dTmp[2];
                                    bytes[17] = dTmp[3];
                                    //Approach
                                    dTmp = BitConverter.GetBytes((float)Math.Round(ptw.pgDataApproach.X, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[18] = dTmp[0];
                                    bytes[19] = dTmp[1];
                                    bytes[20] = dTmp[2];
                                    bytes[21] = dTmp[3];
                                    dTmp = BitConverter.GetBytes((float)Math.Round(ptw.pgDataApproach.Y, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[22] = dTmp[0];
                                    bytes[23] = dTmp[1];
                                    bytes[24] = dTmp[2];
                                    bytes[25] = dTmp[3];
                                    dTmp = BitConverter.GetBytes((float)Math.Round(ptw.pgDataApproach.Z, decimalNumberOfMeasure)).Reverse().ToArray();
                                    bytes[26] = dTmp[0];
                                    bytes[27] = dTmp[1];
                                    bytes[28] = dTmp[2];
                                    bytes[29] = dTmp[3];
                                    //place number
                                    bytes[30] = (byte)Math.Round((placingNumber + 1) / 256.0, 0);
                                    bytes[31] = (byte)((placingNumber + 1) % 256);
                                    //group number
                                    bytes[32] = (byte)Math.Round(ptw.groupNumber / 256.0, 0);
                                    bytes[33] = (byte)(ptw.groupNumber % 256);
                                    //layer number
                                    bytes[34] = (byte)Math.Round(ptw.layerNumber / 256.0, 0);
                                    bytes[35] = (byte)(ptw.layerNumber % 256);
                                    //on a group of
                                    bytes[36] = (byte)Math.Round(ptw.onGroupOf / 256.0, 0);
                                    bytes[37] = (byte)(ptw.onGroupOf % 256);
                                    //on a layer of
                                    bytes[38] = (byte)Math.Round(ptw.onLayerOf / 256.0, 0);
                                    bytes[39] = (byte)(ptw.onLayerOf % 256);

                                    res = Client.DBWrite(int.Parse(dbNumber.ToLower().Replace("db", "")), (placingNumber++ * 40) + 318, 40, bytes);
                                    if (res != 0)
                                    {
                                        MessageBox.Show(translate("string_dataTrasferError") + ": " + res.ToString("0")); // "Problemi nella trasmissione dei dati: " + res.ToString("0"));
                                        p.startTimer();
                                        Client.Disconnect(); //dc.disconnectPLC();
                                        //libnodave.closeSocket(fds.rfd);
                                        return;
                                    }
                                }
                                
                                if (p.getAddInterlayerAndPanelOnPayloadsList())
                                {
                                    if (VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i.ToString() + "_interlayer").Count > 0)
                                    {
                                        if (bool.Parse(VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "layer" + i.ToString() + "_interlayer")[0]))
                                        {
                                            //payload code
                                            bytes[0] = 0;
                                            bytes[1] = dictPayloadNameCode["interLayer"];
                                            //center position
                                            dTmp = BitConverter.GetBytes((float)Math.Round(palletSize.X / 2.0, decimalNumberOfMeasure)).Reverse().ToArray();
                                            bytes[2] = dTmp[0];
                                            bytes[3] = dTmp[1];
                                            bytes[4] = dTmp[2];
                                            bytes[5] = dTmp[3];
                                            dTmp = BitConverter.GetBytes((float)Math.Round(palletSize.Y / 2.0, decimalNumberOfMeasure)).Reverse().ToArray();
                                            bytes[6] = dTmp[0];
                                            bytes[7] = dTmp[1];
                                            bytes[8] = dTmp[2];
                                            bytes[9] = dTmp[3];
                                            dTmp = BitConverter.GetBytes((float)Math.Round(altezzaInterfaldeEPannello + altezzaPallet + ((i + 1) * altezzaScatola), decimalNumberOfMeasure)).Reverse().ToArray();
                                            bytes[10] = dTmp[0];
                                            bytes[11] = dTmp[1];
                                            bytes[12] = dTmp[2];
                                            bytes[13] = dTmp[3];
                                            dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                                            bytes[14] = dTmp[0];
                                            bytes[15] = dTmp[1];
                                            bytes[16] = dTmp[2];
                                            bytes[17] = dTmp[3];
                                            //Approach
                                            dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                                            bytes[18] = dTmp[0];
                                            bytes[19] = dTmp[1];
                                            bytes[20] = dTmp[2];
                                            bytes[21] = dTmp[3];
                                            dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                                            bytes[22] = dTmp[0];
                                            bytes[23] = dTmp[1];
                                            bytes[24] = dTmp[2];
                                            bytes[25] = dTmp[3];
                                            dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                                            bytes[26] = dTmp[0];
                                            bytes[27] = dTmp[1];
                                            bytes[28] = dTmp[2];
                                            bytes[29] = dTmp[3];
                                            //place number
                                            bytes[30] = (byte)0;
                                            bytes[31] = (byte)0;
                                            //group number
                                            bytes[32] = (byte)0;
                                            bytes[33] = (byte)0;
                                            //layer number
                                            bytes[34] = (byte)0;
                                            bytes[35] = (byte)0;
                                            //on a group of
                                            bytes[36] = (byte)0;
                                            bytes[37] = (byte)0;
                                            //on a layer of
                                            bytes[38] = (byte)0;
                                            bytes[39] = (byte)0;

                                            res = Client.DBWrite(int.Parse(dbNumber.ToLower().Replace("db", "")), (placingNumber++ * 40) + 318, 40, bytes);
                                            if (res != 0)
                                            {
                                                MessageBox.Show(translate("string_dataTrasferError") + ": " + res.ToString("0")); ;//"Problemi nella trasmissione dei dati: " + res.ToString("0"));
                                                p.startTimer();
                                                Client.Disconnect(); //dc.disconnectPLC();
                                                //libnodave.closeSocket(fds.rfd);
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                            //payload code
                            bytes[0] = 255;  // -1 CHIUSURA
                            bytes[1] = 255;
                            //center position
                            dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[2] = dTmp[0];
                            bytes[3] = dTmp[1];
                            bytes[4] = dTmp[2];
                            bytes[5] = dTmp[3];
                            dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[6] = dTmp[0];
                            bytes[7] = dTmp[1];
                            bytes[8] = dTmp[2];
                            bytes[9] = dTmp[3];
                            dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[10] = dTmp[0];
                            bytes[11] = dTmp[1];
                            bytes[12] = dTmp[2];
                            bytes[13] = dTmp[3];
                            dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[14] = dTmp[0];
                            bytes[15] = dTmp[1];
                            bytes[16] = dTmp[2];
                            bytes[17] = dTmp[3];
                            //Approach
                            dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[18] = dTmp[0];
                            bytes[19] = dTmp[1];
                            bytes[20] = dTmp[2];
                            bytes[21] = dTmp[3];
                            dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[22] = dTmp[0];
                            bytes[23] = dTmp[1];
                            bytes[24] = dTmp[2];
                            bytes[25] = dTmp[3];
                            dTmp = BitConverter.GetBytes((float)Math.Round(0.0, decimalNumberOfMeasure)).Reverse().ToArray();
                            bytes[26] = dTmp[0];
                            bytes[27] = dTmp[1];
                            bytes[28] = dTmp[2];
                            bytes[29] = dTmp[3];
                            //place number
                            bytes[30] = (byte)0;
                            bytes[31] = (byte)0;
                            //group number
                            bytes[32] = (byte)0;
                            bytes[33] = (byte)0;
                            //layer number
                            bytes[34] = (byte)0;
                            bytes[35] = (byte)0;
                            //on a group of
                            bytes[36] = (byte)0;
                            bytes[37] = (byte)0;
                            //on a layer of
                            bytes[38] = (byte)0;
                            bytes[39] = (byte)0;

                            res = Client.DBWrite(int.Parse(dbNumber.ToLower().Replace("db", "")), (placingNumber++ * 40) + 318, 40, bytes);
                            if (res != 0)
                            {
                                MessageBox.Show(translate("string_dataTrasferError") + ": " + res.ToString("0")); ;//"Problemi nella trasmissione dei dati: " + res.ToString("0"));
                                p.startTimer();
                                Client.Disconnect(); //dc.disconnectPLC();
                                ////libnodave.closeSocket(fds.rfd);
                                return;
                            }
                            p.startTimer();
                        }
                        Client.Disconnect();
                        //Client.Disconnect(); //dc.disconnectPLC();
                        ////libnodave.closeSocket(fds.rfd);
                    }
                    else
                    {
                        MessageBox.Show(translate("string_nonPossoAprireConnecction") + " " + ipAddress);// "Couldn't open TCP connection to " + ipAddress);
                    }
                }
                else
                    MessageBox.Show(translate("string_licenceOutdatedOrNotValid"));// "Licenza non valida o scaduta");
            }
        }

        public void endTimer()
        {
            //libnodave.daveOSserialType fds;
            //libnodave.daveInterface di;
            //libnodave.daveConnection dc;
            int rack = p.getRack();
            int slot = p.getSlot();
            String ipAddress = p.getIp();
            String dbNumber = p.getDb();
            int res = -1;
            //fds.rfd = libnodave.openSocket(102, ipAddress);
            //fds.wfd = fds.rfd;
            //if (fds.rfd > 0)
            S7Client Client = new S7Client();
            int Result = Client.ConnectTo(ipAddress, rack, slot);

            if (Result == 0)
            {
                //di = new libnodave.daveInterface(fds, dbNumber, 0, libnodave.daveProtoISOTCP, libnodave.daveSpeed187k);
                //di.setTimeout(10 * 1000000); //10 secondi
                //dc = new libnodave.daveConnection(di, 0, rack, slot);
                //if (0 == dc.connectPLC())
                {
                    byte[] bytes = new byte[1000];
                    bytes[1] = byte.Parse("0");
                    res = res = Client.DBWrite(int.Parse(dbNumber.ToLower().Replace("db", "")), 0, 2, bytes);
                    if (res != 0)
                        MessageBox.Show("error " + res + " " + res.ToString("0"));
                }
                Client.Disconnect();
                //dc.disconnectPLC();
                //libnodave.closeSocket(fds.rfd);
                if (res == 0)
                    MessageBox.Show(translate("string_writingCorrect"));// "Scrittura eseguita correttamente");
            }
            else
            {
                MessageBox.Show(translate("string_nonPossoAprireConnecction") + " " + ipAddress);// "Couldn't open TCP connection to " + ipAddress);
            }
        }

        public override void sendTag(string tag, string workDataXmlString)
        {

        }
        public override void init(string workDataXmlString)
        {
            p = new panelForConnector(this);
            p.txtExtraData.Text = "";
            p.txtOtherInfo.Text = "";
            List<String> sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "extraData");
            if (sTmp.Count > 0)
                p.txtExtraData.Text = sTmp[0];
            sTmp = VirtualConnectors.VirtualConnectors.getTagContents(workDataXmlString, "palletOnSystem_otherInfo");
            if (sTmp.Count > 0)
                p.txtOtherInfo.Text = sTmp[0];

        }

        public override System.Windows.Forms.Panel getSettingsPanel(string workDataXmlString)
        {
            return p.getPanel();
        }

        private void checkLicenceIdPcFromRegistry(String macAddress)
        {
            if (!isLicencedMacFromRegistry(new List<String>() { macAddress }))
            {
                MessageBox.Show(translate("string_licenceKey"));// "Chiave di licenza non valida o mancante! Impossibile, continuare");
            }
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
                        String hwCode_calculated = HwProtection.Encrypt(macAddresses[i++] + "8rWh785IK3plugin S7Connector v1.0");
                        res = checkIdPcLicence(new StringBuilder(dateCode), new StringBuilder(hwCode_calculated), new StringBuilder(hwCode));
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
        /*
        private bool isLicencedMacFromRegistry(String macAddress)
        {
            bool res = false;
            List<String> keys = new List<string>();
            String nextKey = (String)reg.GetValue("key", "LicenceMac");
            if (!String.IsNullOrEmpty(nextKey))
                keys.Add(nextKey);
            for (int i = 2; i <= 10; i++)
            {
                nextKey = (String)reg.GetValue("key", "LicenceMac" + i);
                if (!String.IsNullOrEmpty(nextKey))
                    keys.Add(nextKey);
            }
            bool foundKey = false;
            int j = 0;
            String key = "";
            String hwCode = "";
            String hwCode_calculated = "";
            String dateCode = "";
            while (!foundKey && j < keys.Count)
            {
                key = keys[j++];
                hwCode = key.Split('-')[0];
                hwCode_calculated = HwProtection.Encrypt(macAddress + "8rWh785IK3plugin S7Connector v1.0");
                dateCode = key.Split('-')[1];
                foundKey = checkIdPcLicence(new StringBuilder(dateCode), new StringBuilder(macAddress), new StringBuilder(hwCode_calculated));
            }
            if (!res)
                MessageBox.Show("Non è stata trovata una chiave di licenza valida per il plugin");
            else
            {
                String date = HwProtection.Decrypt(dateCode);
                DateTime startDate = DateTime.ParseExact(date.Substring(0, 8), "ddMMyyyy", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(date.Substring(8, 8), "ddMMyyyy", CultureInfo.InvariantCulture);
                if (reg.GetValue("key", "lastStartDate") != null && DateTime.Now < DateTime.ParseExact(HwProtection.Decrypt((String)reg.GetValue("key", "lastStartDate")), "ddMMyyyy", CultureInfo.InvariantCulture))
                    MessageBox.Show("E' stata riscontrata una data di sistema incoerente, si prega di reimpostarla");
                else if (DateTime.Now < startDate)
                    MessageBox.Show("E' stata riscontrata una data di sistema incoerente, si prega di reimpostarla");
                else if (DateTime.Now > endDate)
                    MessageBox.Show("La chiave di licenza è scaduta, per ottenere una nuova chiave contattare l'assistenza");
                else if (HwProtection.getMD5(key.Split('-')[0] + key.Split('-')[1]) != key.Split('-')[2])
                    MessageBox.Show("La chiave di licenza non è valida, si prega di contattare l'assistenza");
                else
                    res = true;
            }
            return res;
        }
        */

        // bool decodeWorkData(char* cipherTextIn, char** out_stringChar, char* id, char* hashId, char** out_checkSum)
        [DllImport("lApproachDll.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool checkIdPcLicence(StringBuilder cbcDate, StringBuilder id, StringBuilder hashId);

    }
    public class PayloadToWrite
    {
        public Point3FR pgDataCenter;
        public double pgDataProgressive;
        public Point3F pgDataApproach;
        public int groupNumber;
        public int layerNumber;
        public int onGroupOf;
        public int onLayerOf;
        public int payloadCode;
    }
}
