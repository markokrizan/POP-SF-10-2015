using System;
using System.Collections.Generic;
using System.Text;

namespace POP_SF_10_2015.Model
{
    public class Namestaj
    {
    public int ID { get; set; }
    public bool Obrisan { get; set; }
    public string Naziv { get; set; }
    public string Sifra { get; set; }
    public double Cena { get; set; }
    public int KolicinaUMagacinu { get; set; }
    public TipNamestaja TipNamestaja { get; set; }
    public Akcija Akcija { get; set; }

    }
}
