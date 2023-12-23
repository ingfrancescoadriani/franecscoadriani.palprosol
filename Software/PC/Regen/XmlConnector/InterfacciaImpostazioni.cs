using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XmlConnector
{
    public partial class InterfacciaImpostazioni : UserControl
    {
        public InterfacciaImpostazioni()
        {
            InitializeComponent();
        }

        private void InterfacciaImpostazioni_Load(object sender, EventArgs e)
        {

        }
        public Panel getPanel()
        {
            return this.panel1;
        }
    }
}
