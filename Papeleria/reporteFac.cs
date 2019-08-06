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
    public partial class reporteFac : Form
    {
        public reporteFac()
        {
            InitializeComponent();
        }

        private void reporteFac_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetFACT.FACT' Puede moverla o quitarla según sea necesario.
            // TODO: esta línea de código carga datos en la tabla 'DataSetFACT.FACT' Puede moverla o quitarla según sea necesario.

            this.reportViewer1.RefreshReport();
        }
    }
}
