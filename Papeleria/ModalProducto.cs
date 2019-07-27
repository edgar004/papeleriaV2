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

    public partial class ModalProducto : Form
    {
        public string idPro;

        public ModalProducto()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string cmd = $"insert into productos  (nom_pro,codigo_pro,cantidad,itbis,estanteria,precio,tipoVenta_pro)  values ('{txt_nombre.Text}','{txt_codigo.Text}','{txt_cantidad.Text}','{txt_itbis.Text}','{txt_estanteria.Text}','{txt_precio.Text}','{comboTipoVenta.SelectedItem}')";
                int resp = FuncionesGenerales.FuncionesGenerales.EjecutarQuery(cmd, "Error al registrar el producto.");
                if (resp > 0)
                {
                    MessageBox.Show($"El producto {txt_nombre.Text} se ha registrado correctamente.");
                    FuncionesGenerales.FuncionesGenerales.limpiarCOntroles(this);
                }
                else
                {
                    MessageBox.Show($"Error al registrar el producto {txt_nombre.SelectedText}.");
                }

            }
            catch (Exception)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            
            string cmd = $"update productos set nom_pro = '{txt_nombre.Text}', codigo_pro='{txt_codigo.Text}', cantidad={txt_cantidad.Text}, itbis={txt_itbis.Text},estanteria='{txt_estanteria.Text}',precio={txt_precio.Text}, tipoVenta_pro = '{comboTipoVenta.SelectedItem}' where id_pro={idPro}";
            int resp = FuncionesGenerales.FuncionesGenerales.EjecutarQuery(cmd, "Error al modificar el producto.");
            if (resp > 0)
            {
                MessageBox.Show($"El producto {txt_nombre.Text} se ha modificado correctamente.");
            }
            else
            {
                MessageBox.Show($"Error al modificar el producto {txt_nombre.Text}.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("¿Seguro que quieres eliminar el producto?", "Eliminar", MessageBoxButtons.YesNo);
            if (res == DialogResult.No) return;
            try
            {
                string cmd = $"update productos set estado=0 where id_pro = {idPro}";
                int resp = FuncionesGenerales.FuncionesGenerales.EjecutarQuery(cmd, "Error al eliminar el producto.");
                if (resp > 0)
                {
                    MessageBox.Show($"El producto {txt_nombre.Text} se ha eliminado correctamente.");
                    FuncionesGenerales.FuncionesGenerales.limpiarCOntroles(this);
                    button1.Enabled = true;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    idPro = "";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al eliminar el producto.");
            }
            
        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            FuncionesGenerales.FuncionesGenerales.SoloNumeros(e);
        }

        private void txt_itbis_KeyPress(object sender, KeyPressEventArgs e)
        {
            FuncionesGenerales.FuncionesGenerales.SoloNumeros(e);
        }
    }
}
