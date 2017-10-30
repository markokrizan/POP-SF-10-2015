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


        /*
        public List<Korisnik> Korisnici { get; set; }
        public List<DodatneUsluge> DodatneUsluge { get; set; }
        public List<Akcija> Akcije { get; set; }
        */

        public Projekat()
        {
            

        }







    }
}
