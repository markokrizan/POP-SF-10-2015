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
    public class Akcija: ICloneable, INotifyPropertyChanged
    {
        private int id;
        private bool obrisana;
        private string naziv;
        private decimal popust;
        private DateTime datumpocetka;
        private DateTime datumzavrsetka;



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
        public bool Obrisana
        {
            get
            {
                return obrisana;
            }
            set
            {
                obrisana = value;
                OnPropertyChanged("Obrisana");
            }
        }      

        public string Naziv
        {
            get
            { return naziv;
            }
            set
            {
                naziv = value;
                OnPropertyChanged("Naziv");
            }
        }

        public decimal Popust
        {
            get
            {
                return popust;
            }
            set
            {
                popust = value;
                OnPropertyChanged("Popust");
            }
        }
        public DateTime DatumPocetka
        {
            get
            {
                return datumpocetka;
            }
            set
            {
                datumpocetka = value;
                OnPropertyChanged("DatumPocetka");
            }
        }
        public DateTime DatumZavrsetka
        {
            get
            {
                return datumzavrsetka;
            }
            set
            {
                datumzavrsetka = value;
                OnPropertyChanged("DatumZavrsetka");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
            Akcija kopija = new Akcija();
            kopija.ID = ID;
            kopija.Naziv = Naziv;
            kopija.Obrisana = Obrisana;
            kopija.Popust = Popust;
            kopija.DatumPocetka = DatumPocetka;
            kopija.DatumZavrsetka = DatumZavrsetka;
            return kopija;
        }

        public static Akcija GetById(int id)
        {
            foreach (var akcija in Projekat.Instance.akcija)
            {
                if (akcija.ID == id)
                {
                    return akcija;
                }

            }
            return null;

        }


























    }


}
