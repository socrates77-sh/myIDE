namespace SinoSunIDE
{
    partial class FrmProjOption
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProjOption));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Divice = new System.Windows.Forms.TabPage();
            this.Output = new System.Windows.Forms.TabPage();
            this.Debug = new System.Windows.Forms.TabPage();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Divice);
            this.tabControl1.Controls.Add(this.Output);
            this.tabControl1.Controls.Add(this.Debug);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(594, 312);
            this.tabControl1.TabIndex = 0;
            // 
            // Divice
            // 
            this.Divice.BackColor = System.Drawing.Color.Transparent;
            this.Divice.Location = new System.Drawing.Point(4, 22);
            this.Divice.Name = "Divice";
            this.Divice.Padding = new System.Windows.Forms.Padding(3);
            this.Divice.Size = new System.Drawing.Size(586, 286);
            this.Divice.TabIndex = 0;
            this.Divice.Text = "Divice";
            this.Divice.UseVisualStyleBackColor = true;
            // 
            // Output
            // 
            this.Output.Location = new System.Drawing.Point(4, 22);
            this.Output.Name = "Output";
            this.Output.Padding = new System.Windows.Forms.Padding(3);
            this.Output.Size = new System.Drawing.Size(586, 286);
            this.Output.TabIndex = 1;
            this.Output.Text = "Output";
            this.Output.UseVisualStyleBackColor = true;
            // 
            // Debug
            // 
            this.Debug.Location = new System.Drawing.Point(4, 22);
            this.Debug.Name = "Debug";
            this.Debug.Size = new System.Drawing.Size(586, 286);
            this.Debug.TabIndex = 2;
            this.Debug.Text = "Debug";
            this.Debug.UseVisualStyleBackColor = true;
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(372, 349);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 1;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(474, 349);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // FrmProjOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 384);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmProjOption";
            this.Text = "工程设置";
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Divice;
        private System.Windows.Forms.TabPage Output;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.TabPage Debug;
    }
}