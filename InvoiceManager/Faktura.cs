using System;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceManager
{
    public class Faktura
    {
        public string CisloDokladu { get; set; }
        public DateTime DatumVystaveni { get; set; }
        public List<PolozkaFaktury> Polozky { get; set; }
        public int CenaCelkem => Polozky.Sum(x => x.CelkovaCena);

        public Faktura()
        {
            Polozky = new List<PolozkaFaktury>();
        }

        public void GenerujData()
        {
            var random = new Random();
            int pocetPolozek = random.Next(1, 21);

            for (int i = 0; i < pocetPolozek; i++)
            {
                var cenaZaKus = random.Next(1, 2501);
                var pocetKusu = random.Next(1, 21);

                var polozkaFaktury = new PolozkaFaktury
                {
                    PoradoveCislo = i + 1,
                    CenaZaKus = cenaZaKus,
                    PocetKusu = pocetKusu
                };

                Polozky.Add(polozkaFaktury); 
            }
        }
    }
}
