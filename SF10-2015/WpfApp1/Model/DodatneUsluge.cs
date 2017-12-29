using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace POP_SF_10_2015.Model
{
    public class DodatneUsluge:ICloneable, INotifyPropertyChanged
    {

        private int id;
        private bool obrisan;
        private string naziv;
        private double cena;



        public int  ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        

        public bool Obrisan
        {
            get
            {
                return obrisan;
            }
            set
            {
                obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }

        public string Naziv
        {
            get
            {
                return naziv;
            }
            set
            {
                naziv = value;
                OnPropertyChanged("Naziv");
            }
        }

        public double Cena
        {
            get
            {
                return cena;
            }
            set
            {
                cena = value;
                OnPropertyChanged("Cena");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {               
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object Clone()
        {
            DodatneUsluge kopija = new DodatneUsluge();
            kopija.ID = ID;
            kopija.Obrisan = Obrisan;
            kopija.Naziv = Naziv;
            kopija.Cena = Cena;
            return kopija;
        }


        public override string ToString()
        {
            return $"{Naziv}, {Cena}";
        }



        












    }
}
