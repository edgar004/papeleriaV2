using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FuncionesGenerales;

namespace Papeleria
{
    public partial class facturacion : Form
    {
        public facturacion()
        {
            InitializeComponent();

            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblMiRNC.Text = "1-31906567";

            String sql = "select * from comprobantes";

            DataSet DS = new DataSet();

            DS = FuncionesGenerales.FuncionesGenerales.ExecuteReader(sql, "Error al traer sus comprobantes");

            if (DS.Tables.Count > 0)
            {

                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {

                    int limite = Convert.ToInt32(DS.Tables[0].Rows[i]["cantidadLimite_com"].ToString());
                    int usado = Convert.ToInt32(DS.Tables[0].Rows[i]["usados_com"].ToString());

                    if (usado <= limite)
                    {
                        comboComprobante.Items.Add(DS.Tables[0].Rows[i]["tipo_com"].ToString());
                    }
                }

            }

            txtDescuento.ReadOnly = true;
        }

        private void facturacion_Load(object sender, EventArgs e)
        {

        }

        String idProSelect = "";
        int cantidadPro = 0;
        Double itbisPro = 0;

        private void flowLayoutPanel3_Click(object sender, EventArgs e)
        {
            inventario obj = new inventario();
            obj.button2.Visible = false;
            obj.btnAdd.Visible = false;
            String dato = "";

            if (obj.ShowDialog() == DialogResult.OK)
            {
                dato = obj.dataGridViewProducto.Rows[obj.dataGridViewProducto.CurrentCell.RowIndex].Cells[1].Value.ToString();
            }

            if (!dato.Equals(""))
            {
                DataSet DS = new DataSet();

                DS = SearchEnter(dato);

                if (DS.Tables.Count > 0)
                {
                    textBox3.Text = dato;
                    txtNomPro.Text = DS.Tables[0].Rows[0]["nom_pro"].ToString();
                    txtPrePro.Text = DS.Tables[0].Rows[0]["precio"].ToString();

                    cantidadPro = Convert.ToInt32(DS.Tables[0].Rows[0]["cantidad"].ToString());
                    itbisPro = Convert.ToInt32(DS.Tables[0].Rows[0]["itbis"].ToString());
                    idProSelect = DS.Tables[0].Rows[0]["id_pro"].ToString();
                }
            }

        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        public string idCliente = "";
        private void flowLayoutPanel1_Click(object sender, EventArgs e)
        {
            clientes cli = new clientes();
            cli.button2.Visible = false;
            cli.btnAdd.Visible = false;
            String codcli = "";

            if (cli.ShowDialog() == DialogResult.OK)
            {
                codcli = cli.dataGridViewCliente.Rows[cli.dataGridViewCliente.CurrentCell.RowIndex].Cells[0].Value.ToString();
            }

            if (!codcli.Equals(""))
            {

                String sql = "select * from clientes where codigo_cli = '" + codcli + "'";

                DataSet DS = new DataSet();
                DS = FuncionesGenerales.FuncionesGenerales.ExecuteReader(sql, "Error al traer los datos del cliente.");


                if (DS.Tables.Count > 0)
                {

                    txtCli.Text = DS.Tables[0].Rows[0]["nombre_cli"].ToString().Trim();
                    txtRncCli.Text = DS.Tables[0].Rows[0]["rnc_cli"].ToString();
                    idCliente = DS.Tables[0].Rows[0]["codigo_cli"].ToString();
                }

            }

        }


        public DataSet SearchEnter(String dato)
        {
            DataSet DS = new DataSet();

            String sql = "select * from productos where codigo_pro = '" + dato + "' and estado = 1";

            DS = FuncionesGenerales.FuncionesGenerales.ExecuteReader(sql, "Error al traer el producto.");

            return DS;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                if (!textBox3.Text.Equals(""))
                {
                    DataSet DS = new DataSet();

                    DS = SearchEnter(textBox3.Text);

                    if (DS.Tables.Count > 0)
                    {
                        txtNomPro.Text = DS.Tables[0].Rows[0]["nom_pro"].ToString();
                        txtPrePro.Text = DS.Tables[0].Rows[0]["precio"].ToString();

                        cantidadPro = Convert.ToInt32(DS.Tables[0].Rows[0]["cantidad"].ToString());
                        itbisPro = Convert.ToInt32(DS.Tables[0].Rows[0]["itbis"].ToString());
                        idProSelect = DS.Tables[0].Rows[0]["id_pro"].ToString();

                        txtCantPro.Focus();
                    }

                }

            }
        }

