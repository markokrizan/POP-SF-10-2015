using System;
using System.Collections.Generic;
using System.Text;

namespace POP_SF_10_2015.Model
{

    public enum TipKorisnika
    {
        //vidi jos ovo za int
        Administrator = 1,
        Prodavac = 2
    }
    public class Korisnik
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorIme { get; set; }
        public string Lozinka { get; set; }
        public TipKorisnika TipKorisnika { get; set; }

        /*
        public Korisnik(int ID, string Ime, string Prezime, string KorIme, string Lozinka, TipKorisnika tipKorisnika)
        {
            this.ID = ID;
            this.Ime = Ime;
            this.Prezime = Prezime;
            this.KorIme = KorIme;
            this.Lozinka = Lozinka;
            this.TipKorisnika = tipKorisnika;
        }

       
        */



    }
}
