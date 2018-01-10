using POP_SF_10_2015.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class RacunDetaljiWindow : Window
    {
        private Racun racun;
        ICollectionView viewStavkeNamestaja;
        

        public RacunDetaljiWindow(Racun rac)
        {
            InitializeComponent();
            this.racun = rac;
            Napuni();
            viewStavkeNamestaja = CollectionViewSource.GetDefaultView(ProdatiNamestajDAL.StavkeNamestajaPoRacunu(racun));
            dgStavkeNamestaja.ItemsSource = viewStavkeNamestaja;

            dgStavkeNamestaja.IsSynchronizedWithCurrentItem = true;
            dgStavkeNamestaja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }





        
        public void Napuni()
        {
            /*
            foreach(Namestaj nam in ProdatiNamestajDAL.GetAll(racun))
            {
                lbNamestaj.Items.Add(nam);
            }
            */

            foreach(DodatneUsluge du in ProdateUslugeDAL.GetAll(racun))
            {
                lbUsluge.Items.Add(du);
            }
        }
        






        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void dgStavke_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            
            if ((string)e.Column.Header == "Namestaj")
            {
                e.Column.Width = 215;
            }

           /*

            if ((string)e.Column.Header == "KolicinaUMagacinu")
            {
                e.Column.Header = "Kolicina";
            }
            */
        }

        void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

    }
}
