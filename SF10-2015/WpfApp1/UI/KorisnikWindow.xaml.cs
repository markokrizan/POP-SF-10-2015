using POP_SF_10_2015.Model;
using POP_SF_10_2015.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.DAL;

namespace WpfApp1.UI
{
    /// <summary>
    /// Interaction logic for NamestajWindow.xaml
    /// </summary>
    public partial class KorisnikWindow : Window
    {

        private Korisnik korisnik;
        private Operacija operacija;
        

        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        public KorisnikWindow(Korisnik korisnik, Operacija operacija)
        {
            //this.namestaj = namestaj;

            

            InitializeComponent();

            //InicijalizujVrednosti(namestaj, operacija);


            //inicijalizacija vrednosti pomocu bindinga
            this.korisnik = korisnik;
            this.operacija = operacija;



            
            //cbTipKorisnika.ItemsSource = (IEnumerable)korisnik;
            cbTipKorisnika.ItemsSource = Enum.GetValues(typeof(TipKorisnika)).Cast<TipKorisnika>();
                      
            tbIme.DataContext = korisnik;
            tbPrezime.DataContext = korisnik;
            tbKorisnicko.DataContext = korisnik;
            tbLozinka.DataContext = korisnik;
        }


      
        
        private void sacuvajIzmene(object sender, RoutedEventArgs e)
        {

            if(korisnik.Ime != null && korisnik.Prezime != null && korisnik.KorIme != null && korisnik.Lozinka != null)
            {
                var listaKorisnika = Projekat.Instance.korisnici;


                this.DialogResult = true;

                switch (operacija)
                {
                    case Operacija.DODAVANJE:
                        //korisnik.ID = listaKorisnika.Count + 1;
                        //listaKorisnika.Add(korisnik);


                        KorisnikDAL.Create(korisnik);



                        break;



                    case Operacija.IZMENA:


                        foreach (var k in listaKorisnika)
                        {
                            if (k.ID == korisnik.ID)
                            {

                                k.Ime = korisnik.Ime;
                                k.Prezime = korisnik.Prezime;
                                k.KorIme = korisnik.KorIme;
                                k.Lozinka = korisnik.Lozinka;
                                k.TipKorisnika = korisnik.TipKorisnika;
                                KorisnikDAL.Update(k);
                                break;
                            }
                        }


                        break;

                }

                //nakon svih izmena serijalizuj, znaci prvo radimo sa temp listom, menjamo je kolko treba i onda se pregazi glavna kolekcija seterom iz Projekat
                //GenericSerializer.Serialize("korisnici.xml", listaKorisnika);
                this.Close();
            }
            else
            {

                if (korisnik.Ime == null)
                {
                    MessageBox.Show("Niste uneli ime!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (korisnik.Prezime == null)
                {
                    MessageBox.Show("Uneli uneli prezime!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (korisnik.KorIme == null)
                {
                    MessageBox.Show("Niste uneli korisnicko ime!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (korisnik.Lozinka == null)
                {
                    MessageBox.Show("Niste uneli lozinku!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }

        private void Izadji(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
