using GSB_ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSBHopital
{
    public partial class ListCustomer : Form
    {
        public CustomerServices CustomerService = GSB_ClassLibrary.CustomerServices.getinstance();

        public ListCustomer()
        {
            InitializeComponent();
            DataTable table = CustomerService.listCustomer();
            this.dgvListCustomer.DataSource = table; 
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ListCustomer_Load(object sender, EventArgs e)
        {

        }

        private void dgvListCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
