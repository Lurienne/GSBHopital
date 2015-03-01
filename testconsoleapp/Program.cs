using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSB_ClassLibrary;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerServices CustomerServices = GSB_ClassLibrary.CustomerServices.getInstance();
            
            List<Customer> customers = CustomerServices.GetCustomers();

            foreach (Customer c in customers)
                Console.WriteLine(c.CustomerID + ". " + c.CustomerName);
                   
            bool ok = true;
           
            do
            {
                Console.Write("\r\nVoulez vous cherchez un id? O/N ");
                String reponse = Console.ReadLine();

                if (reponse == "O" || reponse == "o")
                {                 
                    bool ok2 = true;
                    do
                    {
                        Console.Write("Entrez un id : ");
                        try
                        {
                            int customerID = (int)Convert.ToInt32(Console.ReadLine());
                            Customer customer = CustomerServices.GetCustomerById(customerID);           
                           
                            if (customer.CustomerName == null)
                            {
                                Console.WriteLine("Cette id n'existe pas.");
                                ok2 = false;
                            }                                 
                            else
                            {
                                Console.WriteLine(customer.CustomerID + ". " + customer.CustomerName);
                                ok2 = true;
                            }                               
                            
                            if (ok2 == true)
                            {
                                bool ok3 = true;
                                do
                                {
                                    Console.Write("Voulez vous recherchez un autre id? O/N ");
                                    String reponse2 = Console.ReadLine();
                                    if (reponse2 == "o" || reponse2 == "O")
                                    {
                                        ok3 = true;
                                        ok2 = false;
                                    }
                                    else if (reponse2 == "n" || reponse2 == "N")
                                    {
                                        Environment.Exit(0);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Reponse invalide!");
                                        ok3 = false;
                                    }
                                } while (ok3 != true);
                            }                 
                        }
                        catch
                        {
                            Console.WriteLine("Veuillez renseigner un chiffre");
                            ok2 = false;
                        }
                    } while (ok2 != true);                
                } 
                else if(reponse == "n" || reponse == "N")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Reponse invalide!");
                    ok = false;
                }
            } while (ok != true);
                                 
            Console.ReadLine();
        }
    }
}
