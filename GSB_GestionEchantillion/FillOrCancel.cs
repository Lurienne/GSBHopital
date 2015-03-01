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
using GSB_ClassLibrary;

namespace GSBHopital
{
    public partial class FillOrCancel : Form
    {
        private int parsedOrderID;
        private CustomerServices CustomerService = GSB_ClassLibrary.CustomerServices.getinstance();


        public FillOrCancel()
        {
            InitializeComponent();
        }

        private bool isOrderID()
        {
            if (txtOrderId.Text == String.Empty)
            {
                MessageBox.Show("Merci de remplir le numero de la commande.");
                return false;
            }
            else if (Regex.IsMatch(txtOrderId.Text, @"^\D*$")) {
                MessageBox.Show("Merci d'entrer seulement des nombres.");
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
                DataTable table = CustomerService.getOrders(txtOrderId.Text);

                this.dgvCustomeOrders.DataSource = table; 
              
            }
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            if (isOrderID())
            {
                if (!CustomerService.cancelOrder(int.Parse(txtOrderId.Text)))
                {
                    MessageBox.Show("La requete ne peut pas être effectuée.");
                }
            }
        }

        private void btnFillOrder_Click(object sender, EventArgs e)
        {
            if (isOrderID())
            {
                if (!CustomerService.fillOrder(int.Parse(txtOrderId.Text), dtpFillDate.Value))
                {
                    MessageBox.Show("La requete ne peut pas être effectuée.");
                }
            }
        }

        private void btnFinishUpdates_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dgvCustomeOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
