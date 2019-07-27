using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Papeleria
{
    

    public partial class comprobantes : Form
    {
        public class Comprobate
        {
            public string id_com { get; set; }
            public string tipo_com { get; set; }
            public string serie_com { get; set; }
            public string fechaVencimiento_com { get; set; }
            public string usados_com { get; set; }
            public string cantidadLimite_com { get; set; }
            public string estado { get; set; }


        }

        List<Comprobate> CombrobantesListas = new List<Comprobate>();

        public comprobantes()
        {
            InitializeComponent();
            fechaVencimiento.MinDate = DateTime.Now;
            comboCom.DisplayMember = "tipo_com";
            comboCom.ValueMember = "id_com";
            DataSet ds = new DataSet();
            ds = FuncionesGenerales.FuncionesGenerales.ExecuteReader("select * from comprobantes where estado=1", "Error al traer los comprobantes");
            comboCom.DataSource = ds.Tables[0];
            foreach (DataRow row in ds.Tables[0].Rows)
            {

                CombrobantesListas.Add(new Comprobate()
                {
                    id_com = Convert.ToString(row["id_com"]),
                    tipo_com = Convert.ToString(row["tipo_com"]),
                    serie_com = Convert.ToString(row["serie_com"]),
                    fechaVencimiento_com = Convert.ToString(row["fechaVencimiento_com"]),
                    usados_com = Convert.ToString(row["usados_com"]),
                    cantidadLimite_com = Convert.ToString(row["cantidadLimite_com"]),
                    estado = Convert.ToString(row["estado"]),
                });

            }

            txt_serie.Text = CombrobantesListas[0].serie_com;
            txt_usados.Text = CombrobantesListas[0].usados_com;
            txt_cantidad.Text = CombrobantesListas[0].cantidadLimite_com;
            fechaVencimiento.Text = CombrobantesListas[0].fechaVencimiento_com.ToString();

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime fecha_Vencimiento = Convert.ToDateTime(fechaVencimiento.Text, new CultureInfo("es-ES"));
            string cmd = $"update comprobantes set fechaVencimiento_com = '{fecha_Vencimiento.ToString("yyyy-MM-dd")}', cantidadLimite_com = {txt_cantidad.Text} where id_com = {comboCom.SelectedValue}";
            int resp = FuncionesGenerales.FuncionesGenerales.EjecutarQuery(cmd,"Error al modificar el comprobante.");
            if (resp>0)
            {
                MessageBox.Show($"El comprobante {comboCom.SelectedText} se ha modificado correctamente.");
                 CombrobantesListas[0].cantidadLimite_com = txt_cantidad.Text;
                 CombrobantesListas[0].fechaVencimiento_com = fechaVencimiento.Text;
            }
            else
            {
                MessageBox.Show($"Error al modificar el comprobante {comboCom.SelectedText}.");
            }
        }

        private void comboCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int indexSeleccionado = comboCom.SelectedIndex;
                if (indexSeleccionado < 0) return;
                txt_serie.Text = CombrobantesListas[indexSeleccionado].serie_com;
                txt_usados.Text = CombrobantesListas[indexSeleccionado].usados_com;
                txt_cantidad.Text = CombrobantesListas[indexSeleccionado].cantidadLimite_com;
                fechaVencimiento.Text = CombrobantesListas[indexSeleccionado].fechaVencimiento_com.ToString();
            }
            catch (Exception)
            {

            }
            

        }

        private void comprobantes_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_usados_KeyPress(object sender, KeyPressEventArgs e)
        {
            FuncionesGenerales.FuncionesGenerales.SoloNumeros(e);
        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            FuncionesGenerales.FuncionesGenerales.SoloNumeros(e);
        }
    }
}
