using System;
using System.Collections.Generic;
using System.Text;

namespace POP_SF_10_2015.Model
{
    public class Racun
    {
        public int ID { get; set; }
        public List<Namestaj> NamestajZaProdaju { get; set; }
        public DateTime DatumProdaje { get; set; }
        public string BrojRacuna { get; set;}
        public string Kupac { get; set; }
        public List<DodatneUsluge> DodatneUsluge { get; set; }

        public const double PDV = 0.02;
        public double UkupnaCena { get; set; }


    }
}
