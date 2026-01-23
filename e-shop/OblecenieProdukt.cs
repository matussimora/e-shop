namespace HeartzClothing
{
    class OblecenieProdukt : Produkt
    {
        public string Farba { get; set; }
        public string Velkost { get; set; }

        public override string ToString()
        {
            // Include color and size in the display so users see those details when listing products
            return $"{Id}. {Nazov} | {Kategoria} | {Farba} | {Velkost} | {Cena} € | sklad: {MnozstvoNaSklade} ks";
        }
    }
}   