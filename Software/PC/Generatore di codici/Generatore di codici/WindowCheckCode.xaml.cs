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
using System.Windows.Shapes;
using Sintec.Tool;
using System.Globalization;

namespace Generatore_di_codici
{
    /// <summary>
    /// Interaction logic for WindowCheckCode.xaml
    /// </summary>
    public partial class WindowCheckCode : Window
    {
        String code;
        String machineCode;
        public WindowCheckCode(String code, String machineCode)
        {
            InitializeComponent();
            this.code = code;
            this.machineCode = machineCode;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            String key = textBox4.Text;
            String hwCode = key.Split('-')[0];
            String dateCode = key.Split('-')[1];
            String date = HwProtection.Decrypt(dateCode);
            datePicker1.DisplayDate = DateTime.ParseExact(date.Substring(8,8), "ddMMyyyy", CultureInfo.InvariantCulture);
            datePicker1.SelectedDate = datePicker1.DisplayDate;
            datePickStart.DisplayDate = DateTime.ParseExact(date.Substring(0,8), "ddMMyyyy", CultureInfo.InvariantCulture);
            datePickStart.SelectedDate = datePickStart.DisplayDate;
            String checkSumADteCode = key.Split('-')[2];
            lblCheckSum.Content = (HwProtection.getMD5(hwCode + dateCode) == checkSumADteCode) ? "OK" : "NOK";
            String hwCode_calculated = textBox2.Text;
            if (!radioButton1.IsChecked.Value)
                hwCode_calculated = hwCode_calculated.ToUpper();
            if (((ComboBoxItem)comboBox1.SelectedItem).Content.ToString() != "prima versione - nessuna versione")
                hwCode_calculated = HwProtection.Encrypt(hwCode_calculated + "8rWh785IK3" + ((ComboBoxItem)comboBox1.SelectedItem).Content.ToString());
            lblVerifica.Content = (HwProtection.getMD5("prefissoAl2405KvMz=%*39gfMQWocNT03578)-D_ad92rihgbnN%gf893G" + hwCode_calculated + "suffisso") == hwCode) ? "OK" : "NOK";
            lblCheckSum.Background = ((String)lblCheckSum.Content) == "OK" ? Brushes.Green : Brushes.Red;
            lblVerifica.Background = ((String)lblVerifica.Content) == "OK" ? Brushes.Green : Brushes.Red;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBox4.Text = code;
            textBox2.Text = machineCode;
        }
    }
}
