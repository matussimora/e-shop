using System;
using System.Collections.Generic;
using System.Text;

namespace e_shop
{
    internal class Kosik
    {
        private List<PolozkaKosika> polozky = new List<PolozkaKosika>();

        public void PridajProdukt(Produkt produkt, int mnozstvo)
        {
            if (mnozstvo <= 0) return;
            var exist = polozky.Find(p => p.Produkt.Nazov == produkt.Nazov);
            if (exist != null)
            {
                exist.Mnozstvo += mnozstvo;
            }
            else
            {
                polozky.Add(new PolozkaKosika(produkt, mnozstvo));
            }
        }

        public void OdstranProdukt(string nazov)
        {
            polozky.RemoveAll(p => p.Produkt.Nazov == nazov);
        }

        public void ZmenMnozstvo(string nazov, int noveMnozstvo)
        {
            var item = polozky.Find(p => p.Produkt.Nazov == nazov);
            if (item != null)
            {
                if (noveMnozstvo <= 0) polozky.Remove(item);
                else item.Mnozstvo = noveMnozstvo;
            }
        }

        public double CelkovaCena()
        {
            double sum = 0;
            foreach (var p in polozky)
                sum += p.CelkovaCena();
            return sum;
        }

        public void VypisKosik()
        {
            if (polozky.Count == 0)
            {
                Console.WriteLine("Košík je prázdny.");
                return;
            }
            Console.WriteLine("Položky v košíku:");
            foreach (var p in polozky)
            {
                Console.WriteLine($"{p.Produkt.Nazov} x{p.Mnozstvo} - {p.CelkovaCena():0.00} €");
            }
            Console.WriteLine($"Celkom: {CelkovaCena():0.00} €");
        }

        public void Vycistit()
        {
            polozky.Clear();
        }

        public PolozkaKosika NajdiPolozku(string nazov)
        {
            return polozky.Find(p => p.Produkt.Nazov == nazov);
        }
    }
}
