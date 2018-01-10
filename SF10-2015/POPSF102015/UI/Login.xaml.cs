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

namespace WpfApp1.UI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Login : Window
    {

        private Korisnik korisnik;
       

        public Login()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            
        }

        private void LoginBTN_Click(object sender, RoutedEventArgs e)
        {


            
            string uname = tbUName.Text.Trim();
            string pass = tbPass.Password.Trim();

            if (uname == "" || pass == "")
            {
                tbUName.Text = "";
                tbPass.Password = "";
                MessageBox.Show("Niste uneli sve podatke!");

            }else if(CredCheck(uname,pass) == true)
            {
                MainWindow mw = new MainWindow(korisnik);
                //mw.Owner = this;
                mw.Show();
                this.Close();

            }
            else

            {
                tbUName.Text = "";
                tbPass.Password = "";
                MessageBox.Show("Niste uneli dobre podatke!");
            }

          
            
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string uname = tbUName.Text.Trim();
                string pass = tbPass.Password.Trim();

                if (uname == "" || pass == "")
                {
                    tbUName.Text = "";
                    tbPass.Password = "";
                    MessageBox.Show("Niste uneli sve podatke!");

                }
                else if (CredCheck(uname, pass) == true)
                {
                    MainWindow mw = new MainWindow(korisnik);
                    //mw.Owner = this;
                    mw.Show();
                    this.Close();

                }
                else

                {
                    tbUName.Text = "";
                    tbPass.Password = "";
                    MessageBox.Show("Niste uneli dobre podatke!");
                }
            }
        }



        public Boolean CredCheck(String korisnicko, String sifra)
        {
            foreach (Korisnik kor in Projekat.Instance.korisnici)
            {
                
                if (kor.KorIme == korisnicko && kor.Lozinka == sifra)
                {
                    korisnik = kor;
                    return true;
                }
            }
            return false;
        }
        
       

    }
}
