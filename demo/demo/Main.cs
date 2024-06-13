using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace demo
{
    public partial class Main : Form
    {
        private int _userID;
        private string _userRole;
        private DataGridView dgvRequests;
        private DataGridView dgvUsers;
        private Button btnSaveChanges;
        private Button btnSaveUserChanges;
        private Button btnSubmitNewRequest;
        private TextBox txtOrgTechType;
        private TextBox txtOrgTechModel;
        private TextBox txtProblemDescription;
        private bool changesPending = false;

        public Main(int userID, string userRole)
        {
            _userID = userID;
            _userRole = userRole;
            InitializeComponent();
            InitializeCustomComponents();
            if (_userRole == "Заказчик")
            {
                InitializeCustomerInterface();
            }
            else if (_userRole == "Мастер")
            {
                InitializeMasterInterface();
            }
            else // Администратор
            {
                InitializeAdminInterface();
            }
        }

        public string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\db.mdf;Integrated Security=True;Connect Timeout=30";

        private void InitializeCustomComponents()
        {
            Label lblWelcome = new Label
            {
                Location = new System.Drawing.Point(30, 30),
                Size = new System.Drawing.Size(300, 20),
                Text = $"Добро пожаловать, {_userRole}!"
            };
            this.Controls.Add(lblWelcome);
        }

        private void InitializeCustomerInterface()
        {
            dgvRequests = new DataGridView
            {
                Location = new System.Drawing.Point(30, 60),
                Size = new System.Drawing.Size(1000, 300),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = false // Разрешить редактирование ячеек
            };

            // Поля для добавления новой заявки
            Label lblOrgTechType = new Label { Text = "Тип техники", Location = new System.Drawing.Point(30, 410) };
            txtOrgTechType = new TextBox { Location = new System.Drawing.Point(150, 410), Width = 200 };

            Label lblOrgTechModel = new Label { Text = "Модель техники", Location = new System.Drawing.Point(30, 440) };
            txtOrgTechModel = new TextBox { Location = new System.Drawing.Point(150, 440), Width = 200 };

            Label lblProblemDescription = new Label { Text = "Описание проблемы", Location = new System.Drawing.Point(30, 470) };
            txtProblemDescription = new TextBox { Location = new System.Drawing.Point(150, 470), Width = 200 };

            btnSubmitNewRequest = new Button { Location = new System.Drawing.Point(150, 500), Text = "Сохранить новую заявку" };
            btnSubmitNewRequest.Click += BtnSubmitNewRequest_Click;

            // Добавляем элементы на форму
            this.Controls.Add(dgvRequests);
            this.Controls.Add(lblOrgTechType);
            this.Controls.Add(txtOrgTechType);
            this.Controls.Add(lblOrgTechModel);
            this.Controls.Add(txtOrgTechModel);
            this.Controls.Add(lblProblemDescription);
            this.Controls.Add(txtProblemDescription);
            this.Controls.Add(btnSubmitNewRequest);

            dgvRequests.CellBeginEdit += DgvRequests_CellBeginEdit;
            dgvRequests.CellValueChanged += DgvRequests_CellValueChanged;
            dgvRequests.UserDeletingRow += DgvRequests_UserDeletingRow;

            LoadRequestsData1();
        }

        private void InitializeMasterInterface()
        {
            dgvRequests = new DataGridView
            {
                Location = new System.Drawing.Point(30, 60),
                Size = new System.Drawing.Size(1000, 600),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = false // Разрешить редактирование ячеек
            };

            btnSaveChanges = new Button { Location = new System.Drawing.Point(150, 670), Text = "Сохранить изменения" };
            btnSaveChanges.Click += BtnSaveChanges_Click;

            // Добавляем элементы на форму
            this.Controls.Add(dgvRequests);
            this.Controls.Add(btnSaveChanges);

            dgvRequests.CellValueChanged += DgvRequests_CellValueChanged;
            dgvRequests.UserDeletingRow += DgvRequests_UserDeletingRow;

            LoadRequestsData2();
        }

        private void InitializeAdminInterface()
        {
            dgvRequests = new DataGridView
            {
                Location = new System.Drawing.Point(30, 60),
                Size = new System.Drawing.Size(1000, 300),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = false // Разрешить редактирование ячеек
            };

            dgvUsers = new DataGridView
            {
                Location = new System.Drawing.Point(30, 380),
                Size = new System.Drawing.Size(1000, 300),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = true,
                AllowUserToDeleteRows = true,
                ReadOnly = false // Разрешить редактирование ячеек
            };

            btnSaveChanges = new Button { Location = new System.Drawing.Point(150, 700), Text = "Сохранить изменения" };
            btnSaveChanges.Click += BtnSaveChanges_Click;

            btnSaveUserChanges = new Button { Location = new System.Drawing.Point(150, 740), Text = "Сохранить изменения пользователей" };
            btnSaveUserChanges.Click += BtnSaveUserChanges_Click;

            // Добавляем элементы на форму
            this.Controls.Add(dgvRequests);
            this.Controls.Add(dgvUsers);
            this.Controls.Add(btnSaveChanges);
            this.Controls.Add(btnSaveUserChanges);

            dgvRequests.CellValueChanged += DgvRequests_CellValueChanged;
            dgvRequests.UserDeletingRow += DgvRequests_UserDeletingRow;

            LoadRequestsData3();
            LoadUsersData();
        }

        private void LoadRequestsData1()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT 
                        r.requestID,
                        r.startDate,
                        r.orgTechType,
                        r.orgTechModel,
                        r.problemDescryption,
                        r.requestStatus,
                        r.completionDate,
                        r.repairParts,
                        r.masterID
                    FROM 
                        Requests r
                    WHERE
                        r.clientID = @userID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userID", _userID);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count == 0)
                        {
                            MessageBox.Show("У вас нет заявок.");
                        }
                        else
                        {
                            dgvRequests.DataSource = dataTable;

                            // Задаем заголовки столбцов на русском языке
                            dgvRequests.Columns["requestID"].HeaderText = "Номер заявки";
                            dgvRequests.Columns["startDate"].HeaderText = "Дата начала";
                            dgvRequests.Columns["orgTechType"].HeaderText = "Тип техники";
                            dgvRequests.Columns["orgTechModel"].HeaderText = "Модель техники";
                            dgvRequests.Columns["problemDescryption"].HeaderText = "Описание проблемы";
                            dgvRequests.Columns["requestStatus"].HeaderText = "Статус заявки";
                            dgvRequests.Columns["completionDate"].HeaderText = "Дата завершения";
                            dgvRequests.Columns["repairParts"].HeaderText = "Запчасти";
                            dgvRequests.Columns["masterID"].HeaderText = "ID мастера";
                        }
                    }
                }
            }
        }

        private void LoadRequestsData2()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT 
                        r.requestID,
                        r.startDate,
                        r.orgTechType,
                        r.orgTechModel,
                        r.problemDescryption,
                        r.requestStatus,
                        r.completionDate,
                        r.repairParts,
                        r.masterID,
                        c.message AS comment
                    FROM 
                        Requests r
                    LEFT JOIN 
                        Comments c ON r.requestID = c.requestID
                    WHERE
                        r.masterID = @userID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userID", _userID);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dgvRequests.DataSource = dataTable;

                        // Задаем заголовки столбцов на русском языке
                        dgvRequests.Columns["requestID"].HeaderText = "Номер заявки";
                        dgvRequests.Columns["startDate"].HeaderText = "Дата начала";
                        dgvRequests.Columns["orgTechType"].HeaderText = "Тип техники";
                        dgvRequests.Columns["orgTechModel"].HeaderText = "Модель техники";
                        dgvRequests.Columns["problemDescryption"].HeaderText = "Описание проблемы";
                        dgvRequests.Columns["requestStatus"].HeaderText = "Статус заявки";
                        dgvRequests.Columns["completionDate"].HeaderText = "Дата завершения";
                        dgvRequests.Columns["repairParts"].HeaderText = "Запчасти";
                        dgvRequests.Columns["masterID"].HeaderText = "ID мастера";
                        dgvRequests.Columns["comment"].HeaderText = "Комментарий";
                    }
                }
            }
        }

        private void LoadRequestsData3()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT 
                        r.requestID,
                        r.startDate,
                        r.orgTechType,
                        r.orgTechModel,
                        r.problemDescryption,
                        r.requestStatus,
                        r.completionDate,
                        r.repairParts,
                        r.masterID,
                        c.message AS comment
                    FROM 
                        Requests r
                    LEFT JOIN 
                        Comments c ON r.requestID = c.requestID";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dgvRequests.DataSource = dataTable;

                        // Задаем заголовки столбцов на русском языке
                        dgvRequests.Columns["requestID"].HeaderText = "Номер заявки";
                        dgvRequests.Columns["startDate"].HeaderText = "Дата начала";
                        dgvRequests.Columns["orgTechType"].HeaderText = "Тип техники";
                        dgvRequests.Columns["orgTechModel"].HeaderText = "Модель техники";
                        dgvRequests.Columns["problemDescryption"].HeaderText = "Описание проблемы";
                        dgvRequests.Columns["requestStatus"].HeaderText = "Статус заявки";
                        dgvRequests.Columns["completionDate"].HeaderText = "Дата завершения";
                        dgvRequests.Columns["repairParts"].HeaderText = "Запчасти";
                        dgvRequests.Columns["masterID"].HeaderText = "ID мастера";
                        dgvRequests.Columns["comment"].HeaderText = "Комментарий";
                    }
                }
            }
        }

        private void LoadUsersData()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT userID, fio, phone, login, password, type FROM Users ";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dgvUsers.DataSource = dataTable;

                        // Задаем заголовки столбцов на русском языке
                        dgvUsers.Columns["userID"].HeaderText = "ID пользователя";
                        dgvUsers.Columns["fio"].HeaderText = "ФИО";
                        dgvUsers.Columns["phone"].HeaderText = "Телефон";
                        dgvUsers.Columns["login"].HeaderText = "Логин";
                        dgvUsers.Columns["login"].Visible = false;
                        dgvUsers.Columns["password"].HeaderText = "Пароль";
                        dgvUsers.Columns["password"].Visible = false; // Скрыть пароль
                        dgvUsers.Columns["type"].HeaderText = "Тип пользователя";
                    }
                }
            }
        }

        private void BtnSubmitNewRequest_Click(object sender, EventArgs e)
        {
            string orgTechType = txtOrgTechType.Text;
            string orgTechModel = txtOrgTechModel.Text;
            string problemDescription = txtProblemDescription.Text;

            if (string.IsNullOrEmpty(orgTechType) || string.IsNullOrEmpty(orgTechModel) || string.IsNullOrEmpty(problemDescription))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    INSERT INTO Requests (startDate, orgTechType, orgTechModel, problemDescryption, requestStatus, clientID)
                    VALUES (GETDATE(), @orgTechType, @orgTechModel, @problemDescryption, N'Новая заявка', @clientID)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@orgTechType", orgTechType);
                    command.Parameters.AddWithValue("@orgTechModel", orgTechModel);
                    command.Parameters.AddWithValue("@problemDescryption", problemDescription);
                    command.Parameters.AddWithValue("@clientID", _userID);
                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Новая заявка добавлена");
            LoadRequestsData1(); // Перезагрузка данных после добавления новой заявки
        }

        private void BtnSaveChanges_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void SaveChanges()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (DataGridViewRow row in dgvRequests.Rows)
                {
                    if (row.IsNewRow) continue;

                    int requestId = Convert.ToInt32(row.Cells["requestID"].Value);
                    string orgTechType = row.Cells["orgTechType"].Value?.ToString();
                    string orgTechModel = row.Cells["orgTechModel"].Value?.ToString();
                    string problemDescryption = row.Cells["problemDescryption"].Value?.ToString();
                    string requestStatus = row.Cells["requestStatus"].Value?.ToString();
                    string repairParts = row.Cells["repairParts"].Value?.ToString();
                    DateTime? completionDate = row.Cells["completionDate"].Value as DateTime?;
                    string comment = row.Cells["comment"].Value?.ToString();

                    string updateRequestQuery = @"
                        UPDATE Requests 
                        SET 
                            orgTechType = @orgTechType,
                            orgTechModel = @orgTechModel,
                            problemDescryption = @problemDescryption,
                            requestStatus = @requestStatus,
                            repairParts = @repairParts,
                            completionDate = @completionDate
                        WHERE 
                            requestID = @requestID";

                    using (var command = new SqlCommand(updateRequestQuery, connection))
                    {
                        command.Parameters.AddWithValue("@orgTechType", orgTechType ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@orgTechModel", orgTechModel ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@problemDescryption", problemDescryption ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@requestStatus", requestStatus ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@repairParts", repairParts ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@completionDate", completionDate.HasValue ? (object)completionDate.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@requestID", requestId);
                        command.ExecuteNonQuery();
                    }

                    string updateCommentQuery = @"
                        IF EXISTS (SELECT 1 FROM Comments WHERE requestID = @requestID)
                            UPDATE Comments 
                            SET message = @message
                            WHERE requestID = @requestID
                        ELSE
                            INSERT INTO Comments (message, masterID, requestID)
                            VALUES (@message, @masterID, @requestID)";
                    using (var command = new SqlCommand(updateCommentQuery, connection))
                    {
                        command.Parameters.AddWithValue("@message", comment ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@masterID", _userID);
                        command.Parameters.AddWithValue("@requestID", requestId);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Изменения сохранены");
                if (_userRole == "Мастер")
                {
                    LoadRequestsData2(); // Перезагрузка данных после сохранения изменений для мастера
                }
                else if (_userRole == "Менеджер"|| _userRole == "Оператор")
                {
                    LoadRequestsData3(); // Перезагрузка данных после сохранения изменений для администратора
                }
                else
                {
                    LoadRequestsData1(); // Перезагрузка данных после сохранения изменений для заказчика
                }
            }
        }

        private void SaveUserChanges()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (DataGridViewRow row in dgvUsers.Rows)
                {
                    if (row.IsNewRow) continue;

                    int userID = Convert.ToInt32(row.Cells["userID"].Value);
                    string fio = row.Cells["fio"].Value?.ToString();
                    string phone = row.Cells["phone"].Value?.ToString();
                    string login = row.Cells["login"].Value?.ToString();
                    string password = row.Cells["password"].Value?.ToString();
                    string type = row.Cells["type"].Value?.ToString();

                    string updateUserQuery = @"
                        UPDATE Users 
                        SET 
                            fio = @fio,
                            phone = @phone,
                            login = @login,
                            password = @password,
                            type = @type
                        WHERE 
                            userID = @userID";

                    using (var command = new SqlCommand(updateUserQuery, connection))
                    {
                        command.Parameters.AddWithValue("@fio", fio ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@phone", phone ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@login", login ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@password", password ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@type", type ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@userID", userID);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Изменения сохранены");
                LoadUsersData(); // Перезагрузка данных после сохранения изменений
            }
        }

        private void BtnSaveUserChanges_Click(object sender, EventArgs e)
        {
            SaveUserChanges();
        }

        private void DgvRequests_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (_userRole == "Заказчик")
            {
                string status = dgvRequests.Rows[e.RowIndex].Cells["requestStatus"].Value?.ToString();
                string columnName = dgvRequests.Columns[e.ColumnIndex].Name;

                if (status != "Новая заявка" || (columnName != "orgTechType" && columnName != "orgTechModel" && columnName != "problemDescryption"))
                {
                    e.Cancel = true; // Заказчик может изменять только определенные поля для заявок со статусом "Новая заявка"
                }
            }
        }

        private void DgvRequests_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!changesPending)
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите сохранить изменения?", "Подтверждение", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    SaveChanges();
                }
                else
                {
                    // Отмена изменений
                    if (_userRole == "Мастер")
                    {
                        LoadRequestsData2(); // Перезагрузка данных для мастера
                    }
                    else if (_userRole == "Менеджер"|| _userRole == "Оператор")
                    {
                        LoadRequestsData3(); // Перезагрузка данных для администратора
                    }
                    else
                    {
                        LoadRequestsData1(); // Перезагрузка данных для заказчика
                    }
                }
            }
        }

        private void DgvRequests_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "Подтверждение удаления", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                int requestId = Convert.ToInt32(e.Row.Cells["requestID"].Value);

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string deleteRequestQuery = "DELETE FROM Requests WHERE requestID = @requestID";
                    using (var command = new SqlCommand(deleteRequestQuery, connection))
                    {
                        command.Parameters.AddWithValue("@requestID", requestId);
                        command.ExecuteNonQuery();
                    }

                    string deleteCommentsQuery = "DELETE FROM Comments WHERE requestID = @requestID";
                    using (var command = new SqlCommand(deleteCommentsQuery, connection))
                    {
                        command.Parameters.AddWithValue("@requestID", requestId);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
