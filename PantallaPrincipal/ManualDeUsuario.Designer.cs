namespace Proyecto
{
    partial class ManualDeUsuario
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
            this.richTxt = new System.Windows.Forms.RichTextBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTxt
            // 
            this.richTxt.Location = new System.Drawing.Point(12, 12);
            this.richTxt.Name = "richTxt";
            this.richTxt.ReadOnly = true;
            this.richTxt.Size = new System.Drawing.Size(794, 850);
            this.richTxt.TabIndex = 0;
            this.richTxt.Text = "";
            this.richTxt.VScroll += new System.EventHandler(this.richTxt_VScroll);
            // 
            // btnCerrar
            // 
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(824, 23);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(62, 61);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // ManualDeUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 887);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.richTxt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ManualDeUsuario";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ManualDeUsuario_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTxt;
        private System.Windows.Forms.Button btnCerrar;
    }
}