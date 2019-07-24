using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FuncionesGenerales
{
    public class FuncionesGenerales
    {
        public static string con = "Data Source=.;Initial Catalog=papeleria;Integrated Security=True";
        public static SqlConnection db = new SqlConnection(con);

        public static DataSet ExecuteReader(string cmd, string mensaje)
        {
            DataSet ds = new DataSet();

            try
            {
                try
                {
                    db.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("La base de datos no está encendida, por favor comunicarse con soporte.");
                    db.Close();
                    return ds;
                }
                SqlDataAdapter DL = new SqlDataAdapter(cmd, db);
                DL.Fill(ds);

            }
            catch (MySqlException)
            {
                MessageBox.Show(mensaje);
            }
            db.Close();
            return ds;

        }

        public static string GenerarCodigoTabla(string prefijo, string tabla,string mensaje)
        {
            DataSet DS = new DataSet();

            try
            {
                try
                {
                    db.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("La base de datos no está encendida, por favor comunicarse con soporte.");
                    db.Close();
                    return "";
                }
                string cmd = $"select CONCAT('{prefijo}', RIGHT(COUNT(*)+1, 8)) from {tabla}";
                SqlDataAdapter DL = new SqlDataAdapter(cmd, db);
                DL.Fill(DS);
                if (DS.Tables.Count == 0) return "";
                if (DS.Tables[0].Rows.Count > 0)
                {
                    db.Close();
                    return DS.Tables[0].Rows[0][0].ToString();
                }

            }
            catch (MySqlException)
            {
                MessageBox.Show(mensaje);
            }
            db.Close();
            return "";

        }

        public static int EjecutarQuery(string cmd, string mensaje)
        {
            DataSet Ds = new DataSet();
            int resp = 0;
            SqlDataAdapter DL = new SqlDataAdapter(cmd, db);

            try
            {
                try
                {
                    db.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("La base de datos no está encendida, por favor comunicarse con soporte");
                    db.Close();
                    return resp;
                }

                DL.Fill(Ds);

                resp = 1;

            }
            catch (MySqlException)
            {
                MessageBox.Show(mensaje);
            }
            db.Close();
            return resp;

        }

        public static string FormatoDinero(long numero)
        {

            return String.Format(CultureInfo.InvariantCulture,
                                  "{0:0,0}", numero);
        }

        public static bool validarFormulario(Control forms, ErrorProvider err)
        {
            return true;
            /*
            bool ConErrores = false;

            foreach (Control obj in forms.Controls)
            {

                if (obj is TxtBoxError)
                {
                    TxtBoxError obj2 = (TxtBoxError)obj;
                    if (obj2.Validar)
                    {
                        err.SetError(obj2, (string.IsNullOrEmpty(obj2.Text.Trim()) ? "Campo obligatorio" : ""));
                        if (string.IsNullOrEmpty(obj2.Text.Trim())) ConErrores = true;
                    }


                }


                if (obj.Controls.Count > 0)
                {

                    ConErrores = validarFormulario(obj, err);
                }

            }
            return ConErrores;
            */
            
        }
        public static void SoloNumeros(KeyPressEventArgs v)
        {
            if (Char.IsDigit(v.KeyChar))
            {
                v.Handled = false;
            }
            else if (char.IsSeparator(v.KeyChar))
            {
                v.Handled = false;
            }
            else if (Char.IsControl(v.KeyChar))
            {
                v.Handled = false;

            }
            else
            {
                v.Handled = true;
            }
        }
        public static void limpiarCOntroles(Control forms)
        {
            
            foreach (Control obj in forms.Controls)
            {
                if (obj is TextBox || obj is RichTextBox)
                {
                    obj.Text = "";
                }
                else if (obj is DataGridView)
                {
                    DataGridView dataGrid = (DataGridView)obj;
                    dataGrid.Rows.Clear();
                }
                if (obj.Controls.Count > 0)
                {
                    limpiarCOntroles(obj);
                }

            }
            
        }

    }
}
