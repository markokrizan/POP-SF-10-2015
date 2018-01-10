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
    public class Salon:INotifyPropertyChanged
    {
        private string naziv;
        private string adresa;
        private string telefon;
        private string email;
        private string pib;
        private string maticnibroj;
        private string brojziroracuna;
        private string sajt;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Naziv {
           
            set
            {
                naziv = value;
                OnPropertyChanged("Naziv");
            }
            get
            {
                return naziv;
            }
        }
        public string Adresa {
            get
            {
                return adresa;
            }
            set
            {
                adresa = value;
                OnPropertyChanged("Adresa");
            }
        }
        public string Telefon {
            get
            {
                return telefon;
            }
            set
            {
                telefon = value;
                OnPropertyChanged("Telefon");
            }
        }
        public string EMail {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged("EMail");
            }
        }
        public string PIB {
            get
            {
                return pib;
            }
            set
            {
                pib = value;
                OnPropertyChanged("PIB");
            }
        }
        public string  MaticniBroj {
            get
            {
                return maticnibroj;
            }
            set
            {
                maticnibroj = value;
                OnPropertyChanged("MaticniBroj");

            }
        }
        public string BrojZiroRacuna {
            get
            {
                return brojziroracuna;
            }
            set
            {
                brojziroracuna = value;
                OnPropertyChanged("BrojZiroRacuna");
            }
        }
        public string Sajt {
            get
            {
                return sajt;
            }
            set
            {
                sajt = value;
                OnPropertyChanged("Sajt");
            }
        }

        /*
        public Salon()
        {
            this.naziv = "Jugodrvo";
            this.adresa = "Puskinova 1";
            this.telefon = "021/111/222";
            this.email = "jugosalon@biz.rs";
            this.pib = "685798767";
            this.maticnibroj = "00556745";
            this.brojziroracuna = "320-000566677465-88";
            this.sajt = "www.jugosalon.rs";
        }
        */

        
       

        

        protected void OnPropertyChanged(string propertyName)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }






    }
}
