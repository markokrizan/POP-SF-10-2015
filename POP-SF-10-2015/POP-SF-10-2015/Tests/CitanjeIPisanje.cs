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

                    var noviKorisnik = new Korisnik()
                    {
                        ID = ID,
                        Ime = ime,
                        Prezime = prezime,
                        KorIme = korIme,
                        Lozinka = sifra,
                        TipKorisnika = tipKor


                    };

                    
                    listaKorisnici.Add(noviKorisnik);


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

                    TipNamestaja tn = new TipNamestaja()
                    {
                        ID = ID,
                        Obrisan = obrisanBool,
                        Naziv = naziv




                    };
                    listaTipNamestaja.Add(tn);




                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Fajl nije procitan!");
                Console.WriteLine(e.Message);
            }
        }


        

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

                    int idTipaNamestaja = int.Parse(col[6]);
                    

                    TipNamestaja trazeniTipNamestaja = null;
                    foreach (var tipNamestaja in listaTipNamestaja)
                    {
                        if (tipNamestaja.ID == idTipaNamestaja)
                        {
                            trazeniTipNamestaja = tipNamestaja;
                        }
                    }

                    

                    Namestaj nam = new Namestaj()
                    {
                        ID = ID,
                        Obrisan = obrisanBool,
                        Naziv = naziv,
                        Sifra = sifra,
                        Cena = cena,
                        KolicinaUMagacinu = kolicinaUMagacinu,
                        TipNamestaja = trazeniTipNamestaja

                    };

                    
                    listaNamestaj.Add(nam);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Fajl nije procitan!");
                Console.WriteLine(e.Message);
            }




        }

        public void UcitajDodatneUsluge()
        {
            try
            {

                string[] lines = File.ReadAllLines(path: @"Fajlovi/Usluge.txt");
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
                    double cena = double.Parse(col[3]);
                    
                 
                    DodatneUsluge us = new DodatneUsluge()
                    {
                        ID = ID,
                        Obrisana = obrisanBool,
                        Naziv = naziv,                       
                        Cena = cena
                        

                    };


                    listaDodatneUsluge.Add(us);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Fajl nije procitan!");
                Console.WriteLine(e.Message);
            }

        }


       

        /*
        public void PrikaziNamestaj()
        {
            foreach (Namestaj n in listaNamestaj)
            {
                Console.WriteLine();
                Console.WriteLine("ID: " + n.ID);
                if(n.Obrisan == true)
                {
                    Console.WriteLine("Obrisan");
                }
                else
                {
                    Console.WriteLine("Nije obrisan");
                }
                Console.WriteLine("Naziv: " + n.Naziv);
                Console.WriteLine("Sifra: " + n.Sifra);
                Console.WriteLine("Cena: " + n.Cena);
                Console.WriteLine("Kolicina: " + n.KolicinaUMagacinu);
                Console.WriteLine("Tip namestaja: " + n.TipNamestaja.Naziv);
                Console.WriteLine();




            }

        }

        */

        

        public void ObrisiNamestaj()
        {
            Console.WriteLine("Obrisi Namestaj: ");
            Console.WriteLine();
            Console.Write("Unesi ID namestaja: ");
            int id = int.Parse(Console.ReadLine());

            

            
            //namerno ovako od kraja ka pocetku
            for (int i = listaNamestaj.Count - 1; i >= 0; i--)
            {
                if (listaNamestaj[i].ID == id)
                {
                    listaNamestaj.RemoveAt(i);
                }
            }


           

        }

        public void ObrisiUslugu()
        {
            Console.WriteLine("Obrisi uslugu: ");
            Console.WriteLine();
            Console.Write("Unesi ID namestaja: ");
            int id = int.Parse(Console.ReadLine());




            //namerno ovako od kraja ka pocetku
            for (int i = listaDodatneUsluge.Count - 1; i >= 0; i--)
            {
                if (listaDodatneUsluge[i].ID == id)
                {
                    listaDodatneUsluge.RemoveAt(i);
                }
            }




        }

        public void ObrisiTipNamestaja()
        {
            Console.WriteLine("Obrisi tip namestaja: ");
            Console.WriteLine();
            Console.Write("Unesi ID tipa namestaja: ");
            int id = int.Parse(Console.ReadLine());




            //namerno ovako od kraja ka pocetku
            for (int i = listaTipNamestaja.Count - 1; i >= 0; i--)
            {
                if (listaTipNamestaja[i].ID == id)
                {
                    listaTipNamestaja.RemoveAt(i);
                }
            }




        }

        public  void IzmeniNamestaj()
        {
            Console.WriteLine("Izmeni namestaj:");
            Console.WriteLine();
            Console.Write("Unesi ID: ");
            int ID = int.Parse(Console.ReadLine());



            foreach (Namestaj nam in listaNamestaj)
            {
                if (ID == nam.ID)
                {
                    Console.Write("Naziv: ");
                    nam.Naziv = Console.ReadLine();
                    Console.Write("Sifra: ");
                    nam.Sifra = Console.ReadLine();
                    Console.Write("Kolicina: ");
                    nam.KolicinaUMagacinu = int.Parse(Console.ReadLine());
                    Console.Write("Cena: ");
                    nam.Cena = double.Parse(Console.ReadLine());
                    Console.Write("ID Tipa: ");
                    int idTipaNamestaja = int.Parse(Console.ReadLine());
                    TipNamestaja trazeniTipNamestaja = null;
                    foreach (var tipNamestaja in listaTipNamestaja)
                    {
                        if (tipNamestaja.ID == idTipaNamestaja)
                        {
                            trazeniTipNamestaja = tipNamestaja;
                        }
                    }
                    nam.TipNamestaja = trazeniTipNamestaja;

                    Console.WriteLine("Namestaj uspesno izmenjen");
                }
                //Console.WriteLine("Niste uneli dobar naziv namestaja");
            }
        }

        public void IzmeniUslugu()
        {
            Console.WriteLine("Izmeni uslugu:");
            Console.WriteLine();
            Console.Write("Unesi ID: ");
            int ID = int.Parse(Console.ReadLine());



            foreach (DodatneUsluge us in listaDodatneUsluge)
            {
                if (ID == us.ID)
                {
                    Console.Write("Naziv: ");
                    us.Naziv = Console.ReadLine();                   
                    Console.Write("Cena: ");
                    us.Cena = double.Parse(Console.ReadLine());
                    
                    
                    Console.WriteLine("Usluga uspesno izmenjen");
                }
                //Console.WriteLine("Niste uneli dobar naziv namestaja");
            }
        }

        public void IzmeniTipNamstaja()
        {
            Console.WriteLine("Izmeni tip namestaja:");
            Console.WriteLine();
            Console.Write("Unesi ID: ");
            int ID = int.Parse(Console.ReadLine());



            foreach (TipNamestaja tn in listaTipNamestaja)
            {
                if (ID == tn.ID)
                {
                    Console.Write("Naziv: ");
                    tn.Naziv = Console.ReadLine();
                    


                    Console.WriteLine("Tip namestaja uspesno izmenjen");
                }
                //Console.WriteLine("Niste uneli dobar naziv namestaja");
            }
        }

        public void DodajNamestaj()
        {
            Console.WriteLine("Dodaj namestaj: ");
            Console.WriteLine();
            Console.Write("Naziv: ");
            String naziv = Console.ReadLine();
            Console.Write("Sifra: ");
            String sifra = Console.ReadLine();
            Console.Write("Cena: ");
            double cena = double.Parse(Console.ReadLine());
            Console.Write("Kolicina u magacinu: ");
            int kolicina = int.Parse(Console.ReadLine());
            Console.WriteLine("Unesite ID tipa namestaja:"); 
            int idTipaNamestaja = int.Parse(Console.ReadLine());
            TipNamestaja trazeniTipNametstaja = null;
            foreach (var tipNamestaja in listaTipNamestaja)
            {
                if (tipNamestaja.ID == idTipaNamestaja)
                {
                    trazeniTipNametstaja = tipNamestaja;
                }
            }
            {

            }

            var noviNamestaj = new Namestaj()
            {
                ID = listaNamestaj.Count + 1,
                Sifra = sifra,
                Naziv = naziv,
                Obrisan = false,
                Cena = cena,
                KolicinaUMagacinu = kolicina,
                TipNamestaja = trazeniTipNametstaja



            };

            listaNamestaj.Add(noviNamestaj);
        }

        public void DodajTipNamestaja()
        {
            Console.WriteLine("Dodaj tip namestaja: ");
            Console.WriteLine();
            Console.Write("Naziv: ");
            String naziv = Console.ReadLine();
            

            var noviTipNamestaja = new TipNamestaja()
            {
                ID = listaNamestaj.Count + 1,       
                Obrisan = false,
                Naziv = naziv
                

            };

            listaTipNamestaja.Add(noviTipNamestaja);
        }

        public void DodajUslugu()
        {
            Console.WriteLine("Dodaj uslugu: ");
            Console.WriteLine();
            Console.Write("Naziv: ");
            String naziv = Console.ReadLine();
            
            Console.Write("Cena: ");
            double cena = double.Parse(Console.ReadLine());
            
            

            var novaUsluga = new DodatneUsluge()
            {
                ID = listaNamestaj.Count + 1,               
                Naziv = naziv,
                Obrisana = false,
                Cena = cena,
                
            };

            listaDodatneUsluge.Add(novaUsluga);
        }



        //za sada obrisan ostao kao bool, u gornjoj metodi je if, trenutno zakomentarisana - overloading
        public void PrikaziNamestaj()
        {
            Console.WriteLine();
            Console.WriteLine("Listing Namestaja:");
            Console.WriteLine();
            for (int i = 0; i < listaNamestaj.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Naziv: {listaNamestaj[i].Naziv}, Obrisan: {listaNamestaj[i].Obrisan}, Sifra: {listaNamestaj[i].Sifra}, Kolicina: {listaNamestaj[i].KolicinaUMagacinu}, Cena: {listaNamestaj[i].Cena}, Tip namestajaj: {listaNamestaj[i].TipNamestaja.Naziv}");
                
            }
            Console.WriteLine();
        }

        public void PrikaziUsluge()
        {
            Console.WriteLine();
            Console.WriteLine("Listing Usluga:");
            Console.WriteLine();
            for (int i = 0; i < listaDodatneUsluge.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Naziv: {listaDodatneUsluge[i].Naziv}, Cena: {listaDodatneUsluge[i].Cena}  ");

            }
            Console.WriteLine();
        }

        public void PrikaziTipoveNamestaja()
        {
            Console.WriteLine();
            Console.WriteLine("Listing tipova namestaja:");
            Console.WriteLine();
            for (int i = 0; i < listaTipNamestaja.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Naziv: {listaTipNamestaja[i].Naziv} ");

            }
            Console.WriteLine();
        }


        public double izracunajCenu(double cenaNamestaja, double cenaDodatnihUsluga, int BrojKomada, double PDV)
        {
            double UkupnaCena = cenaNamestaja * BrojKomada * PDV + cenaDodatnihUsluga + cenaNamestaja * BrojKomada;
            return UkupnaCena;
        }



        //shitty code
        public void Prodaj()
        {
            List<Namestaj> n = new List<Namestaj>();
            List<DodatneUsluge> u = new List<DodatneUsluge>();

            Console.WriteLine();
            Console.Write("Unesi ID namestaja za prodaju: ");
            int izbor = int.Parse(Console.ReadLine());
            Console.Write("Unesi kolicinu: ");
            int kolicina = int.Parse(Console.ReadLine());
            Console.Write("Unesi broj racuna: ");
            string brRac = Console.ReadLine();
            Console.Write("Unesi kupca: ");
            string kupac = Console.ReadLine();
            Console.Write("Unesite ID dodatne usluge: ");
            int izborUsluge = int.Parse(Console.ReadLine());

            double CenaUsluga = 0;
            

            foreach(DodatneUsluge us in listaDodatneUsluge)
            {
                if(us.ID == izborUsluge)
                {
                    
                    u.Add(us);
                    CenaUsluga += us.Cena;


                }
            }

            

            foreach (Namestaj nam in listaNamestaj)
            {
                if(nam.ID == izbor)
                {
                    n.Add(nam);
                    if (nam.KolicinaUMagacinu == 0)
                    {
                        Console.WriteLine("Nema vise na stanju!");
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        nam.KolicinaUMagacinu = nam.KolicinaUMagacinu - kolicina;
                        //rucno ovde dodao PDV za sad, posto je atribut Prodaje
                        double uc = izracunajCenu(nam.Cena, CenaUsluga, kolicina, 0.02);
                        Prodaja racun = new Prodaja()
                        {
                            ID = listaProdaja.Count + 1,
                            DatumProdaje = DateTime.Now,
                            NamestajZaProdaju = n,
                            BrojRacuna = brRac,
                            Kupac = kupac,
                            DodatneUsluge = u,
                            UkupnaCena = uc

                            
                        };

                        String namestaj = "";
                        String usluge = "";
                        foreach (Namestaj nm in n)
                        {
                            namestaj += " " +  nm.Naziv;
                        }
                        foreach (DodatneUsluge du in u)
                        {
                            usluge += " " + du.Naziv;
                        }

                        Console.Write("Racun: ");
                        Console.WriteLine(racun.DatumProdaje + " " + namestaj + " " + racun.BrojRacuna + " " + racun.Kupac + " " + usluge + " " + racun.UkupnaCena);
                        Console.WriteLine();

                    }
                    


                }
            }


        }






















    }

}
