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
    public partial class TipWindow : Window
    {

        private TipNamestaja tip;
        private Operacija operacija;
        
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        public TipWindow(TipNamestaja tip, Operacija operacija)
        {
            
            InitializeComponent();
            this.tip = tip;
            this.operacija = operacija;                 
            tbNaziv.DataContext = tip;           
            
            
        }

       
        private void sacuvajIzmene(object sender, RoutedEventArgs e)
        {

            
            var listaTipova = Projekat.Instance.TipoviNamestaja;                  
            this.DialogResult = true;

            switch(operacija)
            {
                case Operacija.DODAVANJE:
                    tip.ID = listaTipova.Count + 1;
                    listaTipova.Add(tip);                                      
                    break;
                   
                case Operacija.IZMENA:
                   
                    foreach(var t in listaTipova)
                    {
                        if(t.ID == tip.ID)
                        {
                                                      
                            t.Naziv = tip.Naziv;                           
                            break;
                        }
                    }

                    
                    break;
                
            }            
            GenericSerializer.Serialize("tipovinamestaja.xml", listaTipova);
            this.Close();
        }



        private void Izadji(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
