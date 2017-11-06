using System;
using System.Collections.Generic;
using System.Text;

namespace POP_SF_10_2015.Model
{
    public class Namestaj
    {
        public int ID { get; set; }
        public bool Obrisan { get; set; }
        public string Naziv { get; set; }
        public string Sifra { get; set; }
        public double Cena { get; set; }
        public int KolicinaUMagacinu { get; set; }
        public int IDTipaNamestaja { get; set; }
        //stavi tip namestaja int 
        //public Akcija Akcija { get; set; }

        //da kada se poziva namestaj se ne poziva difolt toString metoda koja ispisuje namestpace.imeobjekta, nego je overrajdujemo ovako
        public override string ToString()
        {
            return $"{Naziv}, {Cena}, {TipNamestaja.GetById(IDTipaNamestaja).Naziv}";
        }








    }

}
