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
    class AkcijaDAL
    {

        #region Database
        public static ObservableCollection<Akcija> GetAll()
        {
            var akcije = new ObservableCollection<Akcija>();

            //tu je ona referenca koju smo otkacili
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {

                SqlCommand cmd = con.CreateCommand();
                //obrisan true 1 false 0
                cmd.CommandText = "SELECT * FROM Akcije WHERE Obrisan = 0";


                //ds i adapter samo kada dobavljamo podatke
                SqlDataAdapter adapter = new SqlDataAdapter();

                //objekat koji moze u sebi da ima vise logickih tabela, objekat u memoriji koji reprezentuje bazu
                DataSet ds = new DataSet();


                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "Akcije"); // izvrsava se query nad bazom

                //za svaki red u data setu u tabelama tim i tim
                foreach (DataRow row in ds.Tables["Akcije"].Rows)
                {
                    var akc = new Akcija();
                    //svi su tipa object, zato se radi ToStrint()
                    akc.ID = int.Parse(row["ID"].ToString());
                    akc.Obrisana = bool.Parse(row["Obrisan"].ToString());
                    akc.Naziv = row["Naziv"].ToString();
                    akc.Popust = Int32.Parse(row["Popust"].ToString());
                    akc.DatumPocetka = DateTime.Parse(row["DatumPocetka"].ToString());
                    akc.DatumZavrsetka = DateTime.Parse(row["DatumZavrsetka"].ToString());
                    //napuni u svakom prolazu puni kontejnersku kolekciju
                    akcije.Add(akc);
                }
            }

            //vrati kolekciju
            return akcije;
        }

        public static Akcija Create(Akcija akc)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {
                //otvori konekciju
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                //@naziv, @obrisan za sql injection, da se ne izvrsava kao poseban upit
                cmd.CommandText = $"INSERT INTO Akcije (Obrisan, Naziv, Popust, DatumPocetka, DatumZavrsetka) VALUES (@Obrisan, @Naziv, @Popust, @DatumPocetka, @DatumZavrsetka);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();"; // metoda koja vrati id poslednjeg upisanog elementa

                cmd.Parameters.AddWithValue("Obrisan", akc.Obrisana);
                cmd.Parameters.AddWithValue("Naziv", akc.Naziv);
                cmd.Parameters.AddWithValue("Popust", akc.Popust);
                cmd.Parameters.AddWithValue("DatumPocetka", akc.DatumPocetka);
                cmd.Parameters.AddWithValue("DatumZavrsetka", akc.DatumZavrsetka);


                //upises u bazu, ali ga vratis da dobijes id da mozes dalje da listas??? vidi to kako 
                int newId = int.Parse(cmd.ExecuteScalar().ToString()); // ExecuteScalar izvrsava query nad bazom
                akc.ID = newId;
            }

            //da se doda odma i observable collection, da ne bi bilo drugo stanje, da se doda u bazu a ne prikaze i tako dalje
            Projekat.Instance.akcija.Add(akc);// Azuriram i model!

            return akc;
        }

        public static void Update(Akcija akc)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Akcije " +
                    "SET Naziv = @Naziv, " +
                    "Obrisan = @Obrisan, " +
                    "Popust = @Popust,  " +
                    "DatumPocetka = @DatumPocetka,  " +
                    "DatumZavrsetka = @DatumZavrsetka  " +

                    "WHERE ID = @ID; ";

                cmd.Parameters.AddWithValue("ID", akc.ID);
                cmd.Parameters.AddWithValue("Naziv", akc.Naziv);
                cmd.Parameters.AddWithValue("Obrisan", akc.Obrisana);
                cmd.Parameters.AddWithValue("Popust", akc.Popust);
                cmd.Parameters.AddWithValue("DatumPocetka", akc.DatumPocetka);
                cmd.Parameters.AddWithValue("DatumZavrsetka", akc.DatumZavrsetka);

                cmd.ExecuteNonQuery();

                // Azuriram model, listu u modelu
                foreach (var akcija in Projekat.Instance.akcija)
                {
                    if (akcija.ID == akc.ID)
                    {
                        akcija.Naziv = akc.Naziv;
                        akcija.Obrisana = akc.Obrisana;
                        akcija.DatumPocetka = akc.DatumPocetka;
                        akcija.DatumZavrsetka = akc.DatumZavrsetka;
                        break;
                    }
                }
            }
        }

        public static void Delete(Akcija akc)
        {
            //setuj ga na true i samo ga prosledi update funkciji
            akc.Obrisana = true;
            Update(akc);
        }
        #endregion 

    }
}
