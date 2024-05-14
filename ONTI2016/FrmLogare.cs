using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI2016
{
    public partial class FrmLogare : Form
    {
        public FrmLogare()
        {
            InitializeComponent();
        }

        private void FrmLogare_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var FrmInregistrare = new FrmInregistrare();
            this.Hide();
            FrmInregistrare.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DatabaseHelper.CheckUser(textBox1.Text, textBox2.Text))
            {
                Rebus___diversitate_și_transparență rebus___Diversitate_Și_Transparență = new Rebus___diversitate_și_transparență();
                this.Hide();
                rebus___Diversitate_Și_Transparență.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Eroare la autentificare!");
            }
        }
    }
}
