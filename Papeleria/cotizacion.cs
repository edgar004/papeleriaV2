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
                    impuestoPro = Convert.ToDouble(pro.dataGridViewProducto.Rows[pro.dataGridViewProducto.CurrentCell.RowIndex].Cells[4].Value.ToString());
                    maximaCant = Convert.ToInt32(pro.dataGridViewProducto.Rows[pro.dataGridViewProducto.CurrentCell.RowIndex].Cells[3].Value.ToString());

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al traer el producto, por favor intente de nuevo.");
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
                Double importe = (Convert.ToDouble(txt_precioPro.Text.Trim()) * Convert.ToDouble(txt_cantidadPro.Text.Trim()));
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
                if (txt_nombreCliente.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Debe elegir un cliente.");
                    return;
                }
                string codigoCot = FuncionesGenerales.FuncionesGenerales.GenerarCodigoTabla("COT", "cotizaciones", "Error al registrar la cotización, por favor intente de nuevo.");
                if (codigoCot == "")
                {
                    MessageBox.Show("Error al registrar la cotización, por favor intente de nuevo.");
                    return;
                }
                DataSet DS = new DataSet();
                DateTime fecha = Convert.ToDateTime(dateTimePicker1.Text, new System.Globalization.CultureInfo("es-ES"));





                string cmd = $"insert into cotizaciones values('{codigoCot}','{txt_codigoCliente.Text}','{fecha.ToString("yyyy-MM-dd")}',{txt_totalFactura.Text.Replace(',','.')},{txtTotalItbis.Text.Replace(',','.')},{txtSubTotal.Text.Replace(',', '.')})";

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

        private void flowLayoutPanel4_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void txt_cantidadPro_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void txt_cantidadPro_KeyPress(object sender, KeyPressEventArgs e)
        {
            FuncionesGenerales.FuncionesGenerales.SoloNumeros(e);

        }

        private void txt_codigoPro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                try
                {

                    if (!txt_codigoPro.Text.Equals(""))
                    {
                        string cmd = $"select * from productos where estado=1 and codigo_pro = '{txt_codigoPro.Text.Trim()}'";
                        DataSet DS = new DataSet();

                        DS = FuncionesGenerales.FuncionesGenerales.ExecuteReader(cmd, "Error al buscar el producto.");

                        if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                        {
                            idPro = DS.Tables[0].Rows[0][0].ToString();
                            txt_codigoPro.Text = DS.Tables[0].Rows[0][3].ToString();
                            txt_nombrePro.Text = DS.Tables[0].Rows[0][1].ToString();
                            txt_precioPro.Text = DS.Tables[0].Rows[0][6].ToString();
                            txt_cantidadPro.Text = "1";
                            impuestoPro = Convert.ToDouble(DS.Tables[0].Rows[0][4].ToString());
                            maximaCant = Convert.ToInt32(DS.Tables[0].Rows[0][3].ToString());
                            txt_codigoPro.Focus();
                        }
                        else
                        {
                            MessageBox.Show("No existe un producto con dicho código.");
                        }

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Erro al traer el producto, por favor intente de nuevo.");
                }
            }

        }

        private void flowLayoutPanel7_Click(object sender, EventArgs e)
        {

            if(dataGridViewProducto.Rows.Count > 0)
            {
                double rItbis = Convert.ToDouble(txtTotalItbis.Text);
                double rSubTotal = Convert.ToDouble(txtSubTotal.Text);
                double rTotal = 0;

                rItbis -= Convert.ToDouble(dataGridViewProducto.Rows[dataGridViewProducto.CurrentRow.Index].Cells[5].Value);
                rSubTotal -= Convert.ToDouble(dataGridViewProducto.Rows[dataGridViewProducto.CurrentRow.Index].Cells[6].Value);
                dataGridViewProducto.Rows.RemoveAt(dataGridViewProducto.CurrentRow.Index);
                if (dataGridViewProducto.Rows.Count == 0)
                {
                    txtSubTotal.Text = "0";
                    txt_totalFactura.Text = "0";
                    txtTotalItbis.Text = "0";
                    return;
                }

                rTotal = rItbis + rSubTotal;

                txt_totalFactura.Text = rTotal.ToString();
                txtSubTotal.Text = rSubTotal.ToString();
                txtTotalItbis.Text = rItbis.ToString();
               
            }
            else
            {
                MessageBox.Show("Debe de tener por lo menos un producto para poder borrar.... Lo sentimos!!!");
            }
           

        }

        private void flowLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cotizacion_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void cotizacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show("¿Seguro que desea salir?", "Salir", MessageBoxButtons.YesNo);
            if (res == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void txt_codigoCliente_Click(object sender, EventArgs e)
        {
            
        }

        private void txt_codigoCliente_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txt_codigoCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
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
                        impuestoPro = Convert.ToDouble(pro.dataGridViewProducto.Rows[pro.dataGridViewProducto.CurrentCell.RowIndex].Cells[4].Value.ToString());
                        maximaCant = Convert.ToInt32(pro.dataGridViewProducto.Rows[pro.dataGridViewProducto.CurrentCell.RowIndex].Cells[3].Value.ToString());

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Erro al traer el producto, por favor intente de nuevo.");
                }
            }
        }
    }
}
