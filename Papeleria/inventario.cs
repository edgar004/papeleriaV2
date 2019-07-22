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
            comboBusqueda.SelectedIndex = 0;
            dataGridViewProducto.AutoGenerateColumns = false;
            idPro.DataPropertyName = "id_pro";
            nombreTable.DataPropertyName = "nom_pro";
            codigoTable.DataPropertyName = "codigo_pro";
            cantidadTable.DataPropertyName = "cantidad";
            itbisTable.DataPropertyName = "itbis";
            estanteTable.DataPropertyName = "estanteria";
            precioTable.DataPropertyName = "precio";
            tipoventaTable.DataPropertyName = "tipoVenta_pro";
            llenarDataGrid("no");

        }

        public void llenarDataGrid(string condicion)
        {
            string cmd = "SELECT * from productos where estado =1 ";
            if (condicion != "no")
            {
                cmd += condicion;
            }
            DataSet DS = new DataSet();
            DS = FuncionesGenerales.FuncionesGenerales.ExecuteReader(cmd, "Error al traer los productos, por favor intente de nuevo.");
            if (DS.Tables.Count > 0)
            {
                dataGridViewProducto.DataSource = DS.Tables[0];

            }


        }

        private void inventario_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModalProducto modPro = new ModalProducto();
            modPro.button2.Enabled = false;
            modPro.button3.Enabled = false;
            modPro.ShowDialog();
            llenarDataGrid("no");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducto.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe selecionar un producto");
                return;
            }
            ModalProducto modPro = new ModalProducto();
            modPro.button1.Enabled = false;
            modPro.idPro= dataGridViewProducto.Rows[dataGridViewProducto.CurrentCell.RowIndex].Cells[0].Value.ToString();
            modPro.txt_codigo.Text = dataGridViewProducto.Rows[dataGridViewProducto.CurrentCell.RowIndex].Cells[1].Value.ToString();
            modPro.txt_nombre.Text = dataGridViewProducto.Rows[dataGridViewProducto.CurrentCell.RowIndex].Cells[2].Value.ToString();
            modPro.txt_cantidad.Text = dataGridViewProducto.Rows[dataGridViewProducto.CurrentCell.RowIndex].Cells[3].Value.ToString();
            modPro.txt_itbis.Text = dataGridViewProducto.Rows[dataGridViewProducto.CurrentCell.RowIndex].Cells[4].Value.ToString();
            modPro.txt_estanteria.Text = dataGridViewProducto.Rows[dataGridViewProducto.CurrentCell.RowIndex].Cells[5].Value.ToString();
            modPro.txt_precio.Text = dataGridViewProducto.Rows[dataGridViewProducto.CurrentCell.RowIndex].Cells[6].Value.ToString();
            modPro.comboTipoVenta.Text = dataGridViewProducto.Rows[dataGridViewProducto.CurrentCell.RowIndex].Cells[7].Value.ToString();
            modPro.ShowDialog();
            llenarDataGrid("no");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox1.Text == "")
            {
                llenarDataGrid("no");
                return;
            }
            string condicion = " and ";
            if (comboBusqueda.SelectedIndex == 0)
            {
                condicion += "codigo_pro";
            }
            else
            {
                condicion += "nom_pro";
            }

            condicion += string.Format(" like '%{0}%'", textBox1.Text);
            llenarDataGrid(condicion);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewProducto.Rows.Count > 0)
            {
                DialogResult = DialogResult.OK;
                this.Hide();
            }
        }
    }
}
