namespace Chess
{
    partial class ChessSetter
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
            this.ChessSet = new System.Windows.Forms.Panel();
            this.t0 = new System.Windows.Forms.Button();
            this.t3 = new System.Windows.Forms.Button();
            this.t2 = new System.Windows.Forms.Button();
            this.t1 = new System.Windows.Forms.Button();
            this.ChessSet.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChessSet
            // 
            this.ChessSet.BackColor = System.Drawing.Color.Black;
            this.ChessSet.Controls.Add(this.t0);
            this.ChessSet.Controls.Add(this.t3);
            this.ChessSet.Controls.Add(this.t2);
            this.ChessSet.Controls.Add(this.t1);
            this.ChessSet.Location = new System.Drawing.Point(42, 0);
            this.ChessSet.Name = "ChessSet";
            this.ChessSet.Size = new System.Drawing.Size(90, 300);
            this.ChessSet.TabIndex = 68;
            // 
            // t0
            // 
            this.t0.BackColor = System.Drawing.Color.Peru;
            this.t0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.t0.FlatAppearance.BorderColor = System.Drawing.Color.Olive;
            this.t0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.t0.Location = new System.Drawing.Point(10, 10);
            this.t0.Name = "t0";
            this.t0.Size = new System.Drawing.Size(70, 70);
            this.t0.TabIndex = 1;
            this.t0.Tag = "B";
            this.t0.UseVisualStyleBackColor = false;
            this.t0.Click += new System.EventHandler(this.Rook_Click);
            // 
            // t3
            // 
            this.t3.BackColor = System.Drawing.Color.PeachPuff;
            this.t3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.t3.FlatAppearance.BorderColor = System.Drawing.Color.Olive;
            this.t3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.t3.Location = new System.Drawing.Point(10, 220);
            this.t3.Name = "t3";
            this.t3.Size = new System.Drawing.Size(70, 70);
            this.t3.TabIndex = 0;
            this.t3.Tag = "W";
            this.t3.UseVisualStyleBackColor = false;
            this.t3.Click += new System.EventHandler(this.Rook_Click);
            // 
            // t2
            // 
            this.t2.BackColor = System.Drawing.Color.Peru;
            this.t2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.t2.FlatAppearance.BorderColor = System.Drawing.Color.Olive;
            this.t2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.t2.Location = new System.Drawing.Point(10, 150);
            this.t2.Name = "t2";
            this.t2.Size = new System.Drawing.Size(70, 70);
            this.t2.TabIndex = 2;
            this.t2.Tag = "B";
            this.t2.UseVisualStyleBackColor = false;
            this.t2.Click += new System.EventHandler(this.Rook_Click);
            // 
            // t1
            // 
            this.t1.BackColor = System.Drawing.Color.PeachPuff;
            this.t1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.t1.FlatAppearance.BorderColor = System.Drawing.Color.Olive;
            this.t1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.t1.Location = new System.Drawing.Point(10, 80);
            this.t1.Name = "t1";
            this.t1.Size = new System.Drawing.Size(70, 70);
            this.t1.TabIndex = 3;
            this.t1.Tag = "W";
            this.t1.UseVisualStyleBackColor = false;
            this.t1.Click += new System.EventHandler(this.Rook_Click);
            // 
            // ChessSetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(166, 300);
            this.Controls.Add(this.ChessSet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ChessSetter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChessSetter";
            this.ChessSet.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ChessSet;
        private System.Windows.Forms.Button t0;
        private System.Windows.Forms.Button t3;
        private System.Windows.Forms.Button t2;
        private System.Windows.Forms.Button t1;
    }
}