using POP_SF_10_2015.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WpfApp1.DAL;

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

        
        public ObservableCollection<Namestaj> namestaj;
        public ObservableCollection<DodatneUsluge> dodatneusluge;
        public ObservableCollection<Korisnik> korisnici;
        public ObservableCollection<TipNamestaja> tipnamestaja;
        public ObservableCollection<Akcija> akcija;
        public ObservableCollection<Racun> racun;
        public ObservableCollection<Salon> salon;




        private Projekat()
        {
            //ovi objekti MORAJU biti postojani ne sme da se koristi nova kolekcija, jer se gubi veza sa mapiranjem, mozes ih prazniti i puniti          
            //Namestaj = new ObservableCollection<Namestaj>(GenericSerializer.Deserialize<Namestaj>("namestaj.xml"));
            //Usluge = new ObservableCollection<DodatneUsluge>(GenericSerializer.Deserialize<DodatneUsluge>("dodatneusluge.xml"));
            //TipoviNamestaja = new ObservableCollection<TipNamestaja>(GenericSerializer.Deserialize<TipNamestaja>("tipovinamestaja.xml"));
            //Korisnici = new ObservableCollection<Korisnik>(GenericSerializer.Deserialize<Korisnik>("korisnici.xml"));
            //Akcije = new ObservableCollection<Akcija>(GenericSerializer.Deserialize<Akcija>("akcije.xml"));
            //Racuni = new ObservableCollection<Racun>(GenericSerializer.Deserialize<Racun>("racuni.xml"));


            //sa bazama
            //NE DIRAJ NIGDE GetAll SAMO TU
            //Samo jednom povuces u kolekciju, radis sa kolekcijom i po potrebi spustas na bazu, ako ti treba iteracija imas u kolekciji
            
            tipnamestaja = TipNamestajaDAL.GetAll();
            namestaj = NamestajDAL.GetAll();
            dodatneusluge = DodatneUslugeDAL.GetAll();
            salon = SalonDAL.GetAll();
            akcija = AkcijaDAL.GetAll();
            korisnici = KorisnikDAL.GetAll();
            racun = RacunDAL.GetAll();



        }


       














    }
}
