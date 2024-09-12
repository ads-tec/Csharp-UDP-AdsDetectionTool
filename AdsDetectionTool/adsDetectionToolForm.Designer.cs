// Copyright (c) 2024, ads-tec IIT GmbH
// All rights reserved.
// This source code is licensed under the BSD 2-Clause License.
// Please see the LICENSE file in the root directory of this source tree for more details.
namespace adsDetectionTool
{
    partial class adsDetectionToolForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvFoundDevices = new System.Windows.Forms.ListView();
            this.columnHeaderSerialNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderIPAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSubnet = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnChangeIPAddress = new System.Windows.Forms.Button();
            this.radioButtonAsyncMode = new System.Windows.Forms.RadioButton();
            this.radioButtonSyncMode = new System.Windows.Forms.RadioButton();
            this.groupBoxMode = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxMode.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvFoundDevices
            // 
            this.lvFoundDevices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSerialNumber,
            this.columnHeaderIPAddress,
            this.columnHeaderSubnet});
            this.lvFoundDevices.FullRowSelect = true;
            this.lvFoundDevices.GridLines = true;
            this.lvFoundDevices.Location = new System.Drawing.Point(30, 163);
            this.lvFoundDevices.MultiSelect = false;
            this.lvFoundDevices.Name = "lvFoundDevices";
            this.lvFoundDevices.Size = new System.Drawing.Size(334, 116);
            this.lvFoundDevices.TabIndex = 24;
            this.lvFoundDevices.UseCompatibleStateImageBehavior = false;
            this.lvFoundDevices.View = System.Windows.Forms.View.Details;
            this.lvFoundDevices.SelectedIndexChanged += new System.EventHandler(this.lvFoundDevices_SelectedIndexChanged);
            this.lvFoundDevices.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvFoundDevices_MouseClick);
            // 
            // columnHeaderSerialNumber
            // 
            this.columnHeaderSerialNumber.Text = "Serial Number";
            this.columnHeaderSerialNumber.Width = 110;
            // 
            // columnHeaderIPAddress
            // 
            this.columnHeaderIPAddress.Text = "IP Address";
            this.columnHeaderIPAddress.Width = 110;
            // 
            // columnHeaderSubnet
            // 
            this.columnHeaderSubnet.Text = "Subnet Mask";
            this.columnHeaderSubnet.Width = 110;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(34, 35);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(147, 40);
            this.btnSearch.TabIndex = 25;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(217, 303);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(147, 40);
            this.btnRefresh.TabIndex = 26;
            this.btnRefresh.Text = "Clear Device List";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnChangeIPAddress
            // 
            this.btnChangeIPAddress.Enabled = false;
            this.btnChangeIPAddress.Location = new System.Drawing.Point(217, 35);
            this.btnChangeIPAddress.Name = "btnChangeIPAddress";
            this.btnChangeIPAddress.Size = new System.Drawing.Size(147, 40);
            this.btnChangeIPAddress.TabIndex = 27;
            this.btnChangeIPAddress.Text = "Change IP Address";
            this.btnChangeIPAddress.UseVisualStyleBackColor = true;
            this.btnChangeIPAddress.Click += new System.EventHandler(this.btnChangeIPAddress_Click);
            // 
            // radioButtonAsyncMode
            // 
            this.radioButtonAsyncMode.AutoSize = true;
            this.radioButtonAsyncMode.Location = new System.Drawing.Point(26, 17);
            this.radioButtonAsyncMode.Name = "radioButtonAsyncMode";
            this.radioButtonAsyncMode.Size = new System.Drawing.Size(54, 17);
            this.radioButtonAsyncMode.TabIndex = 28;
            this.radioButtonAsyncMode.TabStop = true;
            this.radioButtonAsyncMode.Text = "Async";
            this.radioButtonAsyncMode.UseVisualStyleBackColor = true;
            this.radioButtonAsyncMode.CheckedChanged += new System.EventHandler(this.radioButtonAsyncMode_CheckedChanged);
            // 
            // radioButtonSyncMode
            // 
            this.radioButtonSyncMode.AutoSize = true;
            this.radioButtonSyncMode.Location = new System.Drawing.Point(122, 17);
            this.radioButtonSyncMode.Name = "radioButtonSyncMode";
            this.radioButtonSyncMode.Size = new System.Drawing.Size(49, 17);
            this.radioButtonSyncMode.TabIndex = 29;
            this.radioButtonSyncMode.TabStop = true;
            this.radioButtonSyncMode.Text = "Sync";
            this.radioButtonSyncMode.UseVisualStyleBackColor = true;
            this.radioButtonSyncMode.CheckedChanged += new System.EventHandler(this.radioButtonSyncMode_CheckedChanged);
            // 
            // groupBoxMode
            // 
            this.groupBoxMode.Controls.Add(this.radioButtonAsyncMode);
            this.groupBoxMode.Controls.Add(this.radioButtonSyncMode);
            this.groupBoxMode.Location = new System.Drawing.Point(34, 92);
            this.groupBoxMode.Name = "groupBoxMode";
            this.groupBoxMode.Size = new System.Drawing.Size(211, 53);
            this.groupBoxMode.TabIndex = 30;
            this.groupBoxMode.TabStop = false;
            this.groupBoxMode.Text = "Mode";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(328, 421);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 40);
            this.btnClose.TabIndex = 31;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnChangeIPAddress);
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.lvFoundDevices);
            this.groupBox1.Controls.Add(this.groupBoxMode);
            this.groupBox1.Location = new System.Drawing.Point(24, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(397, 379);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 485);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Name = "Form1";
            this.Text = "adsDetectionTool v2.1";
            this.groupBoxMode.ResumeLayout(false);
            this.groupBoxMode.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion
        private System.Windows.Forms.ListView lvFoundDevices;
        private System.Windows.Forms.ColumnHeader columnHeaderSerialNumber;
        private System.Windows.Forms.ColumnHeader columnHeaderIPAddress;
        private System.Windows.Forms.ColumnHeader columnHeaderSubnet;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnChangeIPAddress;
        private System.Windows.Forms.RadioButton radioButtonAsyncMode;
        private System.Windows.Forms.RadioButton radioButtonSyncMode;
        private System.Windows.Forms.GroupBox groupBoxMode;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

