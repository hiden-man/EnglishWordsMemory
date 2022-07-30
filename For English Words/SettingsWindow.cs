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
            }
            if (listBox1.SelectedIndex == 1)
            {
                label1.Text = "2";
            }
            if (listBox1.SelectedIndex == 2)
            {
                label1.Text = "3";
            }
            if (listBox1.SelectedIndex == 3)
            {
                label1.Text = "4";
            }
            if (listBox1.SelectedIndex == 4)
            {
                label1.Text = "5";
            }

        }
    }
}
