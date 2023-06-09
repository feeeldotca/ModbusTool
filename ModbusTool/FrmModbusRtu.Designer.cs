﻿namespace ModbusTool
{
    partial class FrmModbusRtu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmModbusRtu));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbDatabits = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cmb_Parity = new System.Windows.Forms.ComboBox();
            this.cmb_Paud = new System.Windows.Forms.ComboBox();
            this.cmb_Dataformat = new System.Windows.Forms.ComboBox();
            this.cmb_Port = new System.Windows.Forms.ComboBox();
            this.cmb_Stopbits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbListBox = new System.Windows.Forms.Label();
            this.txt_Length = new System.Windows.Forms.TextBox();
            this.txt_Address = new System.Windows.Forms.TextBox();
            this.txt_SlaveAdd = new System.Windows.Forms.TextBox();
            this.lst_Info = new System.Windows.Forms.ListView();
            this.txt_WriteValue = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.cmb_VarType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmb_Storage = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.InfoTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Info = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbDatabits);
            this.groupBox1.Controls.Add(this.btnDisconnect);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.cmb_Parity);
            this.groupBox1.Controls.Add(this.cmb_Paud);
            this.groupBox1.Controls.Add(this.cmb_Dataformat);
            this.groupBox1.Controls.Add(this.cmb_Port);
            this.groupBox1.Controls.Add(this.cmb_Stopbits);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(26, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1027, 159);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Com Parameters";
            // 
            // tbDatabits
            // 
            this.tbDatabits.Location = new System.Drawing.Point(913, 37);
            this.tbDatabits.Name = "tbDatabits";
            this.tbDatabits.Size = new System.Drawing.Size(100, 31);
            this.tbDatabits.TabIndex = 3;
            this.tbDatabits.Text = "8";
            this.tbDatabits.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(861, 89);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(152, 34);
            this.btnDisconnect.TabIndex = 2;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(611, 89);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(152, 34);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cmb_Parity
            // 
            this.cmb_Parity.FormattingEnabled = true;
            this.cmb_Parity.Location = new System.Drawing.Point(611, 37);
            this.cmb_Parity.Name = "cmb_Parity";
            this.cmb_Parity.Size = new System.Drawing.Size(152, 25);
            this.cmb_Parity.TabIndex = 1;
            // 
            // cmb_Paud
            // 
            this.cmb_Paud.FormattingEnabled = true;
            this.cmb_Paud.Location = new System.Drawing.Point(352, 37);
            this.cmb_Paud.Name = "cmb_Paud";
            this.cmb_Paud.Size = new System.Drawing.Size(152, 25);
            this.cmb_Paud.TabIndex = 1;
            // 
            // cmb_Dataformat
            // 
            this.cmb_Dataformat.FormattingEnabled = true;
            this.cmb_Dataformat.Location = new System.Drawing.Point(352, 98);
            this.cmb_Dataformat.Name = "cmb_Dataformat";
            this.cmb_Dataformat.Size = new System.Drawing.Size(152, 25);
            this.cmb_Dataformat.TabIndex = 1;
            // 
            // cmb_Port
            // 
            this.cmb_Port.FormattingEnabled = true;
            this.cmb_Port.Location = new System.Drawing.Point(113, 37);
            this.cmb_Port.Name = "cmb_Port";
            this.cmb_Port.Size = new System.Drawing.Size(117, 25);
            this.cmb_Port.TabIndex = 1;
            // 
            // cmb_Stopbits
            // 
            this.cmb_Stopbits.FormattingEnabled = true;
            this.cmb_Stopbits.Location = new System.Drawing.Point(113, 98);
            this.cmb_Stopbits.Name = "cmb_Stopbits";
            this.cmb_Stopbits.Size = new System.Drawing.Size(117, 25);
            this.cmb_Stopbits.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(802, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "DataBits:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(517, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "Parity:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(243, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 18);
            this.label6.TabIndex = 0;
            this.label6.Text = "PaudRate: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "Port#: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Format: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "StopBits: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbListBox);
            this.groupBox2.Controls.Add(this.txt_Length);
            this.groupBox2.Controls.Add(this.txt_Address);
            this.groupBox2.Controls.Add(this.txt_SlaveAdd);
            this.groupBox2.Controls.Add(this.lst_Info);
            this.groupBox2.Controls.Add(this.txt_WriteValue);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.btnWrite);
            this.groupBox2.Controls.Add(this.btnRead);
            this.groupBox2.Controls.Add(this.cmb_VarType);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cmb_Storage);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(26, 211);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1027, 521);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Read/Write Test";
            // 
            // lbListBox
            // 
            this.lbListBox.AutoSize = true;
            this.lbListBox.Location = new System.Drawing.Point(18, 243);
            this.lbListBox.Name = "lbListBox";
            this.lbListBox.Size = new System.Drawing.Size(152, 18);
            this.lbListBox.TabIndex = 8;
            this.lbListBox.Text = "Read/Write Info:";
            // 
            // txt_Length
            // 
            this.txt_Length.Location = new System.Drawing.Point(492, 128);
            this.txt_Length.Name = "txt_Length";
            this.txt_Length.Size = new System.Drawing.Size(100, 31);
            this.txt_Length.TabIndex = 7;
            this.txt_Length.Text = "1";
            this.txt_Length.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_Address
            // 
            this.txt_Address.Location = new System.Drawing.Point(174, 125);
            this.txt_Address.Name = "txt_Address";
            this.txt_Address.Size = new System.Drawing.Size(100, 31);
            this.txt_Address.TabIndex = 6;
            this.txt_Address.Text = "0";
            this.txt_Address.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_SlaveAdd
            // 
            this.txt_SlaveAdd.Location = new System.Drawing.Point(174, 76);
            this.txt_SlaveAdd.Name = "txt_SlaveAdd";
            this.txt_SlaveAdd.Size = new System.Drawing.Size(100, 31);
            this.txt_SlaveAdd.TabIndex = 5;
            this.txt_SlaveAdd.Text = "1";
            this.txt_SlaveAdd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lst_Info
            // 
            this.lst_Info.BackColor = System.Drawing.SystemColors.Control;
            this.lst_Info.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.InfoTime,
            this.Info});
            this.lst_Info.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lst_Info.HideSelection = false;
            this.lst_Info.Location = new System.Drawing.Point(20, 278);
            this.lst_Info.Name = "lst_Info";
            this.lst_Info.Size = new System.Drawing.Size(991, 217);
            this.lst_Info.SmallImageList = this.imageList1;
            this.lst_Info.TabIndex = 4;
            this.lst_Info.UseCompatibleStateImageBehavior = false;
            this.lst_Info.View = System.Windows.Forms.View.Details;
            // 
            // txt_WriteValue
            // 
            this.txt_WriteValue.Location = new System.Drawing.Point(407, 171);
            this.txt_WriteValue.Name = "txt_WriteValue";
            this.txt_WriteValue.Size = new System.Drawing.Size(397, 31);
            this.txt_WriteValue.TabIndex = 3;
            this.txt_WriteValue.Text = "0";
            this.txt_WriteValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(725, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(134, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "Variable Type:";
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(861, 171);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(152, 34);
            this.btnWrite.TabIndex = 2;
            this.btnWrite.Text = "Write";
            this.btnWrite.UseVisualStyleBackColor = true;
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(861, 118);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(152, 34);
            this.btnRead.TabIndex = 2;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // cmb_VarType
            // 
            this.cmb_VarType.FormattingEnabled = true;
            this.cmb_VarType.Location = new System.Drawing.Point(895, 79);
            this.cmb_VarType.Name = "cmb_VarType";
            this.cmb_VarType.Size = new System.Drawing.Size(118, 25);
            this.cmb_VarType.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(385, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 18);
            this.label8.TabIndex = 0;
            this.label8.Text = "Storage:";
            // 
            // cmb_Storage
            // 
            this.cmb_Storage.FormattingEnabled = true;
            this.cmb_Storage.Location = new System.Drawing.Point(492, 76);
            this.cmb_Storage.Name = "cmb_Storage";
            this.cmb_Storage.Size = new System.Drawing.Size(189, 25);
            this.cmb_Storage.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(47, 182);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(341, 18);
            this.label12.TabIndex = 0;
            this.label12.Text = "Write Value (Split with white sapce):";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(352, 131);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 18);
            this.label11.TabIndex = 0;
            this.label11.Text = "R/W Length:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 128);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(134, 18);
            this.label10.TabIndex = 0;
            this.label10.Text = "Start Address:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 18);
            this.label7.TabIndex = 0;
            this.label7.Text = "Slave Address:";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "info.ico");
            this.imageList1.Images.SetKeyName(1, "warning.ico");
            this.imageList1.Images.SetKeyName(2, "error.ico");
            // 
            // InfoTime
            // 
            this.InfoTime.Text = "Info Time";
            this.InfoTime.Width = 200;
            // 
            // Info
            // 
            this.Info.Text = "Log Info";
            this.Info.Width = 500;
            // 
            // FrmModbusRtu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 744);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI Black", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmModbusRtu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Com Test Platform Based on Modbus Rtu";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cmb_Parity;
        private System.Windows.Forms.ComboBox cmb_Paud;
        private System.Windows.Forms.ComboBox cmb_Dataformat;
        private System.Windows.Forms.ComboBox cmb_Port;
        private System.Windows.Forms.ComboBox cmb_Stopbits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.ComboBox cmb_VarType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmb_Storage;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_WriteValue;
        private System.Windows.Forms.ListView lst_Info;
        private System.Windows.Forms.TextBox tbDatabits;
        private System.Windows.Forms.TextBox txt_Length;
        private System.Windows.Forms.TextBox txt_Address;
        private System.Windows.Forms.TextBox txt_SlaveAdd;
        private System.Windows.Forms.Label lbListBox;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader InfoTime;
        private System.Windows.Forms.ColumnHeader Info;
    }
}

