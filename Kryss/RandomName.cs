using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryssGenerator
{
    class RandomName : MainMenu
    {
        // Denna klass har hand om allt som rör själva slump funktionen.
        private static int comboPeople = -1; // 
        public void Start(Databas db)
        {
            DoRandom(db); //startar db ur databas
        }

        private int DoRandom(Databas db)
        {
            comboPeople = db.OurData.Tables["Namn"].Rows.Count; //Räknar antal deltagare

            //Startar random funktion
            Random random = new Random();

            ////Väljer mellan alla i listboxen
            int rnd = comboPeople;

            //Randomar vilken den väljer i listan
            int choseItem = random.Next(rnd);

            //Visar den slumpmässiga deltagaren
            comboPeople = choseItem;

            return comboPeople; //skickar vidare det slumpade talet
        }
        public static int finishComboPeople // Det slumpade värdet
        {
            get
            {
                return comboPeople;
            }
            set
            {
                comboPeople = value;
            }
        }
    }
}