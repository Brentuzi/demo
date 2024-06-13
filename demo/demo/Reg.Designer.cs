namespace demo
{
    partial class Reg
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.fio_tb = new System.Windows.Forms.TextBox();
            this.tel_tb = new System.Windows.Forms.TextBox();
            this.login_tb = new System.Windows.Forms.TextBox();
            this.password_tb = new System.Windows.Forms.TextBox();
            this.register_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label1.Location = new System.Drawing.Point(31, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "ФИО";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label2.Location = new System.Drawing.Point(31, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Телефон";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label3.Location = new System.Drawing.Point(31, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Логин";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label4.Location = new System.Drawing.Point(31, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "Пароль";
            // 
            // fio_tb
            // 
            this.fio_tb.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.fio_tb.Location = new System.Drawing.Point(143, 44);
            this.fio_tb.Name = "fio_tb";
            this.fio_tb.Size = new System.Drawing.Size(147, 29);
            this.fio_tb.TabIndex = 4;
            // 
            // tel_tb
            // 
            this.tel_tb.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.tel_tb.Location = new System.Drawing.Point(143, 89);
            this.tel_tb.Name = "tel_tb";
            this.tel_tb.Size = new System.Drawing.Size(147, 29);
            this.tel_tb.TabIndex = 5;
            // 
            // login_tb
            // 
            this.login_tb.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.login_tb.Location = new System.Drawing.Point(143, 142);
            this.login_tb.Name = "login_tb";
            this.login_tb.Size = new System.Drawing.Size(147, 29);
            this.login_tb.TabIndex = 6;
            // 
            // password_tb
            // 
            this.password_tb.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.password_tb.Location = new System.Drawing.Point(143, 191);
            this.password_tb.Name = "password_tb";
            this.password_tb.Size = new System.Drawing.Size(147, 29);
            this.password_tb.TabIndex = 7;
            // 
            // register_btn
            // 
            this.register_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.register_btn.Location = new System.Drawing.Point(83, 242);
            this.register_btn.Name = "register_btn";
            this.register_btn.Size = new System.Drawing.Size(207, 37);
            this.register_btn.TabIndex = 8;
            this.register_btn.Text = "Регистрация";
            this.register_btn.UseVisualStyleBackColor = true;
            this.register_btn.Click += new System.EventHandler(this.register_btn_Click);
            // 
            // Reg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 315);
            this.Controls.Add(this.register_btn);
            this.Controls.Add(this.password_tb);
            this.Controls.Add(this.login_tb);
            this.Controls.Add(this.tel_tb);
            this.Controls.Add(this.fio_tb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Reg";
            this.Text = "Reg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox fio_tb;
        private System.Windows.Forms.TextBox tel_tb;
        private System.Windows.Forms.TextBox password_tb;
        private System.Windows.Forms.Button register_btn;
        private System.Windows.Forms.TextBox login_tb;
    }
}