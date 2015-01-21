using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace GSBHopital
{
    public partial class FillOrCancel : Form
    {
        private int parsedOrderID;
        string connstr = GSBHopital.Utility.GetConnectionString();


        public FillOrCancel()
        {
            InitializeComponent();
        }

        private bool isOrderID()
        {
            if (txtOrderId.Text == String.Empty)
            {
                MessageBox.Show("Thank you to fill the order number.");
                return false;
            }
            else if (Regex.IsMatch(txtOrderId.Text, @"^\D*$")) {
                MessageBox.Show("Thank you to enter only numbers.");
                txtOrderId.Clear();
                return false;
            }
            else
            {
                parsedOrderID = Int32.Parse(txtOrderId.Text);
                return true;
            }
        }

        private void btnFindByIrderId_Click(object sender, EventArgs e)
        {
            if (isOrderID())
            {
                SqlConnection conn = new SqlConnection(connstr);
                string sql = "select * from Orders where orderID = @orderID;";
                SqlCommand cmdOrderId = new SqlCommand(sql, conn);
                cmdOrderId.Parameters.Add(new SqlParameter("@orderID", SqlDbType.Int));
                cmdOrderId.Parameters["@orderID"].Value = parsedOrderID;

                try
                {
                    conn.Open();
                    SqlDataReader rdr = cmdOrderId.ExecuteReader();
                    DataTable table = new DataTable();
                    table.Load(rdr);
                    this.dgvCustomeOrders.DataSource = table;
                    rdr.Close();
                }
                catch
                {
                    MessageBox.Show("The request could not be performed.");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            if (isOrderID())
            {
                SqlConnection conn = new SqlConnection(connstr);
                SqlCommand cmdCancel = new SqlCommand("uspCancelOrder", conn);
                cmdCancel.CommandType = CommandType.StoredProcedure;
                cmdCancel.Parameters.Add(new SqlParameter("@orderID", SqlDbType.Int));
                cmdCancel.Parameters["@orderID"].Value = parsedOrderID;

                try
                {
                    conn.Open();
                    cmdCancel.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("The request could not be performed.");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btnFillOrder_Click(object sender, EventArgs e)
        {
            if (isOrderID())
            {
                SqlConnection conn = new SqlConnection(connstr);
                SqlCommand cmdFillOrder = new SqlCommand("uspFillOrder", conn);
                cmdFillOrder.CommandType = CommandType.StoredProcedure;
                cmdFillOrder.Parameters.Add(new SqlParameter("@orderID", SqlDbType.Int));
                cmdFillOrder.Parameters["@orderID"].Value = parsedOrderID;
                cmdFillOrder.Parameters.Add(new SqlParameter("@FilledDate", SqlDbType.DateTime));
                cmdFillOrder.Parameters["@FilledDate"].Value = dtpFillDate.Value;

                try
                {
                    conn.Open();
                    cmdFillOrder.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("The request could not be performed.");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btnFinishUpdates_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
