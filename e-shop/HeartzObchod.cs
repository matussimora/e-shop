using System;
using System.Collections.Generic;
using System.Linq;

namespace HeartzClothing;    

    class ObchodHeartz
    {
        private SpravcaProduktov spravcaProduktov;
        private SpravcaPouzivatelov spravcaPouzivatelov;

        private List<Produkt> produkty;
        private List<Pouzivatel> pouzivatelia;

        private Kosik kosik;
        private Pouzivatel prihlasenyPouzivatel;

        public ObchodHeartz(SpravcaProduktov spravcaProduktov, SpravcaPouzivatelov spravcaPouzivatelov)
        {
            this.spravcaProduktov = spravcaProduktov;
            this.spravcaPouzivatelov = spravcaPouzivatelov;

            this.produkty = new List<Produkt>();
            this.pouzivatelia = new List<Pouzivatel>();
            this.kosik = new Kosik();
            this.prihlasenyPouzivatel = null;
        }

        public void Spusti()
        {
            NacitajAleboVytvorProdukty();
            NacitajAleboVytvorPouzivatelov();
            PrihlasenieRegistraciaMenu();

            string volba = "";

            do
            {
                Console.Clear();
                ZobrazNadpis("HEARTZ CLOTHING - HLAVNE MENU");

                if (prihlasenyPouzivatel != null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(" Prihlaseny: " + prihlasenyPouzivatel.ToString());
                    Console.ResetColor();
                    Console.WriteLine();
                }

                ZobrazMenuRad();

                Console.Write(" Tvoja volba: ");
                volba = Console.ReadLine();

                if (volba == "1")
                {
                    ZobrazVsetkyProdukty();
                    CakajNaEnter();
                }
                else if (volba == "2")
                {
                    FiltrovatPodlaKategorie();
                    CakajNaEnter();
                }
                else if (volba == "3")
                {
                    VyhladatPodlaNazvu();
                    CakajNaEnter();
                }
                else if (volba == "4")
                {
                    MenuKosika();
                }
                else if (volba == "5")
                {
                    UlozProdukty();
                    UlozPouzivatelov();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(" ♥ Dakujeme za pouzitie Heartz Clothing ♥");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(" Neplatna volba.");
                    CakajNaEnter();
                }

            } while (volba != "5");
        }

        private void ZobrazNadpis(string text)
        {
            int sirka = 42;
            string obsah = " " + text + " ";
            if (obsah.Length > sirka - 2)
            {
                obsah = obsah.Substring(0, sirka - 4) + ".. ";
            }

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("╔" + new string('═', sirka) + "╗");

            int medzera = sirka - obsah.Length;
            if (medzera < 0) medzera = 0;
            int lave = medzera / 2;
            int prave = medzera - lave;

            Console.Write("║");
            Console.Write(new string(' ', lave));
            Console.Write(obsah);
            Console.Write(new string(' ', prave));
            Console.WriteLine("║");

            Console.WriteLine("╚" + new string('═', sirka) + "╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        private void ZobrazMenuRad()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" ┌───────────────────────────────────┐");
            Console.WriteLine(" │ 1 │ Zobrazit vsetky produkty     │");
            Console.WriteLine(" │ 2 │ Filtrovat podla kategorie    │");
            Console.WriteLine(" │ 3 │ Vyhladat podla nazvu         │");
            Console.WriteLine(" │ 4 │ Kosik                         │");
            Console.WriteLine(" │ 5 │ Ulozit a skoncit             │");
            Console.WriteLine(" └───────────────────────────────────┘");
            Console.ResetColor();
            Console.WriteLine();
        }

        private void CakajNaEnter()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" Stlac Enter pre pokracovanie...");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void NacitajAleboVytvorProdukty()
        {
            List<Produkt> nacitane = spravcaProduktov.NacitajProdukty();

            if (nacitane.Count == 0)
            {
                VytvorUkazkoveProdukty();
                UlozProdukty();
            }
            else
            {
                produkty = nacitane;
            }
        }

        private void UlozProdukty()
        {
            spravcaProduktov.UlozProdukty(produkty);
        }

        private void VytvorUkazkoveProdukty()
        {
            produkty = new List<Produkt>
            {
                // 5 T-shirts
                new OblecenieProdukt { Id = 1,  Nazov = "Heartz Basic T-Shirt",   Kategoria = "Tricka", Cena = 19.99m, MnozstvoNaSklade = 25, Farba = "Black", Velkost = "M" },
                new OblecenieProdukt { Id = 2,  Nazov = "Heartz Classic Tee",    Kategoria = "Tricka", Cena = 18.50m, MnozstvoNaSklade = 20, Farba = "White", Velkost = "L" },
                new OblecenieProdukt { Id = 3,  Nazov = "Heartz Logo Tee",       Kategoria = "Tricka", Cena = 21.00m, MnozstvoNaSklade = 15, Farba = "Grey", Velkost = "S" },
                new OblecenieProdukt { Id = 4,  Nazov = "Heartz Oversized Tee",  Kategoria = "Tricka", Cena = 22.99m, MnozstvoNaSklade = 12, Farba = "Olive", Velkost = "XL" },
                new OblecenieProdukt { Id = 5,  Nazov = "Heartz Pocket Tee",     Kategoria = "Tricka", Cena = 20.00m, MnozstvoNaSklade = 18, Farba = "Navy", Velkost = "M" },

                // 5 Sneakers
                new OblecenieProdukt { Id = 6,  Nazov = "Heartz Runner",         Kategoria = "Topanky", Cena = 69.99m, MnozstvoNaSklade = 10, Farba = "White", Velkost = "42" },
                new OblecenieProdukt { Id = 7,  Nazov = "Heartz Street Sneak",   Kategoria = "Topanky", Cena = 74.99m, MnozstvoNaSklade = 8,  Farba = "Black", Velkost = "43" },
                new OblecenieProdukt { Id = 8,  Nazov = "Heartz Retro Low",      Kategoria = "Topanky", Cena = 64.50m, MnozstvoNaSklade = 14, Farba = "Beige", Velkost = "41" },
                new OblecenieProdukt { Id = 9,  Nazov = "Heartz High Top",       Kategoria = "Topanky", Cena = 79.99m, MnozstvoNaSklade = 6,  Farba = "Red", Velkost = "44" },
                new OblecenieProdukt { Id = 10, Nazov = "Heartz Knit Slip-On",   Kategoria = "Topanky", Cena = 59.99m, MnozstvoNaSklade = 12, Farba = "Grey", Velkost = "42" },

                // 5 Hoodies
                new OblecenieProdukt { Id = 11, Nazov = "Heartz Oversize Hoodie", Kategoria = "Mikiny", Cena = 49.99m, MnozstvoNaSklade = 10, Farba = "Black", Velkost = "L" },
                new OblecenieProdukt { Id = 12, Nazov = "Heartz Zip Hoodie",      Kategoria = "Mikiny", Cena = 54.99m, MnozstvoNaSklade = 8,  Farba = "White", Velkost = "M" },
                new OblecenieProdukt { Id = 13, Nazov = "Heartz Pullover",        Kategoria = "Mikiny", Cena = 47.50m, MnozstvoNaSklade = 9,  Farba = "Olive", Velkost = "XL" },
                new OblecenieProdukt { Id = 14, Nazov = "Heartz Fleece Hoodie",   Kategoria = "Mikiny", Cena = 52.00m, MnozstvoNaSklade = 7,  Farba = "Grey", Velkost = "S" },
                new OblecenieProdukt { Id = 15, Nazov = "Heartz Logo Hoodie",     Kategoria = "Mikiny", Cena = 49.99m, MnozstvoNaSklade = 11, Farba = "Navy", Velkost = "M" },

                // 5 Jackets
                new OblecenieProdukt { Id = 16, Nazov = "Heartz Windbreaker",     Kategoria = "Bundy", Cena = 69.99m, MnozstvoNaSklade = 6,  Farba = "Black", Velkost = "M" },
                new OblecenieProdukt { Id = 17, Nazov = "Heartz Denim Jacket",    Kategoria = "Bundy", Cena = 79.99m, MnozstvoNaSklade = 5,  Farba = "Blue", Velkost = "L" },
                new OblecenieProdukt { Id = 18, Nazov = "Heartz Puffer Jacket",   Kategoria = "Bundy", Cena = 89.99m, MnozstvoNaSklade = 4,  Farba = "Olive", Velkost = "XL" },
                new OblecenieProdukt { Id = 19, Nazov = "Heartz Coach Jacket",    Kategoria = "Bundy", Cena = 64.99m, MnozstvoNaSklade = 7,  Farba = "Red", Velkost = "M" },
                new OblecenieProdukt { Id = 20, Nazov = "Heartz Leather Look",    Kategoria = "Bundy", Cena = 99.99m, MnozstvoNaSklade = 3,  Farba = "Black", Velkost = "L" }
            };
        }

        private void NacitajAleboVytvorPouzivatelov()
        {
            List<Pouzivatel> nacitani = spravcaPouzivatelov.NacitajPouzivatelov();

            if (nacitani.Count == 0)
            {
                AdminPouzivatel admin = new AdminPouzivatel();
                admin.PrihlasovacieMeno = "admin";
                admin.Heslo = "admin";

                pouzivatelia = new List<Pouzivatel>();
                pouzivatelia.Add(admin);

                UlozPouzivatelov();
            }
            else
            {
                pouzivatelia = nacitani;
            }
        }

        private void UlozPouzivatelov()
        {
            spravcaPouzivatelov.UlozPouzivatelov(pouzivatelia);
        }

        private void PrihlasenieRegistraciaMenu()
        {
            string volba = "";

            while (prihlasenyPouzivatel == null)
            {
                Console.Clear();
                ZobrazNadpis("HEARTZ CLOTHING - PRIHLASENIE");

                Console.WriteLine(" 1 - Prihlasit sa");
                Console.WriteLine(" 2 - Registrovat noveho pouzivatela");
                Console.WriteLine(" 3 - Skoncit");
                Console.WriteLine();
                Console.Write(" Tvoja volba: ");
                volba = Console.ReadLine();

                if (volba == "1")
                {
                    PrihlasitSa();
                    if (prihlasenyPouzivatel == null)
                    {
                        CakajNaEnter();
                    }
                }
                else if (volba == "2")
                {
                    RegistrovatPouzivatela();
                    CakajNaEnter();
                }
                else if (volba == "3")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine(" Neplatna volba.");
                    CakajNaEnter();
                }
            }
        }

        private void PrihlasitSa()
        {
            Console.Write(" Zadaj prihlasovacie meno: ");
            string meno = Console.ReadLine();

            Console.Write(" Zadaj heslo: ");
            string heslo = Console.ReadLine();

            Pouzivatel najdeny = null;

            foreach (Pouzivatel p in pouzivatelia)
            {
                if (p.PrihlasovacieMeno == meno && p.Heslo == heslo)
                {
                    najdeny = p;
                    break;
                }
            }

            if (najdeny == null)
            {
                Console.WriteLine(" Nespravne meno alebo heslo.");
            }
            else
            {
                prihlasenyPouzivatel = najdeny;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" Vitaj, " + prihlasenyPouzivatel.PrihlasovacieMeno + "!");
                Console.ResetColor();
            }
        }

        private void RegistrovatPouzivatela()
        {
            Console.Write(" Zadaj nove prihlasovacie meno: ");
            string meno = Console.ReadLine();

            foreach (Pouzivatel p in pouzivatelia)
            {
                if (p.PrihlasovacieMeno == meno)
                {
                    Console.WriteLine(" Toto meno uz existuje.");
                    return;
                }
            }

            Console.Write(" Zadaj heslo: ");
            string heslo = Console.ReadLine();

            Pouzivatel novy = new Pouzivatel();
            novy.PrihlasovacieMeno = meno;
            novy.Heslo = heslo;

            pouzivatelia.Add(novy);
            UlozPouzivatelov();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Pouzivatel zaregistrovany.");
            Console.ResetColor();
        }

        private void ZobrazVsetkyProdukty()
        {
            Console.Clear();
            ZobrazNadpis("HEARTZ CLOTHING - PRODUKTY");

            List<Produkt> zoradene = produkty
                .OrderBy(p => p.Nazov)
                .ToList();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" ID   NAZOV");
            Console.WriteLine(" ----------------------------------------");
            Console.ResetColor();

            foreach (Produkt p in zoradene)
            {
                Console.WriteLine(" " + p.ToString());
            }
        }

        private void FiltrovatPodlaKategorie()
        {
            Console.Clear();
            ZobrazNadpis("FILTROVANIE PODLA KATEGORIE");
            Console.Write(" Zadaj nazov kategorie (Mikiny, Tricka, Nohavice, Doplnky): ");
            string kategoria = Console.ReadLine();

            List<Produkt> vysledky = new List<Produkt>();

            foreach (Produkt p in produkty)
            {
                if (!string.IsNullOrEmpty(p.Kategoria))
                {
                    string kat = p.Kategoria.ToLower();
                    string hladana = kategoria.ToLower();

                    if (kat.Contains(hladana))
                    {
                        vysledky.Add(p);
                    }
                }
            }

            Console.WriteLine();

            if (vysledky.Count == 0)
            {
                Console.WriteLine(" Nenasli sa ziadne produkty v tejto kategorii.");
            }
            else
            {
                foreach (Produkt p in vysledky)
                {
                    Console.WriteLine(" " + p.ToString());
                }
            }
        }

        private void VyhladatPodlaNazvu()
        {
            Console.Clear();
            ZobrazNadpis("VYHLADAVANIE PODLA NAZVU");
            Console.Write(" Zadaj cast nazvu produktu: ");
            string castNazvu = Console.ReadLine();

            List<Produkt> vysledky = new List<Produkt>();

            foreach (Produkt p in produkty)
            {
                if (!string.IsNullOrEmpty(p.Nazov))
                {
                    string nazov = p.Nazov.ToLower();
                    string hladane = castNazvu.ToLower();

                    if (nazov.Contains(hladane))
                    {
                        vysledky.Add(p);
                    }
                }
            }

            Console.WriteLine();

            if (vysledky.Count == 0)
            {
                Console.WriteLine(" Nenasli sa ziadne produkty.");
            }
            else
            {
                foreach (Produkt p in vysledky)
                {
                    Console.WriteLine(" " + p.ToString());
                }
            }
        }

        private void MenuKosika()
        {
            string volba = "";

            do
            {
                Console.Clear();
                ZobrazNadpis("KOSIK - HEARTZ CLOTHING");
                ZobrazKosik();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(" ┌───────────────────────────────────┐");
                Console.WriteLine(" │ 1 │ Pridat produkt do kosika     │");
                Console.WriteLine(" │ 2 │ Odstranit produkt z kosika   │");
                Console.WriteLine(" │ 3 │ Zmenit mnozstvo v kosiku     │");
                Console.WriteLine(" │ 4 │ Pokladna                     │");
                Console.WriteLine(" │ 5 │ Spat do hlavneho menu        │");
                Console.WriteLine(" └───────────────────────────────────┘");
                Console.ResetColor();
                Console.WriteLine();
                Console.Write(" Tvoja volba: ");
                volba = Console.ReadLine();

                if (volba == "1")
                {
                    PridajDoKosika();
                    CakajNaEnter();
                }
                else if (volba == "2")
                {
                    OdstranZKosika();
                    CakajNaEnter();
                }
                else if (volba == "3")
                {
                    ZmenMnozstvoVKosiku();
                    CakajNaEnter();
                }
                else if (volba == "4")
                {
                    Pokladna();
                    CakajNaEnter();
                }
                else if (volba == "5")
                {
                    return;
                }
                else
                {
                    Console.WriteLine(" Neplatna volba.");
                    CakajNaEnter();
                }

            } while (true);
        }

        private void ZobrazKosik()
        {
            if (kosik.Polozky.Count == 0)
            {
                Console.WriteLine(" Kosik je prazdny.");
                return;
            }

            foreach (PolozkaKosika polozka in kosik.Polozky)
            {
                Console.WriteLine(
                    " " +
                    polozka.Produkt.Nazov +
                    " | " +
                    polozka.Mnozstvo +
                    " ks | spolu: " +
                    polozka.CelkovaCena +
                    " €"
                );
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" ----------------------------------------");
            Console.WriteLine(" ♥ CELKOM: " + kosik.VratCelkovaCena() + " €");
            Console.WriteLine(" ----------------------------------------");
            Console.ResetColor();
        }

        private Produkt NajdiProduktPodlaId(int id)
        {
            foreach (Produkt p in produkty)
            {
                if (p.Id == id)
                {
                    return p;
                }
            }

            return null;
        }

        private PolozkaKosika NajdiPolozkuVKosikuPodlaId(int id)
        {
            foreach (PolozkaKosika polozka in kosik.Polozky)
            {
                if (polozka.Produkt.Id == id)
                {
                    return polozka;
                }
            }

            return null;
        }

        private void PridajDoKosika()
        {
            ZobrazVsetkyProdukty();
            Console.WriteLine();
            Console.Write(" Zadaj ID produktu, ktory chces pridat: ");
            string vstupId = Console.ReadLine();

            int id;
            bool idOk = int.TryParse(vstupId, out id);
            if (!idOk)
            {
                Console.WriteLine(" Neplatne ID.");
                return;
            }

            Produkt produkt = NajdiProduktPodlaId(id);
            if (produkt == null)
            {
                Console.WriteLine(" Produkt s tymto ID neexistuje.");
                return;
            }

            Console.Write(" Zadaj mnozstvo (max " + produkt.MnozstvoNaSklade + "): ");
            string vstupMnozstvo = Console.ReadLine();

            int mnozstvo;
            bool mnozstvoOk = int.TryParse(vstupMnozstvo, out mnozstvo);
            if (!mnozstvoOk)
            {
                Console.WriteLine(" Neplatne mnozstvo.");
                return;
            }

            if (mnozstvo <= 0)
            {
                Console.WriteLine(" Mnozstvo musi byt vacsie ako 0.");
                return;
            }

            if (mnozstvo > produkt.MnozstvoNaSklade)
            {
                Console.WriteLine(" Na sklade nie je tolko kusov.");
                return;
            }

            produkt.MnozstvoNaSklade = produkt.MnozstvoNaSklade - mnozstvo;
            kosik.PridajProdukt(produkt, mnozstvo);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Produkt pridany do kosika.");
            Console.ResetColor();
        }

        private void OdstranZKosika()
        {
            if (kosik.Polozky.Count == 0)
            {
                Console.WriteLine(" Kosik je prazdny.");
                return;
            }

            Console.Write(" Zadaj ID produktu, ktory chces odstranit z kosika: ");
            string vstupId = Console.ReadLine();

            int id;
            bool idOk = int.TryParse(vstupId, out id);
            if (!idOk)
            {
                Console.WriteLine(" Neplatne ID.");
                return;
            }

            PolozkaKosika polozka = NajdiPolozkuVKosikuPodlaId(id);
            if (polozka == null)
            {
                Console.WriteLine(" Tento produkt nie je v kosiku.");
                return;
            }

            polozka.Produkt.MnozstvoNaSklade =
                polozka.Produkt.MnozstvoNaSklade + polozka.Mnozstvo;

            kosik.OdstranProdukt(id);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" Produkt bol odstraneny z kosika.");
            Console.ResetColor();
        }

        private void ZmenMnozstvoVKosiku()
        {
            if (kosik.Polozky.Count == 0)
            {
                Console.WriteLine(" Kosik je prazdny.");
                return;
            }

            Console.Write(" Zadaj ID produktu, ktoremu chces zmenit mnozstvo: ");
            string vstupId = Console.ReadLine();

            int id;
            bool idOk = int.TryParse(vstupId, out id);
            if (!idOk)
            {
                Console.WriteLine(" Neplatne ID.");
                return;
            }

            PolozkaKosika polozka = NajdiPolozkuVKosikuPodlaId(id);
            if (polozka == null)
            {
                Console.WriteLine(" Tento produkt nie je v kosiku.");
                return;
            }

            Console.Write(" Aktualne mnozstvo je " + polozka.Mnozstvo + ". Zadaj nove mnozstvo: ");
            string vstupMnozstvo = Console.ReadLine();

            int noveMnozstvo;
            bool mnozstvoOk = int.TryParse(vstupMnozstvo, out noveMnozstvo);
            if (!mnozstvoOk)
            {
                Console.WriteLine(" Neplatne mnozstvo.");
                return;
            }

            if (noveMnozstvo < 0)
            {
                Console.WriteLine(" Mnozstvo nemoze byt zaporne.");
                return;
            }

            int rozdiel = noveMnozstvo - polozka.Mnozstvo;

            if (rozdiel > 0)
            {
                if (rozdiel > polozka.Produkt.MnozstvoNaSklade)
                {
                    Console.WriteLine(" Na sklade nie je tolko kusov.");
                    return;
                }

                polozka.Produkt.MnozstvoNaSklade =
                    polozka.Produkt.MnozstvoNaSklade - rozdiel;
            }
            else
            {
                int vratitNaSklad = -rozdiel;
                polozka.Produkt.MnozstvoNaSklade =
                    polozka.Produkt.MnozstvoNaSklade + vratitNaSklad;
            }

            if (noveMnozstvo == 0)
            {
                kosik.OdstranProdukt(id);
                Console.WriteLine(" Produkt bol z kosika odstraneny.");
            }
            else
            {
                kosik.ZmenMnozstvo(id, noveMnozstvo);
                Console.WriteLine(" Mnozstvo v kosiku bolo zmenene.");
            }
        }

        private void Pokladna()
        {
            if (kosik.Polozky.Count == 0)
            {
                Console.WriteLine(" Kosik je prazdny.");
                return;
            }

            decimal celkom = kosik.VratCelkovaCena();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(" ♥ Celkova cena objednavky je " + celkom + " €.");
            Console.ResetColor();
            Console.Write(" Potvrdit nakup? (a/n): ");
            string odpoved = Console.ReadLine();

            if (odpoved.ToLower() == "a")
            {
                UlozProdukty();
                kosik.Vycisti();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" ♥ Dakujeme za nakup v Heartz Clothing! ♥");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(" Nakup bol zruseny.");
            }
        }
    }
