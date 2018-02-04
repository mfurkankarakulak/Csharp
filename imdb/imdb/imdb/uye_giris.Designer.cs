namespace imdb
{
    partial class uye_giris
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.isimtext = new System.Windows.Forms.TextBox();
            this.soyadtext = new System.Windows.Forms.TextBox();
            this.nicktext = new System.Windows.Forms.TextBox();
            this.mailtext = new System.Windows.Forms.TextBox();
            this.şifretext = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.Orange;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "İSİM : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.Orange;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "SOYAD : ";
            // 
            // isimtext
            // 
            this.isimtext.Location = new System.Drawing.Point(86, 11);
            this.isimtext.Name = "isimtext";
            this.isimtext.Size = new System.Drawing.Size(180, 20);
            this.isimtext.TabIndex = 1;
            // 
            // soyadtext
            // 
            this.soyadtext.Location = new System.Drawing.Point(86, 40);
            this.soyadtext.Name = "soyadtext";
            this.soyadtext.Size = new System.Drawing.Size(180, 20);
            this.soyadtext.TabIndex = 2;
            // 
            // nicktext
            // 
            this.nicktext.Location = new System.Drawing.Point(86, 66);
            this.nicktext.Name = "nicktext";
            this.nicktext.Size = new System.Drawing.Size(180, 20);
            this.nicktext.TabIndex = 3;
            // 
            // mailtext
            // 
            this.mailtext.Location = new System.Drawing.Point(86, 91);
            this.mailtext.Name = "mailtext";
            this.mailtext.Size = new System.Drawing.Size(180, 20);
            this.mailtext.TabIndex = 4;
            // 
            // şifretext
            // 
            this.şifretext.Location = new System.Drawing.Point(86, 118);
            this.şifretext.Name = "şifretext";
            this.şifretext.PasswordChar = '*';
            this.şifretext.Size = new System.Drawing.Size(180, 20);
            this.şifretext.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.Orange;
            this.label3.Location = new System.Drawing.Point(12, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "NİCK :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.Color.Orange;
            this.label4.Location = new System.Drawing.Point(12, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "MAİL :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.Color.Orange;
            this.label5.Location = new System.Drawing.Point(12, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "ŞİFRE :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(191, 144);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "KAYIT OL";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // uye_giris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(284, 198);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.şifretext);
            this.Controls.Add(this.mailtext);
            this.Controls.Add(this.nicktext);
            this.Controls.Add(this.soyadtext);
            this.Controls.Add(this.isimtext);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.Name = "uye_giris";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KAYIT OL";
            this.Load += new System.EventHandler(this.uye_giris_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox isimtext;
        private System.Windows.Forms.TextBox soyadtext;
        private System.Windows.Forms.TextBox nicktext;
        private System.Windows.Forms.TextBox mailtext;
        private System.Windows.Forms.TextBox şifretext;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
    }
}