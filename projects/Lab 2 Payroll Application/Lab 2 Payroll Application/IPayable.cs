using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_2_Payroll_Application
{
    public interface IPayable : IComparable
    {
        decimal GetPaymentAmount(); // calculate payment; no implementation
    } // end interface IPayable
}
