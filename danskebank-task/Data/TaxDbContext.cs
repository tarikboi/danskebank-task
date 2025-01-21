using danskebank_task.Models;
using Microsoft.EntityFrameworkCore;

public class TaxDbContext : DbContext
{
    public DbSet<Tax> Taxes { get; set; }
    public string DbPath { get; }

    public TaxDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        Console.WriteLine(path);
        DbPath = Path.Join(path, "tax.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}