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
    public partial class Auth : Form
    {
        public string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\Documents\\db.mdf;Integrated Security=True;Connect Timeout=30";
        public Auth()
        {
            InitializeComponent();
        }
        public class UserInfo
        {
            public int UserID { get; set; }
            public string Role { get; set; }
        }
        private void login_btn_Click(object sender, EventArgs e)
        {
            string login = login_tb.Text;
            string password = pass_tb.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) ){
                            
                MessageBox.Show("Введите логин и пароль");
                return;
            }

            UserInfo userInfo = AuthenticateUser(login, password);
            if (userInfo != null)
            {
                MessageBox.Show("Успешная авторизация с ролью " + userInfo.Role);
                Main mainFrom = new Main(userInfo.UserID,userInfo.Role);
                mainFrom.Show();
                this.Hide();
            }else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }
        private UserInfo AuthenticateUser(string username, string password)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT userID, type FROM Users WHERE login = @username AND password = @password";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserInfo
                            {
                                UserID = reader.GetInt32(0),
                                Role = reader.GetString(1)
                            };
                        }
                    }
                }
            }
            return null;
        }

        private void reg_btn_Click(object sender, EventArgs e)
        {
            Reg reg = new Reg();
            reg.Show();
            this.Hide();
        }
    }
}
