namespace SinoSunIDE
{
    partial class FrmCOMSeting
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
            this.COMNameList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // COMNameList
            // 
            this.COMNameList.FormattingEnabled = true;
            this.COMNameList.Location = new System.Drawing.Point(26, 46);
            this.COMNameList.Name = "COMNameList";
            this.COMNameList.Size = new System.Drawing.Size(121, 20);
            this.COMNameList.TabIndex = 0;
            // 
            // FrmCOMSeting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 193);
            this.Controls.Add(this.COMNameList);
            this.Name = "FrmCOMSeting";
            this.Text = "COM Seting";
            this.Load += new System.EventHandler(this.FrmCOMSeting_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox COMNameList;
    }
}