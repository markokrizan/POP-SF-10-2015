using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace POP_SF_10_2015.Model
{
    public class TipNamestaja: INotifyPropertyChanged
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
            foreach(var tipNamestaja in Projekat.Instance.TipoviNamestaja)
            {
                if(tipNamestaja.ID == id)
                {
                    return tipNamestaja;
                }
                
            }
            return null;

        }

        


        public override string ToString()
        {
            return Naziv;
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
