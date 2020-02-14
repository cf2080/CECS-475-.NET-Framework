using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace Lab_3_Stock_App
{
    class Stock
    {
        /// <summary>
        /// These public variables represents the properties that each stock have such as name, initial value
        /// quantity, change in value, notoification threshold abd current value
        /// </summary>
        public string StockName { get; set; }
        public double StockInitialValue { get; set; }
        public int StockQuantity { get; set; }
        public double MaxChange { get; set; }
        public double NotThreshold { get; set; }

        public double CurrentValue { get; set; }
        
        /// <summary>
        /// Object used to lock threads
        /// </summary>
        private System.Object lockIt = new System.Object();

        public double NumChange { get; set; }

        /// <summary>
        /// Thtead used in program
        /// </summary>
        public Thread Stockthread;

        /// <summary>
        /// Constructor for Stock class
        /// </summary>
        /// <param name="name">Name of stock</param>
        /// <param name="val">initial value of stock</param>
        /// <param name="quantity">Quantity of stocks</param>
        /// <param name="change">change of stock value</param>
        /// <param name="notThresh">Notification threshold</param>
        public Stock(string name, double val, int quantity, double change, double notThresh)
        {
            StockQuantity = quantity;
            StockName = name;
            StockInitialValue = val;
            CurrentValue = StockInitialValue;
            NotThreshold = notThresh;
            MaxChange = change;
            Stockthread = new Thread(new ThreadStart(Activate));
            Stockthread.Start();
        }


        /// <summary>
        /// Method that is called to activate the change stock value for each stock
        /// </summary>
        public void Activate()
        {
            //int stockC = 0;
            for(; ; )
            //for(int i = 0; i< 50; i++)
            {
                ChangeStockValue();
                Thread.Sleep(500); // .5 second
                if (Math.Abs((NumChange/StockInitialValue))>=.30 && Math.Abs((NumChange / StockInitialValue))<=.5)
                {
                    //stockC++;
                    //Console.WriteLine("Stopping" + Math.Abs((NumChange / StockInitialValue)).ToString().PadLeft(10) + stockC.ToString().PadLeft(10));
                    break;
                }
            }
        }

        /// <summary>
        /// Method that is used so the stock changes values.
        /// </summary>
        public void ChangeStockValue()
        {
            Random rand = new Random();
            CurrentValue = CurrentValue + rand.Next((int)-MaxChange,(int)MaxChange);
            NumChange++;
            double diff = (CurrentValue - StockInitialValue);
            if(Math.Abs(diff)>NotThreshold)
            {
                lock (lockIt)
                {
                    //Raise the event
                    EventRaise(new EventData(StockName, CurrentValue, StockInitialValue, StockQuantity, diff));
                }

            }
        }

        /// <summary>
        /// Method that raises an event if handler event in the class isn't null after being given data from eventdata
        /// </summary>
        /// <param name="e">Event dara</param>
        protected virtual void EventRaise(EventData e)
        {
            StockNotification handler = StockEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// An event that notifies when when a stock has changed in value
        /// </summary>
        public event StockNotification StockEvent;
        /// <summary>
        /// Delegate method that is used as an event as well
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void StockNotification(object sender,EventData e);
    }

    /// <summary>
    /// Class that contains event data for every specific data when calling an event
    /// </summary>
    public class EventData : EventArgs
    {
        /// <summary>
        /// Represents get set of name of stock
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Represents current value of the stock to be stored in eventdata
        /// </summary>
        public double  CurrentValue { get; set; }
        /// <summary>
        /// Represents initial value of stock to be stored in event data
        /// </summary>
        public double InitiaValue { get; set; }
        /// <summary>
        /// Quantity of stocks
        /// </summary>
        public int QuantityStock { get; set; }
        /// <summary>
        /// Represents change in stocks
        /// </summary>
        public double NumChange { get; set; }

        /// <summary>
        /// Constructor for EventData class
        /// </summary>
        /// <param name="n">Stores name of stock</param>
        /// <param name="value">Represents value of stock</param>
        /// <param name="iValue">Represents initial value of stock</param>
        /// <param name="qStock">represents quantity of stocks</param>
        /// <param name="nc">Represents change in stock</param>
        public EventData(string n, double value, double iValue, int qStock, double nc)
        {
            NumChange = nc;
            Name = n;
            CurrentValue = value;
            InitiaValue = iValue;
            QuantityStock = qStock;

        }
    }
}
