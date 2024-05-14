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
    public partial class FrmInregistrare : Form
    {
        public FrmInregistrare()
        {
            InitializeComponent();
        }

        private void FrmInregistrare_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == textBox3.Text)
            {
                if (DatabaseHelper.CheckUser(textBox4.Text, textBox2.Text))
                {
                    MessageBox.Show("Exista deja acest utilizator!");
                    DatabaseHelper.userlogat = new UserModel();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";

                }
                else
                {
                    UserModel model = new UserModel
                    {
                        Name = textBox1.Text,
                        Parola = textBox2.Text,
                        Email = textBox4.Text,
                        TipUtilizator = comboBox1.SelectedIndex
                    };
                    DatabaseHelper.InsertUser(model);
                    MessageBox.Show("Utilizator inregistrat cu succes!");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
