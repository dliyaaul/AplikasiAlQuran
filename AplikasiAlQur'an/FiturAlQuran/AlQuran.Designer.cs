namespace AplikasiAlQur_an
{
    partial class AlQuran
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
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listSurah = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearchSurah = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listJuz = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.listBookmark = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(136)))), ((int)(((byte)(86)))));
            this.button1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(-1, -1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 49);
            this.button1.TabIndex = 7;
            this.button1.Text = "←";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(-1, 102);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(539, 667);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Desktop;
            this.tabPage1.Controls.Add(this.listSurah);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnSearch);
            this.tabPage1.Controls.Add(this.txtSearchSurah);
            this.tabPage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(136)))), ((int)(((byte)(86)))));
            this.tabPage1.Location = new System.Drawing.Point(4, 40);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(531, 623);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Surah";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // listSurah
            // 
            this.listSurah.BackColor = System.Drawing.SystemColors.WindowText;
            this.listSurah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listSurah.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listSurah.ForeColor = System.Drawing.SystemColors.Window;
            this.listSurah.FormattingEnabled = true;
            this.listSurah.ItemHeight = 28;
            this.listSurah.Location = new System.Drawing.Point(26, 112);
            this.listSurah.Name = "listSurah";
            this.listSurah.Size = new System.Drawing.Size(478, 478);
            this.listSurah.TabIndex = 3;
            this.listSurah.SelectedIndexChanged += new System.EventHandler(this.listSurah_SelectedIndexChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pencarian";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(446, 42);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(58, 38);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "🔍";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtSearchSurah
            // 
            this.txtSearchSurah.BackColor = System.Drawing.SystemColors.ControlText;
            this.txtSearchSurah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchSurah.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchSurah.ForeColor = System.Drawing.SystemColors.Window;
            this.txtSearchSurah.Location = new System.Drawing.Point(26, 42);
            this.txtSearchSurah.Name = "txtSearchSurah";
            this.txtSearchSurah.Size = new System.Drawing.Size(414, 38);
            this.txtSearchSurah.TabIndex = 0;
            this.txtSearchSurah.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Desktop;
            this.tabPage2.Controls.Add(this.listJuz);
            this.tabPage2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(136)))), ((int)(((byte)(86)))));
            this.tabPage2.Location = new System.Drawing.Point(4, 40);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(531, 623);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Juz";
            // 
            // listJuz
            // 
            this.listJuz.BackColor = System.Drawing.SystemColors.WindowText;
            this.listJuz.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listJuz.ForeColor = System.Drawing.SystemColors.Window;
            this.listJuz.FormattingEnabled = true;
            this.listJuz.ItemHeight = 31;
            this.listJuz.Location = new System.Drawing.Point(0, 0);
            this.listJuz.Name = "listJuz";
            this.listJuz.Size = new System.Drawing.Size(535, 622);
            this.listJuz.TabIndex = 0;
            this.listJuz.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Desktop;
            this.tabPage3.Controls.Add(this.listBookmark);
            this.tabPage3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(136)))), ((int)(((byte)(86)))));
            this.tabPage3.Location = new System.Drawing.Point(4, 40);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(531, 623);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Penanda";
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // listBookmark
            // 
            this.listBookmark.BackColor = System.Drawing.SystemColors.WindowText;
            this.listBookmark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBookmark.ForeColor = System.Drawing.SystemColors.Window;
            this.listBookmark.FormattingEnabled = true;
            this.listBookmark.ItemHeight = 31;
            this.listBookmark.Location = new System.Drawing.Point(-1, 1);
            this.listBookmark.Name = "listBookmark";
            this.listBookmark.Size = new System.Drawing.Size(535, 622);
            this.listBookmark.TabIndex = 0;
            this.listBookmark.SelectedIndexChanged += new System.EventHandler(this.listBookmark_SelectedIndexChanged);
            // 
            // AlQuran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(538, 769);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Name = "AlQuran";
            this.Text = "AlQuran";
            this.Load += new System.EventHandler(this.AlQuran_Load_1);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtSearchSurah;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListBox listSurah;
        private System.Windows.Forms.ListBox listJuz;
        private System.Windows.Forms.ListBox listBookmark;
    }
}