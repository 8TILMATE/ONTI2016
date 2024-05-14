using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI2016
{
    public partial class PrintPreview : Form
    {
        RebusModel rebusSelectat = new RebusModel();
        Bitmap bitmap1 = new Bitmap(200, 500);
        public PrintPreview(RebusModel model,Bitmap bitmap)
        {
            InitializeComponent();
            rebusSelectat=model;
            bitmap1= bitmap;
        }

        private void PrintPreview_Load(object sender, EventArgs e)
        {
            PrintDocument doc = new PrintDocument();
            printPreviewControl1.Document = new System.Drawing.Printing.PrintDocument();
            printPreviewControl1.Document.PrintPage += Document_PrintPage;
          
        }

        private void Document_PrintPage(object sender, PrintPageEventArgs e)
        {

            
            e.Graphics.DrawImage(bitmap1, 0, 0,1500,500);
            int yy = 500;
            var prev = new RaspunsModel();
            foreach (var x in DatabaseHelper.raspunsModels)
            {
                if (x.IdRebus == rebusSelectat.Id&&x.Solutie!=prev.Solutie)
                {
                    Pen pen = new Pen(Color.Black);
                    SolidBrush brush = new SolidBrush(Color.Black);
                    
                   
                    Font font = new Font(this.Font, new FontStyle());
                    
                    e.Graphics.DrawString(x.LinieStart.ToString() + "  " + x.Orientare + x.Solutie, label1.Font, brush, new Point(50, yy)); ;
                    yy += 20;
                    prev = x;
                }
            }
           
            e.Graphics.Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
