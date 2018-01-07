using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace POP_SF_10_2015.Model
{
    public class Racun: ICloneable, INotifyPropertyChanged
    {
        private int id;
        private List<Namestaj> namestajzaprodaju;
        private DateTime datumprodaje;
        private string brojracuna;
        private string kupac;
        private List<DodatneUsluge> dodatneusluge;
        private double ukupnacena;
        public const double PDV = 0.02;




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

        [XmlIgnore]
        public List<Namestaj> NamestajZaProdaju
        {
            get
            {
                namestajzaprodaju = new List<Namestaj>();
                return namestajzaprodaju;
            }
            set
            {
                namestajzaprodaju = value;
                OnPropertyChanged("NamestajZaProdaju");
            }
        }

        public DateTime DatumProdaje
        {
            get
            {
                return datumprodaje;
            }
            set
            {
                datumprodaje = value;
                OnPropertyChanged("DatumProdaje");
       
            }
        }

        public string BrojRacuna
        {
            get
            {
                return brojracuna;
            }
            set
            {
                brojracuna = value;
                OnPropertyChanged("BrojRacuna");
            }
        }

        public string Kupac
        {
            get
            {
                return kupac;
            }
            set
            {
                kupac = value;
                OnPropertyChanged("Kupac");
            }
        }

        [XmlIgnore]
        public List<DodatneUsluge> DodatneUsluge
        {
            get
            {
                dodatneusluge = new List<DodatneUsluge>();
                return dodatneusluge;
            }
            set
            {
                dodatneusluge = value;
                OnPropertyChanged("DodatneUsluge");
            }
        }

        
     
        public double UkupnaCena
        {
            get
            {
                return ukupnacena;
            }
            set
            {
                ukupnacena = value;
                OnPropertyChanged("UkupnaCena");
            }
        }

        public object Clone()
        {
            Racun klon = new Racun();
            klon.ID = ID;
            klon.NamestajZaProdaju = NamestajZaProdaju;
            klon.DatumProdaje = DatumProdaje;
            klon.BrojRacuna = BrojRacuna;
            klon.Kupac = Kupac;
            klon.DodatneUsluge = DodatneUsluge;
            klon.UkupnaCena = UkupnaCena;
            

            return klon;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged(string propertyName)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public static double CenaSaPDV(double CenaBezPDV)
        {
            return (CenaBezPDV * (1 + PDV));
        }
    }
}
