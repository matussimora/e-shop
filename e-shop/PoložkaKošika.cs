

namespace HeartzClothing
{
    class PolozkaKosika
    {
        public Produkt Produkt { get; set; }
        public int Mnozstvo { get; set; }

        public decimal CelkovaCena
        {
            get { return Produkt.Cena * Mnozstvo; }
        }
    }
}

