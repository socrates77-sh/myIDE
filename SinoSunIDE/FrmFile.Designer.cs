namespace SinoSunIDE
{
    partial class FrmFile
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
            this.tsFiles = new FarsiLibrary.Win.FATabStrip();
            ((System.ComponentModel.ISupportInitialize)(this.tsFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // tsFiles
            // 
            this.tsFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsFiles.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.tsFiles.Location = new System.Drawing.Point(0, 0);
            this.tsFiles.Name = "tsFiles";
            this.tsFiles.Size = new System.Drawing.Size(559, 313);
            this.tsFiles.TabIndex = 0;
            this.tsFiles.Text = "faTabStrip1";
            // 
            // FrmFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 313);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.ControlBox = false;
            this.Controls.Add(this.tsFiles);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideOnClose = true;
            this.Name = "FrmFile";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "程序编辑区";
            ((System.ComponentModel.ISupportInitialize)(this.tsFiles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public FarsiLibrary.Win.FATabStrip tsFiles;
    }
}