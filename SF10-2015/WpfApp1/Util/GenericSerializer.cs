using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace POP_SF_10_2015.Util
{
    public class GenericSerializer
    {

        //pretraga, upis, umesto u fajl u xml, sve sa tim

        //na xml serializeru
        //znaci cuvanje u xml-u

        //kao parametar za tip T, koji mora biti klasa
        //genericka metoda jer se moze koristiti za bilo koji tip
        //prima ime fajla i taj genericki T objekat

        //USING
        //kao paramatre using dajemo samo one promenjive koje hocemo da postoje samo u njegovom bloku
        //po zavrsetku ide dispose
        //za gasenje serijalajzera da ne bi doslo do nekonzistencije, da jedan proces koristi dok drugi ne moze

        //serialize - pisi
        //deserialize - citaj





  
        public static void Serialize<T>(string fileName, ObservableCollection<T> objToSerialize) where T : class
        {
            //try tab tab
            try
            {
                //ocekuje tip sa kojim radi

                var serializer = new XmlSerializer(typeof(ObservableCollection<T>));


                //serializacija - razbijanje podataka na stream 0 i 1
                //   ../../ da se vratim 2 foldera gore

                //Stream Writer je taj koji zauzima resurse, pa je on kandidat za using
                using (var sw = new StreamWriter($@"../../Data/{fileName}"))
                {
                    serializer.Serialize(sw, objToSerialize);
                }





            }
            catch (Exception)
            {

                throw;
            }
        }

        public static ObservableCollection<T> Deserialize<T>(string fileName) where T : class
        {
            //try tab tab
            try
            {
                //ocekuje tip sa kojim radi

                var serializer = new XmlSerializer(typeof(ObservableCollection<T>));


                //serializacija - razbijanje podataka na stream 0 i 1
                //   ../../ da se vratim 2 foldera gore

                //Stream Writer je taj koji zauzima resurse, pa je on kandidat za using
                //naziv parametra: vrednost
                using (var sw = new StreamReader($@"../../Data/{fileName}"))
                {
                    return (ObservableCollection<T>)serializer.Deserialize(sw);
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
