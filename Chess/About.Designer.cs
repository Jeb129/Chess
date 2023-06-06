namespace Chess
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.NameLB = new System.Windows.Forms.Label();
            this.CompanyLB = new System.Windows.Forms.Label();
            this.CopyrightLB = new System.Windows.Forms.Label();
            this.DescriptionLB = new System.Windows.Forms.Label();
            this.VersionLB = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Chess.Properties.Resources.BlackKnight;
            this.pictureBox1.Location = new System.Drawing.Point(-14, 93);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(218, 196);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(725, 398);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(63, 40);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // NameLB
            // 
            this.NameLB.Dock = System.Windows.Forms.DockStyle.Top;
            this.NameLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameLB.ForeColor = System.Drawing.Color.White;
            this.NameLB.Location = new System.Drawing.Point(0, 0);
            this.NameLB.Name = "NameLB";
            this.NameLB.Size = new System.Drawing.Size(800, 72);
            this.NameLB.TabIndex = 2;
            this.NameLB.Text = "Название";
            this.NameLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CompanyLB
            // 
            this.CompanyLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CompanyLB.ForeColor = System.Drawing.Color.White;
            this.CompanyLB.Location = new System.Drawing.Point(227, 94);
            this.CompanyLB.Name = "CompanyLB";
            this.CompanyLB.Size = new System.Drawing.Size(557, 29);
            this.CompanyLB.TabIndex = 3;
            this.CompanyLB.Text = "Организация";
            this.CompanyLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CopyrightLB
            // 
            this.CopyrightLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CopyrightLB.ForeColor = System.Drawing.Color.White;
            this.CopyrightLB.Location = new System.Drawing.Point(227, 260);
            this.CopyrightLB.Name = "CopyrightLB";
            this.CopyrightLB.Size = new System.Drawing.Size(557, 29);
            this.CopyrightLB.TabIndex = 4;
            this.CopyrightLB.Text = "Права";
            this.CopyrightLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DescriptionLB
            // 
            this.DescriptionLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DescriptionLB.ForeColor = System.Drawing.Color.White;
            this.DescriptionLB.Location = new System.Drawing.Point(227, 176);
            this.DescriptionLB.Name = "DescriptionLB";
            this.DescriptionLB.Size = new System.Drawing.Size(557, 29);
            this.DescriptionLB.TabIndex = 5;
            this.DescriptionLB.Text = "Описание";
            this.DescriptionLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VersionLB
            // 
            this.VersionLB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.VersionLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.VersionLB.ForeColor = System.Drawing.Color.White;
            this.VersionLB.Location = new System.Drawing.Point(0, 421);
            this.VersionLB.Name = "VersionLB";
            this.VersionLB.Size = new System.Drawing.Size(800, 29);
            this.VersionLB.TabIndex = 6;
            this.VersionLB.Text = "Версия";
            this.VersionLB.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DescriptionLB);
            this.Controls.Add(this.CopyrightLB);
            this.Controls.Add(this.CompanyLB);
            this.Controls.Add(this.NameLB);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.VersionLB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "О программе";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label NameLB;
        private System.Windows.Forms.Label CompanyLB;
        private System.Windows.Forms.Label CopyrightLB;
        private System.Windows.Forms.Label DescriptionLB;
        private System.Windows.Forms.Label VersionLB;
    }
}