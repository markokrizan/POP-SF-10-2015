using POP_SF_10_2015.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        //promenio za namestaj i tip namestaja iz liste u obsevable collection, isto i u serijalajzeru
        public ObservableCollection<Namestaj> Namestaj;
        //public ObservableCollection<Namestaj> NamestajNaStanju;
        public ObservableCollection<Korisnik> Korisnici;
        public ObservableCollection<TipNamestaja> TipoviNamestaja;
        //i za ostale
        //private List<DodatneUsluge> dodatneUsluge = new List<DodatneUsluge>();
        //private List<Akcija> akcije = new List<Akcija>();

        private Projekat()
        {
            //ovi objekti MORAJU biti postojani ne sme da se koristi nova kolekcija, jer se gubi veza sa mapiranjem, mozes ih prazniti i puniti
            //ali ne ih menjati drugima
            Namestaj = new ObservableCollection<Namestaj>(GenericSerializer.Deserialize<Namestaj>("namestaj.xml"));
            
            /*
            NamestajNaStanju = new ObservableCollection<Namestaj>();
            foreach(Namestaj nam in Namestaj)
            {
                if (nam.Obrisan == false)
                {
                    NamestajNaStanju.Add(nam);
                }
            }
            */

            TipoviNamestaja = new ObservableCollection<TipNamestaja>(GenericSerializer.Deserialize<TipNamestaja>("tipovinamestaja.xml"));

        }

     









    }
}
