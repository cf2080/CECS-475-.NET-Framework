using System;

namespace Lab_3_Stock_App
{
    /// <summary>
    /// Tester Class used to test the info from the Stock and Stock Customer Class
    /// </summary>
    public class Tester
    {
        public static void Main(string[] args)
        {

            Stock stock1 = new Stock("Tesla", 160, 5, 15,30);
            Stock stock2 = new Stock("Apple", 30, 2, 6,30);
            Stock stock3 = new Stock("Lowes", 90, 4, 10,30);
            Stock stock4 = new Stock("CBS", 500, 20, 50,30);

            Stock stock5 = new Stock("Tesla", 180, 5, 15, 30);
            Stock stock6 = new Stock("Apple", 50, 2, 6, 30);
            Stock stock7 = new Stock("Lowes", 95, 4, 10, 30);
            Stock stock8 = new Stock("CBS", 550, 20, 50, 30);

            StockCustomer b1 = new StockCustomer("Christian Flores");
            b1.AddStock(stock1);
            b1.AddStock(stock2);

            StockCustomer b2 = new StockCustomer("Alexander Hamilton");
            b2.AddStock(stock5);
            b2.AddStock(stock6);
            b2.AddStock(stock8);

            StockCustomer b3 = new StockCustomer("Dwight Eisenhower");
            b3.AddStock(stock3);
            b3.AddStock(stock1);

            StockCustomer b4 = new StockCustomer("John Kerry");
            b4.AddStock(stock1);
            b4.AddStock(stock2);
            b4.AddStock(stock3);
            b4.AddStock(stock4);            

            Console.WriteLine("press any key to continue");
            Console.ReadKey(true);
        }
    }
}
