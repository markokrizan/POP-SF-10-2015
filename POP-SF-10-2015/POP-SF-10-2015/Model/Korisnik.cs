using System;
using System.Collections.Generic;
using System.Text;

namespace POP_SF_10_2015.Model
{

    public enum TipKorisnika
    {
        Administrator,
        Prodavac
    }
    public class Korisnik
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorIme { get; set; }
        public string Lozinka { get; set; }
        public TipKorisnika TipKorisnika { get; set; }


    }
}
