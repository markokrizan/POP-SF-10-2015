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
    public partial class KolicinaWindow : Window
    {

        private int kolicina;
        private Namestaj namestaj;

        public int Kolicina
        {
            get { return kolicina; }
            set { kolicina = value; }
        }


        public KolicinaWindow(Namestaj nam)
        {
            InitializeComponent();
            this.namestaj = nam;
        }

        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Potvrdi
            int n;//drugi parametar tryParse koji vraca taj broj, ali to ti ne treba pa ga ovako samo tu bacis
            if(tbKolicina.Text != "" && Int32.TryParse(tbKolicina.Text, out n) == true && n <= namestaj.KolicinaUMagacinu && n > 0)
            {
                this.kolicina = Int32.Parse(tbKolicina.Text);
                //MessageBox.Show(this.Kolicina.ToString());
                //MessageBox.Show(namestaj.KolicinaUMagacinu.ToString());
                this.Close();
            }
            

            
            if (tbKolicina.Text == "")
            {
                MessageBox.Show("Niste nista uneli!");
            }
            if (Int32.TryParse(tbKolicina.Text, out n) == false)
            {
                MessageBox.Show("Niste uneli broj!");
            }
            if(n > namestaj.KolicinaUMagacinu)
            {
                MessageBox.Show("Nema toliko namestaja na stanju!");
            }
            if(n < 0)
            {
                MessageBox.Show("Ne mozete uneti 0 ili manje od 0!S");
            }
            if(n == 0)
            {
                MessageBox.Show("Ne mozete uneti 0 ili manje od 0!S");
            }
            
            

        }

        
    }
}
