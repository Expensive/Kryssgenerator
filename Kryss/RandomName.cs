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
        // Denna klass har hand om allt som rör själva slump funktionen.
        private static int choseItem = -1; // 
        public void Start(Databas db)
        {
            DoRandom(db); //startar db ur databas
        }

        public int DoRandom(Databas db)
        {
            // Kollar i just nu kolumn 1 vilka av kryssrutorna som är i kryssade. 
            var markedStudentsQuery = from student in db.OurData.Tables["Namn"].AsEnumerable() where student.Field<bool>(valKolFromMain.ToString()) == true select student;
            choseItem =  markedStudentsQuery.Count();
            // Antal i kryssade uppgifter i en viss uppgift

            //Startar random funktion
            Random random = new Random();

            ////Väljer mellan alla i listboxen
            int rnd = choseItem;

            //Randomar vilken den väljer i listan
            choseItem = random.Next(rnd);

            // Vad gjorde denna? Något om att hämta ID ur datatablen
            choseItem = markedStudentsQuery.ElementAt(choseItem).Field<int>("ID");

            return choseItem; //skickar vidare det slumpade talet
        }
        public static int finishComboPeople // Det slumpade värdet
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