﻿using System;
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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }


        private void btnGoToAdd_Click(object sender, EventArgs e)
        {
            Form frm = new NewCustomer();
            frm.Show();
        }

        private void btnGoToFillOrCancel_Click(object sender, EventArgs e)
        {
            Form frm = new FillOrCancel();
            frm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnListCustomer_Click(object sender, EventArgs e)
        {
            Form frm = new ListCustomer();
            frm.ShowDialog();
        }
    }
}
