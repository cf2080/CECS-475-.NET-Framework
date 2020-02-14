using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/// If compiling from the command line, compile with: -doc:Employee.xml
namespace Lab_2_Payroll_Application
{
    /// <summary>
    /// Abstract class which is used to implement many of it's function on classes in which this is called upon
    /// </summary>
    public abstract class Employee : IComparable, IPayable
    {
        /// <summary>
        /// Returns and sets first name of an employee
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Returns and sets last name of an employee
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Returns and sets security number of an employee
        /// </summary>
        public string SocialSecurityNumber { get; private set; }

        /// <summary>
        /// Constructor for Employee class 
        /// </summary>
        /// <param name="first">represents first name of an employee</param>
        /// <param name="last">represents last name of an employee</param>
        /// <param name="ssn">represents social security number of an employee</param>
        public Employee( string first, string last, string ssn)
        {
            FirstName = first;
            LastName = last;
            SocialSecurityNumber = ssn;
        } // end three-parameter Employee constructor

        /// <summary>
        /// Abstract function used to get the salaries of employee's from other classes
        /// </summary>
        /// <returns></returns>
        public abstract decimal GetPaymentAmount();

        /// <summary>
        /// Method used to compare lastname used to sort employee list
        /// </summary>
        /// <param name="obj">Object var, represents an Employee object</param>
        /// <returns>returns Comparison of Last names, an int value</returns>
        public int CompareTo(object obj)
        {
            Employee compareName = (Employee)obj;
            return String.Compare(compareName.LastName, this.LastName);
        }

        /// <summary>
        ///Function that helps in displaying the employee's information 
        /// </summary>
        /// <returns>Employee's information</returns>
        public override string ToString()
        {
            return string.Format( "{0} {1}\nSocial Security Number: {2}", FirstName, LastName, SocialSecurityNumber );
        } // end method ToString

        /// <summary>
        /// Private Class that uses IComparer in order to aid in sorting by pay
        /// </summary>
        private class SortByPayAscendingOrderHelper : IComparer
        {
            /// <summary>
            /// An Icomparer function that is used to compare the salaries of the employee's
            /// </summary>
            /// <param name="a">An object that represents the pay of an employee</param>
            /// <param name="b">An object that represents the pay of an employee</param>
            /// <returns></returns>
            int IComparer.Compare(object a, object b)
            {
                Employee pay1 = (Employee)a;
                Employee pay2 = (Employee)b;

                if(pay1.GetPaymentAmount() > pay2.GetPaymentAmount())
                {
                    return 1;
                }
                if(pay1.GetPaymentAmount() < pay2.GetPaymentAmount())
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
        
        /// <summary>
        /// IComparer function that aids in sorting the pay by returning the helper function
        /// </summary>
        /// <returns>the IComparer function of SortByAscendingOrderHelper() function</returns>
        public static IComparer SortPayAscending()
        {
            return (IComparer)new SortByPayAscendingOrderHelper();
        }

        /// <summary>
        /// function that aid in sorting the Social Security Number in descending order
        /// </summary>
        /// <param name="obj1">An object var that represents an employee object</param>
        /// <param name="obj2">An object that represents an employee object</param>
        /// <returns>The comparison of emp1 and emp2 social security number in order to decide which one is bigger</returns>
        public static int sortSSN(object obj1, object obj2)
        {
            Employee emp1 = (Employee)obj1;
            Employee emp2 = (Employee)obj2;
            //return (emp1.SocialSecurityNumber > emp2.SocialSecurityNumber);
            return String.Compare(emp2.SocialSecurityNumber, emp1.SocialSecurityNumber);
        }
    }
}
