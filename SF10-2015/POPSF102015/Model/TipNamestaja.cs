﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace POP_SF_10_2015.Model
{
    public class TipNamestaja:ICloneable, INotifyPropertyChanged
    {

        private int id;
        private bool obrisan;
        private string naziv;





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

        public event PropertyChangedEventHandler PropertyChanged;

        public static TipNamestaja GetById(int id)
        {
            foreach(var tipNamestaja in Projekat.Instance.tipnamestaja)
            {
                if(tipNamestaja.ID == id)
                {
                    return tipNamestaja;
                }
                
            }
            return null;

        }

        public object Clone()
        {
            TipNamestaja kopija = new TipNamestaja();
            kopija.ID = ID;
            kopija.Obrisan = Obrisan;
            kopija.Naziv = Naziv;           
            return kopija;
        }

        public override string ToString()
        {
            return Naziv;
        }

        protected void OnPropertyChanged(string propertyName)
        {

            if (PropertyChanged != null)
            {              
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        
        



















































    }
}
