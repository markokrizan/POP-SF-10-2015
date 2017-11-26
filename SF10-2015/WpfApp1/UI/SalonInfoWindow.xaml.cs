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
    /// Interaction logic for SalonInfoWindow.xaml
    /// </summary>
    public partial class SalonInfoWindow : Window
    {
        public SalonInfoWindow()
        {
            Salon salon = new Salon();
            InitializeComponent();
            Label1.Content = "Naziv: " + salon.Naziv;
            Label2.Content = "Adresa: " + salon.Adresa;
            Label3.Content = "Telefon: " + salon.Telefon;
            Label4.Content = "EMail: " + salon.EMail;
            Label5.Content = "Sajt: " + salon.Sajt;
            Label6.Content = "PIB: " + salon.PIB;
            Label7.Content = "Mat. Broj: " + salon.MaticniBroj;
            Label8.Content = "Br. Racuna: " + salon.BrojZiroRacuna;
            

        }
    }
}
