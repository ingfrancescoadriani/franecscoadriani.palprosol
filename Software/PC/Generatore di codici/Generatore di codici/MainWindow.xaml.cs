using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sintec.Tool;
using System.Globalization;

namespace Generatore_di_codici
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtMachineOrMacAddrCode.Text = HwProtection.diskCodeMD5();
            //txtHash.Text = HwProtection.getMD5("prefissoAl2405KvMz=%*39gfMQWocNT03578)-D_ad92rihgbnN%gf893G" + txtMachineOrMacAddrCode.Text + "suffisso");
            datePickStart.SelectedDate = datePickEnd.SelectedDate = DateTime.Now;
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickStart.SelectedDate != null && datePickEnd.SelectedDate != null)
            {
                txtCBC.Text = HwProtection.Encrypt(datePickStart.SelectedDate.Value.ToString("ddMMyyyy") +  datePickEnd.SelectedDate.Value.ToString("ddMMyyyy")).ToString();
                txtCheckSum.Text = HwProtection.getMD5(txtHash.Text + txtCBC.Text);

                button1_Click(null, null);
            }
        }

        private void txtMachineOrMacAddrCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((checkBox1!=null && checkBox1.IsChecked == true) || (checkBox1==null))
                txtMachineOrMacAddrCode.Text = txtMachineOrMacAddrCode.Text.ToUpper();
            String hwCode = txtMachineOrMacAddrCode.Text;
            if (((ComboBoxItem)comboBox1.SelectedItem).Content.ToString() != "prima versione - nessuna versione")
                hwCode = HwProtection.Encrypt(hwCode + "8rWh785IK3" + ((ComboBoxItem)comboBox1.SelectedItem).Content.ToString());
            txtHash.Text = HwProtection.getMD5("prefissoAl2405KvMz=%*39gfMQWocNT03578)-D_ad92rihgbnN%gf893G" + hwCode + "suffisso");
            txtCheckSum.Text = HwProtection.getMD5(txtHash.Text + txtCBC.Text);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            txtCompleteCode.Text = txtHash.Text + "-" + txtCBC.Text + "-" + txtCheckSum.Text;
            if (txtCompleteCode.Text.Split('-').Length > 3)
                MessageBox.Show("Codice generato non rileggibile!! si prega di cambiare la data per ottenere un codice leggermente diverso e privo di ulteriori '-'");
        }

        private void txtCompleteCode_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCompleteCode.SelectAll();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            new WindowCheckCode(txtCompleteCode.Text, txtMachineOrMacAddrCode.Text).Show();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            GetMacAddressFromIPAddress g = new GetMacAddressFromIPAddress();
            int timeToken = 5;
            while (!Sintec.Tool.NetworkTool.PingHost(textBox6.Text) && timeToken > 0)
                timeToken--;
            String mac = g.GetMACAddressFromARP(textBox6.Text);
            if (String.IsNullOrEmpty(mac))
                MessageBox.Show("Ip non connesso al pc");
            else
                txtMachineOrMacAddrCode.Text = mac.ToUpper();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            txtMachineOrMacAddrCode.Text = HwProtection.diskCodeMD5();
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            String input = Microsoft.VisualBasic.Interaction.InputBox("Inserire il codice", DefaultResponse: HwProtection.Encrypt("LicenceIdPc" + HwProtection.Encrypt(DateTime.Now.ToString("ddMMyyyy"))) + "-" + HwProtection.diskCodeMD5());

            if (!String.IsNullOrEmpty(input))
            {
                if (input.Split('-').Count() > 0)
                {
                    String decrypt = HwProtection.Decrypt(input.Split('-')[0]);
                    if (decrypt.StartsWith("LicenceIdPc"))
                    {
                        txtMachineOrMacAddrCode.Text = input.Split('-')[1];
                        datePickStart.DisplayDate = DateTime.ParseExact(HwProtection.Decrypt(decrypt.Replace("LicenceIdPc", "")), "ddMMyyyy", CultureInfo.InvariantCulture);
                        datePickStart.SelectedDate = datePickStart.DisplayDate;
                    }
                    else if (decrypt.StartsWith("LicenceMac"))
                    {
                        txtMachineOrMacAddrCode.Text = input.Split('-')[1];
                        datePickStart.DisplayDate = DateTime.ParseExact(HwProtection.Decrypt(decrypt.Replace("LicenceMac", "")), "ddMMyyyy", CultureInfo.InvariantCulture);
                        datePickStart.SelectedDate = datePickStart.DisplayDate;
                    }
                }
            }
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (checkBox1 != null && comboBox1.SelectedItem!=null)
                checkBox1.IsChecked = (((ComboBoxItem)(comboBox1.SelectedItem)).Content.ToString() != "prima versione - nessuna versione");
            txtMachineOrMacAddrCode_TextChanged(null, null);
        }
    }
}
