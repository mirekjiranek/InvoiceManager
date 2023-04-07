using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace InvoiceManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                           .Build();

            var minPocetFaktur = int.Parse(configuration["Faktury:minPocetFaktur"]);
            var maxPocetFaktur = int.Parse(configuration["Faktury:maxPocetFaktur"]);

            var knihaFaktur = new KnihaFaktur();
            knihaFaktur.GenerujFaktury(minPocetFaktur, maxPocetFaktur);

            // Výpis faktur s cenou 100 000 a více, setříděných sestupně dle čísla dokladu a vystavených v předchozích 90 dnech
            var fakturyNad100000 = knihaFaktur.QueryFakturyNad100000
                .Where(f => f.DatumVystaveni >= DateTime.Now.AddDays(-90))
                .OrderByDescending(f => f.CisloDokladu);

            foreach (var faktura in fakturyNad100000)
            {
                Console.WriteLine($"Číslo dokladu: {faktura.CisloDokladu}, Datum vystavení: {faktura.DatumVystaveni.ToString("dd.MM.yyyy")}, Cena celkem: {faktura.CenaCelkem}");
            }
        }
    }
}
