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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.GUI;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<Namestaj> listaNamestaja { get; set; }
        


        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            tabela.AutoGenerateColumns = true;
            tabela.ColumnWidth = new DataGridLength(1,
                DataGridLengthUnitType.Star);
            listaNamestaja = new ObservableCollection<Namestaj>(Projekat.Instance.Namestaj);
            tabela.ItemsSource = listaNamestaja;

        }

        private void btnNamestaj_Click(object sender, RoutedEventArgs e)
        {



            //tabela.ItemsSource = Projekat.Instance.Namestaj;
            tabela.ItemsSource = listaNamestaja;
            //Osvezi();
        }

        public void Osvezi(List<Namestaj> list)
        {
            //tabela.Items.Refresh();
            listaNamestaja = new ObservableCollection<Namestaj>(list);
            tabela.ItemsSource = listaNamestaja;
        }

        
        
        private void dodajNamTest_Click(object sender, RoutedEventArgs e)
        {

            DodajNamestaj dn = new DodajNamestaj(this);

            dn.Show();
            

            


        }



        private void ObrisiNamestaj_Click(object sender, RoutedEventArgs e)
        {
            /*
            foreach (DataGridViewRow item in this.tabela.SelectedRows)
            {
                tabela.Rows.RemoveAt(item.Index);
            }

            */
        }



    }
}
