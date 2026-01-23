using System.Collections.Generic;
using System.Linq;

namespace HeartzClothing
{
    class Kosik
    {
        private List<PolozkaKosika> polozky = new List<PolozkaKosika>();

        public List<PolozkaKosika> Polozky
        {
            get { return polozky; }
        }

        public void PridajProdukt(Produkt produkt, int mnozstvo)
        {
            if (produkt == null)
            {
                return;
            }

            if (mnozstvo <= 0)
            {
                return;
            }

            PolozkaKosika existujuca = polozky.FirstOrDefault(p => p.Produkt.Id == produkt.Id);

            if (existujuca == null)
            {
                PolozkaKosika nova = new PolozkaKosika();
                nova.Produkt = produkt;
                nova.Mnozstvo = mnozstvo;
                polozky.Add(nova);
            }
            else
            {
                int noveMnozstvo = existujuca.Mnozstvo + mnozstvo;
                existujuca.Mnozstvo = noveMnozstvo;
            }
        }

        public void OdstranProdukt(int produktId)
        {
            PolozkaKosika najdena = polozky.FirstOrDefault(p => p.Produkt.Id == produktId);
            if (najdena != null)
            {
                polozky.Remove(najdena);
            }
        }

        public void ZmenMnozstvo(int produktId, int noveMnozstvo)
        {
            PolozkaKosika najdena = polozky.FirstOrDefault(p => p.Produkt.Id == produktId);
            if (najdena != null)
            {
                if (noveMnozstvo <= 0)
                {
                    polozky.Remove(najdena);
                }
                else
                {
                    najdena.Mnozstvo = noveMnozstvo;
                }
            }
        }

        public decimal VratCelkovaCena()
        {
            decimal suma = polozky.Sum(p => p.CelkovaCena);
            return suma;
        }

        public void Vycisti()
        {
            polozky.Clear();
        }
    }
}
