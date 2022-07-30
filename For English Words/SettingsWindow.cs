using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace For_English_Words
{
    public partial class SettingsWindow : Form
    {
        Size screenSize = Screen.PrimaryScreen.Bounds.Size;
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void SettingsWindow_Load(object sender, EventArgs e)
        {
            Location = new Point((screenSize.Width/2)-(Size.Width/2),
                (screenSize.Height/2)-(Size.Height/2));
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 0)
            {
                label1.Text = "1";
                panel1.Visible = true;
            }
            if (listBox1.SelectedIndex == 1)
            {
                label2.Text = "2";
                panel1.Visible = false;
                panel2.Visible = true;
            }
            if (listBox1.SelectedIndex == 2)
            {
                label3.Text = "3";
                panel2.Visible = false;
                panel3.Visible = true;
            }
            if (listBox1.SelectedIndex == 3)
            {
                label4.Text = "4";
                panel3.Visible = false;
                panel4.Visible = true;
            }
            if (listBox1.SelectedIndex == 4)
            {
                label5.Text = "5";
                panel3.Visible= false;
                panel4.Visible = true;
            }

        }
    }
}
