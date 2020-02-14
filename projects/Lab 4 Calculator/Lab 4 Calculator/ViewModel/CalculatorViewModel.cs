using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lab_4_Calculator.ViewModel
{
    public class CalculatorViewModel
    {
        private ICommand mult;
        private ICommand div;
        private ICommand subt;
        private ICommand add;

        public ICommand Mult
        {
            get
            {
                return mult;
            }
        }

        public CalculatorViewModel()
        {

        }

    }
}
