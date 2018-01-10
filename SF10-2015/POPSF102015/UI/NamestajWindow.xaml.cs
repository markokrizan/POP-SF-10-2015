using POP_SF_10_2015.Model;
using POP_SF_10_2015.Util;
using System;
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
    public partial class NamestajWindow : Window
    {

        private Namestaj namestaj;
        private Operacija operacija;
        ICollectionView viewCompoTipovi;
        ICollectionView viewCompoAkcije;

        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        public NamestajWindow(Namestaj namestaj, Operacija operacija)
        {
            //this.namestaj = namestaj;

            

            InitializeComponent();

            //InicijalizujVrednosti(namestaj, operacija);


            //inicijalizacija vrednosti pomocu bindinga
            this.namestaj = namestaj;
            this.operacija = operacija;




            viewCompoTipovi = CollectionViewSource.GetDefaultView(Projekat.Instance.tipnamestaja);
            viewCompoAkcije = CollectionViewSource.GetDefaultView(Projekat.Instance.akcija);
            viewCompoTipovi.Filter = filterTipova;
            viewCompoAkcije.Filter = AkcijeFilter;

            bool filterTipova(object item)
            {
                TipNamestaja tn = item as TipNamestaja;
                return !tn.Obrisan;
            }

            bool AkcijeFilter(object item)
            {
                Akcija akc = item as Akcija;
                return !akc.Obrisana && (Akcija.Uporedi(akc.DatumZavrsetka, DateTime.Now) != "manji");


            }


            cbTipNamestaja.ItemsSource = viewCompoTipovi;
            cbAkcija.ItemsSource = viewCompoAkcije;

            //DataContext je ustvari momenat kada se komponenta mapira na objekat namestaja koji je prosledjen
            //two way binding podrazumevan
            //povezan samo objekat i komponenta ne i xml
            cbTipNamestaja.DataContext = namestaj;
            cbAkcija.DataContext = namestaj;

            //cbTipNamestaja.SelectedIndex = 0;
            //cbTipNamestaja.SelectedItem = 0;
            //cbTipNamestaja.SelectedIndex = cbTipNamestaja.Items.Count - 1;


            //konkretan properti je u xaml-u (Bindig Path = "Naziv")
            //ako je objekat u objektu moze Binding Path = Adresa.Ulica npr
            tbNaziv.DataContext = namestaj;
            tbSifra.DataContext = namestaj;
            tbCena.DataContext = namestaj;
            tbKolicina.DataContext = namestaj;

            //Nek akcija ostane ne selektovana za sada, ako nije selektovana nek joj se doda defaultna akcija koja ima popust 0 posto
            //cbAkcija.SelectedIndex = 1;
            //cbAkcija.SelectedIndex = cbAkcija.Items.Count - 1;
        }


              
        private void sacuvajIzmene(object sender, RoutedEventArgs e)
        {

            //Validate on exceptions zadaje pocetnu vrednost tipa, pa ako uneses asdf za cenu, a tip joj je int cena je 0, pa odatle ovako
            //dalje spageti validacija
            if(namestaj.Cena != 0 && namestaj.Naziv != null && namestaj.Sifra != null && namestaj.KolicinaUMagacinu !=0 && namestaj.Akcija != null && namestaj.KolicinaUMagacinu < 9999999 && namestaj.Cena < 9999999)
            {
                var listaNamestaja = Projekat.Instance.namestaj;


                this.DialogResult = true;

                switch (operacija)
                {
                    case Operacija.DODAVANJE:
                        //namestaj.ID = listaNamestaja.Count + 1;
                        //listaNamestaja.Add(namestaj);

                      
                        


                        NamestajDAL.Create(namestaj);



                        break;



                    case Operacija.IZMENA:

                        /*
                        foreach (var n in listaNamestaja)
                        {
                            if (n.ID == namestaj.ID)
                            {

                                n.IDTipaNamestaja = namestaj.IDTipaNamestaja;
                                n.Naziv = namestaj.Naziv;
                                
                                break;
                            }
                        }
                        */
                        NamestajDAL.Update(namestaj);

                        break;

                }

                //nakon svih izmena serijalizuj, znaci prvo radimo sa temp listom, menjamo je kolko treba i onda se pregazi glavna kolekcija seterom iz Projekat
                //GenericSerializer.Serialize("namestaj.xml", listaNamestaja);
                this.Close();
            }
            else
            {
                if (namestaj.KolicinaUMagacinu == 0)
                {
                    //razdvojiti sad dva problema, da li je bas 0 ili je dao nulu validated on exeption
                    //meh
                    MessageBox.Show("Uneli ste ili 0 ili karaktere!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                    
                }
                if (namestaj.Cena == 0)
                {
                    MessageBox.Show("Nismo socijalna ustanova!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                    
                }
                if (namestaj.Naziv == null)
                {
                    MessageBox.Show("Niste uneli naziv!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (namestaj.Sifra == null)
                {
                    MessageBox.Show("Niste uneli sifru!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (namestaj.Cena >= 9999999)
                {
                    MessageBox.Show("Uneli ste prevelik broj!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (namestaj.KolicinaUMagacinu >= 99999999)
                {
                    MessageBox.Show("Uneli ste prevelik broj!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (namestaj.Akcija == null)
                {
                    foreach(Akcija akc in Projekat.Instance.akcija)
                    {
                        if(akc.Naziv == "Nije na akciji")
                        {
                            namestaj.Akcija = akc;
                        }
                    }
                }
            }
            
        }

        private void Izadji(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
