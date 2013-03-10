using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Controls;
//using System.Data.DataSetExtensions;

namespace KryssGenerator
{
    class RandomName : MainMenu
    {
        //Denna klass har hand om allt som rör själva slump funktionen.
        private static int choseItem = -1; // 
        public void Start(Databas db)
        {
            //Startar db ur databas
            DoRandom(db);
        }

        public int DoRandom(Databas db)
        {
            //Kollar i just nu kolumn 1 vilka av kryssrutorna som är i kryssade. 
            var markedStudentsQuery = from student in db.OurData.Tables["Namn"].AsEnumerable() where student.Field<bool>(valKolFromMain.ToString()) == true select student;
            
            //Antal kryssade uppgifter i en viss uppgift
            choseItem = markedStudentsQuery.Count();
            
            //Get out of here. It's null :O ! (aka Om det är 0 värde hoppas randomfunktionen)
            if (choseItem > 0)
            {
                //Startar random funktion
                Random random = new Random();

                //Väljer mellan alla i listboxen, som är kryssade
                int rnd = choseItem;

                //Randomar vilken den väljer i listan
                choseItem = random.Next(rnd);

                //Vad gjorde denna? Något om att hämta ID ur datatablen
                choseItem = markedStudentsQuery.ElementAt(choseItem).Field<int>("ID");
            }
            //Skickar vidare det slumpade talet
            return choseItem;
        }

        //Det slumpade värdet
        public static int finishComboPeople
        {
            get
            {
                return choseItem;
            }
            set
            {
                choseItem = value;
            }
        }
    }
}