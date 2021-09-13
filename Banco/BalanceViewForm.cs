using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Banco
{
    public partial class BalanceViewForm : Form
    {
        Cuentas cuentaPay;
        public BalanceViewForm(Cuentas cuenta)
        {
            InitializeComponent();
            cuentaPay = cuenta;
            label2.Text = cuentaPay.account;
            label4.Text = cuentaPay.user;
            label6.Text = cuentaPay.debt.ToString();

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();

            LoginForm login = new LoginForm();

            login.Show();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            PaymentForm payment = new PaymentForm(cuentaPay);
            this.Close();
            payment.Show();
                     
        }

        private void BalanceViewForm_Load(object sender, EventArgs e)
        {

        }
    }
}
