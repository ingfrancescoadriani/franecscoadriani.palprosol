using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReGen.View
{
    public partial class toolForm : Form
    {
        public toolForm(double angleChoosen, int startxWeightPower)
        {
            InitializeComponent();
            trackBar1.Value = startxWeightPower;
            if (angleChoosen >= 270)
            {
                Image i = pictureBox1.Image;
                pictureBox1.Image = pictureBox2.Image;
                pictureBox2.Image = i;
            }
        }

        bool pressedScroll = false;
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Program.xWeightPower = trackBar1.Value;
            MainForm.instance.refreshWeigth();
            pressedScroll = true;
        }

        private void toolForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(MainForm.instance.Location.X + MainForm.instance.Size.Width - 200,
                MainForm.instance.Location.Y + 100);
            Text = Program.translate("toolForm");
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            if (pressedScroll)
            {
                MainForm.instance.tokenForResetApproach = true;
                MainForm.instance.refreshWeigth();
            }
        }
    }
}
