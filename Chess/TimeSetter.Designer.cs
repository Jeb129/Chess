namespace Chess
{
    partial class TimeSetter
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
            this.TimerSet = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AddSet = new System.Windows.Forms.NumericUpDown();
            this.SetButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TimerSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddSet)).BeginInit();
            this.SuspendLayout();
            // 
            // TimerSet
            // 
            this.TimerSet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TimerSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TimerSet.Location = new System.Drawing.Point(220, 18);
            this.TimerSet.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.TimerSet.Name = "TimerSet";
            this.TimerSet.Size = new System.Drawing.Size(100, 30);
            this.TimerSet.TabIndex = 1;
            this.TimerSet.ValueChanged += new System.EventHandler(this.TimerSet_ValueChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(20, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 34);
            this.label4.TabIndex = 11;
            this.label4.Text = "Контроль (мин)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(20, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 34);
            this.label1.TabIndex = 13;
            this.label1.Text = "Прибавка (сек)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AddSet
            // 
            this.AddSet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AddSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddSet.Location = new System.Drawing.Point(220, 80);
            this.AddSet.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.AddSet.Name = "AddSet";
            this.AddSet.Size = new System.Drawing.Size(100, 30);
            this.AddSet.TabIndex = 2;
            this.AddSet.ValueChanged += new System.EventHandler(this.TimerSet_ValueChanged);
            // 
            // SetButton
            // 
            this.SetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SetButton.Location = new System.Drawing.Point(20, 140);
            this.SetButton.Name = "SetButton";
            this.SetButton.Size = new System.Drawing.Size(300, 50);
            this.SetButton.TabIndex = 0;
            this.SetButton.Text = "Играть без часов";
            this.SetButton.UseVisualStyleBackColor = true;
            this.SetButton.Click += new System.EventHandler(this.SetButton_Click);
            // 
            // TimeSetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 210);
            this.Controls.Add(this.SetButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AddSet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TimerSet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TimeSetter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TimeSetter";
            ((System.ComponentModel.ISupportInitialize)(this.TimerSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NumericUpDown TimerSet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown AddSet;
        private System.Windows.Forms.Button SetButton;
    }
}