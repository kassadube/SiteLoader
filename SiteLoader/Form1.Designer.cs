namespace SiteLoader
{
    partial class Form1
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
            this.normalbrn = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.ParallelAsync = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // normalbrn
            // 
            this.normalbrn.Location = new System.Drawing.Point(27, 41);
            this.normalbrn.Name = "normalbrn";
            this.normalbrn.Size = new System.Drawing.Size(512, 23);
            this.normalbrn.TabIndex = 0;
            this.normalbrn.Text = "normal";
            this.normalbrn.UseVisualStyleBackColor = true;
            this.normalbrn.Click += new System.EventHandler(this.Normal_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(63, 233);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(683, 97);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(63, 388);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(683, 130);
            this.textBox1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(27, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(512, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Async";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Async_Click);
            // 
            // ParallelAsync
            // 
            this.ParallelAsync.Location = new System.Drawing.Point(27, 100);
            this.ParallelAsync.Name = "ParallelAsync";
            this.ParallelAsync.Size = new System.Drawing.Size(512, 23);
            this.ParallelAsync.TabIndex = 5;
            this.ParallelAsync.Text = "Parralel Async";
            this.ParallelAsync.UseVisualStyleBackColor = true;
            this.ParallelAsync.Click += new System.EventHandler(this.ParallelAsync_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 584);
            this.Controls.Add(this.ParallelAsync);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.normalbrn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button normalbrn;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button ParallelAsync;
    }
}

