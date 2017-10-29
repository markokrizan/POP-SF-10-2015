using System;
using System.Collections.Generic;
using System.Text;

namespace POP_SF_10_2015.Tests
{
    class KonzolnaAplikacija
    {
        static CitanjeIPisanje cp;
        



        public KonzolnaAplikacija()
        {
            cp = new CitanjeIPisanje();
            cp.UcitajKorisnike();
            cp.UcitajTipNamestaja();
            cp.UcitajNamestaj();
            cp.UcitajDodatneUsluge();
            LoginForma();
        }

        public  void LoginForma()
        {

            Console.WriteLine();
            Console.Write("Korisnicko ime: ");
            String KorIme = Console.ReadLine();
            Console.Write("Lozinka: ");
            String Lozinka = Console.ReadLine();

            bool log = cp.login(KorIme, Lozinka);


            while (log == false)
            {
                LoginForma();
                log = cp.login(KorIme, Lozinka);
            };

            if (log == true)
            {
                Console.WriteLine();
                PozdravniMeni();
            }
            else
            {
                LoginForma();
            }



        }







        public void PozdravniMeni()
        {
            Console.WriteLine();
            Console.WriteLine("Dobro dosli u salon namestaja!");
            Console.WriteLine();
            IspisGlavniMeni();
        }

        //isti za sve
        private static void IspisiCRUDMeni()
        {
            Console.WriteLine();
            Console.WriteLine("1. Prikazi ");
            Console.WriteLine("2. Dodaj");
            Console.WriteLine("3. Izmeni");
            Console.WriteLine("4. Obrisi");
            Console.WriteLine("5. Povratak na predhodni meni");
            Console.WriteLine();
            Console.Write("Izbor: ");



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
                    Console.WriteLine("3. Rad sa dodatnim uslugama");
                    Console.WriteLine("4. Rad sa prodajama");
                    Console.WriteLine("5. Rad sa akcijama");                  
                    Console.WriteLine("0. Izlaz iz aplikacije"); 
                    

                    Console.WriteLine();
                    Console.Write("Izbor: ");
                    izbor = int.Parse(Console.ReadLine());

                } while (izbor < 0 || izbor > 5);

                switch (izbor)
                {

                    case 0:
                        break;

                    case 1:
                        NamestajMeni();
                        break;
                    
                    case 2:
                        TipNamestajaMeni();
                        break;
                   
                    case 3:
                        DodatneUslugeMeni();
                        break;
                    
                    
                    case 4:
                        ProdajaMeni();
                        break;

                    /*

                    case 5:
                        DodatneUslugeMeni();
                        break;
                    */





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

                } while (izbor < 0 || izbor > 5);

                switch (izbor)
                {
                    case 1:
                        
                        cp.PrikaziNamestaj();
                        break;

                    case 2:
                        cp.DodajNamestaj();
                        break;

                    case 3:
                        cp.IzmeniNamestaj();
                        break;

                    case 4:
                        cp.ObrisiNamestaj();
                        break;

                    case 5:
                        IspisGlavniMeni();
                        break;

                    default:
                        break;

                }


            } while (izbor != 0);
        }

        private static void TipNamestajaMeni()
        {
            int izbor = 0;

            do
            {
                do
                {

                    Console.WriteLine("Rad sa tipom namestajem:");
                    IspisiCRUDMeni();
                    izbor = int.Parse(Console.ReadLine());

                } while (izbor < 0 || izbor > 5);

                switch (izbor)
                {
                    case 1:

                        cp.PrikaziTipoveNamestaja();
                        break;

                    case 2:
                        cp.DodajTipNamestaja();
                        break;

                    case 3:
                        cp.IzmeniTipNamstaja();
                        break;

                    case 4:
                        cp.ObrisiTipNamestaja();
                        break;

                    case 5:
                        IspisGlavniMeni();
                        break;

                    default:
                        break;

                }


            } while (izbor != 0);
        }

        private static void DodatneUslugeMeni()
        {
            int izbor = 0;

            do
            {
                do
                {

                    Console.WriteLine("Rad sa dodatnim uslugama:");
                    IspisiCRUDMeni();
                    izbor = int.Parse(Console.ReadLine());

                } while (izbor < 0 || izbor > 5);

                switch (izbor)
                {
                    case 1:

                        cp.PrikaziUsluge();
                        break;

                    case 2:
                        cp.DodajUslugu();
                        break;

                    case 3:
                        cp.IzmeniUslugu();
                        break;

                    case 4:
                        cp.ObrisiUslugu();
                        break;

                    case 5:
                        IspisGlavniMeni();
                        break;

                    default:
                        break;

                }


            } while (izbor != 0);
        }

        private static void ProdajaMeni()
        {
            int izbor = 0;

            do
            {
                do
                {

                    Console.WriteLine("Rad sa prodajama:");
                    //IspisiCRUDMeni();

                    Console.WriteLine("1. Prodaj namestaj");
                    Console.WriteLine("2. Na predhodni meni");
                    Console.WriteLine();
                    Console.Write("Izbor: ");
                    izbor = int.Parse(Console.ReadLine());

                } while (izbor < 0 || izbor > 5);

                switch (izbor)
                {
                    case 1:

                        cp.Prodaj();
                        break;

                       
                    case 2:
                        IspisGlavniMeni();
                        break;

                    /*
                    case 3:
                        cp.IzmeniUslugu();
                        break;

                    case 4:
                        cp.ObrisiUslugu();
                        break;

                    case 5:
                        IspisGlavniMeni();
                        break;

                    */
                    default:
                        break;

                }


            } while (izbor != 0);
        }
















































    }
}
