using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab_2_Payroll_Application
{
    // If compiling from the command line, compile with: -doc:PayrollSystemTest.xml
    public class PayrollSystemTest
    {
        /// <summary>
        /// Delegate method used to call a function that aids in sorting employee's by social security
        /// </summary>
        /// <param name="ssn1">Object that theoritaclly represents an employee's social security number</param>
        /// <param name="ssn2">Object that represents an employee used to get it's SSN so it can be sorted</param>
        /// <returns></returns>
        public delegate int CompareDelegateSSN(object ssn1, object ssn2);

        /// <summary>
        /// Main method of Tester class used to start the application and test whether employee information
        /// </summary>
        /// <param name="args">command line arguments</param>
        public static void Main(string[] args)
        {
            IPayable[] payableObjects = new IPayable[8];
            payableObjects[0] = new SalariedEmployee("John", "Smith", "111-11-1111", 700M);
            payableObjects[1] = new SalariedEmployee("Antonio", "Smith", "555-55-5555", 800M);
            payableObjects[2] = new SalariedEmployee("Victor", "Smith", "444-44-4444", 600M);
            payableObjects[3] = new HourlyEmployee("Karen", "Price", "222-22-2222", 16.75M, 40M);
            payableObjects[4] = new HourlyEmployee("Ruben", "Zamora", "666-66-6666", 20.00M, 40M);
            payableObjects[5] = new CommissionEmployee("Sue", "Jones", "333-33-3333", 10000M, .06M);
            payableObjects[6] = new BasePlusCommissionEmployee("Bob", "Lewis", "777-77-7777", 5000M, .04M, 300M);
            payableObjects[7] = new BasePlusCommissionEmployee("Lee", "Duarte", "888-88-888", 5000M, .04M, 300M);

            for (int i = 0; i <= 7; i++)
            {
                Console.WriteLine(payableObjects[i].ToString());
                Console.WriteLine();

            }

            Menu(payableObjects);

            Console.ReadKey(true);
        } // end Main

        /// <summary>
        /// Method that is called in the main method so the user can decide how they want the employee list sorted by
        /// </summary>
        /// <param name="payableObjects">An IPayable object list which contains an employee list</param>
        public static void Menu(IPayable[] payableObjects)
        {
            string option;

            bool cont = true;
            while (cont)
            {
                Console.WriteLine("Please input [1-4]: ");
                Console.WriteLine("[1] Sort last name in descending order using IComparable");
                Console.WriteLine("[2] Sort pay amount in ascending order using IComparer");
                Console.WriteLine("[3] Sort by Social Security number in descending order using a selection sort and delegate");
                Console.WriteLine("[4] Sorting last name in ascending order and pay amount in descinding order by using LINQ");
                option = Console.ReadLine();

                if (option == "1")
                {
                    DescendingOrder(payableObjects);
                }
                else if (option == "2")
                {
                    Console.WriteLine("\nSort by Pay: Ascending Order");

                    Array.Sort(payableObjects, Employee.SortPayAscending());
                    foreach (IPayable emp in payableObjects)
                    {
                        Console.WriteLine(emp.ToString() + "\n" + emp.GetPaymentAmount() + "\n");
                    }
                }
                else if (option == "3")
                {
                    CompareDelegateSSN EmployeeCompareSSN = new CompareDelegateSSN(Employee.sortSSN);
                    SelectionSort(payableObjects, EmployeeCompareSSN);
                    foreach (IPayable emp in payableObjects)
                    {
                        Console.WriteLine(emp.ToString() + "\n");
                    }

                }
                else if (option == "4")
                {
                    List<Employee> emp = new List<Employee>();
                    for (int i = 0; i <= 7; i++)
                    {
                        emp.Add((Employee)payableObjects[i]);
                    }

                    var orderByResult = from e in emp orderby e.LastName, e.GetPaymentAmount() descending select e;
                    foreach (var empl in orderByResult)
                    {
                        Console.WriteLine(empl.ToString() + "\n");
                    }
                }
                else
                {
                    Console.WriteLine("Exiting Out of Application");
                    cont = false;
                }

            }
        }

        /// <summary>
        /// Method that aids in Descending the Employee's by Last name
        /// </summary>
        /// <param name="payableObjects">An object list that contains the employee list with information</param>
        public static void DescendingOrder(IPayable[] payableObjects)
        {
            List<Employee> e = new List<Employee>();
            for (int i = 0; i <= 7; i++)
            {
                e.Add((Employee)payableObjects[i]);
            }

            e.Sort(delegate (Employee x, Employee y)
            {
                if (x.LastName == null && y.LastName == null)
                {
                    return 0;
                }
                else if (x.LastName == null)
                {
                    return -1;
                }
                else if (y.LastName == null)
                {
                    return -1;
                }
                else
                {
                    return y.LastName.CompareTo(x.LastName);
                }

            });

            Console.WriteLine("Sort by name");
            foreach (Employee emp in e)
            {
                Console.WriteLine(emp + "\n");
            }
        }

        /// <summary>
        /// Method that contains the algorith that aids in sorting employee list with SSN using the Selection sort algorithm
        /// </summary>
        /// <param name="arr">list that contains Employee list</param>
        /// <param name="compSSN">delegate of Class</param>
        public static void SelectionSort(IPayable[] empl, CompareDelegateSSN compSSN )
        {
            int n = empl.Length;
            for(int i = 0; i < n - 1; i++)
            {
                int ind = i;
                for(int j = i + 1; j < n; j++)
                {
                    Employee emp1 = (Employee) empl[j];
                    Employee emp2 = (Employee) empl[ind];

                    if (compSSN(empl[j],empl[ind]) < 0)
                    {
                        ind = j;
                    }
                }

                IPayable temp = empl[ind];
                empl[ind] = empl[i];
                empl[i] = temp;
            }
        }
    } // end class PayrollSystemTest
}
