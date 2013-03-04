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

        //public void Start(IEnumerable db) {
        //    DoRandom(db); //startar db ur databas
        //}
        //private int DoRandom(IEnumerable db)
        //{
        //    //comboPeople = db.OurData.Tables["Namn"].Rows.Count; //Räknar antal deltagare
        //    var markedStudentsQuery = from DataRow student in ((DataView)db).Table.AsEnumerable() where student.Field<bool>("1") == true select student;
        //    comboPeople = markedStudentsQuery.Count();
        //    // Antal i kryssade uppgifter i en viss uppgift

        //    //Startar random funktion
        //    Random random = new Random();

        //    ////Väljer mellan alla i listboxen
        //    int rnd = comboPeople;

        //    //Randomar vilken den väljer i listan
        //    int choseItem = random.Next(rnd);

        //    //Visar den slumpmässiga deltagaren
        //    comboPeople = choseItem;

        //    return comboPeople; //skickar vidare det slumpade talet
        //}


        private int DoRandom(Databas db)
        {
            //comboPeople = db.OurData.Tables["Namn"].Rows.Count; //Räknar antal deltagare

            // Kollar i just nu kolumn 1 vilka av kryssrutorna som är i kryssade. 
            var markedStudentsQuery = from student in db.OurData.Tables["Namn"].AsEnumerable() where student.Field<bool>("1") == true select student;
            markedStudentsQuery.ToArray();
            // Antal i kryssade uppgifter i en viss uppgift

            //Startar random funktion
            Random random = new Random();

            ////Väljer mellan alla i listboxen
            int rnd = choseItem;

            //Randomar vilken den väljer i listan
            choseItem = random.Next(rnd);

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