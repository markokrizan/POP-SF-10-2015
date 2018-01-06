using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml.Serialization;

namespace POP_SF_10_2015.Model
{
    public class Namestaj:ICloneable, INotifyPropertyChanged
    {

        
        //OVO SU SVOJSTVA, A DOLE SU METODE USTVARI KOJE IM PRISTUPAJU I MENJAJU IH GETERIMA I SETERIMA
        private int id;
        private bool obrisan;
        private string naziv;
        private string sifra;
        private double cena;
        private int kolicinaumagacinu;       
        private int idtipanamestaja;
        private TipNamestaja tipNamestaja;
        //private int idakcije;
        //private Akcija akcija;


        //polje
        public event PropertyChangedEventHandler PropertyChanged;

        //vidi za 2 konsruktora, jedan bez parametra koji instancira neki povezani
        //observable collection, i jedan sa parametrima koji ne znam cemu sluzi 



        //ova dva konstruktora zbog??????
        public Namestaj()
        {

        }

        
        
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
                    return tipNamestaja = TipNamestaja.GetById(idtipanamestaja);
                }
                return tipNamestaja;
                
                //return TipNamestaja.GetById(idtipanamestaja);
                //return tipNamestaja;
            }
            set
            {
                tipNamestaja = value;
                //tipNamestaja was null
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
        





        public override string ToString()
        {
            return $"{Naziv}, {TipNamestaja.Naziv}, {Cena}";
        }



        


        public static Namestaj GetById(int id)
        {
            foreach (var namestaj in Projekat.Instance.namestaj)
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
