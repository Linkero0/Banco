using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace Banco
{

    public partial class LoginForm : Form
    {
        string numeroDeCuenta;
        DialogResult Mensaje;
        Cuentas cuenta = null;
        int respuesta = 0;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "3";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "5";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "6";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "8";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "9";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "0";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            numeroDeCuenta = textBox1.Text;

            if (numeroDeCuenta.Length != 0)
            {
                textBox1.Text = numeroDeCuenta.Remove(numeroDeCuenta.Length - 1, 1);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {

            this.Close();

            inicioForm inicio = new inicioForm();

            inicio.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            numeroDeCuenta = "";

        }

        private async void btnContinuar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (numeroDeCuenta == " ")
            {
                Mensaje = MessageBox.Show("No se ha capturado un número de cuenta", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                await verifyAccountAsync(textBox1.Text.Trim());
                             
                if (cuenta.user==null)
                {

                    MessageBox.Show("Registro no encontrado");
                    this.Cursor = Cursors.Default;

                }
                else {
                    cuenta.account = textBox1.Text;
                    BalanceViewForm balanceView = new BalanceViewForm(cuenta);
                    this.Close();
                    balanceView.Show();
                }
            }
        }


        private async Task verifyAccountAsync(string account)
        {
            HttpClient client = new HttpClient();
            var url = $"https://api.xenterglobal.com:2053/account_balance?token=f0326dadf35701ca6af5013902189f9d&account=" + account;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
        
            HttpResponseMessage response = await client.GetAsync(url);
        
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                cuenta = JsonConvert.DeserializeObject<Cuentas>(json);
            }
            

        }


    }
}
