using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI2016
{
    public class GenerateRebus
    {
        public static int ii=0, jj=0;
        public static void GenerareRebus(int startx,int starty,int idrebus,Control background,bool solvable, ref TextBox[,] matrice)
        {
            GenerateTextboxes(solvable, startx, starty, background, idrebus, ref matrice);
            PopulateTextBoxes(idrebus, ref matrice,solvable);
        }
        public static void GenerateTextboxes(bool solvable,int startx,int starty,Control background,int rebus,ref TextBox[,] matrice)
        {
            
            foreach(RebusModel model in DatabaseHelper.rebusModels)
            {
                if (model.Id == rebus)
                {
                    ii=model.NrLinii; jj=model.NrColoane;
                }
                for(int i = 1; i <= ii; i++)
                {
                    for (int j = 1; j <= jj; j++)
                    {
                        TextBox textBox = new TextBox
                        {
                            Size = new System.Drawing.Size(22, 22),
                            ReadOnly = !solvable,
                            Location = new System.Drawing.Point(j * 22 + startx, i * 22 + starty)
                        };
                        background.Controls.Add(textBox);
                        matrice[i,j] = textBox;
                    }
                }
            }
        }
        public static void PopulateTextBoxes(int idrebus,ref TextBox[,] matrice,bool editabil)
        {
            foreach(var model in DatabaseHelper.raspunsModels)
            {
                if (model.IdRebus == idrebus)
                {
                    if(model.Orientare.Trim()== "orizontal")
                    {
                        for(int j = 0; j < model.TextDefinitie.Trim().Length; j++)
                        {

                            if (model.TextDefinitie[j] != ' ')
                            {
                                if (!editabil)
                                {
                                    try
                                    {
                                        matrice[model.LinieStart, model.ColoanaStart + j].Text = model.TextDefinitie[j].ToString();
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                            else
                            {
                                try
                                {
                                    matrice[model.LinieStart, model.ColoanaStart + j].BackColor = System.Drawing.Color.Black;
                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < model.TextDefinitie.Trim().Length; j++)
                        {
                            try
                            {
                                if (model.TextDefinitie[j] != ' ')
                                {
                                    if (!editabil)
                                    {
                                        try
                                        {
                                            matrice[model.LinieStart+j, model.ColoanaStart].Text = model.TextDefinitie[j].ToString();
                                        }
                                        catch
                                        {

                                        }
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        matrice[model.LinieStart+j, model.ColoanaStart].BackColor = System.Drawing.Color.Black;
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
        }
    }
}
