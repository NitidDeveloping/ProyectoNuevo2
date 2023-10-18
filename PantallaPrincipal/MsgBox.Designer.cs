namespace Proyecto
{
    partial class MsgBox
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
            this.pL1 = new System.Windows.Forms.Panel();
            this.pL2 = new System.Windows.Forms.Panel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSi = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.pbExito = new System.Windows.Forms.PictureBox();
            this.pbQuest = new System.Windows.Forms.PictureBox();
            this.pbWar = new System.Windows.Forms.PictureBox();
            this.pbError = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExito)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbQuest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbError)).BeginInit();
            this.SuspendLayout();
            // 
            // pL1
            // 
            this.pL1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pL1.Location = new System.Drawing.Point(0, 64);
            this.pL1.Name = "pL1";
            this.pL1.Size = new System.Drawing.Size(481, 4);
            this.pL1.TabIndex = 0;
            // 
            // pL2
            // 
            this.pL2.BackColor = System.Drawing.Color.Silver;
            this.pL2.Location = new System.Drawing.Point(0, 191);
            this.pL2.Name = "pL2";
            this.pL2.Size = new System.Drawing.Size(481, 4);
            this.pL2.TabIndex = 1;
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(13, 76);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(463, 112);
            this.lblMsg.TabIndex = 3;
            this.lblMsg.Text = "label1";
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(111)))), ((int)(((byte)(230)))));
            this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAceptar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(111)))), ((int)(((byte)(230)))));
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(174, 206);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(136, 59);
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.Black;
            this.lblTitulo.Location = new System.Drawing.Point(69, 23);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(70, 25);
            this.lblTitulo.TabIndex = 2;
            this.lblTitulo.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnSi);
            this.panel1.Controls.Add(this.btnNo);
            this.panel1.Controls.Add(this.pbExito);
            this.panel1.Controls.Add(this.pbQuest);
            this.panel1.Controls.Add(this.pbWar);
            this.panel1.Controls.Add(this.btnAceptar);
            this.panel1.Controls.Add(this.pbError);
            this.panel1.Controls.Add(this.lblMsg);
            this.panel1.Controls.Add(this.lblTitulo);
            this.panel1.Controls.Add(this.pL2);
            this.panel1.Controls.Add(this.pL1);
            this.panel1.Location = new System.Drawing.Point(5, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(485, 276);
            this.panel1.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(241, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(2, 65);
            this.label3.TabIndex = 11;
            this.label3.Visible = false;
            // 
            // btnSi
            // 
            this.btnSi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(111)))), ((int)(((byte)(230)))));
            this.btnSi.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnSi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(111)))), ((int)(((byte)(230)))));
            this.btnSi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSi.ForeColor = System.Drawing.Color.White;
            this.btnSi.Location = new System.Drawing.Point(99, 206);
            this.btnSi.Name = "btnSi";
            this.btnSi.Size = new System.Drawing.Size(136, 59);
            this.btnSi.TabIndex = 10;
            this.btnSi.Text = "Sí";
            this.btnSi.UseVisualStyleBackColor = false;
            this.btnSi.Visible = false;
            this.btnSi.Click += new System.EventHandler(this.btnSiLogout_Click);
            // 
            // btnNo
            // 
            this.btnNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(111)))), ((int)(((byte)(230)))));
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnNo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(111)))), ((int)(((byte)(230)))));
            this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNo.ForeColor = System.Drawing.Color.White;
            this.btnNo.Location = new System.Drawing.Point(249, 206);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(136, 59);
            this.btnNo.TabIndex = 9;
            this.btnNo.Text = "No";
            this.btnNo.UseVisualStyleBackColor = false;
            this.btnNo.Visible = false;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // pbExito
            // 
            this.pbExito.Image = global::Proyecto.Properties.Resources.Exclamation;
            this.pbExito.Location = new System.Drawing.Point(13, 10);
            this.pbExito.Name = "pbExito";
            this.pbExito.Size = new System.Drawing.Size(50, 50);
            this.pbExito.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbExito.TabIndex = 8;
            this.pbExito.TabStop = false;
            this.pbExito.Visible = false;
            // 
            // pbQuest
            // 
            this.pbQuest.Image = global::Proyecto.Properties.Resources.Question;
            this.pbQuest.Location = new System.Drawing.Point(13, 10);
            this.pbQuest.Name = "pbQuest";
            this.pbQuest.Size = new System.Drawing.Size(50, 50);
            this.pbQuest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbQuest.TabIndex = 7;
            this.pbQuest.TabStop = false;
            this.pbQuest.Visible = false;
            // 
            // pbWar
            // 
            this.pbWar.Image = global::Proyecto.Properties.Resources.Warning;
            this.pbWar.Location = new System.Drawing.Point(13, 10);
            this.pbWar.Name = "pbWar";
            this.pbWar.Size = new System.Drawing.Size(50, 50);
            this.pbWar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbWar.TabIndex = 6;
            this.pbWar.TabStop = false;
            this.pbWar.Visible = false;
            // 
            // pbError
            // 
            this.pbError.Image = global::Proyecto.Properties.Resources.Error;
            this.pbError.Location = new System.Drawing.Point(13, 10);
            this.pbError.Name = "pbError";
            this.pbError.Size = new System.Drawing.Size(50, 50);
            this.pbError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbError.TabIndex = 4;
            this.pbError.TabStop = false;
            this.pbError.Visible = false;
            // 
            // MsgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(494, 285);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MsgBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Error";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExito)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbQuest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbError)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pL1;
        private System.Windows.Forms.Panel pL2;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.PictureBox pbError;
        private System.Windows.Forms.PictureBox pbWar;
        private System.Windows.Forms.PictureBox pbQuest;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbExito;
        public System.Windows.Forms.Button btnAceptar;
        public System.Windows.Forms.Button btnSi;
        public System.Windows.Forms.Button btnNo;
        public System.Windows.Forms.Label label3;
    }
}