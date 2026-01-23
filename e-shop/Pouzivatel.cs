namespace HeartzClothing
{
    class Pouzivatel
    {
        public string PrihlasovacieMeno { get; set; }
        public string Heslo { get; set; }

        public virtual string Rola
        {
            get { return "Zakaznik"; }
        }

        public override string ToString()
        {
            return PrihlasovacieMeno + " (" + Rola + ")";
        }
    }
}
