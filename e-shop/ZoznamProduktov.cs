using System;
using System.Collections.Generic;
using System.Text;

namespace e_shop
{
    internal static class ZoznamProduktov
    {
        //public List<Tricko> Tricka { get; set; }
        //public List<Dziny> Dziny { get; set; }
        public static void Demo()
        {
            //Tricka = new List<Tricko>();
            Tricko t1 = new Tricko("Basic White", "Biela", "Bavlna", "Nike", 19.99, "M", true);
            Tricko t2 = new Tricko("Sport Black", "Čierna", "Polyester", "Adidas", 24.99, "L", true);
            Tricko t3 = new Tricko("Urban Style", "Modrá", "Bavlna", "Puma", 22.50, "S", false);
            Tricko t4 = new Tricko("Eco Green", "Zelená", "Bio bavlna", "H&M", 17.90, "M", true);
            Tricko t5 = new Tricko("Red Classic", "Červená", "Bavlna", "Levis", 29.99, "XL", true);
            Tricko t6 = new Tricko("Summer Vibes", "Žltá", "Ľan", "Zara", 21.00, "M", false);
            Tricko t7 = new Tricko("Street Grey", "Sivá", "Bavlna", "Supreme", 39.99, "L", true);

            t1.vypisInfo();
            t2.vypisInfo();
            t3.vypisInfo();
            t4.vypisInfo();
            t5.vypisInfo();
            t6.vypisInfo();
            t7.vypisInfo();

            //Dziny = new List<Dziny>();
            Dziny d1 = new Dziny("Classic Blue", "Modrá", "Denim", "Levis", 59.99, true);
            Dziny d2 = new Dziny("Slim Black", "Čierna", "Denim", "Zara", 39.99, true);
            Dziny d3 = new Dziny("Regular Grey", "Sivá", "Denim", "H&M", 34.99, false);
            Dziny d4 = new Dziny("Dark Style", "Tmavomodrá", "Denim", "Diesel", 79.99, true);
            Dziny d5 = new Dziny("Street Fit", "Modrá", "Denim", "Pull&Bear", 44.99, true);
            Dziny d6 = new Dziny("Light Summer", "Svetlomodrá", "Denim", "Reserved", 36.99, false);
            Dziny d7 = new Dziny("Urban Black", "Čierna", "Denim", "Calvin Klein", 89.99, true);

            d1.Vypis();
            d2.Vypis();
            d3.Vypis();
            d4.Vypis();
            d5.Vypis();
            d6.Vypis();
            d7.Vypis();














        }

    }
}



