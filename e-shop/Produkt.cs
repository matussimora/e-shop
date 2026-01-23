namespace HeartzClothing
{
    class Produkt
    {
        public int Id { get; set; }
        public string Nazov { get; set; }
        public string Kategoria { get; set; }
        public decimal Cena { get; set; }
        public int MnozstvoNaSklade { get; set; }

        public override string ToString()
        {
            return Id + ". " + Nazov + " | " + Kategoria + " | " + Cena + " € | sklad: " + MnozstvoNaSklade + " ks";
        }
    }
}
