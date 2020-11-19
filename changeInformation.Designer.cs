namespace CoffeeManagement {
    partial class changeInformation {
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
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.cbbRoleId = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.panel17 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.txtEmployeeName = new System.Windows.Forms.TextBox();
            this.panel18 = new System.Windows.Forms.Panel();
            this.label1212 = new System.Windows.Forms.Label();
            this.txtEmployeeUser = new System.Windows.Forms.TextBox();
            this.panelEmployeeOK = new System.Windows.Forms.Panel();
            this.btnEmployeeCancel = new System.Windows.Forms.Button();
            this.btnEmployeeOK = new System.Windows.Forms.Button();
            this.panel13.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panelEmployeeOK.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.panel20);
            this.panel13.Controls.Add(this.panel17);
            this.panel13.Controls.Add(this.panel18);
            this.panel13.Controls.Add(this.panelEmployeeOK);
            this.panel13.Location = new System.Drawing.Point(73, 15);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(304, 209);
            this.panel13.TabIndex = 3;
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.cbbRoleId);
            this.panel20.Controls.Add(this.label16);
            this.panel20.Location = new System.Drawing.Point(14, 93);
            this.panel20.Margin = new System.Windows.Forms.Padding(2);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(280, 36);
            this.panel20.TabIndex = 1;
            // 
            // cbbRoleId
            // 
            this.cbbRoleId.Enabled = false;
            this.cbbRoleId.FormattingEnabled = true;
            this.cbbRoleId.Items.AddRange(new object[] {
            "Thu ngân",
            "Pha chế"});
            this.cbbRoleId.Location = new System.Drawing.Point(89, 7);
            this.cbbRoleId.Name = "cbbRoleId";
            this.cbbRoleId.Size = new System.Drawing.Size(181, 21);
            this.cbbRoleId.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(19, 10);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(50, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "Chức vụ:";
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.label15);
            this.panel17.Controls.Add(this.txtEmployeeName);
            this.panel17.Location = new System.Drawing.Point(14, 53);
            this.panel17.Margin = new System.Windows.Forms.Padding(2);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(280, 36);
            this.panel17.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(4, 10);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(79, 13);
            this.label15.TabIndex = 1;
            this.label15.Text = "Tên nhân viên:";
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Enabled = false;
            this.txtEmployeeName.Location = new System.Drawing.Point(87, 7);
            this.txtEmployeeName.Margin = new System.Windows.Forms.Padding(2);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(181, 20);
            this.txtEmployeeName.TabIndex = 0;
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.label1212);
            this.panel18.Controls.Add(this.txtEmployeeUser);
            this.panel18.Location = new System.Drawing.Point(14, 13);
            this.panel18.Margin = new System.Windows.Forms.Padding(2);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(280, 36);
            this.panel18.TabIndex = 2;
            // 
            // label1212
            // 
            this.label1212.AutoSize = true;
            this.label1212.Location = new System.Drawing.Point(17, 10);
            this.label1212.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1212.Name = "label1212";
            this.label1212.Size = new System.Drawing.Size(58, 13);
            this.label1212.TabIndex = 1;
            this.label1212.Text = "Tài khoản:";
            // 
            // txtEmployeeUser
            // 
            this.txtEmployeeUser.Enabled = false;
            this.txtEmployeeUser.Location = new System.Drawing.Point(87, 7);
            this.txtEmployeeUser.Margin = new System.Windows.Forms.Padding(2);
            this.txtEmployeeUser.Name = "txtEmployeeUser";
            this.txtEmployeeUser.Size = new System.Drawing.Size(181, 20);
            this.txtEmployeeUser.TabIndex = 0;
            // 
            // panelEmployeeOK
            // 
            this.panelEmployeeOK.Controls.Add(this.btnEmployeeCancel);
            this.panelEmployeeOK.Controls.Add(this.btnEmployeeOK);
            this.panelEmployeeOK.Location = new System.Drawing.Point(14, 133);
            this.panelEmployeeOK.Margin = new System.Windows.Forms.Padding(2);
            this.panelEmployeeOK.Name = "panelEmployeeOK";
            this.panelEmployeeOK.Size = new System.Drawing.Size(280, 58);
            this.panelEmployeeOK.TabIndex = 4;
            this.panelEmployeeOK.Visible = false;
            // 
            // btnEmployeeCancel
            // 
            this.btnEmployeeCancel.Location = new System.Drawing.Point(153, 15);
            this.btnEmployeeCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnEmployeeCancel.Name = "btnEmployeeCancel";
            this.btnEmployeeCancel.Size = new System.Drawing.Size(56, 29);
            this.btnEmployeeCancel.TabIndex = 0;
            this.btnEmployeeCancel.Text = "Hủy";
            this.btnEmployeeCancel.UseVisualStyleBackColor = true;
            // 
            // btnEmployeeOK
            // 
            this.btnEmployeeOK.Location = new System.Drawing.Point(78, 15);
            this.btnEmployeeOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnEmployeeOK.Name = "btnEmployeeOK";
            this.btnEmployeeOK.Size = new System.Drawing.Size(56, 29);
            this.btnEmployeeOK.TabIndex = 0;
            this.btnEmployeeOK.Text = "OK";
            this.btnEmployeeOK.UseVisualStyleBackColor = true;
            // 
            // changeInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 231);
            this.Controls.Add(this.panel13);
            this.Name = "changeInformation";
            this.Text = "Chỉnh sửa thông tin";
            this.panel13.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            this.panelEmployeeOK.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.ComboBox cbbRoleId;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtEmployeeName;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Label label1212;
        private System.Windows.Forms.TextBox txtEmployeeUser;
        private System.Windows.Forms.Panel panelEmployeeOK;
        private System.Windows.Forms.Button btnEmployeeCancel;
        private System.Windows.Forms.Button btnEmployeeOK;
    }
}