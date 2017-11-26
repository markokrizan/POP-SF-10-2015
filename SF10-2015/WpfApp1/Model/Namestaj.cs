using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace POP_SF_10_2015.Model
{
    public class Namestaj:ICloneable, INotifyPropertyChanged
    {

        //dodao za binding
        //dodao private na polja


        //OVO SU SVOJSTVA, A DOLE SU METODE USTVARI KOJE IM PRISTUPAJU I MENJAJU IH GETERIMA I SETERIMA
        private int id;
        private bool obrisan;
        private string naziv;
        private string sifra;
        private double cena;
        private int kolicinaumagacinu;

        //verovatno i za jedan i za drugi treba
        private int idtipanamestaja;
        private TipNamestaja tipNamestaja;

        

        //polje
        public event PropertyChangedEventHandler PropertyChanged;

        //vidi za 2 konsruktora, jedan bez parametra koji instancira neki povezani
        //observable collection, i jedan sa parametrima koji ne znam cemu sluzi 



        //ova dva konstruktora zbog??????
        public Namestaj()
        {

        }

        /*
        public Namestaj(int id, bool obrisan, string naziv, string sifra, double cena, 
            int kolicinaumagacinu, int idtipanamestaja)
        {
            this.id = id;
            this.obrisan = obrisan;
            this.naziv = naziv;
            this.sifra = sifra;
            this.cena = cena;
            this.kolicinaumagacinu = kolicinaumagacinu;
            this.idtipanamestaja = idtipanamestaja;
        }
        */
        
        //propfull tab tab
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

        //da preskoci pisanje u xml da ne ude objekat u objektu
        [XmlIgnore]
        public TipNamestaja TipNamestaja
        {
            get
            {
                //kada kazes namestaj.tipnamestaja uvuci ce samo id
                if(tipNamestaja == null)
                {
                    tipNamestaja = TipNamestaja.GetById(idtipanamestaja);
                }
                return tipNamestaja;
            }
            set
            {
                tipNamestaja = value;
                idtipanamestaja = tipNamestaja.ID;

                OnPropertyChanged("TipNamestaja");
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
                

                OnPropertyChanged("IDTIpaNamestaja");
            }
        }
        //stavi tip namestaja int 
        //public Akcija Akcija { get; set; }

        //da kada se poziva namestaj se ne poziva difolt toString metoda koja ispisuje namestpace.imeobjekta, nego je overrajdujemo ovako





        public override string ToString()
        {
            return $"{Naziv}, {Cena}, {TipNamestaja.GetById(IDTipaNamestaja).Naziv}";
        }



        //izmenio metodu 

        /*
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        */


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





        public object Clone()
        {
            Namestaj kopija = new Namestaj();
            kopija.ID = ID;
            kopija.Obrisan = Obrisan;
            kopija.Naziv = Naziv;
            kopija.Sifra = Sifra;
            kopija.Cena = Cena;
            kopija.KolicinaUMagacinu = KolicinaUMagacinu;
            kopija.IDTipaNamestaja = IDTipaNamestaja;
            return kopija;

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



        

        
    }

}
