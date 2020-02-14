using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Lab_4_Calculator.Model
{
    public class CalculatorModel : INotifyPropertyChanged
    {
        private double num1;
        private double num2;
        private double result;
        //public ICommand Operation { get; private set; }

        public double Num1 {
            get {
                return num1;
            }
            set {
                if (num1 != value)
                {
                    num1 = value;
                    RaisePropertyChanged("num1");
                }
                //Set<double>(() => this.num1, ref num1, value);           
            }
        }

        public double Num2
        {
            get
            {
                return num2;
            }
            set
            {
                if (num2 != value)
                {
                    num2 = value;
                    RaisePropertyChanged("num2");
                }
            }
        }

        public void Division()
        {
            result = num1 / num2;
            //tbx3.Text = 
            //return result;
        }

        public void Addition()
        {
            result = num1 + num2;
            //return result;
        }

        public void Subtraction()
        {
            result = num1 - num2;
            //return result;
        }

       /* private void DoCalculation(string @operator)
        {
            var result = 0;
            switch (@operator)
            {
                case "+": result = Convert.ToInt32(num1) + Convert.ToInt32(num2); break;
                case "-": result = Convert.ToInt32(num1) - Convert.ToInt32(num2); break;
                case "*": result = Convert.ToInt32(num1) * Convert.ToInt32(num2); break;
                case "/": result = Convert.ToInt32(num1) / Convert.ToInt32(num2); break;
            }
            MessageBox.Show(result.ToString());
        }
        */

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
