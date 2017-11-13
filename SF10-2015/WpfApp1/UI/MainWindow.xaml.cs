using POP_SF_10_2015.Model;
using System;
using System.Collections.Generic;
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

namespace WpfApp1.UI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            OsveziPrikaz();
        }

        private void OsveziPrikaz()
        {
            //ocsti dodaj ponovo
            listBoxNamestaj.Items.Clear();

            foreach(var namestaj in Projekat.Instance.Namestaj)
            {
                //ako nije logicki obrisan onda ga dodaj
                if(namestaj.Obrisan == false)
                {
                    listBoxNamestaj.Items.Add(namestaj);
                }
               
            }

            //da uvek bude prvi selektovan da nema null selekcije
            listBoxNamestaj.SelectedIndex = 0;
        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DodajNamestaj(object sender, RoutedEventArgs e)
        {
            //ako se unosi novi, da napravi novi prazan da ga prosledi posto mu treba kao parametar
            var noviNamestaj = new Namestaj()
            {
                Naziv = ""
            };

            var namestajProzor = new NamestajWindow(noviNamestaj, NamestajWindow.Operacija.DODAVANJE);
            //umesto samo show show dialog da bi presao na metodu osvezavanja
            namestajProzor.ShowDialog();

            //ovim azurira prikaz sasvim ok
            OsveziPrikaz();
        }

        private void IzmeniNamestaj(object sender, RoutedEventArgs e)
        {
            //selected item vraca object, potreban kast
            var selektovaniNamestaj = (Namestaj)listBoxNamestaj.SelectedItem;

            var namestajProzor = new NamestajWindow(selektovaniNamestaj, NamestajWindow.Operacija.IZMENA);
            namestajProzor.ShowDialog();

            OsveziPrikaz();
        }

        private void ObrisiNamestaj(object sender, RoutedEventArgs e)
        {
            //selected item je object pa uradi cast
            var izabraniNamestaj = (Namestaj)listBoxNamestaj.SelectedItem;
            var ListaNamestaja = Projekat.Instance.Namestaj;
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {izabraniNamestaj.Naziv}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Namestaj namestaj = null;

                foreach(var n in ListaNamestaja)
                {
                    if(n.ID == izabraniNamestaj.ID)
                    {
                        namestaj = n;
                    }
                }
                namestaj.Obrisan = true;

                Projekat.Instance.Namestaj = ListaNamestaja;

                OsveziPrikaz();
            } 
        }
    }
}
