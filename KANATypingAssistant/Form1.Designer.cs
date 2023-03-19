namespace KANATypingAssistant
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clearTextButton = new System.Windows.Forms.Button();
            this.onButton = new System.Windows.Forms.RadioButton();
            this.toggleFunctionGroup = new System.Windows.Forms.GroupBox();
            this.offButton = new System.Windows.Forms.RadioButton();
            this.toggleFunctionGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(776, 23);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "You can type here to test how it works";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // clearTextButton
            // 
            this.clearTextButton.Location = new System.Drawing.Point(713, 56);
            this.clearTextButton.Name = "clearTextButton";
            this.clearTextButton.Size = new System.Drawing.Size(75, 23);
            this.clearTextButton.TabIndex = 3;
            this.clearTextButton.Text = "Clear";
            this.clearTextButton.UseVisualStyleBackColor = true;
            this.clearTextButton.Click += new System.EventHandler(this.clearTextButton_Click);
            // 
            // onButton
            // 
            this.onButton.AutoSize = true;
            this.onButton.Checked = true;
            this.onButton.Location = new System.Drawing.Point(6, 22);
            this.onButton.Name = "onButton";
            this.onButton.Size = new System.Drawing.Size(43, 19);
            this.onButton.TabIndex = 4;
            this.onButton.TabStop = true;
            this.onButton.Text = "ON";
            this.onButton.UseVisualStyleBackColor = true;
            this.onButton.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // toggleFunctionGroup
            // 
            this.toggleFunctionGroup.Controls.Add(this.offButton);
            this.toggleFunctionGroup.Controls.Add(this.onButton);
            this.toggleFunctionGroup.Location = new System.Drawing.Point(12, 85);
            this.toggleFunctionGroup.Name = "toggleFunctionGroup";
            this.toggleFunctionGroup.Size = new System.Drawing.Size(372, 85);
            this.toggleFunctionGroup.TabIndex = 5;
            this.toggleFunctionGroup.TabStop = false;
            this.toggleFunctionGroup.Text = "Toggle function";
            // 
            // offButton
            // 
            this.offButton.AutoSize = true;
            this.offButton.Location = new System.Drawing.Point(6, 47);
            this.offButton.Name = "offButton";
            this.offButton.Size = new System.Drawing.Size(46, 19);
            this.offButton.TabIndex = 5;
            this.offButton.Text = "OFF";
            this.offButton.UseVisualStyleBackColor = true;
            this.offButton.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 212);
            this.Controls.Add(this.toggleFunctionGroup);
            this.Controls.Add(this.clearTextButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "MainForm";
            this.Text = "KANA Typing Assistant";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toggleFunctionGroup.ResumeLayout(false);
            this.toggleFunctionGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private Button clearTextButton;
        private RadioButton onButton;
        private GroupBox toggleFunctionGroup;
        private RadioButton offButton;
    }
}