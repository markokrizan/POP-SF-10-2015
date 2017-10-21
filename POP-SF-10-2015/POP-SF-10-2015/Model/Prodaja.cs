using System;
using System.Collections.Generic;
using System.Text;

namespace POP_SF_10_2015.Model
{
    public class Prodaja
    {
        public int ID { get; set; }
        List<Namestaj> NamestajZaProdaju { get; set; }
        public DateTime DatumProdaje { get; set; }
        public string BrojRacuna { get; set;}
        public string Kupac { get; set; }
        public List<DodatneUsluge> DodatneUsluge { get; set; }
        public double  PDV { get; set; }
        public double UkupnaCena { get; set; }


    }
}
