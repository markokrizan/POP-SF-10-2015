using POP_SF_10_2015.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
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


                cmd.ExecuteScalar();
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

        //funkcija koja ce vaditi za odredjeni racun njene prodate namestaje
        //koristi ovaj upit:
        //SELECT * FROM DodatneUsluge WHERE ID IN (SELECT IDDodatneUsluge FROM ProdateUsluge WHERE IDRacuna = 8) AND Obrisan != 1;
        public static ObservableCollection<DodatneUsluge> GetAll(Racun rac)
        {
            var usluge = new ObservableCollection<DodatneUsluge>();

            //tu je ona referenca koju smo otkacili
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {

                SqlCommand cmd = con.CreateCommand();
                //obrisan true 1 false 0
                cmd.CommandText = "SELECT * FROM DodatneUsluge WHERE ID IN (SELECT IDDodatneUsluge FROM ProdateUsluge WHERE IDRacuna = @IDRacuna);";
                cmd.Parameters.AddWithValue("IDRacuna", rac.ID);

                //ds i adapter samo kada dobavljamo podatke
                SqlDataAdapter adapter = new SqlDataAdapter();

                //objekat koji moze u sebi da ima vise logickih tabela, objekat u memoriji koji reprezentuje bazu
                DataSet ds = new DataSet();

                //smesti u data set pod nazivom tabele tipNamestaja
                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "DodatneUsluge"); // izvrsava se query nad bazom

                //za svaki red u data setu u tabelama tim i tim
                foreach (DataRow row in ds.Tables["DodatneUsluge"].Rows)
                {
                    var du = new DodatneUsluge();
                    //svi su tipa object, zato se radi ToStrint()
                    du.ID = int.Parse(row["ID"].ToString());
                    du.Obrisan = bool.Parse(row["Obrisan"].ToString());
                    du.Naziv = row["Naziv"].ToString();
                    du.Cena = double.Parse(row["Cena"].ToString());

                    //napuni u svakom prolazu puni kontejnersku kolekciju
                    usluge.Add(du);
                }
            }

            //vrati kolekciju
            return usluge;
        }

    }
}
