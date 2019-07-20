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
    public partial class facturacion : Form
    {
        public facturacion()
        {
            InitializeComponent();
        }

        private void facturacion_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel3_Click(object sender, EventArgs e)
        {
            inventario obj = new inventario();

            if (obj.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = obj.dataGridViewProducto.Rows[obj.dataGridViewProducto.CurrentCell.RowIndex].Cells[0].Value.ToString();
            }
        }
    }
}
