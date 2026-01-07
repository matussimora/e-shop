using e_shop;

class Pouzivatel
{
    public string Meno { get; set; }
    public Kosik Kosik { get; set; }

    public Pouzivatel(string meno)
    {
        Meno = meno;
        Kosik = new Kosik();
    }
}
