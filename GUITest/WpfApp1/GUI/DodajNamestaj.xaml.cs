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

namespace WpfApp1.GUI
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class DodajNamestaj : Window
    {

        private MainWindow referencaNaMain;
        public DodajNamestaj(MainWindow mw)
        {
            InitializeComponent();
            //dodaj listu imena tipova namestaja kao izvor podataka za kombo box
            //cmbTipovi.ItemsSource = 
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            referencaNaMain = mw;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            List<Namestaj> privremenaLista = Projekat.Instance.Namestaj;
            Namestaj nam = new Namestaj()
            {
                ID = int.Parse(tbID.Text),
                Naziv = tbNaziv.Text,
                Sifra = tbSifra.Text,
                Cena = double.Parse(tbCena.Text),
                KolicinaUMagacinu = int.Parse(tbKolicna.Text)

            };

            privremenaLista.Add(nam);
            Projekat.Instance.Namestaj = privremenaLista;
            referencaNaMain.Osvezi();
            this.Close();

            /*
            MainWindow.tabela.ItemsSource = listaNamestaj;
            tabela.Items.Refresh();
            */

            
        }

        
    }
}
