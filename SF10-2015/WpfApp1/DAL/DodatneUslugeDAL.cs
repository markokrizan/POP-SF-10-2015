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
    class DodatneUslugeDAL
    {

        #region Database
        public static ObservableCollection<DodatneUsluge> GetAll()
        {
            var dodatneUsluge = new ObservableCollection<DodatneUsluge>();

            //tu je ona referenca koju smo otkacili
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {

                SqlCommand cmd = con.CreateCommand();
                //obrisan true 1 false 0
                cmd.CommandText = "SELECT * FROM DodatneUsluge WHERE Obrisan = 0";


                //ds i adapter samo kada dobavljamo podatke
                SqlDataAdapter adapter = new SqlDataAdapter();

                //objekat koji moze u sebi da ima vise logickih tabela, objekat u memoriji koji reprezentuje bazu
                DataSet ds = new DataSet();


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
                    dodatneUsluge.Add(du);
                }
            }

            //vrati kolekciju
            return dodatneUsluge;
        }

        public static DodatneUsluge Create(DodatneUsluge du)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {
                //otvori konekciju
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                //@naziv, @obrisan za sql injection, da se ne izvrsava kao poseban upit
                cmd.CommandText = $"INSERT INTO DodatneUsluge (Naziv, Obrisan, Cena) VALUES (@Naziv, @Obrisan, @Cena);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();"; // metoda koja vrati id poslednjeg upisanog elementa

                cmd.Parameters.AddWithValue("Naziv", du.Naziv);
                cmd.Parameters.AddWithValue("Obrisan", du.Obrisan);
                cmd.Parameters.AddWithValue("Cena", du.Cena);

                //upises u bazu, ali ga vratis da dobijes id da mozes dalje da listas??? vidi to kako 
                int newId = int.Parse(cmd.ExecuteScalar().ToString()); // ExecuteScalar izvrsava query nad bazom
                du.ID = newId;
            }

            //da se doda odma i observable collection, da ne bi bilo drugo stanje, da se doda u bazu a ne prikaze i tako dalje
            Projekat.Instance.dodatneusluge.Add(du);// Azuriram i model!

            return du;
        }

        public static void Update(DodatneUsluge du)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE DodatneUsluge " +
                    "SET Naziv = @Naziv, " +
                    "Obrisan = @Obrisan,  " +
                    "Cena = @Cena  " +
                    "WHERE ID = @ID; ";

                cmd.Parameters.AddWithValue("ID", du.ID);
                cmd.Parameters.AddWithValue("Naziv", du.Naziv);
                cmd.Parameters.AddWithValue("Obrisan", du.Obrisan);
                cmd.Parameters.AddWithValue("Cena", du.Cena);

                cmd.ExecuteNonQuery();

                // Azuriram model, listu u modelu
                foreach (var dodatnaUsluga in Projekat.Instance.tipnamestaja)
                {
                    if (dodatnaUsluga.ID == du.ID)
                    {
                        dodatnaUsluga.Naziv = du.Naziv;
                        dodatnaUsluga.Obrisan = du.Obrisan;
                        break;
                    }
                }
            }
        }

        public static void Delete(DodatneUsluge du)
        {
            //setuj ga na true i samo ga prosledi update funkciji
            du.Obrisan = true;
            Update(du);
        }
        #endregion 
    }
}
