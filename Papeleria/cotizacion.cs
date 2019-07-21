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
    public partial class cotizacion : Form
    {


        public cotizacion()
        {
            InitializeComponent();
            lblFecha.Text = DateTime.Now.ToString();


        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void flowLayoutPanel1_Click(object sender, EventArgs e)
        {
            clientes cli = new clientes();
            cli.btnAdd.Visible = false;
            cli.button2.Visible = false;
            if (cli.ShowDialog() == DialogResult.OK)
            {
                txt_codigoCliente.Text = cli.dataGridViewCliente.Rows[cli.dataGridViewCliente.CurrentCell.RowIndex].Cells[0].Value.ToString();
                txt_rncCliente.Text = cli.dataGridViewCliente.Rows[cli.dataGridViewCliente.CurrentCell.RowIndex].Cells[4].Value.ToString();
                txt_nombreCliente.Text= cli.dataGridViewCliente.Rows[cli.dataGridViewCliente.CurrentCell.RowIndex].Cells[1].Value.ToString();
            }
        }

        private void flowLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboCom_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void flowLayoutPanel3_Click(object sender, EventArgs e)
        {
            inventario pro = new inventario();
            pro.btnAdd.Visible = false;
            pro.button2.Visible = false;
            if (pro.ShowDialog() == DialogResult.OK)
            {
                txt_codigoPro.Text = pro.dataGridViewProducto.Rows[pro.dataGridViewProducto.CurrentCell.RowIndex].Cells[1].Value.ToString();
                txt_nombrePro.Text = pro.dataGridViewProducto.Rows[pro.dataGridViewProducto.CurrentCell.RowIndex].Cells[2].Value.ToString();
                txt_precioPro.Text = pro.dataGridViewProducto.Rows[pro.dataGridViewProducto.CurrentCell.RowIndex].Cells[6].Value.ToString();
                txt_cantidadPro.Text = "1";
            }
        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
