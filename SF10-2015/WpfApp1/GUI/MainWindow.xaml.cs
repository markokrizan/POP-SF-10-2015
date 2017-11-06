using POP_SF_10_2015.Model;
using POP_SF_10_2015.Util;
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

        


        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            tabela.AutoGenerateColumns = true;
            tabela.ColumnWidth = new DataGridLength(1,
                DataGridLengthUnitType.Star);

        }

        private void btnNamestaj_Click(object sender, RoutedEventArgs e)
        {
            

            
            tabela.ItemsSource = Projekat.Instance.Namestaj;
            Osvezi();
        }

        public void Osvezi()
        {
            tabela.Items.Refresh();
        }

        
        
        private void dodajNamTest_Click(object sender, RoutedEventArgs e)
        {

            DodajNamestaj dn = new DodajNamestaj(this);

            dn.Show();
            

            


        }

        
    }
}
