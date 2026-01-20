class Produkt
{
    public string Nazov { get; set; }
    public double Cena { get; set; }
    public string Kategoria { get; set; }
    public int MnozstvoNaSklade { get; set; }

    public Produkt(string nazov, double cena, string kategoria, int mnozstvo)
    {
        Nazov = nazov;
        Cena = cena;
        Kategoria = kategoria;
        MnozstvoNaSklade = mnozstvo;
    }

    public override string ToString()
    {
        return $"{Nazov};{Cena};{Kategoria};{MnozstvoNaSklade}";
    }
}
class Dziny
{
    public string nazov;
    public string farba;
    public string material;
    public string znacka;
    public double cena;
    public bool skladom;

    public Dziny(string nazov, string farba, string material, string znacka, double cena, bool skladom)
    {
        this.nazov = nazov;
        this.farba = farba;
        this.material = material;
        this.znacka = znacka;
        this.cena = cena;
        this.skladom = skladom;
    }

    public void Vypis()
    {
        System.Console.WriteLine("Názov: " + nazov);
        System.Console.WriteLine("Značka: " + znacka);
        System.Console.WriteLine("Farba: " + farba);
        System.Console.WriteLine("Materiál: " + material);
        System.Console.WriteLine("Cena: " + cena + " €");

        if (skladom)
            System.Console.WriteLine("Skladom: Áno");
        else
            System.Console.WriteLine("Skladom: Nie");

        System.Console.WriteLine("--------------------");
    }
}
