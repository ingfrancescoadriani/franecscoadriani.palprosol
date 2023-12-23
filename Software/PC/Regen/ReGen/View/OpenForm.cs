using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sintec.Tool;
using System.IO;
using ReGen.Model;
using System.Text.RegularExpressions;
using VirtualConnectors;
using System.Reflection;

namespace ReGen
{
    public partial class OpenForm : Form
    {
        Registry reg = new Registry();
        public delegate void ChosenRecipe(object sender, ChosenRecipeEventArgs a);
        public event ChosenRecipe chosenRecipeHandler;
        VirtualConnector connectorUsing;
        Panel tmpP = null;

        public OpenForm()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size((int)(Screen.PrimaryScreen.Bounds.Width * 0.9), (int)(Screen.PrimaryScreen.Bounds.Height * 0.9));
            this.StartPosition = FormStartPosition.CenterScreen;
            Registry reg = new Registry();
            
            //this.Location = new System.Drawing.Point((int)(Screen.PrimaryScreen.Bounds.Width * 0.05), (int)(Screen.PrimaryScreen.Bounds.Height * 0.05));
            textBox1.Text = (reg.GetValue("openDialog", "defaultDirectory") == null) ? "" : (String)(reg.GetValue("openDialog", "defaultDirectory").ToString());
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.Create);
            }
            if (reg.GetValue("openDialog", "exampleWriteDone") == null)
            {
                List<String> regenFile = new List<string>();
                if (!File.Exists(textBox1.Text + "/cliente2_2.ren"))
                    File.WriteAllBytes(textBox1.Text + "/cliente2_2.ren", ReGen.Properties.Resources.cliente2_2);
                if (!File.Exists(textBox1.Text + "/cliente2_1.ren"))
                    File.WriteAllBytes(textBox1.Text + "/cliente2_1.ren", ReGen.Properties.Resources.cliente2_1);
                if (!File.Exists(textBox1.Text + "/cliente1_2.ren"))
                    File.WriteAllBytes(textBox1.Text + "/cliente1_2.ren", ReGen.Properties.Resources.cliente1_2);
                if (!File.Exists(textBox1.Text + "/prodotti misti.ren"))
                    File.WriteAllBytes(textBox1.Text + "/prodotti misti.ren", ReGen.Properties.Resources.prodotti_misti);
                
                reg.SetValue("openDialog", "exampleWriteDone", "1");
            }
           
            String sTemp = Path.GetTempFileName();
            File.Delete(sTemp);
            File.Copy(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\ThreeDConnector.dll", sTemp);
            Assembly assembly = Assembly.LoadFrom(sTemp);
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsClass)
                {
                    if ((type.GetInterfaces().Count() > 0 && type.GetInterfaces()[0] == typeof(VirtualConnector)) || type.BaseType == typeof(VirtualConnector))
                    {
                        connectorUsing = (VirtualConnector)Activator.CreateInstance(type);
                        break;
                    }
                }
            }
            checkBoxOpenNonMostrareAllAvvio.Checked = (reg.GetValue("openDialog", "autoStart") == null) ? false : bool.Parse((reg.GetValue("openDialog", "autoStart").ToString()));
            button3_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Se nessuna torre di controllo è in ascolto allora l'evento è null
            if (chosenRecipeHandler != null)
            {
                // L'evento è un delegato, quindi eseguendo il delegato eseguiamo tutti i metodi 
                // ad esso registrati.
                chosenRecipeHandler(this, new ChosenRecipeEventArgs(((WorkDataWithPath)(listView1.SelectedItems[0].Tag)).path) { });
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
                reg.SetValue("openDialog", "defaultDirectory", textBox1.Text);
            }
            button3_Click(sender, e);
        }

        public void filtra(String filter)
        {
            try
            {
                foreach (ListViewItem i in listView1.Items)
                {
                    //((WorkData)i.Tag) = null;
                    i.Tag = null;
                }
                listView1.Items.Clear();
                if (listView1.Columns.Count == 0)
                {
                    listView1.Columns.Add(Program.translate("string_nomeCompleto"), 0);
                    listView1.Columns.Add(Program.translate("string_nome"), 150);
                    listView1.Columns.Add(Program.translate("string_dimensionePallet"), 100);
                    listView1.Columns.Add(Program.translate("string_campo1"), 150);
                    listView1.Columns.Add(Program.translate("string_campo2"), 150);
                    listView1.Columns.Add(Program.translate("string_payload"), 250);

                    //li.SubItems.Add(fi.Name);
                    //li.SubItems.Add(work.palletOnSystem.size.X + "; " + work.palletOnSystem.size.Y);
                    //li.SubItems.Add(work.customPickData);
                    //li.SubItems.Add(work.customPlaceData);
                    //li.SubItems.Add(ss);
                }
                foreach (String s in (checkBoxOpenCercaAncheNeiDatiDellRicetta.Checked ? Directory.EnumerateFiles(textBox1.Text, "*.ren") : Directory.EnumerateFiles(textBox1.Text, "*" + filter + "*.ren")))
                {
                    FileInfo fi = new FileInfo(s);
                    if (File.Exists(s) && (fi).Length > 0)
                    {
                        StreamReader sr = new StreamReader(s);
                        String fileContent = sr.ReadToEnd();
                        sr.Close();
                        String key = (String)reg.GetValue("key", "LicenceIdPc");
                        if (!String.IsNullOrEmpty(key) && (HwProtection.getMD5(key.Split('-')[0] + key.Split('-')[1]) == key.Split('-')[2]))
                        {
                            String hwCode = key.Split('-')[0];
                            String dateCode = key.Split('-')[1];
                            StringBuilder cbcDate = new StringBuilder(dateCode);
                            StringBuilder encrypt = new StringBuilder(fileContent.Substring(0, fileContent.Length - 32));
                            String decrypt;
                            String checkSum;
                            bool ok = MainForm.decodeWorkData(encrypt, out decrypt, cbcDate, new StringBuilder(HwProtection.diskCodeMD5()), new StringBuilder(hwCode), out checkSum);

                            // checksum
                            String checkPreCalculated = HwProtection.getMD5("prefisso" + encrypt + hwCode + "suffisso");

                            if ((checkPreCalculated == checkSum) && ok && Sintec.Tool.HwProtection.verifyCustomCodeMD5(fileContent.Substring(fileContent.Length - 32), decrypt.ToString()))
                            {
                                WorkData work = new WorkData(new StringBuilder(decrypt));
                                bool match1 = (work.palletOnSystem.size.X + "; " + work.palletOnSystem.size.Y).Contains(filter);
                                bool match2 = work.customPickData.Contains(filter);
                                bool match3 = work.customPlaceData.Contains(filter);
                                String ss = "";
                                foreach (Payload p in work.payloads)
                                    ss = ss + p.getPayloadStrategy().getName() + " [" + p.getOriginalSize().X + "; " + p.getOriginalSize().Y + "; " + p.getOriginalSize().Z + "] ";
                                bool match4 = ss.Contains(filter);
                                if (!checkBoxOpenCercaAncheNeiDatiDellRicetta.Checked || (checkBoxOpenCercaAncheNeiDatiDellRicetta.Checked && (match1 || match2 || match3 || match4)))
                                {
                                    ListViewItem li = listView1.Items.Add(fi.Name + " - (" + work.palletOnSystem.size.X + "; " + work.palletOnSystem.size.Y + ") - " + work.customPickData + " - " + work.customPlaceData + " - " + ss);
                                    li.Tag = new WorkDataWithPath() { workData = work, path = s };
                                    li.SubItems.Add(fi.Name);
                                    li.SubItems.Add(work.palletOnSystem.size.X + "; " + work.palletOnSystem.size.Y);
                                    li.SubItems.Add(work.customPickData);
                                    li.SubItems.Add(work.customPlaceData);
                                    li.SubItems.Add(ss);
                                    if (!imageList1.Images.ContainsKey(fi.Name))
                                    {
                                        Bitmap objBitmap = LayerThumbnail.thumbFrom(new LayerVisualization(work.palletOnSystem.getLayerAtIndex(0), 0), 1.0f, 128);
                                        imageList1.Images.Add(fi.Name, objBitmap);
                                    }
                                    li.ImageKey = fi.Name;
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            filtra(textBox2.Text);
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ButtonOpenApri.Enabled = (listView1.SelectedItems.Count > 0);
        }
        
        private void checkBox3_Click(object sender, EventArgs e)
        {
            checkBox3.Checked = true;
            listView1.View = System.Windows.Forms.View.LargeIcon;
            checkBox2.Checked = false;
            if (listView1.Items.Count>0)
                listView1.RedrawItems(0, listView1.Items.Count-1, true);
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            checkBox2.Checked = true;
            listView1.View = System.Windows.Forms.View.Details;
            checkBox3.Checked = false;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button3_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (chosenRecipeHandler != null)
                {
                    // L'evento è un delegato, quindi eseguendo il delegato eseguiamo tutti i metodi 
                    // ad esso registrati.
                    chosenRecipeHandler(this, new ChosenRecipeEventArgs(((WorkDataWithPath)(listView1.SelectedItems[0].Tag)).path) { });
                }
                this.Close();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                WorkDataWithPath wdwp = (WorkDataWithPath)listView1.SelectedItems[0].Tag;
                if (tmpP != null && this.tableLayoutPanel1.Controls.Contains(tmpP))
                    tableLayoutPanel1.Controls.Remove(tmpP);
                tmpP = connectorUsing.getSettingsPanel(wdwp.workData.ToString());
                tableLayoutPanel1.Controls.Add(tmpP, 1, 0);
                tmpP.Dock = System.Windows.Forms.DockStyle.Fill;
                //connectorUsing.init(wdwp.workData.ToString());
                //connectorUsing.send(wdwp.workData.ToString());
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            reg.SetValue("openDialog", "autoStart", checkBoxOpenNonMostrareAllAvvio.Checked.ToString());
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (chosenRecipeHandler != null)
            {
                // L'evento è un delegato, quindi eseguendo il delegato eseguiamo tutti i metodi 
                // ad esso registrati.
                chosenRecipeHandler(this, new ChosenRecipeEventArgs("") { });
            }
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenForm_Load(object sender, EventArgs e)
        {
            Text = Program.translate("OpenForm");
            labelOpenCartellaContenenteLeRicette.Text = Program.translate("labelOpenCartellaContenenteLeRicette");
            buttonOpenScegliCartellaContenenteLeRicette.Text = Program.translate("buttonOpenScegliCartellaContenenteLeRicette");
            labelOpenFiltra.Text = Program.translate("labelOpenFiltra");
            buttonOpenCerca.Text = Program.translate("buttonOpenCerca");
            checkBoxOpenCercaAncheNeiDatiDellRicetta.Text = Program.translate("checkBoxOpenCercaAncheNeiDatiDellRicetta");
            checkBoxOpenNonMostrareAllAvvio.Text = Program.translate("checkBoxOpenNonMostrareAllAvvio");
            ButtonOpenAnnulla.Text = Program.translate("ButtonOpenAnnulla");
            ButtonOpenApri.Text = Program.translate("ButtonOpenApri");
        }

        private void OpenForm_Resize(object sender, EventArgs e)
        {
        }
    }

    public class ChosenRecipeEventArgs : EventArgs
    {
        public ChosenRecipeEventArgs(string path)
        {
            this._path = path;
        }

        private string _path;
        public string Path
        {
            get { return _path; }
        }
    }
    public class WorkDataWithPath
    {
        public WorkData workData { get; set; }
        public string path { get; set; }
    }
}
