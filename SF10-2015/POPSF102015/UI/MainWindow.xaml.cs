using POP_SF_10_2015.Model;
using POP_SF_10_2015.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ICollectionView viewNamestaj;
        ICollectionView viewUsluge;
        ICollectionView viewTipovi;
        ICollectionView viewKorisnici;
        ICollectionView viewAkcije;
        ICollectionView viewRacuni;

        //Pretraga:
        ICollectionView viewTrazeniNamestaj;
        ICollectionView viewTrazeneUsluge;
        ICollectionView viewTrazeniKorisnici;
        ICollectionView viewTrazeniRacuni;
        ICollectionView viewTrazeneAkcije;
        ICollectionView viewTrazeniTipovi;

        Racun trenutniRacun;
        Korisnik trenutniKorisnik;

        public MainWindow(Korisnik korisnik)
        {

            InitializeComponent();
            //MessageBox.Show(DateTime.Now.ToString());            
            viewNamestaj = CollectionViewSource.GetDefaultView(Projekat.Instance.namestaj);
            viewUsluge = CollectionViewSource.GetDefaultView(Projekat.Instance.dodatneusluge);
            viewTipovi = CollectionViewSource.GetDefaultView(Projekat.Instance.tipnamestaja);
            viewKorisnici = CollectionViewSource.GetDefaultView(Projekat.Instance.korisnici);
            viewAkcije = CollectionViewSource.GetDefaultView(Projekat.Instance.akcija);
            viewRacuni = CollectionViewSource.GetDefaultView(Projekat.Instance.racun);
            trenutniRacun = new Racun();
            this.trenutniKorisnik = korisnik;


            //delegat kojem se prosledjuje funkcija
            viewNamestaj.Filter = NamestajFilter;
            viewUsluge.Filter = UslugeFilter;
            viewTipovi.Filter = TipoviFilter;
            viewKorisnici.Filter = KorisniciFilter;
            viewAkcije.Filter = AkcijeFilter;


            
            bool NamestajFilter(object item)
            {
                Namestaj nam = item as Namestaj;
                return !nam.Obrisan;
            }

            bool UslugeFilter(object item)
            {
                DodatneUsluge du = item as DodatneUsluge;
                return !du.Obrisan;
            }

            bool TipoviFilter(object item)
            {
                if(item != null)
                {
                    TipNamestaja tn = item as TipNamestaja;
                    return !tn.Obrisan;
                }
                return true;
                
               
                
            }

            bool KorisniciFilter(object item)
            {
                Korisnik kor = item as Korisnik;
                return !kor.Obrisan;
            }

            bool AkcijeFilter(object item)
            {
                if(item != null)
                {
                    Akcija akc = item as Akcija;
                    return !akc.Obrisana && (Akcija.Uporedi(akc.DatumZavrsetka, DateTime.Now) != "manji");
                }
                return true;
                
                    
            }

           



            ////////////SOURCE NAMESTAJ////////////
            dgNamestaj.ItemsSource = viewNamestaj;            
            //da selected item ne bi bio null
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            //ovo resava i prazan prostor            
            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


            ////////////SOURCE USLUGE////////////
            dgUsluge.ItemsSource = viewUsluge;
            dgUsluge.IsSynchronizedWithCurrentItem = true;
            dgUsluge.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


            ////////////SOURCE TIP NAMESTAJA////////////
            dgTipNamestaja.ItemsSource = viewTipovi;
            dgTipNamestaja.IsSynchronizedWithCurrentItem = true;
            dgTipNamestaja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            ////////////SOURCE KORISNICI////////////
            dgKorisnici.ItemsSource = viewKorisnici;
            dgKorisnici.IsSynchronizedWithCurrentItem = true;
            dgKorisnici.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            ////////////SOURCE AKCIJE////////////
            dgAkcije.ItemsSource = viewAkcije;
            dgAkcije.IsSynchronizedWithCurrentItem = true;
            dgAkcije.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            ////////////SOURCE RACUNI////////////
            dgRacuni.ItemsSource = viewRacuni;
            dgRacuni.IsSynchronizedWithCurrentItem = true;
            dgRacuni.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


            ////////////SOURCE PRODAJA////////////
            lblUkupnaCena.DataContext = trenutniRacun;
            tbKupac.DataContext = trenutniRacun;
            tbBrojRacuna.DataContext = trenutniRacun;


            //PRODAVAC VS ADMIN

            if(trenutniKorisnik.TipKorisnika == TipKorisnika.Prodavac)
            {
                btnDodajNamestaj.Visibility = Visibility.Collapsed;
                btnIzmeniNamestaj.Visibility = Visibility.Collapsed;
                btnObrisiNamestaj.Visibility = Visibility.Collapsed;

                btnDodajTip.Visibility = Visibility.Collapsed;
                btnIzmeniTip.Visibility = Visibility.Collapsed;
                btnObrisiTip.Visibility = Visibility.Collapsed;

                btnDodajUslugu.Visibility = Visibility.Collapsed;
                btnIzmeniUslugu.Visibility = Visibility.Collapsed;
                btnObrisiUslugu.Visibility = Visibility.Collapsed;

                btnDodajAkciju.Visibility = Visibility.Collapsed;
                btnIzmeniAkciju.Visibility = Visibility.Collapsed;
                btnObrisiAkciju.Visibility = Visibility.Collapsed;

                btnDodajKorisnika.Visibility = Visibility.Collapsed;
                btnIzmeniKorisnika.Visibility = Visibility.Collapsed;
                btnObrisiKorisnika.Visibility = Visibility.Collapsed;

                TabKorisnici.Visibility = Visibility.Collapsed;

            }




        }


        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /////////////////////////////TAB PRODAJE////////////////////////////////////////

        //proveri da li ce raditi selected item ako je lokalna promenjiva
        //ObservableCollection<Namestaj> listaTrazenogNamestaja;
        //ObservableCollection<DodatneUsluge> listaTrazenihUsluga;

        private void PretraziNamestaj()
        {
            string Unos = tbPretragaNamestaj.Text.ToLower();
            ObservableCollection<Namestaj> listaTrazenogNamestaja = new ObservableCollection<Namestaj>();

            foreach (Namestaj nam in Projekat.Instance.namestaj)
            {

                //CONTAINS
                if (nam.Naziv.ToLower().Trim().Contains(Unos) || nam.Sifra.ToLower().Trim().Contains(Unos) || nam.TipNamestaja.Naziv.ToLower().Trim().Contains(Unos))
                {
                    listaTrazenogNamestaja.Add(nam);
                }



            }

            viewTrazeniNamestaj = CollectionViewSource.GetDefaultView(listaTrazenogNamestaja);

            //umesto u uslovu filter vamo
            //filter okida stalno tako da vazi dole i za promene u prodaji
            viewTrazeniNamestaj.Filter = TrazeniNamestajFilter;
            bool TrazeniNamestajFilter(object item)
            {
                Namestaj nam = item as Namestaj;
                return !nam.Obrisan;
            }
            dgPretragaNamestaj.ItemsSource = viewTrazeniNamestaj;
            dgPretragaNamestaj.IsSynchronizedWithCurrentItem = true;
            dgPretragaNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            viewTrazeniNamestaj.Refresh();

        }



        private void PretraziNamestaj(object sender, RoutedEventArgs e)
        {

            PretraziNamestaj();

        }


        private void tbPretragaNamestaj_TextChanged(object sender, TextChangedEventArgs e)
        {
            PretraziNamestaj();
        }

        /*
        
        


            //metoda filtera, vraca redove koji nisu obrisani
            //pokusaj da smanjis malo redudantnost ovde
       

        
        */

        private void PretraziUsluge()
        {
            string Unos = tbPretragaUsluge.Text.ToLower();
            ObservableCollection<DodatneUsluge> listaTrazenihUsluga = new ObservableCollection<DodatneUsluge>();

            foreach (DodatneUsluge us in Projekat.Instance.dodatneusluge)
            {

                if (us.Naziv.ToLower().Contains(Unos))
                {
                    listaTrazenihUsluga.Add(us);
                }

            }

            viewTrazeneUsluge = CollectionViewSource.GetDefaultView(listaTrazenihUsluga);
            viewTrazeneUsluge.Filter = TrazeneUslugeFilter;
            bool TrazeneUslugeFilter(object item)
            {
                DodatneUsluge du = item as DodatneUsluge;
                return !du.Obrisan;
            }
            dgPretragaUsluge.ItemsSource = viewTrazeneUsluge;
            dgPretragaUsluge.IsSynchronizedWithCurrentItem = true;
            dgPretragaUsluge.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            viewTrazeneUsluge.Refresh();
        }

        private void PretraziUsluge(object sender, RoutedEventArgs e)
        {

            PretraziUsluge();
        }

        private void tbPretragaUsluge_TextChanged(object sender, TextChangedEventArgs e)
        {
            PretraziUsluge();
        }

        private void dgPretragaNamestaj_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }

            if ((string)e.Column.Header == "IDTipaNamestaja")
            {
                e.Cancel = true;
            }

            if ((string)e.Column.Header == "ID")
            {
                e.Cancel = true;
            }

            if ((string)e.Column.Header == "IDAkcije")
            {
                e.Cancel = true;
            }

            if ((string)e.Column.Header == "KolicinaUMagacinu")
            {
                e.Column.Header = "Kolicina";
            }
        }

        private void dgPretragaUsluge_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }

            if ((string)e.Column.Header == "ID")
            {
                e.Cancel = true;
            }


        }

        //FORMIRANJE RACUNA:

        
               
        private void DodajNamestajURacun(object sender, RoutedEventArgs e)
        {
            Namestaj selektovaniNamestaj = (Namestaj)dgPretragaNamestaj.SelectedItem;
            int kolicinaZaProdaju;

            if(selektovaniNamestaj != null)
            {
                KolicinaWindow kw = new KolicinaWindow(selektovaniNamestaj);
                kw.Owner = this;
                kw.ShowDialog();
                kolicinaZaProdaju = kw.Kolicina;
                //MessageBox.Show(kw.Kolicina.ToString());
                for(int i = 0; i <kolicinaZaProdaju; i++)
                {
                    if (selektovaniNamestaj.KolicinaUMagacinu >= 1)
                    {
                        lbRacun.Items.Add(selektovaniNamestaj);
                        trenutniRacun.UkupnaCena = trenutniRacun.UkupnaCena + (selektovaniNamestaj.AkcijskaCena(selektovaniNamestaj.Cena, selektovaniNamestaj.Akcija.Popust));
                        selektovaniNamestaj.KolicinaUMagacinu = selektovaniNamestaj.KolicinaUMagacinu - 1;
                        NamestajDAL.Update(selektovaniNamestaj);
                        viewTrazeniNamestaj.Refresh();
                        viewNamestaj.Refresh();
                    }
                    else if (selektovaniNamestaj.KolicinaUMagacinu == 0)
                    {
                        /*
                        lbRacun.Items.Add(selektovaniNamestaj);
                        trenutniRacun.UkupnaCena = trenutniRacun.UkupnaCena + (selektovaniNamestaj.AkcijskaCena(selektovaniNamestaj.Cena, selektovaniNamestaj.Akcija.Popust));
                        selektovaniNamestaj.KolicinaUMagacinu = selektovaniNamestaj.KolicinaUMagacinu - 1;
                        NamestajDAL.Delete(selektovaniNamestaj);
                        viewTrazeniNamestaj.Refresh();
                        viewNamestaj.Refresh();
                        */
                        MessageBox.Show("Nema ga na stanju!");
                    }
                }

            }
            else
            {
                MessageBox.Show("Niste nista selektovali!");
            }
            









            /*

            Namestaj selektovaniNamestaj = (Namestaj)dgPretragaNamestaj.SelectedItem;
            
            
            if(selektovaniNamestaj.KolicinaUMagacinu != 1)
            {
                lbRacun.Items.Add(selektovaniNamestaj);
                trenutniRacun.UkupnaCena = trenutniRacun.UkupnaCena + (selektovaniNamestaj.AkcijskaCena(selektovaniNamestaj.Cena, selektovaniNamestaj.Akcija.Popust));
                selektovaniNamestaj.KolicinaUMagacinu = selektovaniNamestaj.KolicinaUMagacinu - 1;
                NamestajDAL.Update(selektovaniNamestaj);
                viewTrazeniNamestaj.Refresh();
                viewNamestaj.Refresh();
            }else if(selektovaniNamestaj.KolicinaUMagacinu == 1)
            {
                lbRacun.Items.Add(selektovaniNamestaj);
                trenutniRacun.UkupnaCena = trenutniRacun.UkupnaCena + (selektovaniNamestaj.AkcijskaCena(selektovaniNamestaj.Cena, selektovaniNamestaj.Akcija.Popust));
                selektovaniNamestaj.KolicinaUMagacinu = selektovaniNamestaj.KolicinaUMagacinu - 1;
                NamestajDAL.Delete(selektovaniNamestaj);
                viewTrazeniNamestaj.Refresh();
                viewNamestaj.Refresh();
            }


            //NekiProzor.ShowDialog ce stopirati aplikaciju dok se prozor ne zaustavi

            */

        }

        private void DodajUsluguURacun(object sender, RoutedEventArgs e)
        {
            DodatneUsluge selektovanaUsluga = (DodatneUsluge)dgPretragaUsluge.SelectedItem;
            lbRacun.Items.Add(selektovanaUsluga);
            trenutniRacun.UkupnaCena = trenutniRacun.UkupnaCena + selektovanaUsluga.Cena;
            //trenutniRacun.DodatneUsluge.Add(selektovanaUsluga);

        }


       
        
        private void ZavrsiProdaju(object sender, RoutedEventArgs e)
        {

            //Verovatno najveci primer spageti koda ikad napisan
            //Sramota
            //Ali radi 
            
        

            if (lbRacun.Items.Count != 0 && trenutniRacun.BrojRacuna != null && trenutniRacun.Kupac != null)
            {
                var listaRacuna = Projekat.Instance.racun;

                trenutniRacun.DatumProdaje = DateTime.Now;
                trenutniRacun.UkupnaCena = Racun.CenaSaPDV(trenutniRacun.UkupnaCena);
                
                //pomocu executeScalar:
                //trenutniRacun.ID = listaRacuna.Count + 1;

                //uzmi sad one koje si dodao u listBox
                //Cenu si u menjuvremenu menjao pri dodavanju u listBox
                var lbRacunKolekcija = lbRacun.Items;

                

                RacunDAL.Create(trenutniRacun);


                //id ce se dodati u DAL-u
                //Dodaj u dve kolekcije:

                foreach (var i in lbRacunKolekcija)
                {
                    if (i is Namestaj)
                    {
                        //trenutniRacun.NamestajZaProdaju.Add((Namestaj)i);
                        ProdatiNamestajDAL.Create((Namestaj)i, trenutniRacun);
                    }
                    else
                    {
                        //trenutniRacun.DodatneUsluge.Add((DodatneUsluge)i);
                        ProdateUslugeDAL.Create((DodatneUsluge)i, trenutniRacun);
                    }
                }

                viewRacuni.Refresh();

                //u dal-u se azurira i model
                //listaRacuna.Add(trenutniRacun);


                //RacunDAL.Create(trenutniRacun);

                //GenericSerializer.Serialize("racuni.xml", listaRacuna);
                //RacunDAL.Create(trenutniRacun);

                //ponovo ga setuj na prazan objekat:
                trenutniRacun = new Racun();
                //Racun noviRacun = new Racun();
                

                MessageBox.Show("Prodaja izvrsena");

                //ocisti posle prodaje da pripremis za drugu:
                /*
                tbBrojRacuna.Text = "";
                tbKupac.Text = "";            
                lblUkupnaCena.Content = trenutniRacun.UkupnaCena;
                lbRacun.Items.Clear();
                */
                //REBIND:
                lblUkupnaCena.DataContext = trenutniRacun;
                tbKupac.DataContext = trenutniRacun;
                tbBrojRacuna.DataContext = trenutniRacun;
                lbRacun.Items.Clear();
                //ocisti sve i pripremi za sledecu prodaju:

            }
            else
            {
                if(lbRacun.Items.Count == 0)
                {
                    MessageBox.Show("Niste uneli nista u racun!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (trenutniRacun.BrojRacuna == null)
                {
                    MessageBox.Show("Niste uneli broj racuna!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (trenutniRacun.Kupac == null)
                {
                    MessageBox.Show("Niste uneli ime i prezime kupca!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }





        }


        
        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {

            if(lbRacun.Items.Count != 0)
            {
                Object selektovani = lbRacun.SelectedValue;

                double cenaSelektovanog = 0;
                



                if (selektovani is Namestaj)
                {

                    
                    Namestaj selektovaniNamestaj = selektovani as Namestaj;
                    
                    cenaSelektovanog = selektovaniNamestaj.AkcijskaCena(selektovaniNamestaj.Cena, selektovaniNamestaj.Akcija.Popust);

                    
                    foreach (Namestaj nam in Projekat.Instance.namestaj)
                    {
                        if(nam.ID == selektovaniNamestaj.ID)
                        {
                            nam.KolicinaUMagacinu = nam.KolicinaUMagacinu + 1;
                            NamestajDAL.Update(nam);
                        }

                    }
                }
                else if(selektovani is DodatneUsluge)
                {

                    cenaSelektovanog = (selektovani as DodatneUsluge).Cena;
                }
                else
                {
                    MessageBox.Show("Greska!");
                }

                trenutniRacun.UkupnaCena = trenutniRacun.UkupnaCena - cenaSelektovanog;
                

                lbRacun.Items.Remove(lbRacun.SelectedValue);
            }
            else
            {
                MessageBox.Show("Nije nista selektovano!");
            }
            

            


        }



        /////////////////////////////TAB PRODAJE////////////////////////////////////////


        /////////////////////////////TAB RACUNI////////////////////////////////////////





        private void dgRacuni_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            
            if ((string)e.Column.Header == "ID")
            {
                e.Cancel = true;
            }

            if ((string)e.Column.Header == "NamestajZaProdaju")
            {
                e.Cancel = true;
            }

            if ((string)e.Column.Header == "DodatneUsluge")
            {
                e.Cancel = true;
            }

            


        }

        private void Detalji_Click(object sender, RoutedEventArgs e)
        {
            Racun selektovaniRacun = (Racun)dgRacuni.SelectedItem;
            RacunDetaljiWindow rdw = new RacunDetaljiWindow(selektovaniRacun);
            rdw.Owner = this;
            rdw.ShowDialog();
        }

        private void PretragaRacuna()
        {
            


            string Unos = tbPretragaRacuna.Text.ToLower();
            ObservableCollection<Racun> listaTrazenihRacuna = new ObservableCollection<Racun>();


            /*
            while(Unos != "")
            {

            }
            */

            foreach (Racun rac in Projekat.Instance.racun)
            {

               
                
                try
                {
                    if (rac.Kupac.ToLower().Trim().Contains(Unos) || rac.BrojRacuna.ToLower().Trim().Contains(Unos) || rac.DatumProdaje.Date == DateTime.Parse(Unos))
                    {
                        listaTrazenihRacuna.Add(rac);
                    }
                    
                                   
                }
                catch{}

                /*
                foreach (Namestaj nam in ProdatiNamestajDAL.GetAll(rac))
                {
                    if (nam.Naziv.Contains(Unos))
                    {
                        listaTrazenihRacuna.Add(rac);
                    }
                }
                */


            }

            viewTrazeniRacuni = CollectionViewSource.GetDefaultView(listaTrazenihRacuna);
            dgRacuni.ItemsSource = viewTrazeniRacuni;
            dgRacuni.IsSynchronizedWithCurrentItem = true;
            dgRacuni.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }


        private void PretraziRacune(object sender, RoutedEventArgs e)
        {

            PretragaRacuna();

            
        }

        private void tbPretragaRacuna_TextChanged(object sender, TextChangedEventArgs e)
        {
            PretragaRacuna();
        }


        /////////////////////////////TAB RACUNI////////////////////////////////////////

        /////////////////////////////TAB NAMESTAJ////////////////////////////////////////

        private void DodajNamestaj(object sender, RoutedEventArgs e)
        {                                                             
            Namestaj noviNamestaj = new Namestaj();
            NamestajWindow nw = new NamestajWindow(noviNamestaj, NamestajWindow.Operacija.DODAVANJE);
            nw.Owner = this;
            nw.ShowDialog();
        }

        private void IzmeniNamestaj(object sender, RoutedEventArgs e)
        {
            Namestaj selektovaniNamestaj = (Namestaj)dgNamestaj.SelectedItem;           
            if (selektovaniNamestaj != null)
            {
                Namestaj stari = (Namestaj)selektovaniNamestaj.Clone();
                //samo prosledis objekat selektovanog, sav dalji rad je kroz metode namestaj window
                NamestajWindow nw = new NamestajWindow(selektovaniNamestaj, NamestajWindow.Operacija.IZMENA);
                nw.Owner = this;
                if (nw.ShowDialog() != true)
                {
                    //ako show dialog nije jednako true, znaci da je izasao, sto znaci vrati stari objekat na svoje mesto
                    int index = Projekat.Instance.namestaj.IndexOf(selektovaniNamestaj);
                    Projekat.Instance.namestaj[index] = stari;
                }

            }

        }

        private void ObrisiNamestaj(object sender, RoutedEventArgs e)
        {                 
            var izabraniNamestaj = (Namestaj)dgNamestaj.SelectedItem;           
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {izabraniNamestaj.Naziv}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                //DataGridRow dataGridRow;
                foreach (var n in Projekat.Instance.namestaj)
                {
                    if(n.ID == izabraniNamestaj.ID)
                    {
                        //n.Obrisan = true;
                        NamestajDAL.Delete(n);
                        //dataGridRow = dgNamestaj.ItemContainerGenerator.ContainerFromItem(n) as DataGridRow;
                        //dataGridRow.Visibility = System.Windows.Visibility.Collapsed;
                        // samo refresh view-a
                        viewNamestaj.Refresh(); 
                        break;
                    }
                }
                //GenericSerializer.Serialize("namestaj.xml", Projekat.Instance.namestaj);              
            } 
            
        }

        

        private void dgNamestaj_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
            
            if ((string)e.Column.Header == "IDTipaNamestaja")
            {
                e.Cancel = true;
            }

            if ((string)e.Column.Header == "IDAkcije")
            {
                e.Cancel = true;
            }


            if ((string)e.Column.Header == "ID")
            {
                e.Cancel = true;
            }

            if ((string)e.Column.Header == "KolicinaUMagacinu")
            {
                e.Column.Header = "Kolicina";
            }
        }

        private void NamestajGlavniPretraga()
        {
            string Unos = tbPretragaNamestaja.Text.ToLower();
            ObservableCollection<Namestaj> listaTrazenogNamestaja = new ObservableCollection<Namestaj>();

            foreach (Namestaj nam in Projekat.Instance.namestaj)
            {

                if (nam.Naziv.ToLower().Trim().Contains(Unos) || nam.Sifra.ToLower().Trim().Contains(Unos) || nam.TipNamestaja.Naziv.ToLower().Trim().Contains(Unos))
                {
                    listaTrazenogNamestaja.Add(nam);
                }

            }

            viewTrazeniNamestaj = CollectionViewSource.GetDefaultView(listaTrazenogNamestaja);
            viewTrazeniNamestaj.Filter = TrazeniNamestajFilter;

            bool TrazeniNamestajFilter(object item)
            {
                Namestaj nam = item as Namestaj;
                return !nam.Obrisan;
            }

            dgNamestaj.ItemsSource = viewTrazeniNamestaj;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NamestajGlavniPretraga();
        }

        private void tbPretragaNamestaja_TextChanged(object sender, TextChangedEventArgs e)
        {
            NamestajGlavniPretraga();
        }



        /////////////////////////////TAB NAMESTAJ////////////////////////////////////////

        /////////////////////////////TAB USLUGE////////////////////////////////////////

        private void DodajUslugu(object sender, RoutedEventArgs e)
        {

            DodatneUsluge novaUsluga = new DodatneUsluge();
            UslugeWindow uw = new UslugeWindow(novaUsluga, UslugeWindow.Operacija.DODAVANJE);
            uw.Owner = this;
            uw.ShowDialog();
        }

        private void IzmeniUslugu(object sender, RoutedEventArgs e)
        {

            DodatneUsluge selektovanaUsluga = (DodatneUsluge)dgUsluge.SelectedItem;
            if (selektovanaUsluga != null)
            {
                DodatneUsluge stara = (DodatneUsluge)selektovanaUsluga.Clone();
                
                UslugeWindow uw = new UslugeWindow(selektovanaUsluga, UslugeWindow.Operacija.IZMENA);
                uw.Owner = this;
                if (uw.ShowDialog() != true)
                {
                    
                    int index = Projekat.Instance.dodatneusluge.IndexOf(selektovanaUsluga);
                    Projekat.Instance.dodatneusluge[index] = stara;
                }

            }

        }

        private void ObrisiUslugu(object sender, RoutedEventArgs e)
        {

            var izabranaUsluga = (DodatneUsluge)dgUsluge.SelectedItem;
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {izabranaUsluga.Naziv}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                
                foreach (var u in Projekat.Instance.dodatneusluge)
                {
                    if (u.ID == izabranaUsluga.ID)
                    {

                        //u.Obrisan = true;  
                        DodatneUslugeDAL.Delete(u);
                        viewUsluge.Refresh();
                        break;
                    }
                }
                //GenericSerializer.Serialize("dodatneusluge.xml", Projekat.Instance.dodatneusluge);
            }

        }



        private void dgUsluge_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
          
            if ((string)e.Column.Header == "ID")
            {
                e.Cancel = true;
            }

            
        }

        private void PretragaUsluga()
        {
            string Unos = tbPretragaUsluga.Text.ToLower();
            ObservableCollection<DodatneUsluge> listaTrazenihUsluga = new ObservableCollection<DodatneUsluge>();

            foreach (DodatneUsluge du in Projekat.Instance.dodatneusluge)
            {

                if (du.Naziv.ToLower().Contains(Unos))
                {
                    listaTrazenihUsluga.Add(du);
                }

            }

            viewTrazeneUsluge = CollectionViewSource.GetDefaultView(listaTrazenihUsluga);
            viewTrazeneUsluge.Filter = TrazeneUslugeFilter;

            bool TrazeneUslugeFilter(object item)
            {
                DodatneUsluge du = item as DodatneUsluge;
                return !du.Obrisan;
            }

            dgUsluge.ItemsSource = viewTrazeneUsluge;
            dgUsluge.IsSynchronizedWithCurrentItem = true;
            dgUsluge.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PretragaUsluga();
        }

        private void tbPretragaUsluga_TextChanged(object sender, TextChangedEventArgs e)
        {
            PretragaUsluga();
        }




        /////////////////////////////TAB USLUGE////////////////////////////////////////

        /////////////////////////////TAB TIP NAMESTAJA////////////////////////////////////////

        private void DodajTip(object sender, RoutedEventArgs e)
        {

            TipNamestaja noviTip = new TipNamestaja();
            TipWindow tw = new TipWindow(noviTip, TipWindow.Operacija.DODAVANJE);
            tw.Owner = this;
            tw.ShowDialog();
        }

        private void IzmeniTip(object sender, RoutedEventArgs e)
        {

            TipNamestaja selektovaniTip = (TipNamestaja)dgTipNamestaja.SelectedItem;
            if (selektovaniTip != null)
            {
                TipNamestaja stari = (TipNamestaja)selektovaniTip.Clone();
                
                TipWindow tw = new TipWindow(selektovaniTip, TipWindow.Operacija.IZMENA);
                tw.Owner = this;
                if (tw.ShowDialog() != true)
                {
                    
                    int index = Projekat.Instance.tipnamestaja.IndexOf(selektovaniTip);
                    Projekat.Instance.tipnamestaja[index] = stari;
                }

            }

        }


        //AKO OBRISES TIP NAMESTAJA, BRISE SE I SAV NAMESTAJ KOJI JE TOG TIPA!
        private void ObrisiTip(object sender, RoutedEventArgs e)
        {
            /*
            var izabraniTip = (TipNamestaja)dgTipNamestaja.SelectedItem;
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {izabraniTip.Naziv}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                
                foreach (var t in Projekat.Instance.tipnamestaja)
                {
                    if (t.ID == izabraniTip.ID)
                    {
                                     
                        foreach (var n in Projekat.Instance.namestaj)
                        {
                            if(n.TipNamestaja == t)
                            {

                                //n.Obrisan = true;
                                //vidi da li ovo valja
                                NamestajDAL.Delete(n);
                                NamestajDAL.AnulirajTip(t);                              
                                TipNamestajaDAL.Delete(t);
                                

                                viewNamestaj.Refresh();
                                viewTipovi.Refresh();//OVO JAVLJA NULL
                                break;

                            }                                                    
                        }
                        
                        break;
                    }                  
                }              
            }
            */
            
            var izabraniTip = (TipNamestaja)dgTipNamestaja.SelectedItem;
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {izabraniTip.Naziv}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                foreach(TipNamestaja t in Projekat.Instance.tipnamestaja)
                {
                    if(t.ID == izabraniTip.ID)
                    {
                        TipNamestajaDAL.Delete(t);

                        viewTipovi.Refresh();//OVDE JE PROBLEM

                        viewNamestaj.Refresh();
                    }
                }
            }
            

        }

        private void dgTipNamestaja_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }

            if ((string)e.Column.Header == "ID")
            {
                e.Cancel = true;
            }


        }

        private void PretragaTipova()
        {
            string Unos = tbPretragaTipNamestaja.Text.ToLower();
            ObservableCollection<TipNamestaja> listaTrazenihTipova = new ObservableCollection<TipNamestaja>();

            foreach (TipNamestaja tn in Projekat.Instance.tipnamestaja)
            {

                if (tn.Naziv.ToLower().Contains(Unos))
                {
                    listaTrazenihTipova.Add(tn);
                }

            }

            viewTrazeniTipovi = CollectionViewSource.GetDefaultView(listaTrazenihTipova);
            viewTrazeniTipovi.Filter = TrazeniTipoviFilter;

            bool TrazeniTipoviFilter(object item)
            {
                TipNamestaja tn = item as TipNamestaja;
                return !tn.Obrisan;
            }

            dgTipNamestaja.ItemsSource = viewTrazeniTipovi;
            dgTipNamestaja.IsSynchronizedWithCurrentItem = true;
            dgTipNamestaja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PretragaTipova();
        }

        private void tbPretragaTipNamestaja_TextChanged(object sender, TextChangedEventArgs e)
        {
            PretragaTipova();
        }





        /////////////////////////////TAB TIP NAMESTAJA////////////////////////////////////////

        /////////////////////////////TAB AKCIJE////////////////////////////////////////

        private void DodajAkciju(object sender, RoutedEventArgs e)
        {

            Akcija novaAkcija = new Akcija();
            AkcijeWindow aw = new AkcijeWindow(novaAkcija, AkcijeWindow.Operacija.DODAVANJE);
            aw.Owner = this;
            aw.ShowDialog();
        }

        private void IzmeniAkciju(object sender, RoutedEventArgs e)
        {

            Akcija selektovanaAkcija = (Akcija)dgAkcije.SelectedItem;
            if (selektovanaAkcija != null)
            {
                Akcija stara = (Akcija)selektovanaAkcija.Clone();

                AkcijeWindow aw = new AkcijeWindow(selektovanaAkcija, AkcijeWindow.Operacija.IZMENA);
                aw.Owner = this;
                if (aw.ShowDialog() != true)
                {

                    int index = Projekat.Instance.akcija.IndexOf(selektovanaAkcija);
                    Projekat.Instance.akcija[index] = stara;
                }

            }

        }

        private void ObrisiAkciju(object sender, RoutedEventArgs e)
        {

            var izabranaAkcija = (Akcija)dgAkcije.SelectedItem;
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {izabranaAkcija.Naziv}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                foreach (var a in Projekat.Instance.akcija)
                {
                    if (a.ID == izabranaAkcija.ID)
                    {
                        //a.Obrisana = true;
                        AkcijaDAL.Delete(a);
                        viewAkcije.Refresh();
                        break;
                    }
                }
                //GenericSerializer.Serialize("akcije.xml", Projekat.Instance.akcija);
            }

        }



        private void dgAkcije_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisana")
            {
                e.Cancel = true;
            }

            if ((string)e.Column.Header == "ID")
            {
                e.Cancel = true;
            }
            if ((string)e.Column.Header == "Popust")
            {
                //za format prikaza procenta u koloni, u pozadini se i dalje radi sa decimalnom vrednoscu
                (e.Column as DataGridTextColumn).Binding.StringFormat = "{0}%";
            }

            if ((string)e.Column.Header == "DatumPocetka")
            {
                e.Column.Header = "Pocetak";
            }

            if ((string)e.Column.Header == "DatumZavrsetka")
            {
                e.Column.Header = "Kraj";
            }



        }

        private void dgAkcije_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var izabranaAkcija = (Akcija)dgAkcije.SelectedItem;
            if(izabranaAkcija != null)
            {          
                if (izabranaAkcija.Naziv == "Nije na akciji")
                {
                    btnObrisiAkciju.Visibility = Visibility.Collapsed;
                    btnIzmeniAkciju.Visibility = Visibility.Collapsed;
                }
                else
                {
                    btnObrisiAkciju.Visibility = Visibility.Visible;
                    btnIzmeniAkciju.Visibility = Visibility.Visible;
                }
            }
            
            else
            {
                btnObrisiAkciju.Visibility = Visibility.Visible;
                btnIzmeniAkciju.Visibility = Visibility.Visible;
            }
        }

        private void PretragaAkcija()
        {
            string Unos = tbPretragaAkcija.Text.ToLower();
            ObservableCollection<Akcija> listaTrazenihAkcija = new ObservableCollection<Akcija>();

            foreach (Akcija akc in Projekat.Instance.akcija)
            {

              
                try
                {
                    if (akc.Naziv.ToLower().Trim().Contains(Unos) || akc.Popust == Int32.Parse(Unos) || Akcija.Uporedi(akc.DatumPocetka, DateTime.Parse(Unos)) == "jednak" || Akcija.Uporedi(akc.DatumZavrsetka, DateTime.Parse(Unos))=="jednak")
                    {
                        listaTrazenihAkcija.Add(akc);
                    }
                   
                }
                catch { }




            }

            viewTrazeneAkcije = CollectionViewSource.GetDefaultView(listaTrazenihAkcija);
            viewTrazeneAkcije.Filter = TrazeneAkcijeFilter;

            bool TrazeneAkcijeFilter(object item)
            {
                Akcija akc = item as Akcija;
                return !akc.Obrisana && (Akcija.Uporedi(akc.DatumZavrsetka, DateTime.Now) != "manji");


            }
            dgAkcije.ItemsSource = viewTrazeneAkcije;
            dgAkcije.IsSynchronizedWithCurrentItem = true;
            dgAkcije.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PretragaAkcija();
        }

        private void tbPretragaAkcija_TextChanged(object sender, TextChangedEventArgs e)
        {
            //PretragaAkcija();
        }





        /////////////////////////////TAB AKCIJE////////////////////////////////////////

        /////////////////////////////TAB KORISNICI////////////////////////////////////////

        private void DodajKorisnika(object sender, RoutedEventArgs e)
        {

            Korisnik noviKorisnik = new Korisnik();
            KorisnikWindow kw = new KorisnikWindow(noviKorisnik, KorisnikWindow.Operacija.DODAVANJE);
            kw.Owner = this;
            kw.ShowDialog();
        }

        private void IzmeniKorisnika(object sender, RoutedEventArgs e)
        {

            Korisnik selektovaniKorisnik = (Korisnik)dgKorisnici.SelectedItem;
            if (selektovaniKorisnik != null)
            {
                Korisnik stari = (Korisnik)selektovaniKorisnik.Clone();

                KorisnikWindow kw = new KorisnikWindow(selektovaniKorisnik, KorisnikWindow.Operacija.IZMENA);
                kw.Owner = this;
                if (kw.ShowDialog() != true)
                {

                    int index = Projekat.Instance.korisnici.IndexOf(selektovaniKorisnik);
                    Projekat.Instance.korisnici[index] = stari;
                }

            }

        }

        private void ObrisiKorisnika(object sender, RoutedEventArgs e)
        {

            var izabraniKorisnik = (Korisnik)dgKorisnici.SelectedItem;
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {izabraniKorisnik.Ime}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                foreach (var k in Projekat.Instance.korisnici)
                {
                    if (k.ID == izabraniKorisnik.ID)
                    {
                        //k.Obrisan = true;
                        KorisnikDAL.Delete(k);
                        viewKorisnici.Refresh();
                        break;
                    }
                }
                //GenericSerializer.Serialize("korisnici.xml", Projekat.Instance.korisnici);
            }

        }



        private void dgKorisnici_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }

            if ((string)e.Column.Header == "ID")
            {
                e.Cancel = true;
            }

            if ((string)e.Column.Header == "KorIme")
            {
                e.Column.Header = "Korisnicko Ime";
            }

            if ((string)e.Column.Header == "TipKorisnika")
            {
                e.Column.Header = "Tip Korisnika";
            }


        }

        private void PretragaKorisnika()
        {
            string Unos = tbPretragaKorisnika.Text.ToLower();
            ObservableCollection<Korisnik> listaTrazenihKorisnika = new ObservableCollection<Korisnik>();

            foreach (Korisnik kor in Projekat.Instance.korisnici)
            {

                if (kor.Ime.ToLower().Contains(Unos) || kor.Prezime.ToLower().Contains(Unos) || kor.KorIme.ToLower().Contains(Unos) || kor.TipKorisnika.ToString().ToLower().Trim().Contains(Unos))
                {
                    listaTrazenihKorisnika.Add(kor);
                }

            }

            viewTrazeniKorisnici = CollectionViewSource.GetDefaultView(listaTrazenihKorisnika);
            viewTrazeniKorisnici.Filter = TrazeniKorisniciFilter;

            bool TrazeniKorisniciFilter(object item)
            {
                Korisnik kor = item as Korisnik;
                return !kor.Obrisan;
            }

            dgKorisnici.ItemsSource = viewTrazeniKorisnici;
            dgKorisnici.IsSynchronizedWithCurrentItem = true;
            dgKorisnici.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            //viewTrazeniKorisnici.Refresh();
        }



        private void PretraziKorisnike(object sender, RoutedEventArgs e)
        {

            PretragaKorisnika();

        }

       

        private void tbPretragaKorisnika_TextChanged(object sender, TextChangedEventArgs e)
        {
            PretragaKorisnika();
        }

        /////////////////////////////TAB KORISNICI////////////////////////////////////////


        void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void SalonInfo_Click(object sender, RoutedEventArgs e)
        {
            SalonInfoWindow siw = new SalonInfoWindow();
            siw.Owner = this;
            siw.ShowDialog();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Login lw = new Login();
            lw.Show();
            this.Close();
        }

       

        
    }
}
