using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HeartzClothing
{
    class SpravcaPouzivatelov
    {
        private readonly string cestaSuboru;

        public SpravcaPouzivatelov(string cestaSuboru)
        {
            this.cestaSuboru = cestaSuboru;
        }

        public List<Pouzivatel> NacitajPouzivatelov()
        {
            List<Pouzivatel> pouzivatelia = new List<Pouzivatel>();

            if (!File.Exists(cestaSuboru))
            {
                return pouzivatelia;
            }

            string[] riadky = File.ReadAllLines(cestaSuboru, Encoding.UTF8);

            foreach (string riadok in riadky)
            {
                if (string.IsNullOrWhiteSpace(riadok))
                {
                    continue;
                }

                string[] casti = riadok.Split(';');
                if (casti.Length != 3)
                {
                    continue;
                }

                string meno = casti[0];
                string heslo = casti[1];
                string rola = casti[2];

                Pouzivatel p;

                if (rola == "Admin")
                {
                    p = new AdminPouzivatel();
                }
                else
                {
                    p = new Pouzivatel();
                }

                p.PrihlasovacieMeno = meno;
                p.Heslo = heslo;

                pouzivatelia.Add(p);
            }

            return pouzivatelia;
        }

        public void UlozPouzivatelov(List<Pouzivatel> pouzivatelia)
        {
            List<string> riadky = new List<string>();

            foreach (Pouzivatel p in pouzivatelia)
            {
                string riadok = p.PrihlasovacieMeno + ";" + p.Heslo + ";" + p.Rola;
                riadky.Add(riadok);
            }

            File.WriteAllLines(cestaSuboru, riadky, Encoding.UTF8);
        }
    }
}
