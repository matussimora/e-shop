using System;
using System.Collections.Generic;
using e_shop;

namespace e_shop
{
    class Program
    {
    static List<Produkt> produkty = new List<Produkt>();
    static List<Pouzivatel> pouzivatelia = new List<Pouzivatel>();
    static Pouzivatel aktualnyPouzivatel = null;

    static void Main()
    {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(@"
  _  _              _         ___ _           _   _    _           
 | || |___ __ _ _ _| |_ ___  / __| |___  __ _| |_| |_ (_)_ _  __ _ 
 | __ / -_) _` | '_|  _|_ / | (__| / _ \/ _` |  _| ' \| | ' \/ _` |
 |_||_\___\__,_|_|  \__/__|  \___|_\___/\__,_|\__|_||_|_|_||_\__, |
                                                             |___/  ");

            while (true)
        {
            Console.WriteLine("\n1) Prehliadať produkty\n2) Zobraziť košík\n3) Prihlásiť/registrovať používateľa\n4) Kúpiť (vyčistiť košík)\n0) Koniec");
            Console.Write("Vyberte možnosť: ");
            var key = Console.ReadLine();
            switch (key)
            {
                case "1": ProduktMenu(); break;
                case "2": ShowCart(); break;
                case "3": UserMenu(); break;
                case "4": Purchase(); break;
                case "0": return;
                default: Console.WriteLine("Neplatná možnosť"); break;
            }
        }
    }

    static void InitSampleData()
    {
        produkty.Add(new Produkt("Tričko Basic White", 19.99, "Tričká", 10));
        produkty.Add(new Produkt("Tričko Sport Black", 24.99, "Tričká", 5));
        produkty.Add(new Produkt("Džíny Classic Blue", 59.99, "Nohavice", 3));
        produkty.Add(new Produkt("Mikina Grey", 39.50, "Mikiny", 7));
    }

    static void ProduktMenu()
    {
        while (true)
        {
            Console.WriteLine("\n1) Zobraziť všetky produkty\n2) Filtrovať podľa kategórie\n3) Hľadať podľa názvu\n4) Pridať do košíka\n0) Späť");
            Console.Write("Voľba: ");
            var v = Console.ReadLine();
            ''if (v == "0") return;
            if (v == "1") { ShowAllProducts(produkty); continue; }
            if (v == "2") { FilterByCategory(); continue; }
            if (v == "3") { SearchByName(); continue; }
            if (v == "4") { AddToCart(); continue; }
            Console.WriteLine("Neplatná voľba");
        }
    }

    static void ShowAllProducts(List<Produkt> list)
    {
            ZoznamProduktov.Demo();
        Console.WriteLine("\nProdukty:");
        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine($"{i+1}) {list[i].ToString()}");
        }
    }

    static void FilterByCategory()
    {
        Console.Write("Kategória: ");
        var cat = Console.ReadLine();
        var filtered = produkty.FindAll(p => p.Kategoria.Equals(cat, StringComparison.OrdinalIgnoreCase));
        ShowAllProducts(filtered);
    }

    static void SearchByName()
    {
        Console.Write("Hľadať názov: ");
        var q = Console.ReadLine();
        var found = produkty.FindAll(p => p.Nazov.Contains(q, StringComparison.OrdinalIgnoreCase));
        ShowAllProducts(found);
    }

    static void AddToCart()
    {
        if (!EnsureUser()) return;
        ShowAllProducts(produkty);
        Console.Write("Zadajte číslo produktu: ");
        if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > produkty.Count)
        {
            Console.WriteLine("Neplatné číslo");
            return;
        }
        var prod = produkty[idx-1];
        Console.Write("Množstvo: ");
        if (!int.TryParse(Console.ReadLine(), out int mnozstvo) || mnozstvo <= 0)
        {
            Console.WriteLine("Neplatné množstvo");
            return;
        }
        if (mnozstvo > prod.MnozstvoNaSklade)
        {
            Console.WriteLine("Nedostatok na sklade");
            return;
        }
        aktualnyPouzivatel.Kosik.PridajProdukt(prod, mnozstvo);
        Console.WriteLine("Pridané do košíka");
        prod.MnozstvoNaSklade -= mnozstvo;
    }

    static void ShowCart()
    {
        if (!EnsureUser()) return;
        aktualnyPouzivatel.Kosik.VypisKosik();
        Console.WriteLine("1) Odstrániť položku  2) Zmeniť množstvo  0) Späť");
        var c = Console.ReadLine();
        if (c == "1") {
            Console.Write("Názov produktu: ");
            var n = Console.ReadLine();
            
            var item = aktualnyPouzivatel.Kosik.NajdiPolozku(n);
            if (item != null) { item.Produkt.MnozstvoNaSklade += item.Mnozstvo; }
            aktualnyPouzivatel.Kosik.OdstranProdukt(n);
        }
        else if (c == "2") {
            Console.Write("Názov produktu: ");
            var n = Console.ReadLine();
            Console.Write("Nové množstvo: ");
            if (!int.TryParse(Console.ReadLine(), out int m)) return;
            var item = aktualnyPouzivatel.Kosik.NajdiPolozku(n);
            if (item != null)
            {
               
                int delta = m - item.Mnozstvo;
                if (delta > 0 && delta > item.Produkt.MnozstvoNaSklade)
                {
                    Console.WriteLine("Nedostatok na sklade");
                    return;
                }
                item.Produkt.MnozstvoNaSklade -= delta;
                aktualnyPouzivatel.Kosik.ZmenMnozstvo(n, m);
            }
        }
    }

    

    static void UserMenu()
    {
        Console.WriteLine("1) Registrovať  2) Prihlásiť  0) Späť");
        var c = Console.ReadLine();
        if (c == "1") {
            Console.Write("Meno: "); var m = Console.ReadLine();
            var u = new Pouzivatel(m);
            pouzivatelia.Add(u);
            aktualnyPouzivatel = u;
            Console.WriteLine("Registrované a prihlásené");
        }
        else if (c == "2") {
            Console.Write("Meno: "); var m = Console.ReadLine();
            var u = pouzivatelia.Find(x => x.Meno == m);
            if (u == null) Console.WriteLine("Používateľ neexistuje"); else { aktualnyPouzivatel = u; Console.WriteLine("Prihlásený"); }
        }
    }

    static bool EnsureUser()
    {
        if (aktualnyPouzivatel != null) return true;
        Console.WriteLine("Musíte byť prihlásený.");
        UserMenu();
        return aktualnyPouzivatel != null;
    }

    static void Purchase()
    {
        if (!EnsureUser()) return;
        var total = aktualnyPouzivatel.Kosik.CelkovaCena();
        Console.WriteLine($"Celková cena: {total:0.00} €");
        Console.WriteLine("Potvrdiť nákup? a/n");
        var a = Console.ReadLine();
        if (a?.ToLower() == "a")
        {
            aktualnyPouzivatel.Kosik.Vycistit();
            Console.WriteLine("Ďakujeme za nákup");
        }
    }
    }
}
