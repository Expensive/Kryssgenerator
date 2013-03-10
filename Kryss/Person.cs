using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryssGenerator
{
    //Publik klass för att man ska kunna komma åt rätt tabellkolumn, bland annat när man ska ta bort användare
    public class Person
    {
        int ID;

        public int ID1
        {
            get { return ID; }
            set { ID = value; }
        }

        string Deltagare;

        public string Deltagare1
        {
            get { return Deltagare; }
            set { Deltagare = value; }
        }
    }
}
