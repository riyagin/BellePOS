namespace BellePOS.MVVM.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public int No { get; set; }
        public string KodeItem { get; set; }
        public string Keterangan { get; set; }
        public int Jumlah { get; set; }
        public string Satuan { get; set; }
        public decimal Harga { get; set; }
        public double Pot { get; set; }
        public decimal Total { get; set; }
    }
}
