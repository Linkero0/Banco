using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeviceLibrary;
using DeviceLibrary.Models;
namespace Banco
{
    public partial class inicioForm : Form
    {

        public inicioForm()
        {
        
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Billete = 0
            //Moneda = 1

        }

       

        private void Form1_Click(object sender, EventArgs e)
        {
            this.Hide();

            LoginForm loginform = new LoginForm();

            loginform.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();

            LoginForm loginform = new LoginForm();

            loginform.Show();
        }

        private void btnAcepted_Click(object sender, EventArgs e)
        {
            DeviceLibrary.DeviceLibrary machine = new DeviceLibrary.DeviceLibrary();

            MessageBox.Show("Hola");
            Document insertMoney = new Document(Convert.ToDecimal(25), DeviceLibrary.Models.Enums.DocumentType.Bill, 1);
           
        }
    }
}
