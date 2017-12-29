using POP_SF_10_2015.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.DAL
{
    class ProdateUslugeDAL
    {
        public static DodatneUsluge Create(DodatneUsluge du, Racun rac)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {
                //otvori konekciju
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                //@naziv, @obrisan za sql injection, da se ne izvrsava kao poseban upit
                cmd.CommandText = $"INSERT INTO ProdateUsluge (IDRacuna, IDDodatneUsluge) VALUES (@IDRacuna, @IDDodatneUsluge);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();"; // metoda koja vrati id poslednjeg upisanog elementa

                cmd.Parameters.AddWithValue("IDRacuna", rac.ID);
                cmd.Parameters.AddWithValue("IDDodatneUsluge", du.ID);
                
                /*
                //upises u bazu, ali ga vratis da dobijes id da mozes dalje da listas??? vidi to kako 
                int newId = int.Parse(cmd.ExecuteScalar().ToString()); // ExecuteScalar izvrsava query nad bazom
                du.ID = newId;
                */
            }

            //da se doda odma i observable collection, da ne bi bilo drugo stanje, da se doda u bazu a ne prikaze i tako dalje
            //Projekat.Instance.dodatneusluge.Add(du);// Azuriram i model!
            rac.DodatneUsluge.Add(du);
            return du;
        }
    }
}
