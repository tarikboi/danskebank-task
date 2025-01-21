using danskebank_task.Models;
using danskebank_task.Services;
using danskebank_task.Utils;
using System.Globalization;

public class Program
{
   public static async Task Main(string[] args)
   {
        using var db = new TaxDbContext();
        TaxService ts = new TaxService(db);

        Console.WriteLine("Buying & Owning Real Estate Nordic by Tarik Oksuz");
        Console.WriteLine("'exit' to close application, 'help' to get help");

        while (true)
        {
            Console.Write("\n> ");
            string[] input = Console.ReadLine()!.ToLower().Split(' ');

            var command = input[0];

            switch (command)
            {
                case CommandType.Get:
                    await HandleGetCommand(input, ts);
                    break;

                case CommandType.Add:
                    await HandleAddCommand(input, ts);
                    break;

                case CommandType.Import:
                    await HandleImportCommand(input, ts);
                    break;

                case CommandType.Help:
                    DisplayHelp();
                    break;

                case CommandType.Exit:
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine($"'{command}' is not a valid command. Type 'help' for a list of commands.");
                    break;
            }
        }
    }

    private static async Task HandleGetCommand(string[] input, TaxService taxService)
    {
        if (input.Length != 3)
        {
            Console.WriteLine("Usage: get <municipality> <date>");
            return;
        }

        var municipality = input[1];
        var date = DateOnly.Parse(input[2]);
        var tax = await taxService.GetTax(municipality, date);

        if (tax == null)
        {
            Console.WriteLine("No tax records found");
        }
        else
        {
            Console.WriteLine($"Tax rate for {municipality} on {date}: {tax.TaxRate}");
        }
    }

    private static async Task HandleAddCommand(string[] input, TaxService taxService)
    {
        if (input.Length != 6)
        {
            Console.WriteLine("Usage: add <municipality> <taxType> <taxRate> <startDate> <endDate>");
            return;
        }

        try
        {
            var newTax = new Tax()
            {
                Municipality = input[1],
                TaxType = Enum.Parse<TaxType>(input[2], true),
                TaxRate = double.Parse(input[3], CultureInfo.InvariantCulture),
                StartDate = DateOnly.Parse(input[4]),
                EndDate = DateOnly.Parse(input[5])
            };

            await taxService.AddTax(newTax);
            Console.WriteLine("Tax added successfully");
        }
        catch (FormatException)
        {
            Console.WriteLine("Error in argument formatting. Please follow the correct structure: add copenhagen year 0.2 2016-01-01 2016-12-31");
        }
        catch (ArgumentException)
        {
            Console.WriteLine($"Please enter a valid tax type: day, month, year");
        }
        catch (Exception)
        {
            Console.WriteLine("Error adding the tax");
        }
    }

    private static async Task HandleImportCommand(string[] input, TaxService taxService)
    {
        if (input.Length != 2)
        {
            Console.WriteLine("Usage: import <filePath>");
            return;
        }
        var filePath = input[1];
        var taxes = new List<Tax>();
        try
        {
            taxes = CsvParser.ParseTaxes(filePath);
            await taxService.ImportTaxes(taxes);
            Console.WriteLine($"{taxes.Count} taxes successfully imported");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("The file does not exist");
        }
        catch (FormatException)
        {
            Console.WriteLine("Error in file. Please follow the correct structure: copenhagen,year,0.2,2016-01-01,2016-12-31 in .CSV format");
        }
        catch (ArgumentException)
        {
            Console.WriteLine($"Please enter a valid tax type: day, month, year");
        }
        catch (Exception)
        {
            Console.WriteLine("Error importing taxes");
        }
    }

    private static void DisplayHelp()
    {
        Console.WriteLine("get <municipality> <date>                                         get copenhagen 2023-01-01");
        Console.WriteLine("add <municipality> <taxType> <taxRate> <startDate> <endDate>      add copenhagen year 0.2 2025-01-01 2025-12-31");
        Console.WriteLine("import <filePath>                                                 import C://taxes.csv");
        Console.WriteLine("help");
        Console.WriteLine("exit");
    }
}