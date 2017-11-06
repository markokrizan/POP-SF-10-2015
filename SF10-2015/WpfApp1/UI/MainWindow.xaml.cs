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
            listBoxNamestaj.Items.Clear();

            foreach(var namestaj in Projekat.Instance.Namestaj)
            {
                listBoxNamestaj.Items.Add(namestaj);
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
            namestajProzor.Show();
        }

        private void IzmeniNamestaj(object sender, RoutedEventArgs e)
        {
            //selected item vraca object, potreban kast
            var selektovaniNamestaj = (Namestaj)listBoxNamestaj.SelectedItem;

            var namestajProzor = new NamestajWindow(selektovaniNamestaj, NamestajWindow.Operacija.IZMENA);
            namestajProzor.Show();
        }

        





    }
}
