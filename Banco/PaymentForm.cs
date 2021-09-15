using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using DeviceLibrary;
using DeviceLibrary.Models;
using Newtonsoft.Json;
using RestSharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Banco
{
    public partial class PaymentForm : Form
    {
        Cuentas cuentaPayment;
        int Fail = 0;
        DialogResult Mensaje;
        List<Document> documentos = new List<Document>();

        Document insertMoney;
        DeviceLibrary.DeviceLibrary machine = new DeviceLibrary.DeviceLibrary();

        public PaymentForm(Cuentas cuenta)
        {
            InitializeComponent();
            cuentaPayment = cuenta;
            label2.Text = cuentaPayment.debt.ToString();
            label6.Text = cuentaPayment.debt.ToString();
            lblUser.Text = cuenta.user;
            lblAccount.Text = cuenta.account;


            Bloq(2);

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
            BalanceViewForm balanceView = new BalanceViewForm(cuentaPayment);

            balanceView.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            insertMoney = new Document(Convert.ToDecimal(200), DeviceLibrary.Models.Enums.DocumentType.Bill, 1);
            aceptedMoney(insertMoney, 200);
        }


        private void button1_Click(object sender, EventArgs e)
        {

            insertMoney = new Document(Convert.ToDecimal(500), DeviceLibrary.Models.Enums.DocumentType.Bill, 1);
            aceptedMoney(insertMoney, 500);

        }

        private void button5_Click(object sender, EventArgs e)
        {

            insertMoney = new Document(Convert.ToDecimal(100), DeviceLibrary.Models.Enums.DocumentType.Bill, 1);
            aceptedMoney(insertMoney, 100);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            insertMoney = new Document(Convert.ToDecimal(50), DeviceLibrary.Models.Enums.DocumentType.Bill, 1);
            aceptedMoney(insertMoney, 50);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            insertMoney = new Document(Convert.ToDecimal(20), DeviceLibrary.Models.Enums.DocumentType.Bill, 1);
            aceptedMoney(insertMoney, 20);
        }

        private void label7_Click(object sender, EventArgs e)
        {

            insertMoney = new Document(Convert.ToDecimal(10), DeviceLibrary.Models.Enums.DocumentType.Coin, 1);
            aceptedMoney(insertMoney, 10);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            insertMoney = new Document(Convert.ToDecimal(10), DeviceLibrary.Models.Enums.DocumentType.Coin, 1);
            aceptedMoney(insertMoney, 10);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            insertMoney = new Document(Convert.ToDecimal(5), DeviceLibrary.Models.Enums.DocumentType.Coin, 1);
            aceptedMoney(insertMoney, 5);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            insertMoney = new Document(Convert.ToDecimal(5), DeviceLibrary.Models.Enums.DocumentType.Coin, 1);
            aceptedMoney(insertMoney, 5);

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            insertMoney = new Document(Convert.ToDecimal(2), DeviceLibrary.Models.Enums.DocumentType.Coin, 1);
            aceptedMoney(insertMoney, 2);
        }

        private void label10_Click(object sender, EventArgs e)
        {

            insertMoney = new Document(Convert.ToDecimal(2), DeviceLibrary.Models.Enums.DocumentType.Coin, 1);
            aceptedMoney(insertMoney, 2);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            insertMoney = new Document(Convert.ToDecimal(2), DeviceLibrary.Models.Enums.DocumentType.Coin, 1);
            aceptedMoney(insertMoney, 1);
        }

        private void label9_Click(object sender, EventArgs e)
        {

            insertMoney = new Document(Convert.ToDecimal(1), DeviceLibrary.Models.Enums.DocumentType.Coin, 1);
            aceptedMoney(insertMoney, 1);
        }

        private void label11_Click(object sender, EventArgs e)
        {

            insertMoney = new Document(Convert.ToDecimal(.50), DeviceLibrary.Models.Enums.DocumentType.Coin, 1);
            aceptedMoney(insertMoney, Convert.ToDecimal(.50));
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            insertMoney = new Document(Convert.ToDecimal(.50), DeviceLibrary.Models.Enums.DocumentType.Coin, 1);
            aceptedMoney(insertMoney, Convert.ToDecimal(.50));
        }
        private void PaymentForm_Load(object sender, EventArgs e)
        {
            machine.Enable();
            //Connected: The device is connected.
            //Disconnected: The device is NOT connected.
            //Enabled: The device is currently accepting coins and bills.
            //Disabled: The device is currently NOT accepting coins or bills..Dispensing: The device is currently dispensing documents.


        }
        public void aceptedMoney(Document dc, Decimal dec)
        {
            try
            {
                machine.SimulateInsertion(dc);
                depositadoLabel.Text = "$" + (Convert.ToDecimal(depositadoLabel.Text.Remove(0, 1)) + dec).ToString();
                calculateDiff();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAbonar_Click(object sender, EventArgs e)
        {

            SQLLite InsertSQL = new SQLLite();
            decimal paid = Convert.ToDecimal(depositadoLabel.Text.Remove(0, 1));
            var payment = new sentPayment
            {
                account = cuentaPayment.account,
                paid = Convert.ToDecimal(depositadoLabel.Text.Remove(0, 1))

            };

            PostPayment(payment);
            if (Fail == 1)
            {
                MessageBox.Show("Error con el servicio.. intenta mas tarde", "Gracias.!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {

                InsertSQL.start(payment.account, cuentaPayment.debt, paid, DateTime.Now.ToString("yyyy/MM/dd HH:mm"));

            }

            if (Fail == 0)
            {
                decimal vuelto = Convert.ToDecimal(cuentaPayment.debt - paid);
                if (vuelto == 0) Mensaje = MessageBox.Show("Se ha liquidado la deuda", "Gracias.!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (vuelto < 0) Mensaje = MessageBox.Show("Se ha liquidado la deuda, tome su cambio " + (vuelto * -1).ToString(), "Gracias.!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (vuelto > 0) Mensaje = MessageBox.Show("Se ha realizado el abono de manera correcta", "Gracias.!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                machine.Dispense((vuelto * -1));

                inicioForm inicio = new inicioForm();
                machine.Disable();
                this.Close();
                inicio.Show();
            }
        }

        private async Task PostPayment(sentPayment pagoEnviado)
        {
            var client = new RestClient("https://api.xenterglobal.com:2053/transaction?token=f0326dadf35701ca6af5013902189f9d");
            var param = pagoEnviado;
            var request = new RestRequest();
            request.AddJsonBody(param);
            var response = client.Post(request);


            if (response.StatusCode.ToString().Trim() != "OK")
            {

                Fail = 1;
            }

        }

        public void calculateDiff()
        {

            decimal restante;

            restante = cuentaPayment.debt - Convert.ToDecimal(depositadoLabel.Text.Remove(0, 1));
            label6.Text = restante.ToString();
            if (restante <= 0)
            {
                Bloq(1);

            }
        }

        public void Bloq(int param)
        {
            //1 Bloquea
            //2 Desbloquea
            if (param == 1)
            {

                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                pictureBox1.Enabled = false;
                pictureBox2.Enabled = false;
                pictureBox3.Enabled = false;
                pictureBox4.Enabled = false;
                pictureBox5.Enabled = false;
                label7.Enabled = false;
                label8.Enabled = false;
                label9.Enabled = false;
                label10.Enabled = false;
                label11.Enabled = false;

            }
            if (param == 2)
            {
                btnAbonar.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                pictureBox1.Enabled = true;
                pictureBox2.Enabled = true;
                pictureBox3.Enabled = true;
                pictureBox4.Enabled = true;
                pictureBox5.Enabled = true;
                label7.Enabled = true;
                label8.Enabled = true;
                label9.Enabled = true;
                label10.Enabled = true;
                label11.Enabled = true;

            }
        }

    }

}

