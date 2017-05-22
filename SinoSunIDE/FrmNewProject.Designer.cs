namespace SinoSunIDE
{
    partial class NewProject_From
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProject_From));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel_step1 = new System.Windows.Forms.Panel();
            this.rbC = new System.Windows.Forms.RadioButton();
            this.rbASM = new System.Windows.Forms.RadioButton();
            this.labelCompiler = new System.Windows.Forms.Label();
            this.CompilerSelect = new System.Windows.Forms.ComboBox();
            this.checkBoxCreateDirectory = new System.Windows.Forms.CheckBox();
            this.MCUSelect_comboBox = new System.Windows.Forms.ComboBox();
            this.MCUSelect_label = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.ProjectPath_comboBox = new System.Windows.Forms.ComboBox();
            this.labelProjectLoadPath = new System.Windows.Forms.Label();
            this.ProjectName_textBox = new System.Windows.Forms.TextBox();
            this.labelProjectName = new System.Windows.Forms.Label();
            this.ProjectAbstractepanel = new System.Windows.Forms.Panel();
            this.ProjectAbstractRichTextBox = new System.Windows.Forms.RichTextBox();
            this.btCancle = new System.Windows.Forms.Button();
            this.btNextStep = new System.Windows.Forms.Button();
            this.bt_backStep = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel_step1.SuspendLayout();
            this.ProjectAbstractepanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel_step1);
            this.splitContainer1.Panel2.Controls.Add(this.ProjectAbstractepanel);
            this.splitContainer1.Panel2.Controls.Add(this.btCancle);
            this.splitContainer1.Panel2.Controls.Add(this.btNextStep);
            this.splitContainer1.Panel2.Controls.Add(this.bt_backStep);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::SinoSunIDE.Properties.Resources.h;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // panel_step1
            // 
            this.panel_step1.Controls.Add(this.rbC);
            this.panel_step1.Controls.Add(this.rbASM);
            this.panel_step1.Controls.Add(this.labelCompiler);
            this.panel_step1.Controls.Add(this.CompilerSelect);
            this.panel_step1.Controls.Add(this.checkBoxCreateDirectory);
            this.panel_step1.Controls.Add(this.MCUSelect_comboBox);
            this.panel_step1.Controls.Add(this.MCUSelect_label);
            this.panel_step1.Controls.Add(this.button1);
            this.panel_step1.Controls.Add(this.ProjectPath_comboBox);
            this.panel_step1.Controls.Add(this.labelProjectLoadPath);
            this.panel_step1.Controls.Add(this.ProjectName_textBox);
            this.panel_step1.Controls.Add(this.labelProjectName);
            resources.ApplyResources(this.panel_step1, "panel_step1");
            this.panel_step1.Name = "panel_step1";
            // 
            // rbC
            // 
            resources.ApplyResources(this.rbC, "rbC");
            this.rbC.Name = "rbC";
            this.rbC.TabStop = true;
            this.rbC.UseVisualStyleBackColor = true;
            // 
            // rbASM
            // 
            resources.ApplyResources(this.rbASM, "rbASM");
            this.rbASM.Name = "rbASM";
            this.rbASM.TabStop = true;
            this.rbASM.UseVisualStyleBackColor = true;
            // 
            // labelCompiler
            // 
            resources.ApplyResources(this.labelCompiler, "labelCompiler");
            this.labelCompiler.Name = "labelCompiler";
            // 
            // CompilerSelect
            // 
            resources.ApplyResources(this.CompilerSelect, "CompilerSelect");
            this.CompilerSelect.FormattingEnabled = true;
            this.CompilerSelect.Name = "CompilerSelect";
            // 
            // checkBoxCreateDirectory
            // 
            resources.ApplyResources(this.checkBoxCreateDirectory, "checkBoxCreateDirectory");
            this.checkBoxCreateDirectory.Name = "checkBoxCreateDirectory";
            this.checkBoxCreateDirectory.UseVisualStyleBackColor = true;
            // 
            // MCUSelect_comboBox
            // 
            resources.ApplyResources(this.MCUSelect_comboBox, "MCUSelect_comboBox");
            this.MCUSelect_comboBox.FormattingEnabled = true;
            this.MCUSelect_comboBox.Name = "MCUSelect_comboBox";
            this.MCUSelect_comboBox.SelectedIndexChanged += new System.EventHandler(this.MCUSelectItemsChange);
            // 
            // MCUSelect_label
            // 
            resources.ApplyResources(this.MCUSelect_label, "MCUSelect_label");
            this.MCUSelect_label.Name = "MCUSelect_label";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ProjectPath_comboBox
            // 
            resources.ApplyResources(this.ProjectPath_comboBox, "ProjectPath_comboBox");
            this.ProjectPath_comboBox.FormattingEnabled = true;
            this.ProjectPath_comboBox.Name = "ProjectPath_comboBox";
            // 
            // labelProjectLoadPath
            // 
            resources.ApplyResources(this.labelProjectLoadPath, "labelProjectLoadPath");
            this.labelProjectLoadPath.Name = "labelProjectLoadPath";
            // 
            // ProjectName_textBox
            // 
            resources.ApplyResources(this.ProjectName_textBox, "ProjectName_textBox");
            this.ProjectName_textBox.Name = "ProjectName_textBox";
            // 
            // labelProjectName
            // 
            resources.ApplyResources(this.labelProjectName, "labelProjectName");
            this.labelProjectName.Name = "labelProjectName";
            // 
            // ProjectAbstractepanel
            // 
            this.ProjectAbstractepanel.Controls.Add(this.ProjectAbstractRichTextBox);
            resources.ApplyResources(this.ProjectAbstractepanel, "ProjectAbstractepanel");
            this.ProjectAbstractepanel.Name = "ProjectAbstractepanel";
            // 
            // ProjectAbstractRichTextBox
            // 
            this.ProjectAbstractRichTextBox.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.ProjectAbstractRichTextBox, "ProjectAbstractRichTextBox");
            this.ProjectAbstractRichTextBox.Name = "ProjectAbstractRichTextBox";
            // 
            // btCancle
            // 
            resources.ApplyResources(this.btCancle, "btCancle");
            this.btCancle.Name = "btCancle";
            this.btCancle.UseVisualStyleBackColor = true;
            this.btCancle.Click += new System.EventHandler(this.btCancle_Click);
            // 
            // btNextStep
            // 
            resources.ApplyResources(this.btNextStep, "btNextStep");
            this.btNextStep.Name = "btNextStep";
            this.btNextStep.Tag = "1";
            this.btNextStep.UseVisualStyleBackColor = true;
            this.btNextStep.Click += new System.EventHandler(this.btNextStep_Click);
            // 
            // bt_backStep
            // 
            resources.ApplyResources(this.bt_backStep, "bt_backStep");
            this.bt_backStep.Name = "bt_backStep";
            this.bt_backStep.UseVisualStyleBackColor = true;
            this.bt_backStep.Click += new System.EventHandler(this.bt_backStep_Click);
            // 
            // NewProject_From
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "NewProject_From";
            this.Load += new System.EventHandler(this.NewProject_From_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel_step1.ResumeLayout(false);
            this.panel_step1.PerformLayout();
            this.ProjectAbstractepanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btCancle;
        private System.Windows.Forms.Button btNextStep;
        private System.Windows.Forms.Button bt_backStep;
        private System.Windows.Forms.Panel panel_step1;
        private System.Windows.Forms.Label labelProjectLoadPath;
        private System.Windows.Forms.TextBox ProjectName_textBox;
        private System.Windows.Forms.Label labelProjectName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox ProjectPath_comboBox;
        private System.Windows.Forms.ComboBox MCUSelect_comboBox;
        private System.Windows.Forms.Label MCUSelect_label;
        private System.Windows.Forms.Panel ProjectAbstractepanel;
        private System.Windows.Forms.RichTextBox ProjectAbstractRichTextBox;
        private System.Windows.Forms.CheckBox checkBoxCreateDirectory;
        private System.Windows.Forms.Label labelCompiler;
        private System.Windows.Forms.ComboBox CompilerSelect;
        private System.Windows.Forms.RadioButton rbC;
        private System.Windows.Forms.RadioButton rbASM;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}