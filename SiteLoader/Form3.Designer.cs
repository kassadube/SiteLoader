using Serilog;

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
            Log.Debug("DISPOSE");
            Log.CloseAndFlush();
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
            this.btnClear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTimeFromStart = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtRedoEvery = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStopPeriodic = new System.Windows.Forms.Button();
            this.txtSummary = new System.Windows.Forms.TextBox();
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
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 16;
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
            this.textBox1.Size = new System.Drawing.Size(430, 308);
            this.textBox1.TabIndex = 13;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(1072, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(420, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Time from start";
            // 
            // lblTimeFromStart
            // 
            this.lblTimeFromStart.AutoSize = true;
            this.lblTimeFromStart.Location = new System.Drawing.Point(503, 5);
            this.lblTimeFromStart.Name = "lblTimeFromStart";
            this.lblTimeFromStart.Size = new System.Drawing.Size(13, 13);
            this.lblTimeFromStart.TabIndex = 20;
            this.lblTimeFromStart.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(540, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "sec";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "Redo every";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // txtRedoEvery
            // 
            this.txtRedoEvery.Location = new System.Drawing.Point(165, 51);
            this.txtRedoEvery.Name = "txtRedoEvery";
            this.txtRedoEvery.Size = new System.Drawing.Size(100, 20);
            this.txtRedoEvery.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(271, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "sec";
            // 
            // btnStopPeriodic
            // 
            this.btnStopPeriodic.Location = new System.Drawing.Point(317, 56);
            this.btnStopPeriodic.Name = "btnStopPeriodic";
            this.btnStopPeriodic.Size = new System.Drawing.Size(75, 23);
            this.btnStopPeriodic.TabIndex = 25;
            this.btnStopPeriodic.Text = "Stop";
            this.btnStopPeriodic.UseVisualStyleBackColor = true;
            this.btnStopPeriodic.Click += new System.EventHandler(this.BtnStopPeriodic_Click);
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(623, 156);
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.ReadOnly = true;
            this.txtSummary.Size = new System.Drawing.Size(404, 20);
            this.txtSummary.TabIndex = 26;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 543);
            this.Controls.Add(this.txtSummary);
            this.Controls.Add(this.btnStopPeriodic);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtRedoEvery);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTimeFromStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClear);
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
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTimeFromStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtRedoEvery;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnStopPeriodic;
        private System.Windows.Forms.TextBox txtSummary;
    }
}