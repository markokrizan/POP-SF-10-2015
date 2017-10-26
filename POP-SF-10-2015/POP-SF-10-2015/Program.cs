
using POP_SF_10_2015.Model;
using POP_SF_10_2015.Tests;
using System;
using System.Collections.Generic;

namespace POP_SF_10_2015
{
    class Program
    {
        private static List<Namestaj> Namestaj = new List<Namestaj>();
        private static List<TipNamestaja> Tipovi = new List<TipNamestaja>();
        static void Main(string[] args)
        {
            Salon s1 = new Salon()
            {
                //umesto konstruktora
                ID = 1,
                Naziv = "Moj Salon",
                Adresa = "Trg Dositeja Obradovica",
                BrojZiroRacuna = "23480248092348094",
                EMail = "nekimejl@uns.ac.rs",
                MaticniBroj = 456343,
                PIB = 123,
                Telefon = "021/222/222",
                Sajt = "www.nesto.com",


            };

            

            var tp1 = new TipNamestaja()
            {
                ID = 1,
                Naziv = "Krevet"

            };

            var tp2 = new TipNamestaja()
            {
                ID = 1,
                Naziv = "Stolica"

            };

            var n1 = new Namestaj()
            {
                ID = 1,
                Cena = 777,
                TipNamestaja = tp1,
                Naziv = "Neki krevet",
                KolicinaUMagacinu = 10,
                Sifra="sf1312313"

            };

            Tipovi.Add(tp1);
            Tipovi.Add(tp2);
            Namestaj.Add(n1);



            //Console.WriteLine("Dobro dosli u salon namestaja!"  {s1.Naziv});
            Console.WriteLine("Dobro dosli u salon namestaja!");
            IspisGlavniMeni();



            /*
            //MOJE
            Console.WriteLine();
            CitanjeIPisanje cp = new CitanjeIPisanje();
            cp.UcitajKorisnike();
            cp.UcitajTipNamestaja();
            cp.UcitajNamestaj();
            Forme.LoginForma();
            bool log = cp.login(Forme.uname, Forme.pass);


            while (log == false)
            {
                Forme.LoginForma();
                log = cp.login(Forme.uname, Forme.pass);
            };

            if (log == true)
            {
                Forme.Meni();
            }
            else
            {
                Forme.LoginForma();
            }

            
            while(Forme.izbor != "1" || Forme.izbor != "2")
            {
                Forme.Meni();
            }
            

            if(Forme.izbor == "1") {
                cp.prikaziNamestaj();
                Console.WriteLine("jeste 1");
                Forme.Meni();
            }
            else
            {
                Console.WriteLine("Lenj si previse!");
            }
           

            */











        }

        private static void IspisGlavniMeni()
        {
            int izbor = 0;

            do
            {
                do
                {
                    Console.WriteLine("1. Rad sa namestajem");
                    Console.WriteLine("2. Rad sa tipom namestajem");

                    Console.WriteLine("0. Izlaz iz aplikacije");


                    izbor = int.Parse(Console.ReadLine());

                } while (izbor < 0 || izbor > 2);

                switch (izbor)
                {
                    case 1:
                        NamestajMeni();
                        break;







                    default:
                        break;
                }

                


            } while (izbor != 0);
        }

        private static void NamestajMeni()
        {
            int izbor = 0;

            do
            {
                do
                {
                    Console.WriteLine("Rad sa namestajem:");
                    IspisiCRUDMeni();
                    izbor = int.Parse(Console.ReadLine());

                } while (izbor < 0 || izbor > 4);

                switch (izbor)
                {
                    case 1:
                        PrikaziNamestaj();
                        break;

                    case 2:
                        DodajNamestaj();
                        break;

                    case 3:
                        IzmeniNamestaj();
                        break;

                    case 4:
                        ObrisiNamestaj();
                        break;









                    default:
                        break;

                }


            } while (izbor != 0);

            

        }

        private static void ObrisiNamestaj()
        {
            Console.WriteLine("Obrisi Namestaj: ");
            Console.WriteLine();
            Console.Write("Unesi naziv: ");
            string naziv = Console.ReadLine();

            for (int i = 0; i < Namestaj.Count; i++)
            {
                Namestaj.RemoveAt(i);
                break;
            }
        }

        private static void IzmeniNamestaj()
        {
            Console.WriteLine("Izmeni namestaj:");
            Console.WriteLine();
            Console.Write("Unesi naziv: ");
            string naziv = Console.ReadLine();

            

            foreach(Namestaj nam in Namestaj)
            {
                if(naziv == nam.Naziv)
                {
                    nam.Naziv = naziv;
                    Console.Write("Sifra: ");
                    nam.Sifra = Console.ReadLine();
                    Console.Write("Cena: ");
                    nam.Cena = double.Parse(Console.ReadLine());
                    Console.WriteLine("Namestaj uspesno izmenjen");
                }
                //Console.WriteLine("Niste uneli dobar naziv namestaja");
            }
        }

        private static void DodajNamestaj()
        {
            Console.WriteLine("Dodaj namestaj: ");

            

            Console.Write("Naziv: ");
            String naziv = Console.ReadLine();

            /*
            Console.Write("Sifra: ");
            String sifra = Console.ReadLine();
            */

            Console.Write("Cena: ");
            double cena = double.Parse(Console.ReadLine());

            /*
            Console.Write("Kolicina u magacinu: ");
            int kolicina = int.Parse(Console.ReadLine());
            */

            /*
            Console.Write("Tip Nametaja: ");
            Console.WriteLine("Izaberi tip: ");
            foreach(TipNamestaja tp in Tipovi)
            {
                Console.WriteLine($"{tp.ID}. {tp.Naziv}");
            }
            */

            Console.WriteLine("Unesite ID tipa namestaja:"); //ne radi se ovako, nego preko ID-a
            //string nazivTipaNamestaja = Console.ReadLine();

            int idTipaNamestaja = int.Parse(Console.ReadLine());

            TipNamestaja trazeniTipNametstaja = null;
            foreach (var tipNamestaja in Tipovi)
            {
                if(tipNamestaja.ID == idTipaNamestaja)
                {
                    trazeniTipNametstaja = tipNamestaja;
                }
            }
            {

            }

            var noviNamestaj = new Namestaj()
            {
                ID = Namestaj.Count + 1,
                Naziv = naziv,
                Cena = cena,
                TipNamestaja = trazeniTipNametstaja
            


            };

            Namestaj.Add(noviNamestaj);
        }

        private static void PrikaziNamestaj()
        {
            Console.WriteLine("Listing Namestaja:");
            for (int i = 0; i < Namestaj.Count; i++) 
            {
                Console.WriteLine($"{i + 1}. naziv: {Namestaj[i].Naziv}, cena: {Namestaj[i].Cena}, tip namestajaj: {Namestaj[i].TipNamestaja.Naziv}");
                //Console.WriteLine(Namestaj[0].Naziv);
            }
        }

        private static void IspisiCRUDMeni()
        {
            Console.WriteLine("1. Prikazi ");
            Console.WriteLine("2. Dodaj");
            Console.WriteLine("3. Izmeni");
            Console.WriteLine("4. Obrisi");
            Console.WriteLine("4. Povratak na predhodni meni");



        }
    }

}
