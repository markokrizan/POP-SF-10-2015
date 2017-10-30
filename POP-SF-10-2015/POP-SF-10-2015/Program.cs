using POP_SF_10_2015.Model;
using POP_SF_10_2015.Tests;
using POP_SF_10_2015.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace POP_SF_10_2015
{
    class Program
    {

        static void Main(string[] args)

        {
            //KonzolnaAplikacija ka = new KonzolnaAplikacija();

            /*

            var tn1 = new TipNamestaja()
            {
                ID = 1,
                Naziv = "Podna lamba"
            };

            var n1 = new Namestaj()
            {
                ID = 1,
                Naziv = "Svetlo",
                Cena = 28,
                KolicinaUMagacinu = 3,
                Sifra = "234234234",
                TipNamestaja = tn1
            };

            var n2 = new Namestaj()
            {
                ID = 1,
                Naziv = "Lampurina",
                Cena = 20,
                KolicinaUMagacinu = 2,
                Sifra = "23423425645",
                TipNamestaja = tn1
            };

            var n3 = new Namestaj()
            {
                ID = 2,
                Naziv = "Lampursdfsina",
                Cena = 220,
                KolicinaUMagacinu = 32,
                Sifra = "234233425645",
                TipNamestaja = tn1
            };

            var ln1 = new List<Namestaj>();
            ln1.Add(n1);
            ln1.Add(n2);
            ln1.Add(n3);


            //citam - izmenim - sacuvam // serialize - pisi deserialize - citaj
            Console.WriteLine("Serialization...");

            //uctiaj iz liste u fajl
            GenericSerializer.Serialize<Namestaj>("namestaj.xml", ln1);

            var ln2 = GenericSerializer.Deserialize<Namestaj>("namestaj.xml");

            Console.WriteLine(ln2);

            Console.WriteLine("Finished Serialization...");
            Console.ReadLine();
            */

            List<Namestaj> namestaj = Projekat.Instance.Namestaj;



            //dodavanje:

            namestaj.Add(new Namestaj() { ID = 1, Naziv = "Novi namestaj" });
            Projekat.Instance.Namestaj = namestaj;
                       

            //uradi ostale, pretragu, logovanje








            foreach(var stavka in namestaj)
            {
                Console.WriteLine($"{stavka.Naziv}");
            }
            Console.ReadLine();





        }


    }
}
