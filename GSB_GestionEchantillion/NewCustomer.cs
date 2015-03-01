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
using GSB_ClassLibrary;

namespace GSBHopital
{
    public partial class NewCustomer : Form
    {
        private CustomerServices CustomerService = GSB_ClassLibrary.CustomerServices.getinstance();

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
                MessageBox.Show("Merci de créer un compte avant de passer une commande.");
                return false;
            }
            else if (numOrderAMount.Value < 1) {
                MessageBox.Show("Merci de remplir le montant de la commande.");
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
                CustomerService.addCustomer(txtCustomerName.Text);
                txtCustomerId.Text = CustomerService.getCustomerID();
            }
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (isPlaceOrderReady())
            {
                if (!CustomerService.addOrder(dtpOrderDate.Value, numOrderAMount.Value))
                {
                    MessageBox.Show("Cette commande ne peut pas être effectué.");
                }
            }
        }

        private void btnAddAnotherAccount_Click(object sender, EventArgs e)
        {
            txtCustomerId.Clear();
            txtCustomerName.Clear();
            dtpOrderDate.Value = DateTime.Now;

            numOrderAMount.Value = 0;       
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
