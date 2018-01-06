using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace POP_SF_10_2015.Model
{
    public enum TipKorisnika
    {

        







        
        Administrator = 1,
        Prodavac = 2
    }

    public class Korisnik : ICloneable, INotifyPropertyChanged
    {

        /*
        public IList<TipKorisnika> TipoviKorisnika
        {
            get
            {
                return Enum.GetValues(typeof(TipKorisnika)).Cast<TipKorisnika>().ToList<TipKorisnika>();
               
            }
        }
        */
       




       

        private int id;
        private bool obrisan;
        private string ime;
        private string prezime;
        private string korime;
        private string lozinka;
        private TipKorisnika tipkorisnika;

        public event PropertyChangedEventHandler PropertyChanged;

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
        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                ime = value;
                OnPropertyChanged("Ime");
            }
        }
        public string Prezime
        {
            get
            {
                return prezime;
            }
            set
            {
                prezime = value;
                OnPropertyChanged("Prezime");
            }
        }
        public string KorIme
        {
            get
            {
                return korime;
            }
            set
            {
                korime = value;
                OnPropertyChanged("KorIme");
            }
        }
        public string Lozinka
        {
            get
            {
                return lozinka;
            }
            set
            {
                lozinka = value;
                OnPropertyChanged("Lozinka");
            }
        }
        public TipKorisnika TipKorisnika
        {
            get
            {
                return tipkorisnika;
            }
            set
            {
                tipkorisnika = value;
                OnPropertyChanged("TipKorisnika");
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {

            if (PropertyChanged != null)
            {
                //this, sam namestaj menja dogadjaj
                //parametri kao kod svake standardne metode koja hendluje dogadjaj
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object Clone()
        {
            Korisnik kopija = new Korisnik();
            kopija.ID = ID;
            kopija.Obrisan = Obrisan;
            kopija.Ime = Ime;
            kopija.Prezime = Prezime;
            kopija.KorIme = KorIme;
            kopija.Lozinka = Lozinka;
            kopija.TipKorisnika = TipKorisnika;
            return kopija;
        }


        



    }
}
