using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSB_ClassLibrary
{
    public class CustomerServices
    {
        string connstr = GSB_ClassLibrary.Utility.GetConnectionString();
        private int customerID;
    
        private CustomerServices() { }

        public static CustomerServices getinstance()
        {
            return new CustomerServices();
        }

        public string getCustomerID()
        {
            return this.customerID.ToString();
        }

        public Customer FillCustomerFromReader(SqlDataReader reader)
        {
            Customer customer = new Customer();

            customer.CustomerID = (int)reader["CustomerID"];
            customer.CustomerName = (String)reader["CustomerName"];
            customer.YTDOrders = (int)reader["YTDOrders"];
            customer.YTDSales = (int)reader["YTDSales"];

            return customer;
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connstr))
            {
                try
                {
                    using (SqlCommand cmdAllCustomers =
                    new SqlCommand("SELECT * FROM [Customer]", connection))
                    {
                        connection.Open();
                        SqlDataReader reader = cmdAllCustomers.ExecuteReader();

                        while (reader.Read())
                        {
                            Customer customer = this.FillCustomerFromReader(reader);
                            customers.Add(customer);
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Une erreur est survenue : " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return customers;
        }

        public Customer GetCustomerByid(int id)
        {
            Customer customers = new Customer();

            using (SqlConnection connection = new SqlConnection(connstr))
            {
                try
                {
                    using (SqlCommand cmdAllCustomers =
                    new SqlCommand("SELECT * FROM [Customer] WHERE CustomerID =" + id , connection))
                    {
                        connection.Open();
                        SqlDataReader reader = cmdAllCustomers.ExecuteReader();

                        while (reader.Read())
                        {
                            customers = this.FillCustomerFromReader(reader);
                            
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Une erreur est survenue : " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return customers;
        }

        public bool addCustomer(string CustomerName)
        {
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand cmdNewCustomer = new SqlCommand("uspNewCustomer", conn);
            cmdNewCustomer.CommandType = CommandType.StoredProcedure;

            cmdNewCustomer.Parameters.Add(new SqlParameter("@CustomerName", SqlDbType.NVarChar, 40));
            cmdNewCustomer.Parameters["@CustomerName"].Value = CustomerName;
            cmdNewCustomer.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
            cmdNewCustomer.Parameters["@CustomerID"].Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                cmdNewCustomer.ExecuteNonQuery();
                this.customerID = (int)cmdNewCustomer.Parameters["@CustomerID"].Value;
                return true;
            }
            catch(Exception ex)
            {
                Exception exe = ex;
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        public bool addOrder(DateTime dtpOrderDate, decimal numOrderAMount)
        {
            SqlConnection conn = new SqlConnection(connstr);

               SqlCommand cmdNewOrder = new SqlCommand("uspPlaceNewOrder", conn);
               cmdNewOrder.CommandType = CommandType.StoredProcedure;

               cmdNewOrder.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
               cmdNewOrder.Parameters["@CustomerID"].Value = this.customerID;
               cmdNewOrder.Parameters.Add(new SqlParameter("@OrderDate", SqlDbType.DateTime, 8));
               cmdNewOrder.Parameters["@OrderDate"].Value = dtpOrderDate;
               cmdNewOrder.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Int));
               cmdNewOrder.Parameters["@Amount"].Value = numOrderAMount;
               cmdNewOrder.Parameters.Add(new SqlParameter("@Status", SqlDbType.Char, 1));
               cmdNewOrder.Parameters["@Status"].Value = "O";
               cmdNewOrder.Parameters.Add(new SqlParameter("@RC", SqlDbType.Int));
               cmdNewOrder.Parameters["@RC"].Direction = ParameterDirection.ReturnValue;

               try
               {
                   conn.Open();
                   cmdNewOrder.ExecuteNonQuery();
                   return true;
               }
               catch
               {
                   return false;
               }
               finally
               {
                   conn.Close();
               }
        }
        public DataTable getOrders(string OrderID)
        {
            SqlConnection conn = new SqlConnection(connstr);
            string sql = "select * from Orders where orderID = @orderID;";
            SqlCommand cmdOrderId = new SqlCommand(sql, conn);
            cmdOrderId.Parameters.Add(new SqlParameter("@orderID", SqlDbType.Int));
            cmdOrderId.Parameters["@orderID"].Value = OrderID;

            try
            {
                conn.Open();
                SqlDataReader rdr = cmdOrderId.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(rdr);
                return table;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool fillOrder(int orderID, DateTime dtpFillDate)
        {
             SqlConnection conn = new SqlConnection(connstr);
               SqlCommand cmdFillOrder = new SqlCommand("uspFillOrder", conn);
               cmdFillOrder.CommandType = CommandType.StoredProcedure;
               cmdFillOrder.Parameters.Add(new SqlParameter("@orderID", SqlDbType.Int));
               cmdFillOrder.Parameters["@orderID"].Value = orderID;
               cmdFillOrder.Parameters.Add(new SqlParameter("@FilledDate", SqlDbType.DateTime));
               cmdFillOrder.Parameters["@FilledDate"].Value = dtpFillDate;

               try
               {
                   conn.Open();
                   cmdFillOrder.ExecuteNonQuery();
                   return true;
               }
               catch(Exception ex)
               {
                   return false;
               }
               finally
               {
                   conn.Close();
               }
        }

        public bool cancelOrder(int orderID)
        {
            SqlConnection conn = new SqlConnection(connstr);
                SqlCommand cmdCancel = new SqlCommand("uspCancelOrder", conn);
                cmdCancel.CommandType = CommandType.StoredProcedure;
                cmdCancel.Parameters.Add(new SqlParameter("@orderID", SqlDbType.Int));
                cmdCancel.Parameters["@orderID"].Value = orderID;

                try
                {
                    conn.Open();
                    cmdCancel.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    conn.Close();
                }
        }

        public DataTable listCustomer()
        {
            SqlConnection conn = new SqlConnection(connstr);
            string sql = "select * from Customer";
            SqlCommand cmdOrderId = new SqlCommand(sql, conn);

            try
            {
                conn.Open();
                SqlDataReader rdr = cmdOrderId.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(rdr);
                return table;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }

}
