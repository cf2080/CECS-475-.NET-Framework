using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Lab_3_Stock_App
{
    class StockCustomer
    {
        /// <summary>
        /// Name of the customer
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// List containing object of stocks
        /// </summary>
        public List<Stock> Stocks;

        // Set a variable to the Desktop Path
        string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        /// <summary>
        /// variable that represents lock used in the program
        /// </summary>
        private static ReaderWriterLockSlim lockIt = new ReaderWriterLockSlim();

        /// <summary>
        /// Constructor for StockCustomer class
        /// </summary>
        /// <param name="cName">name of customer</param>
        /// <param name="s">list of stocks of customer owns</param>
        public StockCustomer(string cName, List<Stock> s)
        {
            CustomerName = cName;
            Stocks = s;
        }

        /// <summary>
        /// Constructor for stock customer class
        /// </summary>
        /// <param name="cName">Name of constructor</param>
        public StockCustomer(string cName)
        {
            CustomerName = cName;
            Stocks = new List<Stock>();
        }

        /// <summary>
        /// Adds stocks to the list that the customer owns
        /// </summary>
        /// <param name="s">Stock object</param>
        public void AddStock(Stock s)
        {
            Stocks.Add(s);
            s.StockEvent += EventHandler;
        }

        /// <summary>
        /// An event used when new stocks are added to the customer's list of stocks owned. Also written to stocks list.
        /// </summary>
        /// <param name="sender">Sender of object</param>
        /// <param name="e">Event Data object containing envent information of stocks</param>
        public void EventHandler(object sender, EventData e)
        {
            string date = DateTime.Now.ToString("MM/dd/yyyy");
            string time = DateTime.Now.ToString("HH:mm:ss");
            double nChange = Math.Abs(e.NumChange);

            double pChangeMinLim = (Math.Abs(e.NumChange / e.InitiaValue));
            lockIt.EnterWriteLock();
            try
            {
                if(pChangeMinLim>=.30 && pChangeMinLim<=.50)
                {
                    string SRem = e.Name;

                    Console.WriteLine("Customer Name: {0}\nStock Name: {1}Initial Stock Value: {2}Current Stock Value: {3}Amount of Loss/Gain: {4}  %",
                     CustomerName.PadRight(10), e.Name.PadRight(10), e.InitiaValue.ToString().PadRight(10), e.CurrentValue.ToString().PadRight(10), (Math.Round((e.NumChange/e.InitiaValue)*100)).ToString());

                    using (StreamWriter file = new StreamWriter(Path.Combine(mydocpath, "stocksfile.txt"), true))
                    {
                        file.WriteLine("Customer Name: " + CustomerName.PadRight(15) + "Stock Name: " + e.Name.PadRight(15) + "Initial Stock Value: " 
                            + e.InitiaValue.ToString().PadRight(15) + "Current Stock Value: " + e.CurrentValue.ToString().PadRight(15) + 
                            "Amount of Loss/Gain: " + (Math.Round((e.NumChange / e.InitiaValue) * 100)).ToString().PadRight(15));
//                        file.WriteLine("\n" + date.PadRight(15) + time.PadRight(10) + e.Name.PadRight(10) + e.InitiaValue.ToString().PadRight(10)
//                              + e.CurrentValue.ToString().PadRight(10) + e.NumChange.ToString().PadRight(10) + e.QuantityStock.ToString().PadRight(10));
                        //file.Close();

                    }
                    for (int i = 0; i < Stocks.Count; i++)
                    {
                        if (SRem == Stocks[i].StockName)
                        {
                            Stocks[i].StockEvent -= EventHandler;
                            Stocks.RemoveAt(i);
                        }
                    }
                }
                else
                {
                    //Console.WriteLine(CustomerName.PadRight(20) + e.Name.PadRight(15) + "NO SOLD".PadRight(15)); //+ "Gain/Loss: " + Math.Round((e.NumChange / e.InitiaValue)*100)  + " %");
                }
            }
            finally
            {
                //Release lockk
                lockIt.ExitWriteLock();
            }
        }
    }
}
