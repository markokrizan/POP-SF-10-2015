using POP_SF_10_2015.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace POP_SF_10_2015.Model
{
    public class Projekat
    {
        //ZA singleton design pattern
        //jedan objekat koji sadrzi sve ostale
        //svaki put kad pristupis propertiju npr namestaja on ce da ucita iz fajla zbog ponasanja u geteru

        //samo setter da niko ne moze da ga menja
        //odma instanciramo
        public static Projekat Instance { get;  } = new Projekat();

        
        private List<Namestaj> namestaj = new List<Namestaj>();
        private List<Korisnik> korisnici = new List<Korisnik>();
        private List<TipNamestaja> tipoviNamestaja = new List<TipNamestaja>();
        private List<DodatneUsluge> dodatneUsluge = new List<DodatneUsluge>();
        private List<Akcija> akcije = new List<Akcija>();



        public  List<Namestaj> Namestaj
        {
            get {
                //atribut namestaj(lista) ce biti ono sto procitas
                this.namestaj = GenericSerializer.Deserialize<Namestaj>("namestaj.xml");
                return this.namestaj;
            }
            set {
                //uzmi sad to
                this.namestaj = value;
                GenericSerializer.Serialize<Namestaj>("namestaj.xml", this.namestaj);
            }
        }

        public List<Korisnik> Korisnici
        {
            get
            {
                //atribut namestaj(lista) ce biti ono sto procitas
                this.korisnici = GenericSerializer.Deserialize<Korisnik>("korisnici.xml");
                return this.korisnici;
            }
            set
            {
                //uzmi sad to
                this.korisnici = value;
                GenericSerializer.Serialize<Korisnik>("korisnici.xml", this.korisnici);
            }
        }



        public List<TipNamestaja> TipoviNamestaja
        {
            get
            {
                //atribut namestaj(lista) ce biti ono sto procitas
                this.tipoviNamestaja = GenericSerializer.Deserialize<TipNamestaja>("tipovinamestaja.xml");
                return this.tipoviNamestaja;
            }
            set
            {
                //uzmi sad to
                this.tipoviNamestaja = value;
                GenericSerializer.Serialize<TipNamestaja>("tipovinamestaja.xml", this.tipoviNamestaja);
            }
        }

        public List<DodatneUsluge> DodatneUsluge
        {
            get
            {
                //atribut namestaj(lista) ce biti ono sto procitas
                this.dodatneUsluge = GenericSerializer.Deserialize<DodatneUsluge>("dodatneusluge.xml");
                return this.dodatneUsluge;
            }
            set
            {
                //uzmi sad to
                this.dodatneUsluge = value;
                GenericSerializer.Serialize<DodatneUsluge>("dodatneusluge.xml", this.dodatneUsluge);
            }
        }












    }
}
