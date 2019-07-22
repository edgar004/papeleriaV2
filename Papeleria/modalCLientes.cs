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
    public partial class modalCLientes : Form
    {
        public string codigoCli;
        public modalCLientes()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Click(object sender, EventArgs e)
        {
            try
            {
                string codigoCli = FuncionesGenerales.FuncionesGenerales.GenerarCodigoTabla("CLI","clientes","Error al registrar el cliente, por favor intente de nuevo.");
                if (codigoCli=="")
                {
                    MessageBox.Show("Error al registrar el cliente, por favor intente de nuevo.");
                    return;
                }
                string cmd = $"insert into clientes  (codigo_cli,nombre_cli,direccion_cli,rnc_cli,telefono_cli)  values ('{codigoCli}', '{txt_nombre.Text}','{txt_nombre.Text}','{txt_direccion.Text}','{txt_rnc.Text}')";
                int resp = FuncionesGenerales.FuncionesGenerales.EjecutarQuery(cmd, "Error al registrar el cliente.");
                if (resp > 0)
                {
                    MessageBox.Show($"El cliente {txt_nombre.Text} se ha registrado correctamente.");
                    FuncionesGenerales.FuncionesGenerales.limpiarCOntroles(this);
                }
            }
            catch (Exception)
            {

            }
        }

        private void flowLayoutPanel2_Click(object sender, EventArgs e)
        {
            try
            {
                string cmd = $"update clientes  set nombre_cli = '{txt_nombre.Text}' ,direccion_cli = '{txt_direccion.Text}' ,rnc_cli = '{txt_rnc.Text}',telefono_cli = '{txt_telefono.Text}' where codigo_cli ='{codigoCli}'";
                int resp = FuncionesGenerales.FuncionesGenerales.EjecutarQuery(cmd, "Error al modificar el cliente.");
                if (resp > 0)
                {
                    MessageBox.Show($"El cliente {txt_nombre.Text} se ha modificado correctamente.");
                }

            }
            catch (Exception) { }
        }

        private void flowLayoutPanel3_Click(object sender, EventArgs e)
        {
            try
            {
                string cmd = $"update clientes set estado=0 where codigo_cli = '{codigoCli}'";
                int resp = FuncionesGenerales.FuncionesGenerales.EjecutarQuery(cmd, "Error al eliminar el cliente.");
                if (resp > 0)
                {
                    MessageBox.Show($"El cliente {txt_nombre.Text} se ha eliminado correctamente.");
                    FuncionesGenerales.FuncionesGenerales.limpiarCOntroles(this);
                    flowLayoutPanel1.Enabled = true;
                    flowLayoutPanel2.Enabled = false;
                    flowLayoutPanel3.Enabled = false;
                    codigoCli = "";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al eliminar el producto.");
            }

        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {
            DialogResult res = MessageBox.Show("¿Seguro que quieres eliminar el producto?", "Eliminar", MessageBoxButtons.YesNo);
            if (res == DialogResult.No) return;
        }
    }
}
