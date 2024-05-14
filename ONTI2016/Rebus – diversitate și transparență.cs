using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI2016
{
    public partial class Rebus___diversitate_și_transparență : Form
    {
        private int nrgreseli = 0;
        public Rebus___diversitate_și_transparență()
        {
            InitializeComponent();
        }
        private RebusModel model = new RebusModel();
        private static TextBox[,] textBoxes = new TextBox[30,30];
        private static TextBox[,] answerTextbox = new TextBox[30,30];
        private int nrore=0,nrminute=0,nrsecond=0;
        private int ii, jj;
        List<int> nrvocale = new List<int>();
        List<string> cuvinte = new List<string> ();

        private void Rebus___diversitate_și_transparență_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            if (DatabaseHelper.userlogat.TipUtilizator != 1)
            {
                menuStrip1.Items.RemoveAt(0);
            }
            DatabaseHelper.GetRebus();
            DatabaseHelper.GetRaspunsuri();
            foreach(var x in DatabaseHelper.rebusModels)
            {
                comboBox1.Items.Add(x.Denumire);
            }
            foreach (var x in DatabaseHelper.rebusModels)
            {
                comboBox2.Items.Add(x.Denumire);
            }
            chart1.Series.Add("Timp estimat");
            foreach(var x in DatabaseHelper.rebusModels)
            {
                chart1.Series[0].Points.AddXY(x.Denumire, x.TimpEstimat);
            }
        }

        private void inregistrareUtilizatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FrmInregistrare = new FrmInregistrare();
            this.Hide();
            FrmInregistrare.ShowDialog();
            this.Show();
        }

        private void logareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FrmInregistrare = new FrmLogare();
            this.Hide();
            FrmInregistrare.ShowDialog();
            this.Show();
        }

        private void adaugareRebusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialg = new OpenFileDialog();
            dialg.ShowDialog();
            var text = dialg.FileName;
            try
            {
                using (StreamReader rdr = new StreamReader(text))
                {
                    int indexdecitire = 0;
                    int idrebus = 0;
                    while (rdr.Peek() >= 1)
                    {
                        var line = rdr.ReadLine().Split('|');
                        if (indexdecitire == 0)
                        {
                            RebusModel rebusModel = new RebusModel
                            {
                                Id = Int32.Parse(line[0]),
                                Denumire = line[1],
                                NrColoane = Int32.Parse(line[2]),
                                NrLinii = Int32.Parse(line[3]),
                                TimpEstimat = Int32.Parse(line[4])
                            };
                            idrebus = rebusModel.Id;
                            DatabaseHelper.InsertRebus(rebusModel);
                        }
                        else
                        {
                            RaspunsModel model = new RaspunsModel
                            {
                                IdRebus = idrebus,
                                ColoanaStart = Int32.Parse(line[0]),
                                LinieStart = Int32.Parse(line[1]),
                                Orientare = line[2],
                                TextDefinitie = line[3],
                                Solutie = line[4],
                            };
                            DatabaseHelper.InsertRaspuns(model);
                        }
                        indexdecitire++;

                    }
                }
            }
            catch
            {

            }
            DatabaseHelper.GetRebus();
            DatabaseHelper.GetRaspunsuri();
            comboBox1.Items.Clear();
            dataGridView1.Rows.Clear();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedid= comboBox1.SelectedIndex;

            dataGridView1.Rows.Clear();
            dataGridView4.Rows.Clear();
            nrvocale.Clear();
            cuvinte.Clear();
            foreach(var i in DatabaseHelper.raspunsModels)
            {
                
                if(i.IdRebus == selectedid+1)
                {
                    dataGridView1.Rows.Add(i.LinieStart,i.Orientare,i.Solutie);
                    int vocale = 0;
                    foreach(var x in i.TextDefinitie)
                    {
                        if ("aeiou".Contains(x))
                        {
                            vocale++;
                        }
                    }
                    cuvinte.Add(i.TextDefinitie);
                    nrvocale.Add(vocale);
                }
            }
            for(int i= 0;i < nrvocale.Count-1; i++)
            {
                for(int j= i + 1; j < nrvocale.Count; j++)
                {
                    if (nrvocale[i] > nrvocale[j])
                    {
                        var z = nrvocale[i];
                        nrvocale[i]= nrvocale[j];
                        nrvocale[j]= z;
                        var x = cuvinte[i];
                        cuvinte[i] = cuvinte[j];
                        cuvinte[j]=x;
                    }
                }
            }
            for (int i = nrvocale.Count-1; i >=0; i--)
            {
                dataGridView4.Rows.Add(cuvinte[i], nrvocale[i]);
            }

            GenerateRebus.GenerareRebus(30, 120, selectedid+1, tabControl1.SelectedTab, false, ref textBoxes);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedid = comboBox2.SelectedIndex;
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            foreach (var i in DatabaseHelper.raspunsModels)
            {
                if (i.IdRebus == selectedid + 1)
                {
                    if (i.Orientare.Contains("orizontal"))
                    {
                        dataGridView2.Rows.Add(i.LinieStart, i.Solutie);
                        
                    }
                    else
                    {
                        dataGridView3.Rows.Add(i.ColoanaStart, i.Solutie);
                    }
                }
            }
            foreach (var i in DatabaseHelper.rebusModels)
            {
                if (i.Id == selectedid + 1)
                {
                    model = i;
                    int timp = i.TimpEstimat;
                    ii = i.NrLinii;
                    jj = i.NrColoane;
                    while (timp > 60)
                    {
                        timp -= 60;
                        nrminute++;
                        if (nrminute == 60)
                        {
                            nrore++;
                        }
                    }
                    nrsecond = timp;
                    break;
                }

            }
            GenerateRebus.GenerareRebus(30, 120, selectedid + 1, this, false, ref textBoxes);
            GenerateRebus.GenerareRebus(30, 120, selectedid + 1, tabControl1.SelectedTab, true, ref answerTextbox);
            textBox2.Text=nrore.ToString();
            textBox3.Text=nrminute.ToString();
            textBox4.Text=nrsecond.ToString();
            timer1.Stop();
            timer1.Start();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
         
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void tipRebusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Bitmap bitmap = new Bitmap(tabPage2.Size.Width,tabPage2.Size.Height);
            tabPage3.DrawToBitmap(bitmap,new Rectangle(0,0,tabPage2.Width-500,tabPage2.Height));
            PrintPreview printPreview = new PrintPreview(model,bitmap);
            printPreview.ShowDialog();
            this.Show();
        }

        private void iesireDinAplicatieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void delogareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmLogare frmLogare = new FrmLogare();
            frmLogare.ShowDialog();
            this.Close();
        }

        private void selectareRebusToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            for(int i = 1; i <= ii; i++)
            {
                for(int j= 1; j <= jj; j++)
                {
                    if (textBoxes[i, j].Text != answerTextbox[i, j].Text)
                    {
                        nrgreseli++;
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (nrsecond >= 1)
            {
                nrsecond--;
            }
            else if(nrminute>=1)
            {
                nrminute--;
                nrsecond = 59;
            }
            else if(nrore>=1) 
            {
                nrore--;
                nrminute = 59;
                nrsecond = 59;
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("Timp Expirat!");
            }
            textBox7.Text=nrore.ToString();
            textBox6.Text=nrminute.ToString();
            textBox5.Text=nrsecond.ToString();
        }
    }
}
