using Lab_4_Calculator.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab_4_Calculator.ViewModel
{
    /// <summary>
    /// Class that controls what happends in the user interface when the app is running
    /// </summary>
    public class ViewModel: INotifyPropertyChanged
    {
        /// <summary>
        /// ICommand used for Addition function
        /// </summary>
        public ICommand MyCommand { get; set; }

        /// <summary>
        /// ICommand used to subtract for the calculator
        /// </summary>
        public ICommand MyCommand2 { get; set; }

        /// <summary>
        /// ICommand used to multiply for the calculator
        /// </summary>
        public ICommand MyCommand3 { get; set; }

        /// <summary>
        /// ICommand used to divide for the calculator
        /// </summary>
        public ICommand MyCommand4 { get; set; }

        /// <summary>
        /// Event handler used for the onpropertychanged method
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method that is invoed if property isn't null
        /// </summary>
        /// <param name="property"></param>
        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Integer variable that represent first variable in the textbox of the calculator
        /// </summary>
        private int num1;

        /// <summary>
        /// Integer variable that represent the second variable in the textbox of the calculator
        /// </summary>
        private int num2;
 
        /// <summary>
        /// Method that represents the first numbers that is inputted in the calculator
        /// </summary>
        public int Number1
        {
            get { return num1; }
            set { num1 = value; OnPropertyChanged("Number1"); }
        }

        /// <summary>
        /// Number that represents the second number that is inputted in the calculator
        /// </summary>
        public int Number2
        {
            get { return num2; }
            set { num2 = value; OnPropertyChanged("Number2"); }
        }

        /// <summary>
        /// Integer variable that reprsents the result answers from num1 and num2
        /// </summary>
        private double result;
        
        /// <summary>
        /// Method that returns the result answer whenever it is invoked. Also sets the value of result
        /// </summary>
        public double Result
        {
            get { return result; }
            set { result = value ; OnPropertyChanged("Result"); }
        }

        /// <summary>
        /// Constructor for viewmodel that opens up all the mycoomand with a new value using relay command function
        /// </summary>
        public ViewModel()
        {
            MyCommand = new RelayCommand(ExecuteSum, CanExecute);
            MyCommand2 = new RelayCommand(ExecuteSub, CanExecute);
            MyCommand3 = new RelayCommand(ExecuteMult, CanExecute);
            MyCommand4 = new RelayCommand(ExecuteDiv, CanExecute);
        }

        /// <summary>
        /// Boolean method that returns whether it is true that it can execute
        /// </summary>
        /// <param name="par">An object</param>
        /// <returns>TRUE</returns>
        private bool CanExecute(object par)
        {
            return true;
        }

        /// <summary>
        /// Function that add num1 and num2 whenever the addition button is clicked in the caluclator
        /// </summary>
        /// <param name="par">object that most likely represents the button clicked</param>
        private void ExecuteSum(object par)
        {
            Result = num1 + num2;
        }

        /// <summary>
        /// Function that add num1 and num2 whenever the subtraction button is clicked in the caluclator
        /// </summary>
        /// <param name="par">object that most likely represents the button clicked</param>
        private void ExecuteSub(object par)
        {
            Result = num1 - num2;
        }

        /// <summary>
        /// Function that add num1 and num2 whenever the multiply button is clicked in the caluclator
        /// </summary>
        /// <param name="par">object that most likely represents the button clicked</param>
        private void ExecuteMult(object par)
        {
            Result = num1 * num2;
        }

        /// <summary>
        /// Function that add num1 and num2 whenever the division button is clicked in the caluclator
        /// </summary>
        /// <param name="par">object that most likely represents the button clicked</param>
        private void ExecuteDiv(object par)
        {
            Result = (double) num1 / (double) num2;
        }
    }
}
