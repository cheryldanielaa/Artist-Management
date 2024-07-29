namespace ManajemenArtis_Bersama_Kita_ISA
{
    partial class FormTambahKontrak
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
            this.buttonSimpan = new System.Windows.Forms.Button();
            this.dtpTglAkhir = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpTglMulai = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDeskEvent = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxEvent = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxArtis = new System.Windows.Forms.ComboBox();
            this.comboBoxManajer = new System.Windows.Forms.ComboBox();
            this.buttonKeluar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelID = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNominal = new System.Windows.Forms.TextBox();
            this.dtpTglDibuat = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonKosongi = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTTDEvents = new System.Windows.Forms.Button();
            this.pictureBoxTTDEvents = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnTTDManajer = new System.Windows.Forms.Button();
            this.pictureBoxTTTDManajer = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTTDEvents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTTTDManajer)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSimpan
            // 
            this.buttonSimpan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonSimpan.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSimpan.ForeColor = System.Drawing.Color.White;
            this.buttonSimpan.Location = new System.Drawing.Point(15, 1093);
            this.buttonSimpan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSimpan.Name = "buttonSimpan";
            this.buttonSimpan.Size = new System.Drawing.Size(203, 80);
            this.buttonSimpan.TabIndex = 56;
            this.buttonSimpan.Text = "&SIMPAN";
            this.buttonSimpan.UseVisualStyleBackColor = false;
            this.buttonSimpan.Click += new System.EventHandler(this.buttonSimpan_Click);
            // 
            // dtpTglAkhir
            // 
            this.dtpTglAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTglAkhir.Location = new System.Drawing.Point(331, 416);
            this.dtpTglAkhir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpTglAkhir.Name = "dtpTglAkhir";
            this.dtpTglAkhir.Size = new System.Drawing.Size(329, 31);
            this.dtpTglAkhir.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(37, 418);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(257, 33);
            this.label10.TabIndex = 22;
            this.label10.Text = "Tanggal Berakhir:";
            // 
            // dtpTglMulai
            // 
            this.dtpTglMulai.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTglMulai.Location = new System.Drawing.Point(331, 360);
            this.dtpTglMulai.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpTglMulai.Name = "dtpTglMulai";
            this.dtpTglMulai.Size = new System.Drawing.Size(329, 31);
            this.dtpTglMulai.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(83, 360);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(214, 33);
            this.label9.TabIndex = 20;
            this.label9.Text = "Tanggal Mulai:";
            // 
            // txtDeskEvent
            // 
            this.txtDeskEvent.Location = new System.Drawing.Point(331, 202);
            this.txtDeskEvent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDeskEvent.Multiline = true;
            this.txtDeskEvent.Name = "txtDeskEvent";
            this.txtDeskEvent.Size = new System.Drawing.Size(329, 138);
            this.txtDeskEvent.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(139, 202);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(158, 33);
            this.label8.TabIndex = 18;
            this.label8.Text = "Deskripsi :";
            // 
            // comboBoxEvent
            // 
            this.comboBoxEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEvent.FormattingEnabled = true;
            this.comboBoxEvent.Location = new System.Drawing.Point(331, 146);
            this.comboBoxEvent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxEvent.Name = "comboBoxEvent";
            this.comboBoxEvent.Size = new System.Drawing.Size(329, 33);
            this.comboBoxEvent.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(92, 142);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(203, 33);
            this.label7.TabIndex = 16;
            this.label7.Text = "Nama Events:";
            // 
            // comboBoxArtis
            // 
            this.comboBoxArtis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxArtis.FormattingEnabled = true;
            this.comboBoxArtis.Location = new System.Drawing.Point(331, 29);
            this.comboBoxArtis.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxArtis.Name = "comboBoxArtis";
            this.comboBoxArtis.Size = new System.Drawing.Size(329, 33);
            this.comboBoxArtis.TabIndex = 15;
            // 
            // comboBoxManajer
            // 
            this.comboBoxManajer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxManajer.Enabled = false;
            this.comboBoxManajer.FormattingEnabled = true;
            this.comboBoxManajer.Location = new System.Drawing.Point(331, 82);
            this.comboBoxManajer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxManajer.Name = "comboBoxManajer";
            this.comboBoxManajer.Size = new System.Drawing.Size(335, 33);
            this.comboBoxManajer.TabIndex = 14;
            // 
            // buttonKeluar
            // 
            this.buttonKeluar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonKeluar.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonKeluar.ForeColor = System.Drawing.Color.White;
            this.buttonKeluar.Location = new System.Drawing.Point(796, 1093);
            this.buttonKeluar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonKeluar.Name = "buttonKeluar";
            this.buttonKeluar.Size = new System.Drawing.Size(203, 80);
            this.buttonKeluar.TabIndex = 57;
            this.buttonKeluar.Text = "&KELUAR";
            this.buttonKeluar.UseVisualStyleBackColor = false;
            this.buttonKeluar.Click += new System.EventHandler(this.buttonKeluar_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Tan;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(984, 95);
            this.label1.TabIndex = 54;
            this.label1.Text = "TAMBAH KONTRAK";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelID.Location = new System.Drawing.Point(820, 39);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(32, 33);
            this.labelID.TabIndex = 53;
            this.labelID.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(123, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(175, 33);
            this.label6.TabIndex = 13;
            this.label6.Text = "Nama Artis:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(69, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 33);
            this.label2.TabIndex = 11;
            this.label2.Text = "Nama Manajer:";
            // 
            // txtNominal
            // 
            this.txtNominal.Location = new System.Drawing.Point(331, 472);
            this.txtNominal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNominal.Name = "txtNominal";
            this.txtNominal.Size = new System.Drawing.Size(335, 31);
            this.txtNominal.TabIndex = 10;
            // 
            // dtpTglDibuat
            // 
            this.dtpTglDibuat.Enabled = false;
            this.dtpTglDibuat.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTglDibuat.Location = new System.Drawing.Point(331, 525);
            this.dtpTglDibuat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpTglDibuat.Name = "dtpTglDibuat";
            this.dtpTglDibuat.Size = new System.Drawing.Size(335, 31);
            this.dtpTglDibuat.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 525);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(285, 33);
            this.label4.TabIndex = 5;
            this.label4.Text = "Tanggal Pengajuan:";
            // 
            // buttonKosongi
            // 
            this.buttonKosongi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonKosongi.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonKosongi.ForeColor = System.Drawing.Color.White;
            this.buttonKosongi.Location = new System.Drawing.Point(411, 1093);
            this.buttonKosongi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonKosongi.Name = "buttonKosongi";
            this.buttonKosongi.Size = new System.Drawing.Size(203, 80);
            this.buttonKosongi.TabIndex = 58;
            this.buttonKosongi.Text = "K&OSONGI";
            this.buttonKosongi.UseVisualStyleBackColor = false;
            this.buttonKosongi.Click += new System.EventHandler(this.buttonKosongi_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(160, 471);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 33);
            this.label5.TabIndex = 9;
            this.label5.Text = "Nominal:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.panel1.Controls.Add(this.btnTTDEvents);
            this.panel1.Controls.Add(this.pictureBoxTTDEvents);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.btnTTDManajer);
            this.panel1.Controls.Add(this.pictureBoxTTTDManajer);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.dtpTglAkhir);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.dtpTglMulai);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtDeskEvent);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.comboBoxEvent);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.comboBoxArtis);
            this.panel1.Controls.Add(this.comboBoxManajer);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtNominal);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dtpTglDibuat);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(15, 145);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 934);
            this.panel1.TabIndex = 55;
            // 
            // btnTTDEvents
            // 
            this.btnTTDEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTTDEvents.Location = new System.Drawing.Point(166, 856);
            this.btnTTDEvents.Margin = new System.Windows.Forms.Padding(4);
            this.btnTTDEvents.Name = "btnTTDEvents";
            this.btnTTDEvents.Size = new System.Drawing.Size(117, 56);
            this.btnTTDEvents.TabIndex = 57;
            this.btnTTDEvents.Text = "TTD";
            this.btnTTDEvents.UseVisualStyleBackColor = true;
            this.btnTTDEvents.Click += new System.EventHandler(this.btnTTDEvents_Click);
            // 
            // pictureBoxTTDEvents
            // 
            this.pictureBoxTTDEvents.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxTTDEvents.Location = new System.Drawing.Point(122, 670);
            this.pictureBoxTTDEvents.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxTTDEvents.Name = "pictureBoxTTDEvents";
            this.pictureBoxTTDEvents.Size = new System.Drawing.Size(195, 179);
            this.pictureBoxTTDEvents.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTTDEvents.TabIndex = 56;
            this.pictureBoxTTDEvents.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(131, 621);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(168, 33);
            this.label12.TabIndex = 55;
            this.label12.Text = "TTD Events";
            // 
            // btnTTDManajer
            // 
            this.btnTTDManajer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTTDManajer.Location = new System.Drawing.Point(718, 856);
            this.btnTTDManajer.Margin = new System.Windows.Forms.Padding(4);
            this.btnTTDManajer.Name = "btnTTDManajer";
            this.btnTTDManajer.Size = new System.Drawing.Size(117, 56);
            this.btnTTDManajer.TabIndex = 54;
            this.btnTTDManajer.Text = "TTD";
            this.btnTTDManajer.UseVisualStyleBackColor = true;
            this.btnTTDManajer.Click += new System.EventHandler(this.btnTTDManajer_Click);
            // 
            // pictureBoxTTTDManajer
            // 
            this.pictureBoxTTTDManajer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxTTTDManajer.Location = new System.Drawing.Point(673, 670);
            this.pictureBoxTTTDManajer.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxTTTDManajer.Name = "pictureBoxTTTDManajer";
            this.pictureBoxTTTDManajer.Size = new System.Drawing.Size(195, 179);
            this.pictureBoxTTTDManajer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTTTDManajer.TabIndex = 53;
            this.pictureBoxTTTDManajer.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(680, 621);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(188, 33);
            this.label11.TabIndex = 52;
            this.label11.Text = "TTD Manajer";
            // 
            // FormTambahKontrak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(1027, 1184);
            this.Controls.Add(this.buttonSimpan);
            this.Controls.Add(this.buttonKeluar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.buttonKosongi);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormTambahKontrak";
            this.Text = "FormTambahKontrak";
            this.Load += new System.EventHandler(this.FormTambahKontrak_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTTDEvents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTTTDManajer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSimpan;
        private System.Windows.Forms.DateTimePicker dtpTglAkhir;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpTglMulai;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDeskEvent;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxEvent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxArtis;
        private System.Windows.Forms.ComboBox comboBoxManajer;
        private System.Windows.Forms.Button buttonKeluar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNominal;
        private System.Windows.Forms.DateTimePicker dtpTglDibuat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonKosongi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTTDEvents;
        public System.Windows.Forms.PictureBox pictureBoxTTDEvents;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnTTDManajer;
        public System.Windows.Forms.PictureBox pictureBoxTTTDManajer;
        private System.Windows.Forms.Label label11;
    }
}