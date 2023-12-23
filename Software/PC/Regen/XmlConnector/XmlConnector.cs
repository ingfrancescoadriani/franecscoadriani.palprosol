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
namespace XmlConnector
{
    public class XmlConnector : VirtualConnector
    {
        public override void refreshLanguage()
        {
            //throw new NotImplementedException();
        }

        public override string getPreview(string workDataXmlString)
        {
            if (isLicencedIdPcFromRegistry())
                return workDataXmlString;
            else
                return "Licenza non trovata o non valida";
        }
        public override string getIstruction(string workDataXmlString)
        {
            String lStr = "Premere 'Invia' e scegliere dove salvare il file in formato xml";
            return lStr;
        }

        public override void send(string workDataXmlString)
        {
            if (isLicencedIdPcFromRegistry())
            {
                try
                {
                    SaveFileDialog openFileDialog1 = new SaveFileDialog();
                    openFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                    openFileDialog1.FilterIndex = 1;
                    openFileDialog1.RestoreDirectory = true;

                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        FileStream fileStream = new FileStream(openFileDialog1.FileName, FileMode.Create);
                        fileStream.Close();
                        System.IO.File.WriteAllText(openFileDialog1.FileName, workDataXmlString);
                    }
                }
                catch
                {
                    MessageBox.Show("C'è stato qualche problema nel salvataggio del file");
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
            return new InterfacciaImpostazioni().getPanel();
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
                        String hwCode_calculated = HwProtection.Encrypt(macAddresses[i++] + "8rWh785IK3plugin XmlConnector v1.0");
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
                MessageBox.Show("E' stata riscontrata una data di sistema incoerente, si prega di reimpostarla");
            else if (noLicenceCode == 2)
                MessageBox.Show("E' stata riscontrata una data di sistema incoerente, si prega di reimpostarla");
            else if (noLicenceCode == 3)
                MessageBox.Show("La chiave di licenza è scaduta, per ottenere una nuova chiave contattare l'assistenza");
            else if (noLicenceCode == 4)
                MessageBox.Show("La chiave di licenza non è valida, si prega di contattare l'assistenza");
            return res;
        }



    }
}
