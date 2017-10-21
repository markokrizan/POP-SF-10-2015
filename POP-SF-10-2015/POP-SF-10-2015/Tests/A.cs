using System;
using System.Collections.Generic;
using System.Text;

namespace POP_SF_10_2015.Tests
{
    public class A
    //sam dodajes modifikator pristupa
    {
        private string ime = "";
        //ako se redefinisu geteri i seteri mora ovako kao u javi da se poziva properti

        //public string GetIme()
        //{
        //    return this.ime;
        //}

        //public void SetIme(string ime)
        //{
        //    this.ime = ime;
        //}


        //public string Ime{get;set;}

        //ustvari metoda:
        public string Ime {
            get
            {
                Console.WriteLine(this.ime);
                return this.ime;
            }
            set
            {
                this.ime = value;
                //umesto this.ime = ime jer nema parametara
            }
        }
        //cs nacin getera i setera
    }
}
