namespace demo
{
    partial class Auth
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.login_tb = new System.Windows.Forms.TextBox();
            this.pass_tb = new System.Windows.Forms.TextBox();
            this.reg_btn = new System.Windows.Forms.Button();
            this.login_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // login_tb
            // 
            this.login_tb.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.login_tb.Location = new System.Drawing.Point(88, 54);
            this.login_tb.Name = "login_tb";
            this.login_tb.Size = new System.Drawing.Size(150, 29);
            this.login_tb.TabIndex = 0;
            // 
            // pass_tb
            // 
            this.pass_tb.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.pass_tb.Location = new System.Drawing.Point(88, 112);
            this.pass_tb.Name = "pass_tb";
            this.pass_tb.PasswordChar = '*';
            this.pass_tb.Size = new System.Drawing.Size(150, 29);
            this.pass_tb.TabIndex = 1;
            // 
            // reg_btn
            // 
            this.reg_btn.Location = new System.Drawing.Point(26, 193);
            this.reg_btn.Name = "reg_btn";
            this.reg_btn.Size = new System.Drawing.Size(92, 23);
            this.reg_btn.TabIndex = 2;
            this.reg_btn.Text = "Регистрация";
            this.reg_btn.UseVisualStyleBackColor = true;
            this.reg_btn.Click += new System.EventHandler(this.reg_btn_Click);
            // 
            // login_btn
            // 
            this.login_btn.Location = new System.Drawing.Point(163, 193);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(91, 23);
            this.login_btn.TabIndex = 3;
            this.login_btn.Text = "Авторизация";
            this.login_btn.UseVisualStyleBackColor = true;
            this.login_btn.Click += new System.EventHandler(this.login_btn_Click);
            // 
            // Auth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 291);
            this.Controls.Add(this.login_btn);
            this.Controls.Add(this.reg_btn);
            this.Controls.Add(this.pass_tb);
            this.Controls.Add(this.login_tb);
            this.Name = "Auth";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox login_tb;
        private System.Windows.Forms.TextBox pass_tb;
        private System.Windows.Forms.Button reg_btn;
        private System.Windows.Forms.Button login_btn;
    }
}

