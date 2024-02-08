namespace WinReproductorMp3
{
    partial class Loging
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.txtu = new System.Windows.Forms.Button();
            this.txtUs = new System.Windows.Forms.TextBox();
            this.txtp = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(120, 316);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Registro";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtu
            // 
            this.txtu.Location = new System.Drawing.Point(284, 316);
            this.txtu.Name = "txtu";
            this.txtu.Size = new System.Drawing.Size(75, 23);
            this.txtu.TabIndex = 1;
            this.txtu.Text = "Loging";
            this.txtu.UseVisualStyleBackColor = true;
            this.txtu.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtUs
            // 
            this.txtUs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUs.Location = new System.Drawing.Point(120, 175);
            this.txtUs.Name = "txtUs";
            this.txtUs.Size = new System.Drawing.Size(239, 26);
            this.txtUs.TabIndex = 2;
            this.txtUs.Text = "Usuario";
            this.txtUs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUs.Click += new System.EventHandler(this.textBox1_Click);
            this.txtUs.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // txtp
            // 
            this.txtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtp.Location = new System.Drawing.Point(120, 235);
            this.txtp.Name = "txtp";
            this.txtp.Size = new System.Drawing.Size(239, 26);
            this.txtp.TabIndex = 3;
            this.txtp.Text = "Contraseña";
            this.txtp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtp.Click += new System.EventHandler(this.textBox2_Click);
            this.txtp.TextChanged += new System.EventHandler(this.txtp_TextChanged);
            this.txtp.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::WinReproductorMp3.Properties.Resources.Sin_título_1;
            this.pictureBox1.Location = new System.Drawing.Point(189, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(99, 94);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.ForeColor = System.Drawing.Color.DarkTurquoise;
            this.label9.Location = new System.Drawing.Point(164, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Paax";
            // 
            // Loging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WinReproductorMp3.Properties.Resources._374764_turquoise_wallpaper2;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtp);
            this.Controls.Add(this.txtUs);
            this.Controls.Add(this.txtu);
            this.Controls.Add(this.button1);
            this.Name = "Loging";
            this.Text = "Appax";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Loging_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button txtu;
        private System.Windows.Forms.TextBox txtUs;
        private System.Windows.Forms.TextBox txtp;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label9;
    }
}

