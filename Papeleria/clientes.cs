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
    public partial class clientes : Form
    {

        public clientes()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            dataGridViewCliente.AutoGenerateColumns = false;
            codigo.DataPropertyName = "codigo_cli";
            nombre.DataPropertyName = "nombre_cli";
            telefono.DataPropertyName = "telefono_cli";
            RNC.DataPropertyName = "rnc_cli";
            direccion.DataPropertyName = "direccion_cli";
            llenarDataGrid("no");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox1.SelectedIndex = 0;
            llenarDataGrid("no");
        }

        public void llenarDataGrid(string condicion)
        {
            string cmd = "SELECT * from clientes where estado =1 ";
            if (condicion != "no")
            {
                cmd += condicion;
            }
            DataSet DS = new DataSet();
            DS = FuncionesGenerales.FuncionesGenerales.ExecuteReader(cmd, "Error al traer los clientes, por favor intente de nuevo.");
            if (DS.Tables.Count > 0)
            {
                dataGridViewCliente.DataSource = DS.Tables[0];

            }


        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox1.Text == "")
            {
                llenarDataGrid("no");
                return;
            }
            string condicion = " and ";
            if (comboBox1.SelectedIndex == 0)
            {
                condicion += "nombre_cli";
            }
            else 
            {
                condicion += "codigo_cli";
            }
            
            condicion += string.Format(" like '%{0}%'", textBox1.Text);
            llenarDataGrid(condicion);
        }

        private void clientes_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            modalCLientes modCli = new modalCLientes();
            modCli.flowLayoutPanel2.Enabled = false;
            modCli.flowLayoutPanel3.Enabled = false;
            modCli.ShowDialog();
            llenarDataGrid("no");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            modalCLientes modCli = new modalCLientes();
            modCli.flowLayoutPanel1.Enabled = false;
            modCli.codigoCli = dataGridViewCliente.Rows[dataGridViewCliente.CurrentCell.RowIndex].Cells[0].Value.ToString();
            modCli.txt_nombre.Text = dataGridViewCliente.Rows[dataGridViewCliente.CurrentCell.RowIndex].Cells[1].Value.ToString();
            modCli.txt_telefono.Text = dataGridViewCliente.Rows[dataGridViewCliente.CurrentCell.RowIndex].Cells[2].Value.ToString();
            modCli.txt_direccion.Text = dataGridViewCliente.Rows[dataGridViewCliente.CurrentCell.RowIndex].Cells[3].Value.ToString();
            modCli.txt_rnc.Text = dataGridViewCliente.Rows[dataGridViewCliente.CurrentCell.RowIndex].Cells[4].Value.ToString();
            modCli.ShowDialog();
            llenarDataGrid("no");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (dataGridViewCliente.Rows.Count > 0)
            {
                DialogResult = DialogResult.OK;
                this.Hide();
            }
        }
    }
}
