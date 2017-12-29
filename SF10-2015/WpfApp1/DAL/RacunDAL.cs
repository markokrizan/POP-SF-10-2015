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
    class RacunDAL
    {

        public static ObservableCollection<Racun> GetAll()
        {
            var racuni = new ObservableCollection<Racun>();

            //tu je ona referenca koju smo otkacili
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {

                SqlCommand cmd = con.CreateCommand();
                //obrisan true 1 false 0
                cmd.CommandText = "SELECT * FROM Racun";


                //ds i adapter samo kada dobavljamo podatke
                SqlDataAdapter adapter = new SqlDataAdapter();

                //objekat koji moze u sebi da ima vise logickih tabela, objekat u memoriji koji reprezentuje bazu
                DataSet ds = new DataSet();

                //smesti u data set pod nazivom tabele tipNamestaja
                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "Racun"); // izvrsava se query nad bazom

                //za svaki red u data setu u tabelama tim i tim
                foreach (DataRow row in ds.Tables["Racun"].Rows)
                {
                    var rac = new Racun();
                    //svi su tipa object, zato se radi ToStrint()
                    rac.ID = int.Parse(row["ID"].ToString());
                    rac.DatumProdaje = DateTime.Parse(row["DatumProdaje"].ToString());
                    rac.BrojRacuna = row["BrojRacuna"].ToString();
                    rac.Kupac = row["Kupac"].ToString();
                    rac.UkupnaCena = double.Parse(row["Cena"].ToString());
                    //napuni u svakom prolazu puni kontejnersku kolekciju
                    racuni.Add(rac);
                }
            }

            //vrati kolekciju
            return racuni;
        }

        public static Racun Create(Racun rac)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {
                //otvori konekciju
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                //@naziv, @obrisan za sql injection, da se ne izvrsava kao poseban upit
                cmd.CommandText = $"INSERT INTO Racun (DatumProdaje, BrojRacuna, Kupac, Cena) VALUES (@DatumProdaje, @BrojRacuna, @Kupac, @Cena);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();"; // metoda koja vrati id poslednjeg upisanog elementa

                cmd.Parameters.AddWithValue("DatumProdaje", rac.DatumProdaje);
                cmd.Parameters.AddWithValue("BrojRacuna", rac.BrojRacuna);
                cmd.Parameters.AddWithValue("Kupac", rac.Kupac);
                cmd.Parameters.AddWithValue("Cena", rac.UkupnaCena);

                //upises u bazu, ali ga vratis da dobijes id da mozes dalje da listas??? vidi to kako 
                int newId = int.Parse(cmd.ExecuteScalar().ToString()); // ExecuteScalar izvrsava query nad bazom
                rac.ID = newId;
            }

            //da se doda odma i observable collection, da ne bi bilo drugo stanje, da se doda u bazu a ne prikaze i tako dalje
            Projekat.Instance.racun.Add(rac); // Azuriram i model!

            return rac;
        }

        /*
        public static void Update(TipNamestaja tn)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE TipNamestaja " +
                    "SET Naziv = @Naziv, Obrisan = @Obrisan  " +
                    "WHERE ID = @ID; ";

                cmd.Parameters.AddWithValue("ID", tn.ID);
                cmd.Parameters.AddWithValue("Naziv", tn.Naziv);
                cmd.Parameters.AddWithValue("Obrisan", tn.Obrisan);

                cmd.ExecuteNonQuery();

                // Azuriram model, listu u modelu
                foreach (var tipNamestaja in Projekat.Instance.tipnamestaja)
                {
                    if (tipNamestaja.ID == tn.ID)
                    {
                        tipNamestaja.Naziv = tn.Naziv;
                        tipNamestaja.Obrisan = tn.Obrisan;
                        break;
                    }
                }
            }
        }

        public static void Delete(TipNamestaja tn)
        {
            //setuj ga na true i samo ga prosledi update funkciji
            tn.Obrisan = true;
            Update(tn);
        }

        */

    }
}
