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
    class NamestajDAL
    {
        #region Database
        public static ObservableCollection<Namestaj> GetAll()
        {
            var namestaj = new ObservableCollection<Namestaj>();

            //tu je ona referenca koju smo otkacili
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {

                SqlCommand cmd = con.CreateCommand();
                //obrisan true 1 false 0
                cmd.CommandText = "SELECT * FROM Namestaj WHERE obrisan = 0";


                //ds i adapter samo kada dobavljamo podatke
                SqlDataAdapter adapter = new SqlDataAdapter();

                //objekat koji moze u sebi da ima vise logickih tabela, objekat u memoriji koji reprezentuje bazu
                DataSet ds = new DataSet();

                //smesti u data set pod nazivom tabele tipNamestaja
                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "Namestaj"); // izvrsava se query nad bazom

                //za svaki red u data setu u tabelama tim i tim
                foreach (DataRow row in ds.Tables["Namestaj"].Rows)
                {
                    var nam = new Namestaj();
                    //svi su tipa object, zato se radi ToStrint()
                    nam.ID = int.Parse(row["ID"].ToString());
                    nam.IDTipaNamestaja = int.Parse(row["TipNamestajaID"].ToString());
                    nam.IDAkcije = int.Parse(row["AkcijaID"].ToString());
                    //AKCIJA -- dodat default 0
                    nam.Naziv = row["Naziv"].ToString();
                    nam.Sifra = row["Sifra"].ToString();
                    nam.Cena = Double.Parse(row["Cena"].ToString());
                    nam.KolicinaUMagacinu = Int32.Parse(row["Kolicina"].ToString());
                    nam.Obrisan = bool.Parse(row["Obrisan"].ToString());
                    //napuni u svakom prolazu puni kontejnersku kolekciju
                    namestaj.Add(nam);
                }
            }

            //vrati kolekciju
            return namestaj;
        }

        public static Namestaj Create(Namestaj nam)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {
                //otvori konekciju
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                //@naziv, @obrisan za sql injection, da se ne izvrsava kao poseban upit
                //AkcijaID posle TipNamestajaID
                cmd.CommandText = $"INSERT INTO Namestaj (TipNamestajaID, AkcijaID, Naziv, Sifra,  Cena, Kolicina, Obrisan) VALUES (@TipNamestajaID, @AkcijaID, @Naziv, @Sifra,  @Cena, @Kolicina, @Obrisan);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();"; // metoda koja vrati id poslednjeg upisanog elementa

                //kada ovde prosledis namestaj iz namestajWindow, ti bindingom za tip namestaja posaljes njemu ceo objekat
                //u kompo boksu je samo lista objekata sa overridovanim to string da pokazuje naziv, ali ovde za tip namestaja stize ceo objekat

                //cmd.Parameters.AddWithValue("TipNamestajaID", nam.IDTipaNamestaja);
                cmd.Parameters.AddWithValue("TipNamestajaID", nam.TipNamestaja.ID);
                cmd.Parameters.AddWithValue("AkcijaID", nam.Akcija.ID);
                cmd.Parameters.AddWithValue("Naziv", nam.Naziv);
                cmd.Parameters.AddWithValue("Sifra", nam.Sifra);
                cmd.Parameters.AddWithValue("Cena", nam.Cena);
                cmd.Parameters.AddWithValue("Kolicina", nam.KolicinaUMagacinu);
                cmd.Parameters.AddWithValue("Obrisan", nam.Obrisan);


                //upises u bazu, ali ga vratis da dobijes id da mozes dalje da listas??? vidi to kako 
                int newId = int.Parse(cmd.ExecuteScalar().ToString()); // ExecuteScalar izvrsava query nad bazom
                nam.ID = newId;
            }

            //da se doda odma i observable collection, da ne bi bilo drugo stanje, da se doda u bazu a ne prikaze i tako dalje
            Projekat.Instance.namestaj.Add(nam); // Azuriram i model!

            return nam;
        }

        public static void Update(Namestaj nam)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Namestaj " +

                    "SET TipNamestajaID = @TipNamestajaID, " +
                    "AkcijaID = @AkcijaID,  " +
                    "Naziv = @Naziv,  " +
                    "Sifra = @Sifra,  " +
                    "Cena = @Cena,  " +
                    "Kolicina = @Kolicina,  " +
                    "Obrisan = @Obrisan " +
                    "WHERE ID = @ID; ";

                cmd.Parameters.AddWithValue("ID", nam.ID);

                //cmd.Parameters.AddWithValue("TipNamestajaID", nam.IDTipaNamestaja);
                cmd.Parameters.AddWithValue("TipNamestajaID", nam.TipNamestaja.ID);
                cmd.Parameters.AddWithValue("AkcijaID", nam.Akcija.ID);
                cmd.Parameters.AddWithValue("Naziv", nam.Naziv);
                cmd.Parameters.AddWithValue("Sifra", nam.Sifra);
                cmd.Parameters.AddWithValue("Cena", nam.Cena);
                cmd.Parameters.AddWithValue("Kolicina", nam.KolicinaUMagacinu);
                cmd.Parameters.AddWithValue("Obrisan", nam.Obrisan);

                cmd.ExecuteNonQuery();

                // Azuriram model, listu u modelu
                foreach (var n in Projekat.Instance.namestaj)
                {
                    if (n.ID == nam.ID)
                    {
                        n.IDTipaNamestaja = nam.IDTipaNamestaja;
                        n.IDAkcije = nam.IDAkcije;

                        //mozda?
                        n.Akcija = nam.Akcija;
                        n.TipNamestaja = nam.TipNamestaja;


                        n.Naziv = nam.Naziv;
                        n.Sifra = nam.Sifra;
                        n.Cena = nam.Cena;
                        n.KolicinaUMagacinu = nam.KolicinaUMagacinu;
                        n.Obrisan = nam.Obrisan;

                        break;
                    }
                }
            }
        }

        public static void AnulirajTip(TipNamestaja tn)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {
                con.Open();

                
                SqlCommand cmd1 = con.CreateCommand();              
                cmd1.CommandText = "UPDATE Namestaj SET TipNamestajaID = NULL WHERE TipNamestajaID = @TipNamestajaID;";              
                cmd1.Parameters.AddWithValue("TipNamestajaID", tn.ID);              
                cmd1.ExecuteNonQuery();
                

                



            }
        }









        public static void Delete(Namestaj nam)
        {
            //setuj ga na true i samo ga prosledi update funkciji
            nam.Obrisan = true;
            Update(nam);
        }
        #endregion
    }
}
