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
    class ProdatiNamestajDAL
    {

        public static Namestaj Create(Namestaj nam, Racun rac)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {
                //otvori konekciju
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                //@naziv, @obrisan za sql injection, da se ne izvrsava kao poseban upit
                //AkcijaID posle TipNamestajaID
                cmd.CommandText = $"INSERT INTO ProdatiNamestaj (IDRacuna, IDNamestaja) VALUES (@IDRacuna, @IDNamestaja);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();"; // metoda koja vrati id poslednjeg upisanog elementa

                //kada ovde prosledis namestaj iz namestajWindow, ti bindingom za tip namestaja posaljes njemu ceo objekat
                //u kompo boksu je samo lista objekata sa overridovanim to string da pokazuje naziv, ali ovde za tip namestaja stize ceo objekat

                //cmd.Parameters.AddWithValue("TipNamestajaID", nam.IDTipaNamestaja);
                cmd.Parameters.AddWithValue("IDRacuna", nam.ID);
                cmd.Parameters.AddWithValue("IDNamestaja", rac.ID);

                /*
                //upises u bazu, ali ga vratis da dobijes id da mozes dalje da listas??? vidi to kako 
                int newId = int.Parse(cmd.ExecuteScalar().ToString()); // ExecuteScalar izvrsava query nad bazom
                nam.ID = newId;
                */
            }
            /*
            //da se doda odma i observable collection, da ne bi bilo drugo stanje, da se doda u bazu a ne prikaze i tako dalje
            Projekat.Instance.namestaj.Add(nam); // Azuriram i model!
            */
            //provuceno kroz bazu pa vamo azuriran model:
            rac.NamestajZaProdaju.Add(nam);

            return nam;
            
        }
    }
}
