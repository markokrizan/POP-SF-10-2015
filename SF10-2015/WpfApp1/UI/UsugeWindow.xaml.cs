﻿using POP_SF_10_2015.Model;
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
using WpfApp1.DAL;

namespace WpfApp1.UI
{
    /// <summary>
    /// Interaction logic for NamestajWindow.xaml
    /// </summary>
    public partial class UslugeWindow : Window
    {

        private DodatneUsluge usluga;
        private Operacija operacija;
        
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };

        public UslugeWindow(DodatneUsluge usluga, Operacija operacija)
        {
            
            InitializeComponent();
            this.usluga = usluga;
            this.operacija = operacija;
                   
            tbNaziv.DataContext = usluga;           
            tbCena.DataContext = usluga;
            
        }

       
        private void sacuvajIzmene(object sender, RoutedEventArgs e)
        {

            
            var listaUsluga = Projekat.Instance.dodatneusluge;                  
            this.DialogResult = true;

            switch(operacija)
            {
                case Operacija.DODAVANJE:
                    //usluga.ID = listaUsluga.Count + 1;
                    //listaUsluga.Add(usluga);    
                    DodatneUslugeDAL.Create(usluga);
                    break;
                   
                case Operacija.IZMENA:
                   
                    foreach(var u in listaUsluga)
                    {
                        if(u.ID == usluga.ID)
                        {
                            
                           
                            u.Naziv = usluga.Naziv;
                            u.Cena = usluga.Cena;
                            DodatneUslugeDAL.Update(u);
                            break;
                        }
                    }

                    
                    break;
                
            }            
            //GenericSerializer.Serialize("dodatneusluge.xml", listaUsluga);
            this.Close();
        }



        private void Izadji(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
