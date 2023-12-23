﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sintec.Tool;

    public partial class panelForConnector : UserControl
    {
        Registry res = new Registry();
        S7Connector.S7Connector s7Con;
        public panelForConnector(S7Connector.S7Connector s7Con)
        {
            this.s7Con = s7Con;
            InitializeComponent();

            String ip = (String)res.GetValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, "ip");
            if (!String.IsNullOrEmpty(ip))
                textBox11.Text = ip;
            String rack = (String)res.GetValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, "rack");
            if (!String.IsNullOrEmpty(rack))
                textBox12.Text = rack;
            String slot = (String)res.GetValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, "slot");
            if (!String.IsNullOrEmpty(slot))
                textBox13.Text = slot;
            String dbNumber = (String)res.GetValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, "dbNumber");
            if (!String.IsNullOrEmpty(dbNumber))
                textBox14.Text = dbNumber;
            String maxPayloadPlaced = (String)res.GetValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, "maxPayloadPlaced");
            if (!String.IsNullOrEmpty(maxPayloadPlaced))
                textBox3.Text = maxPayloadPlaced;
            String addInterlayerAndPanelOnPayloadsList = (String)res.GetValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, "addInterlayerAndPanelOnPayloadsList");
            if (!String.IsNullOrEmpty(addInterlayerAndPanelOnPayloadsList))
                chkAddInterlayerToPayload.Checked = (addInterlayerAndPanelOnPayloadsList=="1");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.s7Con.endTimer();
        }

        public void startTimer()
        {
            timer1.Enabled = true;
        }

        public int getRack()
        {
            try
            {
                return int.Parse(textBox12.Text);
            }
            catch {
                MessageBox.Show("Errore nel formato RACK");
            }
            return 0;
        }
        public int getSlot()
        {
            try
            {
                return int.Parse(textBox13.Text);}
            catch {
                MessageBox.Show("Errore nel formato SLOT");
            }
            return 0;
        }
        public String getDb()
        {
            return textBox14.Text;
        }
        public String getIp()
        {
            return textBox11.Text;
        }
        public String getExtraData()
        {
            return txtExtraData.Text;
        }
        public String getOtherInfo()
        {
            return txtOtherInfo.Text;
        }
        public int getMaxPayloadPlacedNumber()
        {
            return int.Parse(textBox3.Text);
        }
        public bool getAddInterlayerAndPanelOnPayloadsList()
        {
            return chkAddInterlayerToPayload.Checked;
        }

        private void textBox11_Leave(object sender, EventArgs e)
        {
            res.SetValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, "ip", textBox11.Text);
        }

        private void textBox12_Leave(object sender, EventArgs e)
        {
            res.SetValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, "rack", textBox12.Text);
        }

        private void textBox13_Leave(object sender, EventArgs e)
        {
            res.SetValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, "slot", textBox13.Text);
        }

        private void textBox14_Leave(object sender, EventArgs e)
        {
            res.SetValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, "dbNumber", textBox14.Text);
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            res.SetValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, "maxPayloadPlaced", textBox3.Text);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            res.SetValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, "addInterlayerAndPanelOnPayloadsList", chkAddInterlayerToPayload.Checked?"1":"0");
        }
    }