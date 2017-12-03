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
using System.Windows.Shapes;
using WpfApp1.UI;

namespace WpfApp1.GUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Login : Window
    {
        

        public Login()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            
        }

        private void LoginBTN_Click(object sender, RoutedEventArgs e)
        {
            //var listaKorisnici = GenericSerializer.Deserialize<Korisnik>("korisnici.xml");
            var listaKorisnici = Projekat.Instance.Korisnici;

            foreach(Korisnik kor in listaKorisnici)
            {
                if(kor.KorIme == tbUName.Text && kor.Lozinka == tbPass.Password)
                {

                    MainWindow mw = new MainWindow();
                    mw.Show();                   
                    this.Close();
                    break;
                }
                else
                {
                    tbUName.Text = "";
                    tbPass.Password = "";   
                    MessageBox.Show("Niste uneli dobre podatke!");
                    break;
                }

            }
        }

         

    }
}
