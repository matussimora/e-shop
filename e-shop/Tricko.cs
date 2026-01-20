public class Tricko
{


    public string nazov;
    public string farba;
    public string material;
    public string znacka;
    private double cena;
    public string velkost;
    public bool skladom;


    public Tricko(string nazov, string farba, string material, string znacka,
                  double cena, string velkost, bool skladom)
    {
        this.nazov = nazov;
        this.farba = farba;
        this.material = material;
        this.znacka = znacka;
        this.cena = cena;
        this.velkost = velkost;
        this.skladom = skladom;
    }
    
public void vypisInfo()
    {
        System.Console.WriteLine("Názov: " + nazov);
        System.Console.WriteLine("Značka: " + znacka);
        System.Console.WriteLine("Farba: " + farba);
        System.Console.WriteLine("Materiál: " + material);
        System.Console.WriteLine("Veľkosť: " + velkost);
        System.Console.WriteLine("Cena: " + cena + " ");
        System.Console.WriteLine("Skladom: " + (skladom ? "Áno" : "Nie"));
        System.Console.WriteLine("----------------------------");
    }
}
