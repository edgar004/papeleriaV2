using System;
using System.Data;
using System.Windows.Forms;

namespace Papeleria
{
    public partial class cotizacion : Form
    {

        int maximaCant = 0;
        Double impuestoPro = 0;
        bool seleciono = false;
        string idPro;

        public cotizacion()
        {
            InitializeComponent();
            dateTimePicker1.Text = DateTime.Now.ToString();


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
            try
            {
                inventario pro = new inventario();
                pro.btnAdd.Visible = false;
                pro.button2.Visible = false;
                if (pro.ShowDialog() == DialogResult.OK)
                {
                    idPro = pro.dataGridViewProducto.Rows[pro.dataGridViewProducto.CurrentCell.RowIndex].Cells[0].Value.ToString();
                    txt_codigoPro.Text = pro.dataGridViewProducto.Rows[pro.dataGridViewProducto.CurrentCell.RowIndex].Cells[1].Value.ToString();
                    txt_nombrePro.Text = pro.dataGridViewProducto.Rows[pro.dataGridViewProducto.CurrentCell.RowIndex].Cells[2].Value.ToString();
                    txt_precioPro.Text = pro.dataGridViewProducto.Rows[pro.dataGridViewProducto.CurrentCell.RowIndex].Cells[6].Value.ToString();
                    txt_cantidadPro.Text = "1";
                    impuestoPro = Convert.ToInt32(pro.dataGridViewProducto.Rows[pro.dataGridViewProducto.CurrentCell.RowIndex].Cells[4].Value.ToString());
                    maximaCant = Convert.ToInt32(pro.dataGridViewProducto.Rows[pro.dataGridViewProducto.CurrentCell.RowIndex].Cells[3].Value.ToString());

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro al traer el producto, por favor intente de nuevo.");
            }
           
        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel4_Click(object sender, EventArgs e)
        {

            if (txt_nombrePro.Text.Trim().Equals(""))
            {
                MessageBox.Show("Por favor seleccionar un producto válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txt_cantidadPro.Text.Trim().Equals(""))
            {
                MessageBox.Show("Por favor digite una cantidad.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            if (Convert.ToInt32(txt_cantidadPro.Text.Trim()) > maximaCant)
            {
                MessageBox.Show($"La cantidad máxima disponible del producto {txt_nombrePro.Text.Trim()} son {maximaCant} ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Double totalFact = 0;
            Double totalItbis = 0;
            bool existePro = false;
            foreach (DataGridViewRow registsros in dataGridViewProducto.Rows)
            {
                if (registsros.Cells[0].Value.ToString() == txt_codigoPro.Text.Trim())
                {
                    if (seleciono)
                    {

                        registsros.Cells[2].Value = txt_precioPro.Text.Trim();
                        registsros.Cells[3].Value = txt_cantidadPro.Text.Trim();
                        seleciono = false;
                    }
                    else
                    {
                        int cantidaSuma = Convert.ToInt32(registsros.Cells[3].Value) + Convert.ToInt32(txt_cantidadPro.Text.Trim());
                        if (cantidaSuma > maximaCant)
                        {
                            MessageBox.Show($"La cantidad máxima disponible del producto {txt_nombrePro.Text.Trim()} son {maximaCant} ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        registsros.Cells[3].Value = cantidaSuma;
                    }
                    Double importe = Convert.ToInt32(registsros.Cells[3].Value) * Convert.ToInt32(registsros.Cells[2].Value);
                    registsros.Cells[5].Value = importe * (Convert.ToDouble(impuestoPro / 100));
                    registsros.Cells[6].Value = importe.ToString();
                    existePro = true;


                }

                totalFact += Convert.ToDouble(registsros.Cells[6].Value);
                totalItbis += Convert.ToDouble(registsros.Cells[5].Value);

            }

            if (!existePro)
            {
                Double importe = Convert.ToDouble(txt_precioPro.Text.Trim()) * Convert.ToDouble(txt_cantidadPro.Text.Trim());
                Double itbis = importe * (Convert.ToDouble(impuestoPro / 100)) ;
                dataGridViewProducto.Rows.Add(txt_codigoPro.Text.Trim(), txt_nombrePro.Text.Trim(), txt_precioPro.Text.Trim(), txt_cantidadPro.Text.Trim(), impuestoPro, itbis, importe, idPro);
                seleciono = false;
                totalFact += importe;
                totalItbis += itbis;
            }

            txt_codigoPro.Text = "";
            txt_nombrePro.Text = "";
            txt_precioPro.Text = "";
            txt_cantidadPro.Text="";
            txt_codigoPro.Focus();
            txtSubTotal.Text = totalFact.ToString();
            txtTotalItbis.Text = totalItbis.ToString();
            txt_totalFactura.Text = (totalFact + totalItbis).ToString();

          
       
        }

        private void cotizacion_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel6_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("¿Seguro que quieres limpiar toda la factura?", "Limpiar", MessageBoxButtons.YesNo);
            if (res == DialogResult.No) return;
            FuncionesGenerales.FuncionesGenerales.limpiarCOntroles(this);
        }

        private void flowLayoutPanel5_Click(object sender, EventArgs e)
        {

            try
            {

            }catch (Exception) { 
}
            if (dataGridViewProducto.Rows.Count > 0)
            {
                string codigoCot = FuncionesGenerales.FuncionesGenerales.GenerarCodigoTabla("COT", "cotizaciones", "Error al registrar la cotización, por favor intente de nuevo.");
                if (codigoCot == "")
                {
                    MessageBox.Show("Error al registrar la cotización, por favor intente de nuevo.");
                    return;
                }
                DataSet DS = new DataSet();
                DateTime fecha = Convert.ToDateTime(dateTimePicker1.Text, new System.Globalization.CultureInfo("es-ES"));



                if ((Convert.ToDouble(txt_totalFactura.Text) % 1) < 0.5)
                {
                    txt_totalFactura.Text = Math.Round(Convert.ToDouble(txt_totalFactura.Text)).ToString();
                }
                else
                {
                    txt_totalFactura.Text = Math.Round(Convert.ToDouble(txt_totalFactura.Text)).ToString();

                }


                if ((Convert.ToDouble(txt_totalFactura.Text) % 1) < 0.5)
                {
                    txt_totalFactura.Text = Math.Round(Convert.ToDouble(txt_totalFactura.Text)).ToString();
                }
                else
                {
                    txt_totalFactura.Text = Math.Round(Convert.ToDouble(txt_totalFactura.Text)).ToString();

                }



                if ((Convert.ToDouble(txtTotalItbis.Text) % 1) < 0.5)
                {
                    txtTotalItbis.Text = Math.Round(Convert.ToDouble(txtTotalItbis.Text)).ToString();
                }
                else
                {
                    txtTotalItbis.Text = Math.Round(Convert.ToDouble(txtTotalItbis.Text)).ToString();

                }


                if ((Convert.ToDouble(txtSubTotal.Text) % 1) < 0.5)
                {
                    txtSubTotal.Text = Math.Round(Convert.ToDouble(txtSubTotal.Text)).ToString();
                }
                else
                {
                    txtSubTotal.Text = Math.Round(Convert.ToDouble(txtSubTotal.Text)).ToString();

                }


                string cmd = $"insert into cotizaciones values('{codigoCot}','{txt_codigoCliente.Text}','{fecha.ToString("yyyy-MM-dd")}',{txt_totalFactura.Text},{txtTotalItbis.Text},{txtSubTotal.Text})";
                int res = FuncionesGenerales.FuncionesGenerales.EjecutarQuery(cmd, "Error al crear la cotización.");
                if (res == 0)
                {
                    return;
                }
                    foreach (DataGridViewRow productos in dataGridViewProducto.Rows)
                    {
                        cmd =$"insert into detalles_cotizacion values ('{codigoCot}',{productos.Cells[7].Value},{productos.Cells[3].Value},{productos.Cells[2].Value},{productos.Cells[4].Value})";
                        DS = FuncionesGenerales.FuncionesGenerales.ExecuteReader(cmd,$"Error al agregar el producto {productos.Cells[1].Value} a la cotización.");
                    }

               
                cmd = $"EXEC COT  '{codigoCot}'";
                DS = FuncionesGenerales.FuncionesGenerales.ExecuteReader(cmd, "Error al crear EL PDF de la cotización");
                    reporteCOT obj = new reporteCOT();
                    obj.reportViewer1.LocalReport.DataSources[0].Value = DS.Tables[0];
                    obj.ShowDialog();
                    FuncionesGenerales.FuncionesGenerales.limpiarCOntroles(this);

              
                


            }
        }

        private void flowLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
