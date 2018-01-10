using POP_SF_10_2015.Model;
using POP_SF_10_2015.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class AkcijeWindow : Window
    {

        private Akcija akcija;
        private Operacija operacija;
        
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        public AkcijeWindow(Akcija akcija, Operacija operacija)
        {
            
            InitializeComponent();
            this.akcija = akcija;
            this.operacija = operacija;
                   
            tbNaziv.DataContext = akcija;           
            tbPopust.DataContext = akcija;
            dpPocetak.DataContext = akcija;
            dpKraj.DataContext = akcija;
            dpPocetak.SelectedDate = new DateTime(2017, 1, 1);
            dpKraj.SelectedDate = new DateTime(2018, 1, 1);



            //Startni i pocetni datum za datepicker kontrolu:            
            //dpPocetak.DisplayDateStart = new DateTime(2017, 1, 1);
            //dpKraj.DisplayDateEnd = new DateTime(2019, 1, 1);



        }

       
        private void sacuvajIzmene(object sender, RoutedEventArgs e)
        {

            if(akcija.Naziv != null && akcija.Popust >  0 &&  Akcija.Uporedi(akcija.DatumZavrsetka, DateTime.Now) != "manji" && Akcija.Uporedi(akcija.DatumPocetka, akcija.DatumZavrsetka)!= "veci" && akcija.Popust < 90)
            {
                var listaAkcija = Projekat.Instance.akcija;
                this.DialogResult = true;

                switch (operacija)
                {
                    case Operacija.DODAVANJE:
                        
                        //akcija.ID = listaAkcija.Count + 1;
                        //listaAkcija.Add(akcija); 
                        AkcijaDAL.Create(akcija);
                        break;

                    case Operacija.IZMENA:
                        /*
                        foreach (var a in listaAkcija)
                        {
                            if (a.ID == akcija.ID)
                            {

                                a.Naziv = akcija.Naziv;
                                a.Popust = akcija.Popust;
                                a.DatumPocetka = akcija.DatumPocetka;
                                a.DatumZavrsetka = akcija.DatumZavrsetka;
                                
                                break;
                            }
                        }
                        */
                        AkcijaDAL.Update(akcija);

                        break;

                }

                this.Close();
            }
            else
            {
                if(akcija.Naziv == null)
                {
                    MessageBox.Show("Niste uneli naziv!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if(akcija.Popust <= 0)
                {
                    MessageBox.Show("Niste uneli dobar popust!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (Akcija.Uporedi(akcija.DatumZavrsetka, DateTime.Now) == "manji")
                {
                    MessageBox.Show("Niste uneli dobar datum!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (Akcija.Uporedi(akcija.DatumPocetka, akcija.DatumZavrsetka)!= "veci")
                {
                    MessageBox.Show("Niste uneli dobar datum!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (akcija.Popust > 90)
                {
                    MessageBox.Show("Uneli ste prevelik popust! (Maksimalno 90%)", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
            
        }



        private void Izadji(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
