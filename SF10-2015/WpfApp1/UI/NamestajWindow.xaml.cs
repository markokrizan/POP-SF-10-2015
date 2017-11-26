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

            //InicijalizujVrednosti(namestaj, operacija);


            //inicijalizacija vrednosti pomocu bindinga
            this.namestaj = namestaj;
            this.operacija = operacija;


            
            



            cbTipNamestaja.ItemsSource = Projekat.Instance.TipoviNamestaja;


            //DataContext je ustvari momenat kada se komponenta mapira na objekat namestaja koji je prosledjen
            //two way binding podrazumevan
            //povezan samo objekat i komponenta ne i xml
            cbTipNamestaja.DataContext = namestaj;
            //konkretan properti je u xaml-u (Bindig Path = "Naziv")
            //ako je objekat u objektu moze Binding Path = Adresa.Ulica npr
            tbNaziv.DataContext = namestaj;
            tbSifra.DataContext = namestaj;
            tbCena.DataContext = namestaj;
            tbKolicina.DataContext = namestaj;
        }


        

        



        
        
        private void sacuvajIzmene(object sender, RoutedEventArgs e)
        {

            
            var listaNamestaja = Projekat.Instance.Namestaj;
           
        
            this.DialogResult = true;

            switch(operacija)
            {
                case Operacija.DODAVANJE:
                    namestaj.ID = listaNamestaja.Count + 1;
                    listaNamestaja.Add(namestaj);
                    
                    
                  

                    break;
                   


                case Operacija.IZMENA:

                    //STAVI KLON STAROG OBJEKTA KAO IZ PRIMERA PROFESORA
                    foreach(var n in listaNamestaja)
                    {
                        if(n.ID == namestaj.ID)
                        {
                            
                            n.IDTipaNamestaja = namestaj.IDTipaNamestaja;
                            n.Naziv = namestaj.Naziv;
                            break;
                        }
                    }

                    
                    break;
                
            }

            //nakon svih izmena serijalizuj, znaci prvo radimo sa temp listom, menjamo je kolko treba i onda se pregazi glavna kolekcija seterom iz Projekat
            GenericSerializer.Serialize("namestaj.xml", listaNamestaja);
            this.Close();
        }

        private void Izadji(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
