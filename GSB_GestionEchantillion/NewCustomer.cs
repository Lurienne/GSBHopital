using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace GSBHopital
{
    public partial class NewCustomer : Form
    {
        private int parseCustomerID;
        private int orderID;
        string connstr = GSBHopital.Utility.GetConnectionString();

        public NewCustomer()
        {
            InitializeComponent();
        }

        private bool isCustomerName()
        {
            if (txtCustomerName.Text == String.Empty)
            {
                MessageBox.Show("Merci de préciser un nom.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool isPlaceOrderReady()
        {
            if (txtCustomerId.Text == String.Empty)
            {
                MessageBox.Show("Thank you to create an account before placing an order.");
                return false;
            }
            else if (numOrderAMount.Value < 1) {
                MessageBox.Show("Thank you to fill the amount of the order.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if (isCustomerName())
            {
                SqlConnection conn= new SqlConnection(connstr);
                SqlCommand cmdNewCustomer = new SqlCommand("uspNewCustomer", conn);
                cmdNewCustomer.CommandType = CommandType.StoredProcedure;

                cmdNewCustomer.Parameters.Add(new SqlParameter("@CustomerName", SqlDbType.NVarChar, 40));
                cmdNewCustomer.Parameters["@CustomerName"].Value = txtCustomerName.Text;
                cmdNewCustomer.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
                cmdNewCustomer.Parameters["@CustomerID"].Direction = ParameterDirection.Output;

                try{
                    conn.Open();
                    cmdNewCustomer.ExecuteNonQuery();
                    this.parseCustomerID = (int)cmdNewCustomer.Parameters["@CustomerID"].Value;
                    this.txtCustomerId.Text = Convert.ToString(parseCustomerID);
                } 

                catch{
                    MessageBox.Show("Customer ID was not returned. Account could not be created.");
                }

                finally{
                    conn.Close();
                }

            }
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (isPlaceOrderReady())
            {
                SqlConnection conn = new SqlConnection(connstr);

                SqlCommand cmdNewOrder = new SqlCommand("uspPlaceNewOrder", conn);
                cmdNewOrder.CommandType = CommandType.StoredProcedure;

                cmdNewOrder.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
                cmdNewOrder.Parameters["@CustomerID"].Value = this.parseCustomerID;

                cmdNewOrder.Parameters.Add(new SqlParameter("@OrderDate", SqlDbType.DateTime, 8));
                cmdNewOrder.Parameters["@OrderDate"].Value = dtpOrderDate.Value;

                cmdNewOrder.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Int));
                cmdNewOrder.Parameters["@Amount"].Value = numOrderAMount.Value;

                cmdNewOrder.Parameters.Add(new SqlParameter("@Status", SqlDbType.Char, 1));
                cmdNewOrder.Parameters["@Status"].Value = "O";

                cmdNewOrder.Parameters.Add(new SqlParameter("@RC", SqlDbType.Int));
                cmdNewOrder.Parameters["@RC"].Direction = ParameterDirection.ReturnValue;

                try
                {
                    conn.Open();
                    cmdNewOrder.ExecuteNonQuery();
                    this.orderID = (int)cmdNewOrder.Parameters["@RC"].Value;
                    MessageBox.Show("The order was sent.");
                }
                catch
                {
                    MessageBox.Show("The order could not be performed.");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btnAddAnotherAccount_Click(object sender, EventArgs e)
        {
            txtCustomerId.Clear();
            txtCustomerName.Clear();
            dtpOrderDate.Value = DateTime.Now;

            numOrderAMount.Value = 0;
            this.parseCustomerID = 0;
            
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