        private void txtCantPro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') || e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;

                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    SetDatosDataGrid();
                }
            }
        }

        public String idComprobante = "";
        public String CantComprobanteUsados = "";
        private void comboComprobante_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!comboComprobante.Text.Equals(""))
            {

                String sql = "select * from comprobantes where tipo_com = '" + comboComprobante.Text + "'";

                DataSet DS = new DataSet();

                DS = FuncionesGenerales.FuncionesGenerales.ExecuteReader(sql, "Error al traer la serie de este comprobante");

                if (DS.Tables.Count > 0)
                {


                    int usados = Convert.ToInt32(DS.Tables[0].Rows[0]["usados_com"].ToString());

                    usados += 1;

                    String usadost = Convert.ToString(usados).PadLeft(8, '0');

                    txtserieComprobante.Text = DS.Tables[0].Rows[0]["serie_com"].ToString() + "" + usadost;
                    idComprobante = DS.Tables[0].Rows[0]["id_com"].ToString();
                    CantComprobanteUsados = DS.Tables[0].Rows[0]["usados_com"].ToString();
                }
            }

        }

        public static int cont_fila = 0;
        public static double total = 0;

        //PARA PONER LOS TXT DEBAJO

        public static double sumItbis = 0;
        public static double sumSubTotal = 0;
        public static double sumTotal = 0;

        private void SetDatosDataGrid()
        {
            if (!txtNomPro.Text.Equals(""))
            {
                if (!txtCantPro.Text.Equals(""))
                {
                    int canti = Convert.ToInt32(txtCantPro.Text);

                    if (canti > cantidadPro)
                    {
                        MessageBox.Show("La cantidad de productos que requiere sobrepasa a la cantidad que esta en el inventario");
                    }
                    else
                    {
                        Boolean key = false;
                        int num_fila = 0;

                        int precioPro = Convert.ToInt32(txtPrePro.Text) * Convert.ToInt32(txtCantPro.Text);
                        Double calItbis = precioPro * (itbisPro / 100);

                        if (cont_fila == 0)
                        {
                            dataGridViewFacturacion.Rows.Add(textBox3.Text, txtNomPro.Text, txtPrePro.Text, canti, itbisPro + "%");

                            dataGridViewFacturacion.Rows[cont_fila].Cells[5].Value = calItbis.ToString("N");
                            dataGridViewFacturacion.Rows[cont_fila].Cells[6].Value = precioPro.ToString("N");
                            dataGridViewFacturacion.Rows[cont_fila].Cells[7].Value = idProSelect;

                            sumItbis += calItbis;
                            sumSubTotal += precioPro;
                            sumTotal = 0;
                            sumTotal = sumItbis + sumSubTotal;

                            txtTotalItbis.Text = "$ " + sumItbis;
                            txtSubTotal.Text = "$ " + sumSubTotal;
                            txtTotal.Text = "$ " + sumTotal.ToString().Replace(',', '.');

                            cont_fila++;
                        }

                        else
                        {
                            foreach (DataGridViewRow fila in dataGridViewFacturacion.Rows)
                            {
                                if (fila.Cells[0].Value.ToString() == textBox3.Text)
                                {
                                    key = true;
                                    num_fila = fila.Index;
                                }
                            }

                            if (key)
                            {
                                dataGridViewFacturacion.Rows[num_fila].Cells[3].Value = (Convert.ToDouble(txtCantPro.Text) + Convert.ToDouble(dataGridViewFacturacion.Rows[num_fila].Cells[3].Value)).ToString();
                                dataGridViewFacturacion.Rows[num_fila].Cells[6].Value = (precioPro + Convert.ToDouble(dataGridViewFacturacion.Rows[num_fila].Cells[6].Value)).ToString("N");
                                dataGridViewFacturacion.Rows[num_fila].Cells[5].Value = (calItbis + Convert.ToDouble(dataGridViewFacturacion.Rows[num_fila].Cells[5].Value)).ToString("N");

                                sumItbis += calItbis;
                                sumSubTotal += precioPro;
                                sumTotal = 0;
                                sumTotal = sumItbis + sumSubTotal;

                                txtTotalItbis.Text = "$ " + sumItbis;
                                txtSubTotal.Text = "$ " + sumSubTotal;
                                txtTotal.Text = "$ " + sumTotal.ToString().Replace(',','.');

                                cont_fila++;
                            }
                            else
                            {
                                dataGridViewFacturacion.Rows.Add(textBox3.Text, txtNomPro.Text, txtPrePro.Text, canti, itbisPro + "%");

                                dataGridViewFacturacion.Rows[cont_fila].Cells[5].Value = calItbis.ToString("N");
                                dataGridViewFacturacion.Rows[cont_fila].Cells[6].Value = precioPro.ToString("N");
                                dataGridViewFacturacion.Rows[cont_fila].Cells[7].Value = idProSelect;

                                sumItbis += calItbis;
                                sumSubTotal += precioPro;
                                sumTotal = 0;
                                sumTotal = sumItbis + sumSubTotal;

                                txtTotalItbis.Text = "RD$ " + sumItbis;
                                txtSubTotal.Text = "RD$ " + sumSubTotal;
                                txtTotal.Text = "RD$ " + sumTotal.ToString().Replace(',', '.');

                                cont_fila++;
                            }
                        }

                        txtCantPro.Text = "";
                        textBox3.Text = "";
                        txtNomPro.Text = "";
                        txtPrePro.Text = "";

                        textBox3.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Debe poner la cantidad que desea de este producto");
                }
            }
            else
            {
                MessageBox.Show("Debe Buscar el producto que va a ser vendido!!");
            }
        }


        private void flowLayoutPanel4_Click(object sender, EventArgs e)
        {
            SetDatosDataGrid();
        }

        private void flowLayoutPanel6_Click(object sender, EventArgs e)
        {
            FuncionesGenerales.FuncionesGenerales.limpiarCOntroles(this);
            sumItbis = 0;
            sumSubTotal = 0;
            sumTotal = 0;
            cont_fila = 0;
        }

        private void flowLayoutPanel7_Click(object sender, EventArgs e)
        {
            if (cont_fila > 0)
            {

                sumItbis -= Convert.ToDouble(dataGridViewFacturacion.Rows[dataGridViewFacturacion.CurrentRow.Index].Cells[5].Value);
                sumSubTotal -= Convert.ToDouble(dataGridViewFacturacion.Rows[dataGridViewFacturacion.CurrentRow.Index].Cells[6].Value);
                dataGridViewFacturacion.Rows.RemoveAt(dataGridViewFacturacion.CurrentRow.Index);

                sumTotal = 0;
                sumTotal = sumItbis + sumSubTotal;

                txtTotalItbis.Text = "RD$ " + sumItbis;
                txtSubTotal.Text = "RD$ " + sumSubTotal;
                txtTotal.Text = "RD$ " + sumTotal;

                cont_fila--;
            }
            else
            {
                MessageBox.Show("No tiene productos el para eliminar");
            }
        }

        public double totalsindescuento = 0;
        private void flowLayoutPanel8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkDescuento_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkDescuento.Checked)
            {
                if (txtTotal.Text.Equals("") || txtTotal.Text.Equals("$ 0"))
                {
                    checkDescuento.Checked = false;
                    MessageBox.Show("No tiene productos para realizar descuentos!!");
                }
                else
                {
                    txtDescuento.ReadOnly = false;
                    totalsindescuento = sumTotal;
                }
            }
            else
            {
                txtDescuento.Text = "";
                txtDescuento.ReadOnly = true;
                txtTotal.Text = "$ " + totalsindescuento;
            }
        }

        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {

            Boolean key = false;

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                key = true;

            }

            if (key)
            {
                if (!txtTotal.Text.Equals(""))
                {

                    double total = totalsindescuento;
                    double desc = Convert.ToDouble(txtDescuento.Text);
                    double aux = total - (total * (desc / 100));

                    txtTotal.Text = "";
                    txtTotal.Text = "$ " + aux.ToString().Replace(',','.');
                }
            }
        }

        private void txtDescuento_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void flowLayoutPanel6_Click_1(object sender, EventArgs e)
        {
            FuncionesGenerales.FuncionesGenerales.limpiarCOntroles(this);
            sumItbis = 0;
            sumSubTotal = 0;
            sumTotal = 0;
            cont_fila = 0;
        }

        private void flowLayoutPanel8_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void flowLayoutPanel7_Click_1(object sender, EventArgs e)
        {
            if (cont_fila > 0)
            {

                sumItbis -= Convert.ToDouble(dataGridViewFacturacion.Rows[dataGridViewFacturacion.CurrentRow.Index].Cells[5].Value);
                sumSubTotal -= Convert.ToDouble(dataGridViewFacturacion.Rows[dataGridViewFacturacion.CurrentRow.Index].Cells[6].Value);
                dataGridViewFacturacion.Rows.RemoveAt(dataGridViewFacturacion.CurrentRow.Index);

                sumTotal = 0;
                sumTotal = sumItbis + sumSubTotal;

                txtTotalItbis.Text = "RD$ " + sumItbis;
                txtSubTotal.Text = "RD$ " + sumSubTotal;
                txtTotal.Text = "RD$ " + sumTotal;

                cont_fila--;
            }
            else
            {
                MessageBox.Show("No tiene productos el para eliminar");
            }
        }

        private void flowLayoutPanel5_Click(object sender, EventArgs e)
        {
            if(cont_fila > 0 && !txtCli.Text.Equals("") && !txtserieComprobante.Text.Equals(""))
            {
                //INGRESAR PRIMERO LA FACTURA
                String totalfact = txtTotal.Text.Replace('$', ' ');
                totalfact = totalfact.Replace('R', ' ');
                totalfact = totalfact.Replace('D', ' ');
                int respuesta = 0;

                String fechaFormato = DateTime.Now.ToString("yyyy-MM-dd");

                if(checkDescuento.Checked && !txtDescuento.Equals(""))
                {
                    

                    String sql = $"INSERT INTO facturas (id_cli, fecha_fac, NFC_com, id_com, total_fac, itbisTotal_fac, subtotal_fac, descuento) VALUES ('{idCliente}','{fechaFormato}','{txtserieComprobante.Text}',{idComprobante},{totalfact},{sumItbis.ToString().Replace(',','.')},{sumSubTotal.ToString().Replace(',', '.')},{txtDescuento.Text})";
                    
                    respuesta = FuncionesGenerales.FuncionesGenerales.EjecutarQuery(sql, "Error al guardar la factura");
                }
                else
                {
                    String sql = $"INSERT INTO facturas (id_cli, fecha_fac, NFC_com, id_com, total_fac, itbisTotal_fac, subtotal_fac, descuento) VALUES ('{idCliente}','{fechaFormato}','{txtserieComprobante.Text}',{idComprobante},{totalfact},{sumItbis.ToString().Replace(',', '.')},{sumSubTotal.ToString().Replace(',', '.')},0)";
                    
                    respuesta = FuncionesGenerales.FuncionesGenerales.EjecutarQuery(sql, "Error al guardar la factura");
                    
                }

                //INGRESAMOS LOS DETALLES

                if(respuesta > 0)
                {
                    String sql = "select max(id_fac) as id_fac  from facturas";
                    

                    DataSet DS = new DataSet();

                    DS = FuncionesGenerales.FuncionesGenerales.ExecuteReader(sql, "Error al traer la serie de este comprobante");

                    if(DS.Tables.Count > 0)
                    {
                        String idFact = DS.Tables[0].Rows[0]["id_fac"].ToString();

                        Boolean key = true;

                        foreach (DataGridViewRow fila in dataGridViewFacturacion.Rows)
                        {
                            if (key)
                            {
                                String idPro = fila.Cells[7].Value.ToString();
                                String cantpro = fila.Cells[3].Value.ToString();
                                String precioPro = fila.Cells[2].Value.ToString();
                                String poritbis = fila.Cells[4].Value.ToString().Replace('%',' ');
                                int res = 0;

                                String sql2 = $"INSERT INTO detalles_facturas(id_fac, id_pro, cantidad_pro, precio_pro, porcientoItbis_pro) VALUES ({idFact},{idPro},{cantpro},{precioPro},{poritbis})";
                               
                                res = FuncionesGenerales.FuncionesGenerales.EjecutarQuery(sql2, "Error al guardar el detalle con id " + idPro + ".");

                                if(res < 1)
                                {
                                    key = false;
                                }

                                String sqlUpdate = $"update productos set cantidad = cantidad - {cantpro} where id_pro = {idPro}";
                                int resUpdate = 0;
                                

                                resUpdate = FuncionesGenerales.FuncionesGenerales.EjecutarQuery(sqlUpdate, "Error al actualizar la cantidad del producto con el id " + idPro + ".");
                            }
                        }

                        if (key)
                        {
                            FuncionesGenerales.FuncionesGenerales.limpiarCOntroles(this);
                            sumItbis = 0;
                            sumSubTotal = 0;
                            sumTotal = 0;
                            cont_fila = 0;

                            comboComprobante.SelectedIndex = -1;

                            int usado = Convert.ToInt32(CantComprobanteUsados), res3 =0;
                            usado++;

                            string sql3 = $"update comprobantes set usados_com = {usado} where id_com = {idComprobante}";
                            
                            res3 = FuncionesGenerales.FuncionesGenerales.EjecutarQuery(sql3, "Error al aumentar la secuencia del comprobante usado");

                            if (res3 > 0)
                            {
                               

                               string  cmd = $"EXEC FACT  {idFact}";
                                DS = FuncionesGenerales.FuncionesGenerales.ExecuteReader(cmd, "Error al crear EL PDF de la factución");
                                reporteFac obj = new reporteFac();
                                obj.reportViewer1.LocalReport.DataSources[0].Value = DS.Tables[0];
                                obj.ShowDialog();
                                MessageBox.Show("Realizado correctamente!!");
                                FuncionesGenerales.FuncionesGenerales.limpiarCOntroles(this);
                            }
                            else
                            {
                                MessageBox.Show("Error al aumentar la secuencia del comprobante usado");
                            }
                        }
                    }

                    
                }

            }
            else
            {
                MessageBox.Show("No puede generar una factura antes de eleguir los productos");
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

