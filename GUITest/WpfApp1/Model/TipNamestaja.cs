using System;
using System.Collections.Generic;
using System.Text;

namespace POP_SF_10_2015.Model
{
    public class TipNamestaja
    {
        public int ID { get; set; }
        
        public bool Obrisan { get; set; }
        public string Naziv { get; set; }


        
        public static TipNamestaja GetById(int id)
        {
            foreach(var tipNamestaja in Projekat.Instance.TipoviNamestaja)
            {
                if(tipNamestaja.ID == id)
                {
                    return tipNamestaja;
                }
                
            }
            return null;

        }

        

    }
}
