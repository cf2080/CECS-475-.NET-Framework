using System;
using System.Collections.Generic;
using System.Text;

// If compiling from the command line, compile with: -doc:IntegerSet.xml
namespace Lab_2_Payroll_Application
{
    public class SalariedEmployee : Employee
    {
        private decimal weeklySalary;

        // four-parameter constructor
        public SalariedEmployee(string first, string last, string ssn,
           decimal salary) : base(first, last, ssn)
        {
            WeeklySalary = salary; // validate salary via property
        } // end four-parameter SalariedEmployee constructor

        // property that gets and sets salaried employee's salary
        public decimal WeeklySalary
        {
            get
            {
                return weeklySalary;
            } // end get
            set
            {
                if (value >= 0) // validation
                    weeklySalary = value;
                else
                    throw new ArgumentOutOfRangeException("WeeklySalary",
                       value, "WeeklySalary must be >= 0");
            } // end set
        } // end property WeeklySalary

        // calculate earnings; override abstract method Earnings in Employee
        public override decimal GetPaymentAmount()
        {
            return WeeklySalary;
        } // end method Earnings          

        // return string representation of SalariedEmployee object
        public override string ToString()
        {
            return string.Format("salaried employee: {0}\n{1}: {2:C}",
               base.ToString(), "weekly salary", WeeklySalary);
        } // end method ToString                                      
    } // end class SalariedEmployee
}
