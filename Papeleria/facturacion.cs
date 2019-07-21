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

            String sql = "select tipo_com from comprobantes";

            DataSet DS = new DataSet();

            DS = FuncionesGenerales.FuncionesGenerales.ExecuteReader(sql, "Error al traer sus comprobantes");

            if(DS.Tables.Count > 0)
            {

                for(int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    comboComprobante.Items.Add(DS.Tables[0].Rows[i]["tipo_com"].ToString());
                }
                
            }
        }

        private void facturacion_Load(object sender, EventArgs e)
        {

        }

        int cantidadPro = 0;
        Double itbisPro = 0;

        private void flowLayoutPanel3_Click(object sender, EventArgs e)
        {
            inventario obj = new inventario();
            String dato = "";

            if (obj.ShowDialog() == DialogResult.OK)
            {
                dato= obj.dataGridViewProducto.Rows[obj.dataGridViewProducto.CurrentCell.RowIndex].Cells[0].Value.ToString();
            }

            DataSet DS = new DataSet();

            DS = SearchEnter(dato);

            txtNomPro.Text = DS.Tables[0].Rows[0]["nom_pro"].ToString();
            txtPrePro.Text = DS.Tables[0].Rows[0]["precio"].ToString();

            cantidadPro = Convert.ToInt32(DS.Tables[0].Rows[0]["cantidad"].ToString());
            itbisPro = Convert.ToInt32(DS.Tables[0].Rows[0]["itbis"].ToString());
        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Click(object sender, EventArgs e)
        {
            clientes cli = new clientes();
            String codcli = "";

            if (cli.ShowDialog() == DialogResult.OK)
            {
                codcli = cli.dataGridViewCliente.Rows[cli.dataGridViewCliente.CurrentCell.RowIndex].Cells[0].Value.ToString();
            }

            if(!codcli.Equals(""))
            {

                String sql = "select nombre_cli ,rnc_cli from clientes where codigo_cli = '" + codcli + "'";

                DataSet DS = new DataSet();
                DS = FuncionesGenerales.FuncionesGenerales.ExecuteReader(sql, "Error al traer los datos del cliente.");


                if (DS.Tables.Count > 0)
                {
                    MessageBox.Show("hola");
                    txtCli.Text = DS.Tables[0].Rows[0]["nombre_cli"].ToString().Trim();
                    txtRncCli.Text = DS.Tables[0].Rows[0]["rnc_cli"].ToString();

                }

            }

        }


        public DataSet SearchEnter (String dato)
        {
            DataSet DS = new DataSet();

            String sql = "select * from productos where codigo_pro = '" + dato+"' and estado = 1";
             
            DS = FuncionesGenerales.FuncionesGenerales.ExecuteReader(sql, "Error al traer el producto.");

            return DS;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==Convert.ToChar(Keys.Enter))
            {
                DataSet DS = new DataSet();

                DS = SearchEnter(textBox3.Text);

                txtNomPro.Text =  DS.Tables[0].Rows[0]["nom_pro"].ToString();
                txtPrePro.Text =  DS.Tables[0].Rows[0]["precio"].ToString();

                cantidadPro = Convert.ToInt32(DS.Tables[0].Rows[0]["cantidad"].ToString());
                itbisPro = Convert.ToInt32(DS.Tables[0].Rows[0]["itbis"].ToString());

            }
        }

        private void txtCantPro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')){
                e.Handled = true;
            }
        }

        private void comboComprobante_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!comboComprobante.Text.Equals(""))
            {

                String sql = "select serie_com, usados_com from comprobantes where tipo_com = '" + comboComprobante.Text + "'";

                DataSet DS = new DataSet();

                DS = FuncionesGenerales.FuncionesGenerales.ExecuteReader(sql, "Error al traer la serie de este comprobante");

                if(DS.Tables.Count > 0)
                {
                   

                    int usados = Convert.ToInt32(DS.Tables[0].Rows[0]["usados_com"].ToString());

                    usados += 1;

                    String usadost = Convert.ToString(usados).PadLeft(8, '0');

                    txtserieComprobante.Text = usadost;
                }
            }

        }

        public static int cont_fila = 0;
        public static double total = 0;

        //PARA PONER LOS TXT DEBAJO

        public static double sumItbis = 0;
        public static double sumSubTotal = 0;
        public static double sumTotal = 0;

        private void flowLayoutPanel4_Click(object sender, EventArgs e)
        {
            if (!txtCantPro.Text.Equals(""))
            {
                int canti = Convert.ToInt32(txtCantPro.Text);

                if(canti > cantidadPro)
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
                        dataGridViewFacturacion.Rows.Add(textBox3.Text, txtNomPro.Text, txtPrePro.Text, canti, itbisPro+"%");

                        
                        dataGridViewFacturacion.Rows[cont_fila].Cells[5].Value = calItbis;
                        dataGridViewFacturacion.Rows[cont_fila].Cells[6].Value = precioPro;

                        sumItbis += calItbis;
                        sumSubTotal += precioPro;
                        sumTotal = 0;
                        sumTotal = sumItbis + sumSubTotal;

                        txtTotalItbis.Text = "$ "+sumItbis;
                        txtSubTotal.Text = "$ " + sumSubTotal;
                        txtTotal.Text = "$ " + sumTotal;

                        cont_fila++;
                    }

                    else
                    {
                        foreach(DataGridViewRow fila in dataGridViewFacturacion.Rows)
                        {
                            if(fila.Cells[0].Value.ToString() == textBox3.Text)
                            {
                                key = true;
                                num_fila = fila.Index;
                            }
                        }

                        if (key)
                        {
                            dataGridViewFacturacion.Rows[num_fila].Cells[3].Value = (Convert.ToDouble(txtCantPro.Text) + Convert.ToDouble(dataGridViewFacturacion.Rows[num_fila].Cells[3].Value)).ToString();
                            dataGridViewFacturacion.Rows[num_fila].Cells[6].Value = (precioPro + Convert.ToDouble(dataGridViewFacturacion.Rows[num_fila].Cells[6].Value)).ToString();
                            dataGridViewFacturacion.Rows[num_fila].Cells[5].Value = (calItbis + Convert.ToDouble(dataGridViewFacturacion.Rows[num_fila].Cells[5].Value)).ToString();

                            sumItbis += calItbis;
                            sumSubTotal += precioPro;
                            sumTotal = 0;
                            sumTotal = sumItbis + sumSubTotal;

                            txtTotalItbis.Text = "$ " + sumItbis;
                            txtSubTotal.Text = "$ " + sumSubTotal;
                            txtTotal.Text = "$ " + sumTotal;
                        }
                        else
                        {
                            dataGridViewFacturacion.Rows.Add(textBox3.Text, txtNomPro.Text, txtPrePro.Text, canti, itbisPro + "%");


                            dataGridViewFacturacion.Rows[cont_fila].Cells[5].Value = calItbis;
                            dataGridViewFacturacion.Rows[cont_fila].Cells[6].Value = precioPro;

                            sumItbis += calItbis;
                            sumSubTotal += precioPro;
                            sumTotal = 0;
                            sumTotal = sumItbis + sumSubTotal;

                            txtTotalItbis.Text = "$ " + sumItbis;
                            txtSubTotal.Text = "$ " + sumSubTotal;
                            txtTotal.Text = "$ " + sumTotal;
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

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
