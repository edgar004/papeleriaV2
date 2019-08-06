namespace Papeleria
{
    partial class reporteFac
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.FACTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSetFACT = new Papeleria.DataSetFACT();
            this.FACTTableAdapter = new Papeleria.DataSetFACTTableAdapters.FACTTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.FACTBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetFACT)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet3";
            reportDataSource1.Value = this.FACTBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Papeleria.informeFact.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1319, 620);
            this.reportViewer1.TabIndex = 0;
            // 
            // FACTBindingSource
            // 
            this.FACTBindingSource.DataMember = "FACT";
            this.FACTBindingSource.DataSource = this.DataSetFACT;
            // 
            // DataSetFACT
            // 
            this.DataSetFACT.DataSetName = "DataSetFACT";
            this.DataSetFACT.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // FACTTableAdapter
            // 
            this.FACTTableAdapter.ClearBeforeFill = true;
            // 
            // reporteFac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1319, 620);
            this.Controls.Add(this.reportViewer1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "reporteFac";
            this.Text = "reporteFac";
            this.Load += new System.EventHandler(this.reporteFac_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FACTBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetFACT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource FACTBindingSource;
        private DataSetFACT DataSetFACT;
        private DataSetFACTTableAdapters.FACTTableAdapter FACTTableAdapter;
    }
}