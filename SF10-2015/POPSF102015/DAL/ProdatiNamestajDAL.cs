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
using WpfApp1.Model;

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
                cmd.Parameters.AddWithValue("IDRacuna", rac.ID);
                cmd.Parameters.AddWithValue("IDNamestaja", nam.ID);


                cmd.ExecuteScalar();
                /*
                 * 
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

        //funkcija koja ce vaditi za odredjeni racun njene prodate namestaje
        //koristi ovaj upit:
        //SELECT * FROM Namestaj WHERE ID IN (SELECT IDNamestaja FROM ProdatiNamestaj WHERE IDRacuna = 8) AND Obrisan != 1;


        
        public static ObservableCollection<Namestaj> GetAll(Racun rac)
        {

            var namestaj = new ObservableCollection<Namestaj>();

            //tu je ona referenca koju smo otkacili
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {

                SqlCommand cmd = con.CreateCommand();
                //obrisan true 1 false 0
                cmd.CommandText = "SELECT * FROM Namestaj WHERE ID IN (SELECT IDNamestaja FROM ProdatiNamestaj WHERE IDRacuna = @IDRacuna);";
                cmd.Parameters.AddWithValue("IDRacuna", rac.ID);

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

        

        public static ObservableCollection<StavkaProdaje> StavkeNamestajaPoRacunu(Racun rac)
        {

            var stavkeProdaje = new ObservableCollection<StavkaProdaje>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {

                SqlCommand cmd = con.CreateCommand();
                //obrisan true 1 false 0
                cmd.CommandText = "SELECT IDNamestaja, COUNT(*) AS 'BrojKomada' FROM ProdatiNamestaj WHERE IDRacuna = @IDRacuna GROUP BY IDNamestaja;";
                cmd.Parameters.AddWithValue("IDRacuna", rac.ID);

                //ds i adapter samo kada dobavljamo podatke
                SqlDataAdapter adapter = new SqlDataAdapter();

                //objekat koji moze u sebi da ima vise logickih tabela, objekat u memoriji koji reprezentuje bazu
                DataSet ds = new DataSet();

                //smesti u data set pod nazivom tabele tipNamestaja
                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "ProdatiNamestaj"); // izvrsava se query nad bazom

                //za svaki red u data setu u tabelama tim i tim
                foreach (DataRow row in ds.Tables["ProdatiNamestaj"].Rows)
                {
                    StavkaProdaje stavka = new StavkaProdaje();


                    foreach(Namestaj nam in Projekat.Instance.namestaj)
                    {
                        if (nam.ID == int.Parse(row["IDNamestaja"].ToString()))
                        {
                            stavka.Namestaj = nam;
                        }
                    }

                    stavka.Kolicina = int.Parse(row["BrojKomada"].ToString());

                   

                    stavkeProdaje.Add(stavka);
                }
            }

            return stavkeProdaje;
        }


        }


}
