namespace SiteLoader
{
    partial class Form3
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
            this.txtReqCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ParallelAsync = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtReqCount
            // 
            this.txtReqCount.Location = new System.Drawing.Point(191, -1);
            this.txtReqCount.Name = "txtReqCount";
            this.txtReqCount.Size = new System.Drawing.Size(100, 20);
            this.txtReqCount.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "label1";
            // 
            // ParallelAsync
            // 
            this.ParallelAsync.Location = new System.Drawing.Point(12, 2);
            this.ParallelAsync.Name = "ParallelAsync";
            this.ParallelAsync.Size = new System.Drawing.Size(118, 23);
            this.ParallelAsync.TabIndex = 15;
            this.ParallelAsync.Text = "Parralel Async";
            this.ParallelAsync.UseVisualStyleBackColor = true;
            this.ParallelAsync.Click += new System.EventHandler(this.ParallelAsync_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(86, 156);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(683, 308);
            this.textBox1.TabIndex = 13;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 450);
            this.Controls.Add(this.txtReqCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ParallelAsync);
            this.Controls.Add(this.textBox1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtReqCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ParallelAsync;
        private System.Windows.Forms.TextBox textBox1;
    }
}