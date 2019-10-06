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
            this.SuspendLayout();
            // 
            // ParallelAsync
            // 
            this.ParallelAsync.Location = new System.Drawing.Point(130, 115);
            this.ParallelAsync.Name = "ParallelAsync";
            this.ParallelAsync.Size = new System.Drawing.Size(512, 23);
            this.ParallelAsync.TabIndex = 9;
            this.ParallelAsync.Text = "Parralel Async";
            this.ParallelAsync.UseVisualStyleBackColor = true;
            this.ParallelAsync.Click += new System.EventHandler(this.ParallelAsync_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(130, 86);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(512, 23);
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
            this.normalbrn.Location = new System.Drawing.Point(130, 56);
            this.normalbrn.Name = "normalbrn";
            this.normalbrn.Size = new System.Drawing.Size(512, 23);
            this.normalbrn.TabIndex = 6;
            this.normalbrn.Text = "normal";
            this.normalbrn.UseVisualStyleBackColor = true;
            this.normalbrn.Click += new System.EventHandler(this.Normalbrn_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 582);
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
    }
}