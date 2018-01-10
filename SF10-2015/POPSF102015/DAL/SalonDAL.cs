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
    class SalonDAL
    {

        public static ObservableCollection<Salon> GetAll()
        {
            var salon = new ObservableCollection<Salon>();
            //var s = new Salon();

            //tu je ona referenca koju smo otkacili
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Platforme"].ConnectionString))
            {

                SqlCommand cmd = con.CreateCommand();
                //obrisan true 1 false 0
                cmd.CommandText = "SELECT * FROM Salon";


                //ds i adapter samo kada dobavljamo podatke
                SqlDataAdapter adapter = new SqlDataAdapter();

                //objekat koji moze u sebi da ima vise logickih tabela, objekat u memoriji koji reprezentuje bazu
                DataSet ds = new DataSet();


                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "Salon"); // izvrsava se query nad bazom

                //za svaki red u data setu u tabelama tim i tim
                foreach (DataRow row in ds.Tables["Salon"].Rows)
                {
                    var s = new Salon();
                    //svi su tipa object, zato se radi ToStrint()
                    s.Naziv = row["Naziv"].ToString();
                    s.Adresa = row["Adresa"].ToString();
                    s.Telefon = row["Telefon"].ToString();
                    s.EMail = row["EMail"].ToString();
                    s.PIB = row["Pib"].ToString();
                    s.MaticniBroj = row["MaticniBroj"].ToString();
                    s.BrojZiroRacuna = row["BrojZiroRacuna"].ToString();
                    s.Sajt = row["Sajt"].ToString();

                    salon.Add(s);
                    //napuni u svakom prolazu puni kontejnersku kolekciju

                }
            }

            //vrati kolekciju (U konstruktoru Projekat)
            return salon;
        }
    }
}
