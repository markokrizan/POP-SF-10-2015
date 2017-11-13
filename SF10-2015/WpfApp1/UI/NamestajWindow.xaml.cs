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
    /// Interaction logic for NamestajWindow.xaml
    /// </summary>
    public partial class NamestajWindow : Window
    {

        private Namestaj namestaj;
        private Operacija operacija;
        
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        public NamestajWindow(Namestaj namestaj, Operacija operacija)
        {
            //this.namestaj = namestaj;

            

            InitializeComponent();

            InicijalizujVrednosti(namestaj, operacija);
        }

        private void InicijalizujVrednosti(Namestaj namestaj, Operacija operacija)
        {
            this.namestaj = namestaj;
            this.operacija = operacija;

            //da kada koristis ovu formu za izmenu, da bude vec popunjeno
            tbNaziv.Text = namestaj.Naziv;

            //napuni combo box
            foreach(var tipNamestaja in Projekat.Instance.TipoviNamestaja)
            {
                cbTipNamestaja.Items.Add(tipNamestaja);
            }

            //da bude setovan kombo box za izmenu
            //cbTipNamestaja.Items vraca object moras da konvertujes
            foreach(TipNamestaja tipNamestaja in cbTipNamestaja.Items)
            {
                if(tipNamestaja.ID == namestaj.IDTipaNamestaja)
                {
                    cbTipNamestaja.SelectedItem = tipNamestaja;
                }
            }
            
        }

        private void sacuvajIzmene(object sender, RoutedEventArgs e)
        {
            List<Namestaj> postojeciNamestaj = Projekat.Instance.Namestaj;
            Namestaj namestajZaIzmenu = null;
            int tipNamestajaId = ((TipNamestaja)cbTipNamestaja.SelectedItem).ID;
            

            switch(operacija)
            {
                case Operacija.DODAVANJE:
                    var noviNamestaj = new Namestaj()
                    {
                        ID = postojeciNamestaj.Count + 1,
                        Naziv = tbNaziv.Text,
                        //ID = listaNamestaja.Count + 1;
                        IDTipaNamestaja = tipNamestajaId
                    };
                    postojeciNamestaj.Add(noviNamestaj);
                    break;
                   


                case Operacija.IZMENA:
                    foreach(var n in postojeciNamestaj)
                    {
                        if(n.ID == namestaj.ID)
                        {
                            /*
                            n.Naziv = tbNaziv.Text;
                            break;
                            */
                            namestajZaIzmenu = n;
                        }
                    }

                    namestajZaIzmenu.Naziv = tbNaziv.Text;
                    namestajZaIzmenu.IDTipaNamestaja = tipNamestajaId;

                   




                    break;
                
            }

            //nakon svih izmena serijalizuj, znaci prvo radimo sa temp listom, menjamo je kolko treba i onda se pregazi glavna kolekcija seterom iz Projekat
            Projekat.Instance.Namestaj = postojeciNamestaj;
            this.Close();
        }

    }
}
