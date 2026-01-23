using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;

namespace HeartzClothing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Heartz Clothing";
             
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║            HEARTZ CLOTHING          ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("          ♥ Streetwear Brand ♥");
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine(" Zadaj cestu k suboru s produktmi.");
            Console.WriteLine(" Ak subor neexistuje, vytvori sa a doplnia sa produkty.");
            Console.WriteLine();

            Console.Write(" Cesta k suboru produktov (napr. C:\\data\\heartz_products.txt): ");
            string cestaProdukty = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine(" Zadaj cestu k suboru s pouzivatelmi.");
            Console.WriteLine(" Ak subor neexistuje, vytvori sa a prida sa admin/admin.");
            Console.WriteLine();

            Console.Write(" Cesta k suboru pouzivatelov (napr. C:\\data\\heartz_users.txt): ");
            string cestaPouzivatelia = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(cestaProdukty) || string.IsNullOrWhiteSpace(cestaPouzivatelia))
            {
                Console.WriteLine();
                Console.WriteLine(" Cesty nemozu byt prazdne. Stlac Enter na koniec.");
                Console.ReadLine();
                return;
            }

            SpravcaProduktov spravcaProduktov = new SpravcaProduktov(cestaProdukty);
            SpravcaPouzivatelov spravcaPouzivatelov = new SpravcaPouzivatelov(cestaPouzivatelia);

            ObchodHeartz obchod = new ObchodHeartz(spravcaProduktov, spravcaPouzivatelov);
            obchod.Spusti();
        }
    }

    internal class SpravcaProduktov
    {
        private readonly string cestaProdukty;

        public SpravcaProduktov(string cestaProdukty)
        {
            this.cestaProdukty = cestaProdukty;
        }

        public List<Produkt> NacitajProdukty()
        {
            var produkty = new List<Produkt>();

            if (!File.Exists(cestaProdukty))
            {
                return produkty;
            }

            string[] riadky = File.ReadAllLines(cestaProdukty, Encoding.UTF8);

            foreach (string riadok in riadky)
            {
                if (string.IsNullOrWhiteSpace(riadok))
                {
                    continue;
                }

                string[] casti = riadok.Split(';');

                // Support two formats:
                // 1) Legacy: id;nazov;kategoria;cena;mnozstvo
                // 2) Extended (OblecenieProdukt): id;nazov;kategoria;cena;mnozstvo;farba;velkost
                if (casti.Length < 5)
                {
                    continue;
                }

                if (!int.TryParse(casti[0], out int id))
                {
                    continue;
                }

                string nazov = casti[1];
                string kategoria = casti[2];

                if (!decimal.TryParse(casti[3], NumberStyles.Number, CultureInfo.InvariantCulture, out decimal cena))
                {
                    continue;
                }

                if (!int.TryParse(casti[4], out int mnozstvoNaSklade))
                {
                    continue;
                }

                if (casti.Length >= 7)
                {
                    // OblecenieProdukt with color and size
                    var op = new OblecenieProdukt
                    {
                        Id = id,
                        Nazov = nazov,
                        Kategoria = kategoria,
                        Cena = cena,
                        MnozstvoNaSklade = mnozstvoNaSklade,
                        Farba = casti[5],
                        Velkost = casti[6]
                    };
                    produkty.Add(op);
                }
                else
                {
                    var p = new Produkt
                    {
                        Id = id,
                        Nazov = nazov,
                        Kategoria = kategoria,
                        Cena = cena,
                        MnozstvoNaSklade = mnozstvoNaSklade
                    };
                    produkty.Add(p);
                }
            }

            return produkty;
        }

        public void UlozProdukty(List<Produkt> produkty)
        {
            var riadky = new List<string>(produkty.Count);

            foreach (var p in produkty)
            {
                string cena = p.Cena.ToString(CultureInfo.InvariantCulture);

                if (p is OblecenieProdukt op)
                {
                    // Save extended format with color and size
                    riadky.Add($"{op.Id};{op.Nazov};{op.Kategoria};{cena};{op.MnozstvoNaSklade};{op.Farba};{op.Velkost}");
                }
                else
                {
                    riadky.Add($"{p.Id};{p.Nazov};{p.Kategoria};{cena};{p.MnozstvoNaSklade}");
                }
            }

            // Ensure directory creation handles empty or rootless paths
            string dir = Path.GetDirectoryName(cestaProdukty);
            if (string.IsNullOrWhiteSpace(dir))
            {
                dir = ".";
            }

            Directory.CreateDirectory(dir);
            File.WriteAllLines(cestaProdukty, riadky, Encoding.UTF8);
        
    }
}