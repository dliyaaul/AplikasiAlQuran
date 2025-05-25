namespace AplikasiAlQur_an.FiturAlQuran
{
    partial class FormJuz
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
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.richJuz = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(165, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(229, 31);
            this.label3.TabIndex = 15;
            this.label3.Text = "بِسْمِ اللّٰهِ الرَّحْمٰنِ الرَّحِيْمِ";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(22, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 28);
            this.label1.TabIndex = 13;
            this.label1.Text = "1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPrev
            // 
            this.btnPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(136)))), ((int)(((byte)(86)))));
            this.btnPrev.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnPrev.Location = new System.Drawing.Point(-1, -1);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(68, 49);
            this.btnPrev.TabIndex = 16;
            this.btnPrev.Text = "←";
            this.btnPrev.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPrev.UseVisualStyleBackColor = false;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(136)))), ((int)(((byte)(86)))));
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnNext.Location = new System.Drawing.Point(471, -1);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(68, 49);
            this.btnNext.TabIndex = 17;
            this.btnNext.Text = "→";
            this.btnNext.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // richJuz
            // 
            this.richJuz.BackColor = System.Drawing.SystemColors.WindowText;
            this.richJuz.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richJuz.ForeColor = System.Drawing.SystemColors.Window;
            this.richJuz.Location = new System.Drawing.Point(12, 159);
            this.richJuz.Name = "richJuz";
            this.richJuz.Size = new System.Drawing.Size(514, 598);
            this.richJuz.TabIndex = 19;
            this.richJuz.Text = "";
            this.richJuz.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // FormJuz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(538, 769);
            this.Controls.Add(this.richJuz);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.Name = "FormJuz";
            this.Text = "FormJuz";
            this.Load += new System.EventHandler(this.FormJuz_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.RichTextBox richJuz;
    }
}