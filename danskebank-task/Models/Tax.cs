namespace danskebank_task.Models
{
    public class Tax
    {
        public int Id { get; set; }
        public required string Municipality { get; set; }
        public TaxType TaxType { get; set; }
        public double TaxRate { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
