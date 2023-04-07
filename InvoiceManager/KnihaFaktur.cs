using System;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceManager
{
    public class KnihaFaktur
    {
        public Dictionary<string, Faktura> Faktury { get; set; }
        public IQueryable<Faktura> QueryFakturyNad100000 =>
            Faktury.Values.AsQueryable().Where(f => f.CenaCelkem >= 100000);

        public KnihaFaktur()
        {
            Faktury = new Dictionary<string, Faktura>();
        }

        public void PridatFakturu(Faktura faktura)
        {
            if (!Faktury.ContainsKey(faktura.CisloDokladu))
            {
                Faktury.Add(faktura.CisloDokladu, faktura);
                return;
            }
            throw new ArgumentException($"Faktura s číslem dokladu '{faktura.CisloDokladu}' již existuje.");
        }

        public Faktura VyhledejFakturu(string cisloDokladu)
        {
            if (Faktury.TryGetValue(cisloDokladu, out var faktura))
            {
                return faktura;
            }
            throw new KeyNotFoundException($"Faktura pod číslem dokladu '{cisloDokladu}' neexistuje.");
        }

        public void GenerujFaktury(int minPocetFaktur, int maxPocetFaktur)
        {
            Random random = new Random();
            int pocetFaktur = random.Next(minPocetFaktur, maxPocetFaktur + 1);
            DateTime datum = new DateTime(2021, 1, random.Next(1, 32));

            for (int i = 0; i < pocetFaktur; i++)
            {
                Faktura faktura = new Faktura
                {
                    CisloDokladu = $"FV{i + 1:0000}",
                    DatumVystaveni = datum
                };

                faktura.GenerujData();
                PridatFakturu(faktura);

                datum = datum.AddDays(random.Next(0, 26));
            }
        }
    }
}
