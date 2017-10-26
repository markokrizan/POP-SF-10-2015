using System;
using System.Collections.Generic;
using System.Text;

namespace POP_SF_10_2015.Model
{
    public class Akcija
    {
        public int ID { get; set; }
        public bool Obrisana { get; set; }
        public decimal Popust { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public DateTime DatumPocetka { get; set; }


        public Akcija(int ID, bool Obrisana, DateTime DatumPocetka, DateTime DatumZavrsetka, decimal Popust)
        {
            this.ID = ID;
            this.Obrisana = Obrisana;
            this.DatumPocetka = DatumPocetka;
            this.DatumZavrsetka = DatumZavrsetka;
            this.Popust = Popust;
        }



















    }

    
}
