namespace e_shop
{
    class PolozkaKosika
    {
        public Produkt Produkt { get; set; }
        public int Mnozstvo { get; set; }

        public PolozkaKosika(Produkt produkt, int mnozstvo)
        {
            Produkt = produkt;
            Mnozstvo = mnozstvo;
        }

        public double CelkovaCena()
        {
            return Produkt.Cena * Mnozstvo;
        }
    }
}
