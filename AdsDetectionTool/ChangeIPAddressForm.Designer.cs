// Copyright (c) 2024, ads-tec IIT GmbH
// All rights reserved.
// This source code is licensed under the BSD 2-Clause License.
// Please see the LICENSE file in the root directory of this source tree for more details.
namespace adsDetectionTool
{
    partial class ChangeIPAddressForm
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
            this.textBoxPwd = new System.Windows.Forms.TextBox();
            this.labelPwd = new System.Windows.Forms.Label();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.labelUser = new System.Windows.Forms.Label();
            this.tbCurrentIPAddress = new System.Windows.Forms.TextBox();
            this.lblCurrentIPAddress = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textSerialNo = new System.Windows.Forms.TextBox();
            this.labelSerialNo = new System.Windows.Forms.Label();
            this.btnChange = new System.Windows.Forms.Button();
            this.tbNewSubnetMask = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbNewIPAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbCurrentSubnetMask = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxPwd
            // 
            this.textBoxPwd.Location = new System.Drawing.Point(132, 52);
            this.textBoxPwd.Name = "textBoxPwd";
            this.textBoxPwd.Size = new System.Drawing.Size(129, 20);
            this.textBoxPwd.TabIndex = 2;
            this.textBoxPwd.Text = "admin";
            // 
            // labelPwd
            // 
            this.labelPwd.AutoSize = true;
            this.labelPwd.Location = new System.Drawing.Point(15, 52);
            this.labelPwd.Name = "labelPwd";
            this.labelPwd.Size = new System.Drawing.Size(56, 13);
            this.labelPwd.TabIndex = 28;
            this.labelPwd.Text = "Password:";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(132, 22);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(129, 20);
            this.textBoxUser.TabIndex = 1;
            this.textBoxUser.Text = "admin";
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(15, 23);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(32, 13);
            this.labelUser.TabIndex = 26;
            this.labelUser.Text = "User:";
            // 
            // tbCurrentIPAddress
            // 
            this.tbCurrentIPAddress.Location = new System.Drawing.Point(132, 112);
            this.tbCurrentIPAddress.Name = "tbCurrentIPAddress";
            this.tbCurrentIPAddress.ReadOnly = true;
            this.tbCurrentIPAddress.Size = new System.Drawing.Size(129, 20);
            this.tbCurrentIPAddress.TabIndex = 4;
            this.tbCurrentIPAddress.TabStop = false;
            this.tbCurrentIPAddress.Text = "192.168.0.254";
            // 
            // lblCurrentIPAddress
            // 
            this.lblCurrentIPAddress.AutoSize = true;
            this.lblCurrentIPAddress.Location = new System.Drawing.Point(42, 135);
            this.lblCurrentIPAddress.Name = "lblCurrentIPAddress";
            this.lblCurrentIPAddress.Size = new System.Drawing.Size(98, 13);
            this.lblCurrentIPAddress.TabIndex = 24;
            this.lblCurrentIPAddress.Text = "Current IP Address:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textSerialNo);
            this.groupBox4.Controls.Add(this.textBoxPwd);
            this.groupBox4.Controls.Add(this.labelSerialNo);
            this.groupBox4.Controls.Add(this.textBoxUser);
            this.groupBox4.Controls.Add(this.btnChange);
            this.groupBox4.Controls.Add(this.tbCurrentIPAddress);
            this.groupBox4.Controls.Add(this.tbNewSubnetMask);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.tbNewIPAddress);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.tbCurrentSubnetMask);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.labelPwd);
            this.groupBox4.Controls.Add(this.labelUser);
            this.groupBox4.Location = new System.Drawing.Point(27, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(278, 305);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            // 
            // textSerialNo
            // 
            this.textSerialNo.Location = new System.Drawing.Point(132, 80);
            this.textSerialNo.Name = "textSerialNo";
            this.textSerialNo.Size = new System.Drawing.Size(129, 20);
            this.textSerialNo.TabIndex = 3;
            // 
            // labelSerialNo
            // 
            this.labelSerialNo.AutoSize = true;
            this.labelSerialNo.Location = new System.Drawing.Point(15, 83);
            this.labelSerialNo.Name = "labelSerialNo";
            this.labelSerialNo.Size = new System.Drawing.Size(53, 13);
            this.labelSerialNo.TabIndex = 36;
            this.labelSerialNo.Text = "Serial No:";
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(18, 246);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(235, 37);
            this.btnChange.TabIndex = 8;
            this.btnChange.Text = "Change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // tbNewSubnetMask
            // 
            this.tbNewSubnetMask.Location = new System.Drawing.Point(132, 204);
            this.tbNewSubnetMask.Name = "tbNewSubnetMask";
            this.tbNewSubnetMask.Size = new System.Drawing.Size(129, 20);
            this.tbNewSubnetMask.TabIndex = 7;
            this.tbNewSubnetMask.Text = "255.255.255.0";
            this.tbNewSubnetMask.TextChanged += new System.EventHandler(this.tbNewSubnetMask_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "New Subnet Mask:";
            // 
            // tbNewIPAddress
            // 
            this.tbNewIPAddress.Location = new System.Drawing.Point(132, 177);
            this.tbNewIPAddress.Name = "tbNewIPAddress";
            this.tbNewIPAddress.Size = new System.Drawing.Size(129, 20);
            this.tbNewIPAddress.TabIndex = 6;
            this.tbNewIPAddress.Text = "192.168.0.100";
            this.tbNewIPAddress.TextChanged += new System.EventHandler(this.tbNewIPAddress_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "New IP Address:";
            // 
            // tbCurrentSubnetMask
            // 
            this.tbCurrentSubnetMask.Location = new System.Drawing.Point(132, 146);
            this.tbCurrentSubnetMask.Name = "tbCurrentSubnetMask";
            this.tbCurrentSubnetMask.ReadOnly = true;
            this.tbCurrentSubnetMask.Size = new System.Drawing.Size(129, 20);
            this.tbCurrentSubnetMask.TabIndex = 5;
            this.tbCurrentSubnetMask.TabStop = false;
            this.tbCurrentSubnetMask.Text = "192.168.0.254";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Current Subnet Mask:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(196, 347);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 34);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ChangeIPAddressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 406);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblCurrentIPAddress);
            this.Controls.Add(this.groupBox4);
            this.Name = "ChangeIPAddressForm";
            this.Text = "Change IP Address";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPwd;
        private System.Windows.Forms.Label labelPwd;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.TextBox tbCurrentIPAddress;
        private System.Windows.Forms.Label lblCurrentIPAddress;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbNewSubnetMask;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbNewIPAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbCurrentSubnetMask;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox textSerialNo;
        private System.Windows.Forms.Label labelSerialNo;
    }
}