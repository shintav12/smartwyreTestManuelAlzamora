using System;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{

    static void Main(string[] args)
    {
        var rebateDataStore = new RebateDataStore();
        var productDataStore = new ProductDataStore(); 
        var rebateService = new RebateService(rebateDataStore, productDataStore);

        Console.WriteLine("Press any key to start or Escape (Esc) key to quit: ");
        Console.WriteLine();
        var ch = Console.ReadKey();
        do 
        {
            Console.Write("Insert Rebate Identifier: ");
            string rebateIdentifier = Console.ReadLine();
            Console.Write("Insert Product Identifier: ");
            string productIdentifier = Console.ReadLine();
            Console.Write("Insert Volume: ");
            string volumeString = Console.ReadLine();

            if (!Decimal.TryParse(volumeString, out decimal volume))
            {
                Console.WriteLine("Conversion of {0} failed", volumeString);
                continue;
            }

            var rebateRequest = new CalculateRebateRequest
            {
                ProductIdentifier = productIdentifier,
                Volume = volume,
                RebateIdentifier = rebateIdentifier,
            };

            var calculateRebateResult = rebateService.Calculate(rebateRequest);

            Console.WriteLine($"The result rebate was {calculateRebateResult.Calculation}");
            Console.WriteLine();
            Console.WriteLine("Press any key to start or Escape (Esc) key to quit: ");
            ch = Console.ReadKey();

        } while (ch.Key != ConsoleKey.Escape);

    }
}