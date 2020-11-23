namespace CoffeeManagement {
    partial class fReport {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fReport));
            this.rpViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rpViewer
            // 
            this.rpViewer.LocalReport.ReportEmbeddedResource = "CoffeeManagement.Report.Report.rdlc";
            this.rpViewer.Location = new System.Drawing.Point(2, -2);
            this.rpViewer.Name = "rpViewer";
            this.rpViewer.ServerReport.BearerToken = null;
            this.rpViewer.Size = new System.Drawing.Size(442, 566);
            this.rpViewer.TabIndex = 1;
            this.rpViewer.Load += new System.EventHandler(this.rpViewer_Load);
            // 
            // fReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 561);
            this.Controls.Add(this.rpViewer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hóa đơn thanh toán";
            this.Load += new System.EventHandler(this.ReportViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpViewer;
    }
}