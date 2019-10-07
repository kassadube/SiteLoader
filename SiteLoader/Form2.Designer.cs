namespace SiteLoader
{
    partial class Form2
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
            this.ParallelAsync = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.normalbrn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtReqCount = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ParallelAsync
            // 
            this.ParallelAsync.Location = new System.Drawing.Point(0, 62);
            this.ParallelAsync.Name = "ParallelAsync";
            this.ParallelAsync.Size = new System.Drawing.Size(118, 23);
            this.ParallelAsync.TabIndex = 9;
            this.ParallelAsync.Text = "Parralel Async";
            this.ParallelAsync.UseVisualStyleBackColor = true;
            this.ParallelAsync.Click += new System.EventHandler(this.ParallelAsync_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Async";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Async_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(54, 172);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(683, 308);
            this.textBox1.TabIndex = 7;
            // 
            // normalbrn
            // 
            this.normalbrn.Location = new System.Drawing.Point(0, 3);
            this.normalbrn.Name = "normalbrn";
            this.normalbrn.Size = new System.Drawing.Size(118, 23);
            this.normalbrn.TabIndex = 6;
            this.normalbrn.Text = "normal";
            this.normalbrn.UseVisualStyleBackColor = true;
            this.normalbrn.Click += new System.EventHandler(this.Normalbrn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(299, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            // 
            // txtReqCount
            // 
            this.txtReqCount.Location = new System.Drawing.Point(176, 3);
            this.txtReqCount.Name = "txtReqCount";
            this.txtReqCount.Size = new System.Drawing.Size(100, 20);
            this.txtReqCount.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(973, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 582);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtReqCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ParallelAsync);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.normalbrn);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ParallelAsync;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button normalbrn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReqCount;
        private System.Windows.Forms.Button button1;
    }
}