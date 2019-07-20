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
    public partial class inventario : Form
    {
        public inventario()
        {
            InitializeComponent();

        }

        private void inventario_Load(object sender, EventArgs e)
        {
            dataGridViewProducto.Rows.Add("PRO00001", "Regla", "50", "18", "-------", "100", "Por unidad");
            dataGridViewProducto.Rows.Add("PRO00002", "Saca punta", "25", "18", "-------", "25", "Por caja");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModalProducto modPro = new ModalProducto();
            modPro.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducto.Rows.Count > 0)
            {
                DialogResult = DialogResult.OK;
                this.Hide();
            }
        }
    }
}
