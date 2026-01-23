using System;
using System.Text;


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
        private string cestaProdukty;

        public SpravcaProduktov(string cestaProdukty)
        {
            this.cestaProdukty = cestaProdukty;
        }

        internal List<Produkt> NacitajProdukty()
        {
            throw new NotImplementedException();
        }

        internal void UlozProdukty(List<Produkt> produkty)
        {
            throw new NotImplementedException();
        }
    }
}