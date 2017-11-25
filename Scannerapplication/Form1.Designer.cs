using System.Windows.Forms;
namespace Scannerapplication
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.showImages = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnl_capture = new System.Windows.Forms.Panel();
            this.Panel1 = new ns1.BunifuGradientPanel();
            this.msgsplash = new ns1.BunifuCustomLabel();
            this.bunifuImageButton2 = new ns1.BunifuImageButton();
            this.bunifuImageButton1 = new ns1.BunifuImageButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvHuespedes = new System.Windows.Forms.DataGridView();
            this.VN_SECUENCIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VN_APELLIDO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VN_NOMBRE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VN_PASAPORTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESCANEAR = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pic_scan = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.bunifuElipse1 = new ns1.BunifuElipse(this.components);
            this.bunifuElipse2 = new ns1.BunifuElipse(this.components);
            this.bunifuElipse3 = new ns1.BunifuElipse(this.components);
            this.bunifuElipse4 = new ns1.BunifuElipse(this.components);
            this.pnl_capture.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHuespedes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_scan)).BeginInit();
            this.SuspendLayout();
            // 
            // showImages
            // 
            this.showImages.Location = new System.Drawing.Point(242, 216);
            this.showImages.Name = "showImages";
            this.showImages.Size = new System.Drawing.Size(85, 23);
            this.showImages.TabIndex = 5;
            this.showImages.Text = "Show Images";
            this.showImages.UseVisualStyleBackColor = true;
            this.showImages.Visible = false;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(767, 3);
            this.splitter1.TabIndex = 38;
            this.splitter1.TabStop = false;
            // 
            // pnl_capture
            // 
            this.pnl_capture.BackColor = System.Drawing.Color.Transparent;
            this.pnl_capture.Controls.Add(this.Panel1);
            this.pnl_capture.Controls.Add(this.bunifuImageButton1);
            this.pnl_capture.Controls.Add(this.label1);
            this.pnl_capture.Controls.Add(this.dgvHuespedes);
            this.pnl_capture.Controls.Add(this.pic_scan);
            this.pnl_capture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_capture.Location = new System.Drawing.Point(0, 3);
            this.pnl_capture.Name = "pnl_capture";
            this.pnl_capture.Size = new System.Drawing.Size(767, 670);
            this.pnl_capture.TabIndex = 39;
            // 
            // Panel1
            // 
            this.Panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel1.BackgroundImage")));
            this.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel1.Controls.Add(this.msgsplash);
            this.Panel1.Controls.Add(this.bunifuImageButton2);
            this.Panel1.GradientBottomLeft = System.Drawing.Color.DarkRed;
            this.Panel1.GradientBottomRight = System.Drawing.Color.DarkRed;
            this.Panel1.GradientTopLeft = System.Drawing.Color.Red;
            this.Panel1.GradientTopRight = System.Drawing.Color.Red;
            this.Panel1.Location = new System.Drawing.Point(0, 228);
            this.Panel1.Name = "Panel1";
            this.Panel1.Quality = 10;
            this.Panel1.Size = new System.Drawing.Size(767, 50);
            this.Panel1.TabIndex = 13;
            // 
            // msgsplash
            // 
            this.msgsplash.AutoSize = true;
            this.msgsplash.BackColor = System.Drawing.Color.Transparent;
            this.msgsplash.Cursor = System.Windows.Forms.Cursors.No;
            this.msgsplash.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.msgsplash.Font = new System.Drawing.Font("Candara", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msgsplash.ForeColor = System.Drawing.Color.White;
            this.msgsplash.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.msgsplash.Location = new System.Drawing.Point(31, 18);
            this.msgsplash.Margin = new System.Windows.Forms.Padding(0);
            this.msgsplash.Name = "msgsplash";
            this.msgsplash.Size = new System.Drawing.Size(31, 29);
            this.msgsplash.TabIndex = 14;
            this.msgsplash.Text = "...";
            this.msgsplash.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // bunifuImageButton2
            // 
            this.bunifuImageButton2.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton2.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton2.Image")));
            this.bunifuImageButton2.ImageActive = null;
            this.bunifuImageButton2.Location = new System.Drawing.Point(0, 0);
            this.bunifuImageButton2.Name = "bunifuImageButton2";
            this.bunifuImageButton2.Size = new System.Drawing.Size(31, 30);
            this.bunifuImageButton2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton2.TabIndex = 0;
            this.bunifuImageButton2.TabStop = false;
            this.bunifuImageButton2.Zoom = 10;
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton1.Image")));
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(717, 0);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(50, 43);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton1.TabIndex = 12;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 40;
            this.bunifuImageButton1.Click += new System.EventHandler(this.bunifuImageButton1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(257, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "SELECCIONE EL PAX A ESCANEAR";
            // 
            // dgvHuespedes
            // 
            this.dgvHuespedes.AllowUserToAddRows = false;
            this.dgvHuespedes.AllowUserToDeleteRows = false;
            this.dgvHuespedes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHuespedes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VN_SECUENCIA,
            this.VN_APELLIDO,
            this.VN_NOMBRE,
            this.VN_PASAPORTE,
            this.ESCANEAR});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHuespedes.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvHuespedes.Location = new System.Drawing.Point(99, 43);
            this.dgvHuespedes.MultiSelect = false;
            this.dgvHuespedes.Name = "dgvHuespedes";
            this.dgvHuespedes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHuespedes.Size = new System.Drawing.Size(569, 179);
            this.dgvHuespedes.TabIndex = 9;
            // 
            // VN_SECUENCIA
            // 
            this.VN_SECUENCIA.DataPropertyName = "VN_SECUENCIA";
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.VN_SECUENCIA.DefaultCellStyle = dataGridViewCellStyle1;
            this.VN_SECUENCIA.HeaderText = "SECUENCIA";
            this.VN_SECUENCIA.Name = "VN_SECUENCIA";
            this.VN_SECUENCIA.Width = 80;
            // 
            // VN_APELLIDO
            // 
            this.VN_APELLIDO.DataPropertyName = "VN_APELLIDO";
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.VN_APELLIDO.DefaultCellStyle = dataGridViewCellStyle2;
            this.VN_APELLIDO.HeaderText = "APELLIDO";
            this.VN_APELLIDO.Name = "VN_APELLIDO";
            this.VN_APELLIDO.ReadOnly = true;
            this.VN_APELLIDO.Width = 120;
            // 
            // VN_NOMBRE
            // 
            this.VN_NOMBRE.DataPropertyName = "VN_NOMBRE";
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.VN_NOMBRE.DefaultCellStyle = dataGridViewCellStyle3;
            this.VN_NOMBRE.HeaderText = "NOMBRE";
            this.VN_NOMBRE.Name = "VN_NOMBRE";
            this.VN_NOMBRE.ReadOnly = true;
            this.VN_NOMBRE.Width = 120;
            // 
            // VN_PASAPORTE
            // 
            this.VN_PASAPORTE.DataPropertyName = "VN_PASAPORTE";
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.VN_PASAPORTE.DefaultCellStyle = dataGridViewCellStyle4;
            this.VN_PASAPORTE.HeaderText = "PASAPORTE";
            this.VN_PASAPORTE.Name = "VN_PASAPORTE";
            this.VN_PASAPORTE.ReadOnly = true;
            this.VN_PASAPORTE.Width = 120;
            // 
            // ESCANEAR
            // 
            this.ESCANEAR.DataPropertyName = "ESCANEAR";
            this.ESCANEAR.FalseValue = "N";
            this.ESCANEAR.HeaderText = "ESCANEAR";
            this.ESCANEAR.IndeterminateValue = "N";
            this.ESCANEAR.Name = "ESCANEAR";
            this.ESCANEAR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ESCANEAR.TrueValue = "Y";
            this.ESCANEAR.Width = 70;
            // 
            // pic_scan
            // 
            this.pic_scan.BackColor = System.Drawing.Color.LightGray;
            this.pic_scan.Location = new System.Drawing.Point(99, 284);
            this.pic_scan.Name = "pic_scan";
            this.pic_scan.Size = new System.Drawing.Size(569, 355);
            this.pic_scan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_scan.TabIndex = 6;
            this.pic_scan.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.InitialiseTimer);
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 10;
            this.bunifuElipse1.TargetControl = this;
            // 
            // bunifuElipse2
            // 
            this.bunifuElipse2.ElipseRadius = 5;
            this.bunifuElipse2.TargetControl = this;
            // 
            // bunifuElipse3
            // 
            this.bunifuElipse3.ElipseRadius = 5;
            this.bunifuElipse3.TargetControl = this;
            // 
            // bunifuElipse4
            // 
            this.bunifuElipse4.ElipseRadius = 5;
            this.bunifuElipse4.TargetControl = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(767, 673);
            this.Controls.Add(this.pnl_capture);
            this.Controls.Add(this.splitter1);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Passport Reader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnl_capture.ResumeLayout(false);
            this.pnl_capture.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHuespedes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_scan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion



        //camera
        private System.Windows.Forms.Button showImages;
        private Splitter splitter1;
        private Panel pnl_capture;
        private DataGridView dgvHuespedes;
        private Label label1;
        private Timer timer1;
        private ns1.BunifuElipse bunifuElipse1;
        private ns1.BunifuElipse bunifuElipse2;
        private ns1.BunifuImageButton bunifuImageButton1;
        private ns1.BunifuElipse bunifuElipse3;
        private ns1.BunifuElipse bunifuElipse4;
        protected PictureBox pic_scan;
        private ns1.BunifuGradientPanel Panel1;
        private ns1.BunifuImageButton bunifuImageButton2;
        private ns1.BunifuCustomLabel msgsplash;
        private DataGridViewTextBoxColumn VN_SECUENCIA;
        private DataGridViewTextBoxColumn VN_APELLIDO;
        private DataGridViewTextBoxColumn VN_NOMBRE;
        private DataGridViewTextBoxColumn VN_PASAPORTE;
        private DataGridViewCheckBoxColumn ESCANEAR;
    }
}

