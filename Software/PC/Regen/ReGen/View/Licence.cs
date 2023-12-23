using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sintec.Tool;
using System.Globalization;

namespace ReGen.View
{
    public partial class LicenceForm : Form
    {
        Registry reg = new Registry();
        public LicenceForm()
        {
            InitializeComponent();
        }

        private void Licence_Load(object sender, EventArgs e)
        {
            Text = Program.translate("LicenceForm");
            groupLicencePc.Text = Program.translate("groupLicencePc");
            labelLicencePcCodice.Text = Program.translate("labelLicencePcCodice");
            labelLicencePcLicenza.Text = Program.translate("labelLicencePcLicenza");
            labelLicencePcScadenza.Text = Program.translate("labelLicencePcScadenza");
            groupLicencePlugin.Text = Program.translate("groupLicencePlugin");
            labelLicencePluginIp.Text = Program.translate("labelLicencePluginIp");
            labelLicencePluginCodice.Text = Program.translate("labelLicencePluginCodice");
            buttonLicencePluginCodiceTrova.Text = Program.translate("buttonLicencePluginCodiceTrova");
            labelLicencePluginLicenza1.Text = Program.translate("labelLicencePluginLicenza1");
            labelLicencePluginScadenza1.Text = Program.translate("labelLicencePluginScadenza1");
            labelLicencePluginLicenza2.Text = Program.translate("labelLicencePluginLicenza2");
            labelLicencePluginScadenza2.Text = Program.translate("labelLicencePluginScadenza2");
            labelLicencePluginLicenza3.Text = Program.translate("labelLicencePluginLicenza3");
            labelLicencePluginScadenza3.Text = Program.translate("labelLicencePluginScadenza3");
            labelLicencePluginLicenza4.Text = Program.translate("labelLicencePluginLicenza4");
            labelLicencePluginScadenza4.Text = Program.translate("labelLicencePluginScadenza4");
            labelLicencePluginLicenza5.Text = Program.translate("labelLicencePluginLicenza5");
            labelLicencePluginScadenza5.Text = Program.translate("labelLicencePluginScadenza5");
            labelLicencePluginLicenza6.Text = Program.translate("labelLicencePluginLicenza6");
            labelLicencePluginScadenza6.Text = Program.translate("labelLicencePluginScadenza6");
            labelLicencePluginLicenza7.Text = Program.translate("labelLicencePluginLicenza7");
            labelLicencePluginScadenza7.Text = Program.translate("labelLicencePluginScadenza7");
            labelLicencePluginLicenza8.Text = Program.translate("labelLicencePluginLicenza8");
            labelLicencePluginScadenza8.Text = Program.translate("labelLicencePluginScadenza8");
            labelLicencePluginLicenza9.Text = Program.translate("labelLicencePluginLicenza9");
            labelLicencePluginScadenza9.Text = Program.translate("labelLicencePluginScadenza9");
            labelLicencePluginLicenza10.Text = Program.translate("labelLicencePluginLicenza10");
            labelLicencePluginScadenza10.Text = Program.translate("labelLicencePluginScadenza10");
            buttonLicenceSalva.Text = Program.translate("buttonLicenceSalva");

            try
            {
                if (reg.GetValue("key", "firstStartDate")==null){
                    MessageBox.Show(Program.translate("string_AvviareIlProgrammaComeAmministratore"));
                    Environment.Exit(0);
                }
            }
            catch { lblExpireDateIdPc.Text = Program.translate("string_AvviareIlProgrammaComeAmministratore"); }
            try
            {
                txtPcId.Text = HwProtection.Encrypt("LicenceIdPc" + (String)reg.GetValue("key", "firstStartDate")) + "-" + HwProtection.diskCodeMD5();
                String key = (String)reg.GetValue("key", "LicenceIdPc");
                if (!String.IsNullOrEmpty(key))
                {
                    txtLicenceIdPc.Text = key;
                    String hwCode = key.Split('-')[0];
                    String dateCode = key.Split('-')[1];
                    String date = HwProtection.Decrypt(dateCode);
                    if (HwProtection.getMD5(key.Split('-')[0] + key.Split('-')[1]) == key.Split('-')[2])
                        lblExpireDateIdPc.Text = DateTime.ParseExact(date.Substring(8, 8), "ddMMyyyy", CultureInfo.InvariantCulture).ToLongDateString();
                    else
                        lblExpireDateIdPc.Text = Program.translate("string_CodiceNonValido");
                }

            }
            catch { lblExpireDateIdPc.Text = Program.translate("string_ErroreNellCodificaDelCodice"); }
            try
            {
                String key = (String)reg.GetValue("key", "LicenceMac");
                if (!String.IsNullOrEmpty(key))
                {
                    txtLicenceMac.Text = key;
                    String hwCode = key.Split('-')[0];
                    String dateCode = key.Split('-')[1];
                    String date = HwProtection.Decrypt(dateCode);
                    if (HwProtection.getMD5(key.Split('-')[0] + key.Split('-')[1]) == key.Split('-')[2])
                        lblExpireDateMac.Text = DateTime.ParseExact(date.Substring(8, 8), "ddMMyyyy", CultureInfo.InvariantCulture).ToLongDateString();
                    else
                        lblExpireDateMac.Text = Program.translate("string_CodiceNonValido");
                }
            }
            catch { lblExpireDateIdPc.Text = Program.translate("string_ErroreNellCodificaDelCodice"); }
            try
            {
                String key = (String)reg.GetValue("key", "LicenceMac2");
                if (!String.IsNullOrEmpty(key))
                {
                    txtLicenceMac2.Text = key;
                    String hwCode = key.Split('-')[0];
                    String dateCode = key.Split('-')[1];
                    String date = HwProtection.Decrypt(dateCode);
                    if (HwProtection.getMD5(key.Split('-')[0] + key.Split('-')[1]) == key.Split('-')[2])
                        lblExpireDateMac2.Text = DateTime.ParseExact(date.Substring(8, 8), "ddMMyyyy", CultureInfo.InvariantCulture).ToLongDateString();
                    else
                        lblExpireDateMac2.Text = Program.translate("string_CodiceNonValido");
                }
            }
            catch { lblExpireDateMac2.Text = Program.translate("string_ErroreNellCodificaDelCodice"); }
            try
            {
                String key = (String)reg.GetValue("key", "LicenceMac3");
                if (!String.IsNullOrEmpty(key))
                {
                    txtLicenceMac3.Text = key;
                    String hwCode = key.Split('-')[0];
                    String dateCode = key.Split('-')[1];
                    String date = HwProtection.Decrypt(dateCode);
                    if (HwProtection.getMD5(key.Split('-')[0] + key.Split('-')[1]) == key.Split('-')[2])
                        lblExpireDateMac3.Text = DateTime.ParseExact(date.Substring(8, 8), "ddMMyyyy", CultureInfo.InvariantCulture).ToLongDateString();
                    else
                        lblExpireDateMac3.Text = Program.translate("string_CodiceNonValido");
                }
            }
            catch { lblExpireDateMac3.Text = Program.translate("string_ErroreNellCodificaDelCodice"); }
            try
            {
                String key = (String)reg.GetValue("key", "LicenceMac4");
                if (!String.IsNullOrEmpty(key))
                {
                    txtLicenceMac4.Text = key;
                    String hwCode = key.Split('-')[0];
                    String dateCode = key.Split('-')[1];
                    String date = HwProtection.Decrypt(dateCode);
                    if (HwProtection.getMD5(key.Split('-')[0] + key.Split('-')[1]) == key.Split('-')[2])
                        lblExpireDateMac4.Text = DateTime.ParseExact(date.Substring(8, 8), "ddMMyyyy", CultureInfo.InvariantCulture).ToLongDateString();
                    else
                        lblExpireDateMac4.Text = Program.translate("string_CodiceNonValido");
                }
            }
            catch { lblExpireDateMac4.Text = Program.translate("string_ErroreNellCodificaDelCodice"); }
            try
            {
                String key = (String)reg.GetValue("key", "LicenceMac5");
                if (!String.IsNullOrEmpty(key))
                {
                    txtLicenceMac5.Text = key;
                    String hwCode = key.Split('-')[0];
                    String dateCode = key.Split('-')[1];
                    String date = HwProtection.Decrypt(dateCode);
                    if (HwProtection.getMD5(key.Split('-')[0] + key.Split('-')[1]) == key.Split('-')[2])
                        lblExpireDateMac5.Text = DateTime.ParseExact(date.Substring(8, 8), "ddMMyyyy", CultureInfo.InvariantCulture).ToLongDateString();
                    else
                        lblExpireDateMac5.Text = Program.translate("string_CodiceNonValido");
                }
            }
            catch { lblExpireDateMac5.Text = Program.translate("string_ErroreNellCodificaDelCodice"); }
            try
            {
                String key = (String)reg.GetValue("key", "LicenceMac6");
                if (!String.IsNullOrEmpty(key))
                {
                    txtLicenceMac6.Text = key;
                    String hwCode = key.Split('-')[0];
                    String dateCode = key.Split('-')[1];
                    String date = HwProtection.Decrypt(dateCode);
                    if (HwProtection.getMD5(key.Split('-')[0] + key.Split('-')[1]) == key.Split('-')[2])
                        lblExpireDateMac6.Text = DateTime.ParseExact(date.Substring(8, 8), "ddMMyyyy", CultureInfo.InvariantCulture).ToLongDateString();
                    else
                        lblExpireDateMac6.Text = Program.translate("string_CodiceNonValido");
                }
            }
            catch { lblExpireDateMac6.Text = Program.translate("string_ErroreNellCodificaDelCodice"); }
            try
            {
                String key = (String)reg.GetValue("key", "LicenceMac7");
                if (!String.IsNullOrEmpty(key))
                {
                    txtLicenceMac7.Text = key;
                    String hwCode = key.Split('-')[0];
                    String dateCode = key.Split('-')[1];
                    String date = HwProtection.Decrypt(dateCode);
                    if (HwProtection.getMD5(key.Split('-')[0] + key.Split('-')[1]) == key.Split('-')[2])
                        lblExpireDateMac7.Text = DateTime.ParseExact(date.Substring(8, 8), "ddMMyyyy", CultureInfo.InvariantCulture).ToLongDateString();
                    else
                        lblExpireDateMac7.Text = Program.translate("string_CodiceNonValido");
                }
            }
            catch { lblExpireDateMac7.Text = Program.translate("string_ErroreNellCodificaDelCodice"); }
            try
            {
                String key = (String)reg.GetValue("key", "LicenceMac8");
                if (!String.IsNullOrEmpty(key))
                {
                    txtLicenceMac8.Text = key;
                    String hwCode = key.Split('-')[0];
                    String dateCode = key.Split('-')[1];
                    String date = HwProtection.Decrypt(dateCode);
                    if (HwProtection.getMD5(key.Split('-')[0] + key.Split('-')[1]) == key.Split('-')[2])
                        lblExpireDateMac8.Text = DateTime.ParseExact(date.Substring(8, 8), "ddMMyyyy", CultureInfo.InvariantCulture).ToLongDateString();
                    else
                        lblExpireDateMac8.Text = Program.translate("string_CodiceNonValido");
                }
            }
            catch { lblExpireDateMac8.Text = Program.translate("string_ErroreNellCodificaDelCodice"); }
            try
            {
                String key = (String)reg.GetValue("key", "LicenceMac9");
                if (!String.IsNullOrEmpty(key))
                {
                    txtLicenceMac9.Text = key;
                    String hwCode = key.Split('-')[0];
                    String dateCode = key.Split('-')[1];
                    String date = HwProtection.Decrypt(dateCode);
                    if (HwProtection.getMD5(key.Split('-')[0] + key.Split('-')[1]) == key.Split('-')[2])
                        lblExpireDateMac9.Text = DateTime.ParseExact(date.Substring(8, 8), "ddMMyyyy", CultureInfo.InvariantCulture).ToLongDateString();
                    else
                        lblExpireDateMac9.Text = Program.translate("string_CodiceNonValido");
                }
            }
            catch { lblExpireDateMac9.Text = Program.translate("string_ErroreNellCodificaDelCodice"); }
            try
            {
                String key = (String)reg.GetValue("key", "LicenceMac10");
                if (!String.IsNullOrEmpty(key))
                {
                    txtLicenceMac10.Text = key;
                    String hwCode = key.Split('-')[0];
                    String dateCode = key.Split('-')[1];
                    String date = HwProtection.Decrypt(dateCode);
                    if (HwProtection.getMD5(key.Split('-')[0] + key.Split('-')[1]) == key.Split('-')[2])
                        lblExpireDateMac10.Text = DateTime.ParseExact(date.Substring(8, 8), "ddMMyyyy", CultureInfo.InvariantCulture).ToLongDateString();
                    else
                        lblExpireDateMac10.Text = Program.translate("string_CodiceNonValido");
                }
            }
            catch { lblExpireDateMac10.Text = Program.translate("string_ErroreNellCodificaDelCodice"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reg.SetValue("key", "LicenceIdPc", txtLicenceIdPc.Text);
            reg.SetValue("key", "LicenceMac", txtLicenceMac.Text);
            reg.SetValue("key", "LicenceMac2", txtLicenceMac2.Text);
            reg.SetValue("key", "LicenceMac3", txtLicenceMac3.Text);
            reg.SetValue("key", "LicenceMac4", txtLicenceMac4.Text);
            reg.SetValue("key", "LicenceMac5", txtLicenceMac5.Text);
            reg.SetValue("key", "LicenceMac6", txtLicenceMac6.Text);
            reg.SetValue("key", "LicenceMac7", txtLicenceMac7.Text);
            reg.SetValue("key", "LicenceMac8", txtLicenceMac8.Text);
            reg.SetValue("key", "LicenceMac9", txtLicenceMac9.Text);
            reg.SetValue("key", "LicenceMac10", txtLicenceMac10.Text);
            Licence_Load(sender, e);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            GetMacAddressFromIPAddress g = new GetMacAddressFromIPAddress();
            int timeToken = 5;
            while (!Sintec.Tool.NetworkTool.PingHost(txtIp.Text) && timeToken > 0)
                timeToken--;
            String mac = g.GetMACAddressFromARP(txtIp.Text);
            if (String.IsNullOrEmpty(mac))
                MessageBox.Show(Program.translate("string_IpNonConnessoAlPc"));
            else
                txtMac.Text = HwProtection.Encrypt("LicenceMac" + (String)reg.GetValue("key", "firstStartDate")) + "-" + mac.ToUpper();
        }
    }
}
