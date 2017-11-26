using System;
using System.Collections.Generic;
using System.Text;

namespace POP_SF_10_2015.Model
{
    public class Salon
    {
        private string naziv;
        private string adresa;
        private string telefon;
        private string email;
        private string pib;
        private string maticnibroj;
        private string brojziroracuna;
        private string sajt;

        public string Naziv {
           
            set
            {
                this.naziv = value;
                
            }
            get
            {
                return naziv;
            }
        }
        public string Adresa {
            get
            {
                return adresa;
            }
            set
            {
                this.adresa = value;
            }
        }
        public string Telefon {
            get
            {
                return telefon;
            }
            set
            {
                this.telefon = value;
            }
        }
        public string EMail {
            get
            {
                return email;
            }
            set
            {
                this.email = value;
            }
        }
        public string PIB {
            get
            {
                return pib;
            }
            set
            {
                this.pib = value;
            }
        }
        public string  MaticniBroj {
            get
            {
                return maticnibroj;
            }
            set
            {
                this.maticnibroj = value;

            }
        }
        public string BrojZiroRacuna {
            get
            {
                return brojziroracuna;
            }
            set
            {
                this.brojziroracuna = value;
            }
        }
        public string Sajt {
            get
            {
                return sajt;
            }
            set
            {
                this.sajt = value;
            }
        }

        public Salon()
        {
            this.naziv = "Jugodrvo";
            this.adresa = "Puskinova 1";
            this.telefon = "021/111/222";
            this.email = "jugosalon@biz.rs";
            this.pib = "685798767";
            this.maticnibroj = "00556745";
            this.brojziroracuna = "320-000566677465-88";
            this.sajt = "www.jugosalon.rs";
        }



    }
}
