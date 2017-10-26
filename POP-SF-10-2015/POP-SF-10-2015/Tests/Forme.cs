using System;
using System.Collections.Generic;
using System.Text;

namespace POP_SF_10_2015.Tests
{
    class Forme
    {
        public static String uname;
        public static  String pass;
        public static String izbor;

        public static void LoginForma()
        {
            
            Console.WriteLine();
            Console.Write("Korisnicko ime: ");   
            String korIme = Console.ReadLine();
            Console.Write("Lozinka: ");
            String Lozinka = Console.ReadLine();

            uname = korIme;
            pass = Lozinka;


            
        }

        public static void Meni()
        {
            Console.WriteLine();
            Console.WriteLine("Izaberi opciju: ");
            Console.WriteLine();
            Console.WriteLine("1.Prikazi namestaj: ");
            Console.WriteLine("2.Prikazi korisnike: ");

            Console.Write("Opcija: ");
            String i = Console.ReadLine();

            izbor = i;

        }

        public static void PrikaziNamestaj()
        {

        }










    }
}
