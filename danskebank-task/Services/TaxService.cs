using danskebank_task.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace danskebank_task.Services
{
    public class TaxService
    {
        private readonly TaxDbContext _context;

        public TaxService(TaxDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Retrieves the applicable tax record for a municipality on a specific date.
        /// </summary>
        public async Task<Tax?> GetTax(string municipality, DateOnly date)
        {
            var tax = await _context.Taxes
                .Where(t => t.Municipality == municipality && t.StartDate <= date && t.EndDate >= date)
                .OrderBy(t => t.TaxType)
                .ThenByDescending(t => t.StartDate)
                .FirstOrDefaultAsync();

            return tax;
        }

        /// <summary>
        /// Adds a new tax record to the database.
        /// </summary>
        public async Task AddTax(Tax tax)
        {
            _context.Add(tax);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Imports tax records from a CSV file.
        /// </summary>
        public async Task ImportTaxes(List<Tax> taxes)
        {
            foreach (var tax in taxes)
            {
                _context.Add(tax);
            }

            await _context.SaveChangesAsync();
        }
    }
}
