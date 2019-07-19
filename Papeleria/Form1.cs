using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Papeleria
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel5_Click(object sender, EventArgs e)
        {
            comprobantes com = new comprobantes();
            com.TopLevel = false;
            com.Visible = true;
            contenedor.Controls.Add(com);
        }

        private void flowLayoutPanel1_Click(object sender, EventArgs e)
        {
            facturacion fac = new facturacion();
            fac.TopLevel = false;
            fac.Visible = true;
            contenedor.Controls.Add(fac);
        }

        private void flowLayoutPanel2_Click(object sender, EventArgs e)
        {
            cotizacion cot = new cotizacion();
            cot.TopLevel = false;
            cot.Visible = true;
            contenedor.Controls.Add(cot);
        }

        private void flowLayoutPanel3_Click(object sender, EventArgs e)
        {
            inventario inv = new inventario();
            inv.TopLevel = false;
            inv.Visible = true;
            contenedor.Controls.Add(inv);
        }

        private void flowLayoutPanel4_Click(object sender, EventArgs e)
        {
            clientes cli = new clientes();
            cli.TopLevel = false;
            cli.Visible = true;
            contenedor.Controls.Add(cli);
        }
    }
}
