﻿namespace Papeleria
{
    partial class reporteCOT
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
            this.COTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.COTTableAdapter = new Papeleria.DataSetCotizacionTableAdapters.COTTableAdapter();
            this.DataSetCotizacion = new Papeleria.DataSetCotizacion();
            ((System.ComponentModel.ISupportInitialize)(this.COTBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetCotizacion)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DatasetCotizacion";
            reportDataSource1.Value = this.COTBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Papeleria.informeCOT.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(884, 561);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // COTTableAdapter
            // 
            this.COTTableAdapter.ClearBeforeFill = true;
            // 
            // DataSetCotizacion
            // 
            this.DataSetCotizacion.DataSetName = "DataSetCotizacion";
            this.DataSetCotizacion.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reporteCOT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.reportViewer1);
            this.Name = "reporteCOT";
            this.Text = "reporteCOT";
            this.Load += new System.EventHandler(this.reporteCOT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.COTBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetCotizacion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource COTBindingSource;
        private DataSetCotizacionTableAdapters.COTTableAdapter COTTableAdapter;
        private DataSetCotizacion DataSetCotizacion;
    }
}