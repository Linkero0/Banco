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
        DeviceLibrary.DeviceLibrary machine = new DeviceLibrary.DeviceLibrary();
        int statusService = 0;
        public inicioForm()
        {


            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Billete = 0
            //Moneda = 1

            machine.Open();

            if (machine.Status == DeviceLibrary.Models.Enums.DeviceStatus.Disconnected)
            {
                label1.Text = "Actualmente no se encuentra \n disponible el servicio";
                statusService = 1;
                

            }
            if (machine.Status == DeviceLibrary.Models.Enums.DeviceStatus.Disabled)
            {
                label1.Text = "Servicio en mantenimiento \n  intente mas tarde";
                statusService = 1;

            }


        }



        private void Form1_Click(object sender, EventArgs e)
        {

            if (statusService ==0)
            {
                
                this.Hide();

                LoginForm loginform = new LoginForm();

                loginform.Show();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (statusService == 0)
            {
                
                this.Hide();

                LoginForm loginform = new LoginForm();

                loginform.Show();
            }
        }
        private void btnAcepted_Click(object sender, EventArgs e)
        {



            Document insertMoney = new Document(Convert.ToDecimal(25), DeviceLibrary.Models.Enums.DocumentType.Bill, 1);

        }

        private void inicioForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            machine.Close();
        }
    }
}
