using ONTI2016.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ONTI2016
{
    public static class DatabaseHelper
    {
        public static UserModel userlogat = new UserModel();
        public static List<RebusModel> rebusModels = new List<RebusModel>();
        public static List<RaspunsModel> raspunsModels = new List<RaspunsModel>();
        public static void InsertIntoDB()
        {
            
        }
        public static bool CheckUser(string email,string parola)
        {
            using(SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                using(SqlCommand cmd = new SqlCommand("Select * From Utilizatori Where (NumeUtilizator = @e and Parola= @p)",con))
                {
                    cmd.Parameters.AddWithValue("e", email);
                    cmd.Parameters.AddWithValue("p", parola);
                    using(SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while(rdr.Read())
                        {
                            try
                            {
                                Console.WriteLine(rdr.GetValue(0).ToString());
                                userlogat = new UserModel
                                {
                                    Id = rdr.GetInt32(0),
                                    Parola = rdr.GetValue(1).ToString(),
                                    Name = rdr.GetValue(2).ToString(),
                                    Email = rdr.GetValue(3).ToString(),
                                    TipUtilizator = rdr.GetInt32(4)
                                };
                                return true;
                            }
                            catch
                            {
                                return false;
                            }
                        }
                        return false;
                    }
                }
            }
        }
        public static void GetRaspunsuri()
        {
            raspunsModels.Clear();
            using (SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Select * From Rezolvari", con))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            RaspunsModel model = new RaspunsModel();
                                model = new RaspunsModel
                                {
                                    IdRebus = rdr.GetInt32(0),
                                    ColoanaStart = rdr.GetInt32(1),
                                    LinieStart = rdr.GetInt32(2),
                                    Orientare= rdr.GetValue(3).ToString(),
                                    Solutie = rdr.GetString(4),
                                    TextDefinitie=rdr.GetString(5),
                                };
                            raspunsModels.Add(model);
                        }
                    }
                }
            }
        }
        public static void GetRebus()
        {
            rebusModels.Clear();
            using (SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Select * From Rebusuri", con))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            RebusModel model = new RebusModel();
                            model = new RebusModel
                            {
                                Id= rdr.GetInt32(0),
                                Denumire = rdr.GetString(1),
                                NrColoane = rdr.GetInt32(2),
                                NrLinii = rdr.GetInt32(3),
                                TimpEstimat = rdr.GetInt32(4),
                            };
                            rebusModels.Add(model);
                        }
                    }
                }
            }
        }
        public static void InsertUser(UserModel user)
        {
            using (SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Insert into Utilizatori values(@p,@n,@e,@t)", con))
                {
                    cmd.Parameters.AddWithValue("e", user.Email);
                    cmd.Parameters.AddWithValue("p", user.Parola);
                    cmd.Parameters.AddWithValue("n", user.Name);
                    cmd.Parameters.AddWithValue("t", user.TipUtilizator);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void InsertRebus(RebusModel rebus)
        {
            using(SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                using(SqlCommand cmd = new SqlCommand("Insert into Rebusuri values(@d,@c,@l,@t)", con))
                {
                    cmd.Parameters.AddWithValue("d", rebus.Denumire);
                    cmd.Parameters.AddWithValue("c", rebus.NrColoane);
                    cmd.Parameters.AddWithValue("l", rebus.NrLinii);
                    cmd.Parameters.AddWithValue("t", rebus.TimpEstimat);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void InsertRaspuns(RaspunsModel raspuns)
        {
            using (SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Insert into Rezolvari values(@i,@c,@l,@o,@s,@t)", con))
                {
                    cmd.Parameters.AddWithValue("i",raspuns.IdRebus);
                    cmd.Parameters.AddWithValue("c", raspuns.ColoanaStart);
                    cmd.Parameters.AddWithValue("l", raspuns.LinieStart);
                    cmd.Parameters.AddWithValue("o", raspuns.Orientare);
                    cmd.Parameters.AddWithValue("s", raspuns.Solutie);
                    cmd.Parameters.AddWithValue("t", raspuns.TextDefinitie);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
