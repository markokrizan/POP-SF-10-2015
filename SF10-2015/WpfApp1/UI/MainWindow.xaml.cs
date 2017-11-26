using POP_SF_10_2015.Model;
using POP_SF_10_2015.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
    public partial class MainWindow : Window
    {

        ICollectionView view;

        public MainWindow()
        {
            InitializeComponent();
            //dgNamestaj.Width = Double.NaN;




            //default je bilo projekat.instance.namestaj
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaj);

            //dgNamestaj.ItemsSource = view;

            DataGridTextColumn kolonaID = new DataGridTextColumn();
            kolonaID.Header = "ID";
            kolonaID.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            kolonaID.Binding = new Binding("ID");
            dgNamestaj.Columns.Add(kolonaID);

            DataGridTextColumn kolonaNaziv = new DataGridTextColumn();
            kolonaNaziv.Header = "Naziv";
            kolonaNaziv.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            kolonaNaziv.Binding = new Binding("Naziv");
            dgNamestaj.Columns.Add(kolonaNaziv);

            DataGridTextColumn kolonaSifra = new DataGridTextColumn();
            kolonaSifra.Header = "Sifra";
            kolonaSifra.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            kolonaSifra.Binding = new Binding("Sifra");
            dgNamestaj.Columns.Add(kolonaSifra);

            DataGridTextColumn kolonaCena = new DataGridTextColumn();
            kolonaCena.Header = "Cena";
            kolonaCena.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            kolonaCena.Binding = new Binding("Cena");
            dgNamestaj.Columns.Add(kolonaCena);

            DataGridTextColumn kolonaKolicina = new DataGridTextColumn();
            kolonaKolicina.Header = "Kolicina";
            kolonaKolicina.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            kolonaKolicina.Binding = new Binding("KolicinaUMagacinu");
            dgNamestaj.Columns.Add(kolonaKolicina);

            DataGridTextColumn kolonaTip = new DataGridTextColumn();
            kolonaTip.Header = "Tip";
            kolonaTip.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            kolonaTip.Binding = new Binding("TipNamestaja");
            dgNamestaj.Columns.Add(kolonaTip);


            dgNamestaj.ItemsSource = view;
            //dgNamestaj.ItemsSource = Projekat.Instance.Namestaj;
            
            



            //da selected item ne bi bio null
            dgNamestaj.IsSynchronizedWithCurrentItem = true;

            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);




            
        }

        

        

        


        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void DodajNamestaj(object sender, RoutedEventArgs e)
        {

                                                             
            Namestaj noviNamestaj = new Namestaj();
            NamestajWindow nw = new NamestajWindow(noviNamestaj, NamestajWindow.Operacija.DODAVANJE);
            nw.Owner = this;
            nw.ShowDialog();

        }

        private void IzmeniNamestaj(object sender, RoutedEventArgs e)
        {

            Namestaj selektovaniNamestaj = (Namestaj)dgNamestaj.SelectedItem;
            //System.Diagnostics.Debug.WriteLine(selektovaniNamestaj);

            if (selektovaniNamestaj != null)
            {

                Namestaj stari = (Namestaj)selektovaniNamestaj.Clone();

                //samo prosledis objekat selektovanog, sav dalji rad je kroz metode namestaj window
                NamestajWindow nw = new NamestajWindow(selektovaniNamestaj, NamestajWindow.Operacija.IZMENA);
                nw.Owner = this;

                if (nw.ShowDialog() != true)
                {
                    //ako show dialog nije jednako true, znaci da je izasao, sto znaci vrati stari objekat na svoje mesto
                    int index = Projekat.Instance.Namestaj.IndexOf(selektovaniNamestaj);
                    Projekat.Instance.Namestaj[index] = stari;


                }





            }



            /*

            //selected item vraca object, potreban kast
            //listBoxNamestaj.SelectedItem
            var selektovaniNamestaj = (Namestaj)dgNamestaj.SelectedItem;

            var namestajProzor = new NamestajWindow(selektovaniNamestaj, NamestajWindow.Operacija.IZMENA);
            namestajProzor.ShowDialog();

            //OsveziPrikaz();

            */
        }

        private void ObrisiNamestaj(object sender, RoutedEventArgs e)
        {

            
      
            var izabraniNamestaj = (Namestaj)dgNamestaj.SelectedItem;
            
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {izabraniNamestaj.Naziv}?", "Brisanje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DataGridRow dataGridRow;
                foreach (var n in Projekat.Instance.Namestaj)
                {
                    if(n.ID == izabraniNamestaj.ID)
                    {
                        n.Obrisan = true;
                        //dgNamestaj.Items.Remove(izabraniNamestaj);
                        //int selectedIndex = dgNamestaj.CurrentCell.RowIndex;
                        //dgNamestaj.Rows.RemoveAt(selectedIndex);
                        dataGridRow = dgNamestaj.ItemContainerGenerator.ContainerFromItem(n) as DataGridRow;
                        dataGridRow.Visibility = System.Windows.Visibility.Collapsed;
                        //return; 


                        break;
                    }
                }

                //dataGridRow = dgNamestaj.ItemContainerGenerator.ContainerFromItem(people[2]) as DataGridRow;
                //dataGridRow.Visibility = System.Windows.Visibility.Visible;

                GenericSerializer.Serialize("namestaj.xml", Projekat.Instance.Namestaj);

                
            } 
            
            


        }

        private void SalonInfo_Click(object sender, RoutedEventArgs e)
        {
            SalonInfoWindow siw = new SalonInfoWindow();

            //posto je u xamlu receno WindowStartupLocation="CenterOwner", moras da naglasis vlasnika
            siw.Owner = this;
            siw.ShowDialog();
        }
    }
}
