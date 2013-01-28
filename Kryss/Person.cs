using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kryss
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Person(string FName, string lname)
        {
            FirstName = FName;
            LastName = lname;
        
        }

    }
}
