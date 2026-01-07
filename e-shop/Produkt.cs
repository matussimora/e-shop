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
