using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace POP_SF_10_2015.Model
{
    public class Namestaj:INotifyPropertyChanged
    {

        //dodao za binding
        int id;
        bool obrisan;
        string naziv;
        string sifra;
        double cena;
        int kolicinaumagacinu;
        int idtipanamestaja;

        public int ID
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
        public string Sifra
        {
            get
            {
                return sifra;
            }
            set
            {
                sifra = value;
                OnPropertyChanged("Sifra");
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
        public int KolicinaUMagacinu
        {
            get
            {
                return kolicinaumagacinu;
            }
            set
            {
                kolicinaumagacinu = value;
                OnPropertyChanged("KolicinaUMagacinu");
            }
        }
        public int IDTipaNamestaja
        {
            get
            {
                return idtipanamestaja;
            }
            set
            {
                idtipanamestaja = value;
                OnPropertyChanged("IDTipaNamestaja");
            }
        }
        //stavi tip namestaja int 
        //public Akcija Akcija { get; set; }

        //da kada se poziva namestaj se ne poziva difolt toString metoda koja ispisuje namestpace.imeobjekta, nego je overrajdujemo ovako



        

        public override string ToString()
        {
            return $"{Naziv}, {Cena}, {TipNamestaja.GetById(IDTipaNamestaja).Naziv}";
        }



        

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        public static Namestaj GetById(int id)
        {
            foreach (var namestaj in Projekat.Instance.Namestaj)
            {
                if (namestaj.ID == id)
                {
                    return namestaj;
                }

            }
            return null;

        }


    }

}
