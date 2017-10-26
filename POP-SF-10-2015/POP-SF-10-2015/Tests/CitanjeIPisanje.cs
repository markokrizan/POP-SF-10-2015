using POP_SF_10_2015.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace POP_SF_10_2015.Tests
{
    class CitanjeIPisanje
    {
        public List<Korisnik> listaKorisnici { get; set; }
        public List<Namestaj> listaNamestaj { get; set; }
        public List<TipNamestaja> listaTipNamestaja { get; set; }
        public List<DodatneUsluge> listaDodatneUsluge { get; set; }
        public List<Prodaja> listaProdaja { get; set; }
        public List<Akcija> listaAkcija { get; set; }



        public CitanjeIPisanje()
        {
            this.listaKorisnici = new List<Korisnik>();
            this.listaNamestaj = new List<Namestaj>();
            this.listaTipNamestaja = new List<TipNamestaja>();
            this.listaDodatneUsluge = new List<DodatneUsluge>();
            this.listaProdaja = new List<Prodaja>();
            this.listaAkcija = new List<Akcija>();


        }



        public void UcitajKorisnike()
        {
            try
            {

                string[] lines = File.ReadAllLines(path: @"Fajlovi/Korisnici.txt");
                foreach (string line in lines)
                {
                    string[] col = line.Split(new char[] { '|' });
                    int ID = int.Parse(col[0]);


                    String ime = col[1];

                    String prezime = col[2];
                    String korIme = col[3];
                    String sifra = col[4];
                    int intEnum = Int32.Parse(col[5]);
                    TipKorisnika tipKor = (TipKorisnika)intEnum;

                    Korisnik kor = new Korisnik(ID, ime, prezime, korIme, sifra, tipKor);
                    listaKorisnici.Add(kor);




                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Fajl nije procitan!");
                Console.WriteLine(e.Message);
            }
        }

        public bool login(String korisnickoIme, String sifra)
        {
            foreach (Korisnik kor in listaKorisnici)
            {
                if (kor.KorIme == korisnickoIme &&
                        kor.Lozinka == sifra)
                {

                    Console.WriteLine("Ulogovani korisnik je: " + kor.Ime);

                    return true;
                }

            }
            Console.WriteLine("Niste uneli dobre podatke!");
            return false;
        }


        public void UcitajTipNamestaja()
        {
            try
            {

                string[] lines = File.ReadAllLines(path: @"Fajlovi/TipNamestaja.txt");
                foreach (string line in lines)
                {
                    string[] col = line.Split(new char[] { '|' });

                    int ID = int.Parse(col[0]);
                    String obrisanStr = col[1];
                    bool obrisanBool = false;
                    if (obrisanStr == "Da")
                    {
                        obrisanBool = true;
                    }
                    else if (obrisanStr == "Ne")
                    {
                        obrisanBool = false;
                    }
                    else
                    {
                        Console.WriteLine("Za vrednost obrisan(da/ne) je uneseno nesto drugo");
                    }
                    String naziv = col[2];

                    TipNamestaja tn = new TipNamestaja();
                    listaTipNamestaja.Add(tn);




                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Fajl nije procitan!");
                Console.WriteLine(e.Message);
            }
        }




        /*
        public void UcitajAkcije()
        {
            try
            {

                string[] lines = File.ReadAllLines(path: @"Fajlovi/Akcije.txt");
                foreach (string line in lines)
                {
                    string[] col = line.Split(new char[] { '|' });

                    int ID = int.Parse(col[0]);
                    String obrisanStr = col[1];
                    bool obrisanBool = false;
                    if (obrisanStr == "Da")
                    {
                        obrisanBool = true;
                    }
                    else if (obrisanStr == "Ne")
                    {
                        obrisanBool = false;
                    }
                    else
                    {
                        Console.WriteLine("Za vrednost obrisan(da/ne) je uneseno nesto drugo");
                    }

                    DateTime datumPocetka = DateTime.Parse(col[2]);
                    DateTime datumKraja = DateTime.Parse(col[3]);

                    Akcija a = new Akcija(ID, obrisanBool, datumPocetka, datumKraja);
                    listaAkcija.Add(a);




                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Fajl nije procitan!");
                Console.WriteLine(e.Message);
            }
        }


        */










        public void UcitajNamestaj()
        {
            try
            {

                string[] lines = File.ReadAllLines(path: @"Fajlovi/Namestaj.txt");
                foreach (string line in lines)
                {
                    string[] col = line.Split(new char[] { '|' });

                    int ID = int.Parse(col[0]);
                    String obrisanStr = col[1];
                    bool obrisanBool = false;
                    if (obrisanStr == "Da")
                    {
                        obrisanBool = true;
                    }
                    else if (obrisanStr == "Ne")
                    {
                        obrisanBool = false;
                    }
                    else
                    {
                        Console.WriteLine("Za vrednost obrisan(da/ne) je uneseno nesto drugo");
                    }
                    String naziv = col[2];
                    String sifra = col[3];
                    double cena = double.Parse(col[4]);
                    int kolicinaUMagacinu = int.Parse(col[5]);

                    String tipNam = col[6];
                    //TipNamestaja tipNamestaja = null;


                    /*
                    foreach (TipNamestaja tipN in listaTipNamestaja)
                    {
                        if (tipN.Naziv == tipNam)
                        {
                            tipNamestaja = tipN;

                        }
                    }

                    */

                    Namestaj nam = new Namestaj();
                    listaNamestaj.Add(nam);






                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Fajl nije procitan!");
                Console.WriteLine(e.Message);
            }




        }

        public void prikaziNamestaj()
        {
            foreach (Namestaj n in listaNamestaj)
            {
                Console.WriteLine();
                Console.WriteLine("ID: " + n.ID);
                Console.WriteLine("Naziv: " + n.Naziv);
                Console.WriteLine("Sifra: " + n.Sifra);
                Console.WriteLine("Cena: " + n.Cena);
                Console.WriteLine("Kolicina: " + n.KolicinaUMagacinu);
                Console.WriteLine("Tip namestaja: " + n.TipNamestaja);
                Console.WriteLine();




            }

        }



    }

}
