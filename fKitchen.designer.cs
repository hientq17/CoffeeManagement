﻿namespace CoffeeManagement
{
    partial class fKitchen
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
            this.lbTable = new System.Windows.Forms.Label();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.listViewInvoice = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.đăngXuấtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thôngTinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.thôngTinTàiKhoảnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flpTable = new System.Windows.Forms.FlowLayoutPanel();
            this.btnHoanThanh = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTable
            // 
            this.lbTable.AutoSize = true;
            this.lbTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTable.Location = new System.Drawing.Point(129, 35);
            this.lbTable.Name = "lbTable";
            this.lbTable.Size = new System.Drawing.Size(0, 38);
            this.lbTable.TabIndex = 3;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Số lượng";
            this.columnHeader2.Width = 85;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tên món";
            this.columnHeader1.Width = 96;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listViewInvoice);
            this.panel2.Location = new System.Drawing.Point(510, 125);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(518, 312);
            this.panel2.TabIndex = 5;
            // 
            // listViewInvoice
            // 
            this.listViewInvoice.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader5});
            this.listViewInvoice.FullRowSelect = true;
            this.listViewInvoice.GridLines = true;
            this.listViewInvoice.HideSelection = false;
            this.listViewInvoice.Location = new System.Drawing.Point(3, 8);
            this.listViewInvoice.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewInvoice.Name = "listViewInvoice";
            this.listViewInvoice.Size = new System.Drawing.Size(514, 304);
            this.listViewInvoice.TabIndex = 0;
            this.listViewInvoice.UseCompatibleStateImageBehavior = false;
            this.listViewInvoice.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Số bàn";
            // 
            // đăngXuấtToolStripMenuItem
            // 
            this.đăngXuấtToolStripMenuItem.Name = "đăngXuấtToolStripMenuItem";
            this.đăngXuấtToolStripMenuItem.Size = new System.Drawing.Size(268, 34);
            this.đăngXuấtToolStripMenuItem.Text = "Đăng xuất";
            this.đăngXuấtToolStripMenuItem.Click += new System.EventHandler(this.đăngXuấtToolStripMenuItem_Click);
            // 
            // thôngTinToolStripMenuItem
            // 
            this.thôngTinToolStripMenuItem.Name = "thôngTinToolStripMenuItem";
            this.thôngTinToolStripMenuItem.Size = new System.Drawing.Size(268, 34);
            this.thôngTinToolStripMenuItem.Text = "Thông tin tài khoản";
            this.thôngTinToolStripMenuItem.Click += new System.EventHandler(this.thôngTinToolStripMenuItem_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbTable);
            this.panel4.Location = new System.Drawing.Point(507, 18);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(521, 88);
            this.panel4.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Location = new System.Drawing.Point(17, 18);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(486, 532);
            this.panel1.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thôngTinTàiKhoảnToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(486, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // thôngTinTàiKhoảnToolStripMenuItem
            // 
            this.thôngTinTàiKhoảnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thôngTinToolStripMenuItem,
            this.đăngXuấtToolStripMenuItem});
            this.thôngTinTàiKhoảnToolStripMenuItem.Name = "thôngTinTàiKhoảnToolStripMenuItem";
            this.thôngTinTàiKhoảnToolStripMenuItem.Size = new System.Drawing.Size(102, 29);
            this.thôngTinTàiKhoảnToolStripMenuItem.Text = "Tài khoản";
            // 
            // flpTable
            // 
            this.flpTable.AutoScroll = true;
            this.flpTable.Location = new System.Drawing.Point(17, 109);
            this.flpTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flpTable.Name = "flpTable";
            this.flpTable.Size = new System.Drawing.Size(480, 440);
            this.flpTable.TabIndex = 1;
            this.flpTable.Paint += new System.Windows.Forms.PaintEventHandler(this.flpTable_Paint);
            // 
            // btnHoanThanh
            // 
            this.btnHoanThanh.Location = new System.Drawing.Point(706, 472);
            this.btnHoanThanh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnHoanThanh.Name = "btnHoanThanh";
            this.btnHoanThanh.Size = new System.Drawing.Size(134, 51);
            this.btnHoanThanh.TabIndex = 8;
            this.btnHoanThanh.Text = "Hoàn Thành";
            this.btnHoanThanh.UseVisualStyleBackColor = true;
            this.btnHoanThanh.Visible = false;
            this.btnHoanThanh.Click += new System.EventHandler(this.btnHoanThanh_Click);
            // 
            // fKitchen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 555);
            this.Controls.Add(this.btnHoanThanh);
            this.Controls.Add(this.flpTable);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "fKitchen";
            this.Text = "Kitchen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fKitchen_FormClosing);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbTable;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView listViewInvoice;
        private System.Windows.Forms.ToolStripMenuItem đăngXuấtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thôngTinToolStripMenuItem;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flpTable;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem thôngTinTàiKhoảnToolStripMenuItem;
        private System.Windows.Forms.Button btnHoanThanh;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}