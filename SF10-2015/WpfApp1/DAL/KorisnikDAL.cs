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
    class KorisnikDAL
    {

        #region Database
        public static ObservableCollection<Korisnik> GetAll()
        {
            var korisnici = new ObservableCollection<Korisnik>();

            //tu je ona referenca koju smo otkacili
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {

                SqlCommand cmd = con.CreateCommand();
                //obrisan true 1 false 0
                cmd.CommandText = "SELECT * FROM Korisnici WHERE Obrisan = 0";


                //ds i adapter samo kada dobavljamo podatke
                SqlDataAdapter adapter = new SqlDataAdapter();

                //objekat koji moze u sebi da ima vise logickih tabela, objekat u memoriji koji reprezentuje bazu
                DataSet ds = new DataSet();


                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "Korisnici"); // izvrsava se query nad bazom

                //za svaki red u data setu u tabelama tim i tim
                foreach (DataRow row in ds.Tables["Korisnici"].Rows)
                {
                    var kor = new Korisnik();
                    //svi su tipa object, zato se radi ToStrint()
                    kor.ID = int.Parse(row["ID"].ToString());
                    kor.Obrisan = bool.Parse(row["Obrisan"].ToString());
                    kor.Ime = row["Ime"].ToString();
                    kor.Prezime = row["Prezime"].ToString();
                    kor.KorIme = row["KorIme"].ToString();
                    kor.Lozinka = row["Lozinka"].ToString();
                    //vidi jos ovo
                    kor.TipKorisnika = (TipKorisnika)(Int32.Parse(row["TipKorisnika"].ToString()));
                    

                    //napuni u svakom prolazu puni kontejnersku kolekciju
                    korisnici.Add(kor);
                }
            }

            //vrati kolekciju (U konstruktoru Projekat)
            return korisnici;
        }

        public static Korisnik Create(Korisnik kor)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {
                //otvori konekciju
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                //@naziv, @obrisan za sql injection, da se ne izvrsava kao poseban upit
                cmd.CommandText = $"INSERT INTO Korisnici (Obrisan, Ime, Prezime, KorIme, Lozinka, TipKorisnika) VALUES (@Obrisan, @Ime, @Prezime, @KorIme, @Lozinka, @TipKorisnika);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();"; // metoda koja vrati id poslednjeg upisanog elementa

                cmd.Parameters.AddWithValue("Obrisan", kor.Obrisan);
                cmd.Parameters.AddWithValue("Ime", kor.Ime);
                cmd.Parameters.AddWithValue("Prezime", kor.Prezime);
                cmd.Parameters.AddWithValue("KorIme", kor.KorIme);
                cmd.Parameters.AddWithValue("Lozinka", kor.Lozinka);
                cmd.Parameters.AddWithValue("TipKorisnika", (int)kor.TipKorisnika);


                //upises u bazu, ali ga vratis da dobijes id da mozes dalje da listas??? vidi to kako 
                int newId = int.Parse(cmd.ExecuteScalar().ToString()); // ExecuteScalar izvrsava query nad bazom
                kor.ID = newId;
            }

            //da se doda odma i observable collection, da ne bi bilo drugo stanje, da se doda u bazu a ne prikaze i tako dalje
            Projekat.Instance.korisnici.Add(kor);// Azuriram i model!

            return kor;
        }

        public static void Update(Korisnik kor)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Korisnici " +
                    "SET Obrisan = @Obrisan, " +
                    "Ime = @Ime,  " +
                    "Prezime = @Prezime,  " +
                    "KorIme = @KorIme,  " +
                    "Lozinka = @Lozinka  " +
                    "WHERE ID = @ID; ";

                cmd.Parameters.AddWithValue("ID", kor.ID);
                cmd.Parameters.AddWithValue("Obrisan", kor.Obrisan);
                cmd.Parameters.AddWithValue("Ime", kor.Ime);
                cmd.Parameters.AddWithValue("Prezime", kor.Prezime);
                cmd.Parameters.AddWithValue("KorIme", kor.KorIme);
                cmd.Parameters.AddWithValue("Lozinka", kor.Lozinka);

                cmd.ExecuteNonQuery();

                // Azuriram model, listu u modelu
                foreach (var korisnik in Projekat.Instance.korisnici)
                {
                    if (korisnik.ID == kor.ID)
                    {
                        korisnik.Obrisan = kor.Obrisan;
                        korisnik.Ime = kor.Ime;
                        korisnik.Prezime = kor.Prezime;
                        korisnik.KorIme = kor.KorIme;
                        korisnik.Lozinka = kor.Lozinka;
                        korisnik.TipKorisnika = kor.TipKorisnika;
                        break;
                    }
                }
            }
        }

        public static void Delete(Korisnik kor)
        {
            //setuj ga na true i samo ga prosledi update funkciji
            kor.Obrisan = true;
            Update(kor);
        }
        #endregion 
    }
}
