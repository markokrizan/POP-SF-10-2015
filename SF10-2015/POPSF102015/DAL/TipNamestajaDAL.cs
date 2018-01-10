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
    class TipNamestajaDAL
    {
        #region Database
        public static ObservableCollection<TipNamestaja> GetAll()
        {
            var tipNamestaja = new ObservableCollection<TipNamestaja>();

            //tu je ona referenca koju smo otkacili
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {

                SqlCommand cmd = con.CreateCommand();
                //obrisan true 1 false 0
                cmd.CommandText = "SELECT * FROM TipNamestaja WHERE obrisan = 0";


                //ds i adapter samo kada dobavljamo podatke
                SqlDataAdapter adapter = new SqlDataAdapter();

                //objekat koji moze u sebi da ima vise logickih tabela, objekat u memoriji koji reprezentuje bazu
                DataSet ds = new DataSet();

                //smesti u data set pod nazivom tabele tipNamestaja
                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "TipNamestaja"); // izvrsava se query nad bazom

                //za svaki red u data setu u tabelama tim i tim
                foreach (DataRow row in ds.Tables["TipNamestaja"].Rows)
                {
                    var tn = new TipNamestaja();
                    //svi su tipa object, zato se radi ToStrint()
                    tn.ID = int.Parse(row["ID"].ToString());
                    tn.Naziv = row["Naziv"].ToString();
                    tn.Obrisan = bool.Parse(row["Obrisan"].ToString());
                    //napuni u svakom prolazu puni kontejnersku kolekciju
                    tipNamestaja.Add(tn);
                }
            }

            //vrati kolekciju
            return tipNamestaja;
        }

        public static TipNamestaja Create(TipNamestaja tn)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {
                //otvori konekciju
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                //@naziv, @obrisan za sql injection, da se ne izvrsava kao poseban upit
                cmd.CommandText = $"INSERT INTO tipNamestaja (Naziv, Obrisan) VALUES (@Naziv, @Obrisan);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();"; // metoda koja vrati id poslednjeg upisanog elementa

                cmd.Parameters.AddWithValue("Naziv", tn.Naziv);
                cmd.Parameters.AddWithValue("Obrisan", tn.Obrisan);

                //upises u bazu, ali ga vratis da dobijes id da mozes dalje da listas??? vidi to kako 
                int newId = int.Parse(cmd.ExecuteScalar().ToString()); // ExecuteScalar izvrsava query nad bazom
                tn.ID = newId;
            }

            //da se doda odma i observable collection, da ne bi bilo drugo stanje, da se doda u bazu a ne prikaze i tako dalje
            Projekat.Instance.tipnamestaja.Add(tn); // Azuriram i model!

            return tn;
        }

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
            /*
            tn.Obrisan = true;
            Update(tn);
            */

            
            //radi u bazi ali sada treba i u modelu da se desi ista stvar da bi okinuli refreshovi
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd0 = con.CreateCommand();
                SqlCommand cmd1 = con.CreateCommand();
                SqlCommand cmd2 = con.CreateCommand();

                cmd0.CommandText = "UPDATE Namestaj SET Obrisan = 1 WHERE TipNamestajaID = @TipNamestajaID;";
                cmd1.CommandText = "UPDATE Namestaj SET TipNamestajaID = NULL WHERE TipNamestajaID = @TipNamestajaID;";
                cmd2.CommandText = "UPDATE TipNamestaja SET Obrisan = 1 WHERE ID = @TipNamestajaID;";
               

                cmd0.Parameters.AddWithValue("TipNamestajaID", tn.ID);
                cmd1.Parameters.AddWithValue("TipNamestajaID", tn.ID);
                cmd2.Parameters.AddWithValue("TipNamestajaID", tn.ID);


                cmd0.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();


                foreach (Namestaj nam in Projekat.Instance.namestaj)
                {
                    if (nam.IDTipaNamestaja == tn.ID)
                    {
                        nam.Obrisan = true;
                    }
                }

                //azuriraj i model:
                foreach (TipNamestaja tip in Projekat.Instance.tipnamestaja)
                {
                    if(tip.ID == tn.ID)
                    {
                        tip.Obrisan = true;
                    }
                }

                



            }

            
        }
        #endregion 



    }
}
