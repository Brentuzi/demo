using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demo
{
    public partial class Reg : Form
    {
        public Reg()
        {
            InitializeComponent();
        }

        private void register_btn_Click(object sender, EventArgs e)
        {
            string fio = fio_tb.Text;
            string phone = tel_tb.Text;
            string login = login_tb.Text;
            string password = password_tb.Text;

            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fio)) {
                MessageBox.Show("Пожулйста заполните все поля!");
                return;
            }
            if (RegisterUser(fio, phone, login, password))
            {
                MessageBox.Show("Успешная регистрация"+ fio);
                Auth auth = new Auth();
                auth.Show();
                this.Hide();
            }else
            {
                MessageBox.Show("Ошибка при регистрации,попробуйте еще раз");
            }

        }
        private bool RegisterUser(string fio, string phone, string username, string password)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\db.mdf;Integrated Security=True;Connect Timeout=30";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (fio, phone, login, password, type) VALUES (@fio, @phone, @login, @password, N'Заказчик')";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fio", fio);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@login", username);
                    command.Parameters.AddWithValue("@password", password);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
