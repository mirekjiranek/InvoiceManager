namespace InvoiceManager
{
    public class PolozkaFaktury
    {
        public int PoradoveCislo { get; set; }
        public int CenaZaKus { get; set; }
        public int PocetKusu { get; set; }
        public int CelkovaCena => CenaZaKus * PocetKusu;
    }
}
