using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kryss
{
    class Slump
    {
        // Denna klass har hand om allt som rör själva slump funktionen.

        public void KorSlump()
        {
            AntalDeltagare();

            AntalUppgifter();
        }

        private void AntalDeltagare()
        {
            throw new NotImplementedException();
        }

        private void AntalUppgifter()
        {
            throw new NotImplementedException();
        }
    }
}

           // //Startar random funktion
           //Random random = new Random();

           // //Väljer mellan alla i listboxen
           //int rnd = comboPeople.Items.Count;

           // //Randomar vilken den väljer i listan
           //int choseItem = random.Next(rnd);

           // //Visar den slumpmässiga deltagaren
           //comboPeople.SelectedIndex = choseItem;