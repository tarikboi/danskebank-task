using danskebank_task.Models;
using System.Globalization;

namespace danskebank_task.Utils
{
    public class CsvParser
    {
        public static List<Tax> ParseTaxes(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var taxes = new List<Tax>();

            foreach (var line in lines)
            {
                var parts = line.Split(',');

                taxes.Add(new Tax
                {
                    Municipality = parts[0].ToLower(),
                    TaxType = Enum.Parse<TaxType>(parts[1], true),
                    TaxRate = double.Parse(parts[2], CultureInfo.InvariantCulture),
                    StartDate = DateOnly.Parse(parts[3]),
                    EndDate = DateOnly.Parse(parts[4])
                });
            }
            return taxes;
        }
    }
}
