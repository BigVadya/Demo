using System;
using System.Data;
using System.Linq;
using System.Windows;
using Npgsql;

namespace Demo
{
    public partial class AdminWindow : Window
    {
        private const string ConnectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;";

        public AdminWindow()
        {
            InitializeComponent();
            FixSequence();
            LoadRoles();
            LoadUsers();
            button3.Click += AddUser_Click;
            button4.Click += SaveChanges_Click;
            buttonClose.Click += buttonClose_Click;
            buttonDelete.Click += buttonDelete_Click;
        }

        private void FixSequence()
        {
            try
            {
                using var conn = new NpgsqlConnection(ConnectionString);
                using var cmd = new NpgsqlCommand("SELECT setval(pg_get_serial_sequence('users','id'),COALESCE((SELECT MAX(id) FROM users),0)+1,false)", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка синхронизации ID: {ex.Message}", "Ошибка");
            }
        }

        private void LoadRoles()
        {
            try
            {
                using var conn = new NpgsqlConnection(ConnectionString);
                using var cmd = new NpgsqlCommand("SELECT id,role FROM roles", conn);
                conn.Open();
                typeRole.ItemsSource = cmd.ExecuteReader()
                    .Cast<IDataRecord>()
                    .Select(r => new { Id = r.GetInt32(0), Name = r.GetString(1) })
                    .ToList();
                typeRole.DisplayMemberPath = "Name";
                typeRole.SelectedValuePath = "Id";
                if (typeRole.Items.Count > 0) typeRole.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки ролей: {ex.Message}", "Ошибка");
            }
        }

        private void LoadUsers()
        {
            try
            {
                using var conn = new NpgsqlConnection(ConnectionString);
                conn.Open();
                var dt = new DataTable();
                new NpgsqlDataAdapter(
                    "SELECT u.id AS \"ID\", u.surname, u.name, u.otchestvo AS \"othcestvo\", " +
                    "r.role AS \"role2\", u.login, u.password, u.count, u.active, u.date, u.role_id " +
                    "FROM users u JOIN roles r ON u.role_id=r.id", conn).Fill(dt);
                dt.PrimaryKey = new[] { dt.Columns["ID"] };
                GridUser.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки пользователей: {ex.Message}", "Ошибка");
            }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox1.Text) || string.IsNullOrWhiteSpace(TextBox2.Text))
            {
                MessageBox.Show("Логин и пароль обязательны", "Ошибка");
                return;
            }

            try
            {
                using var conn = new NpgsqlConnection(ConnectionString);
                conn.Open();
                var role = (dynamic)typeRole.SelectedItem;

                using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE login=@login", conn))
                {
                    cmd.Parameters.AddWithValue("@login", TextBox1.Text);
                    if ((long)cmd.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Логин уже существует", "Ошибка");
                        return;
                    }
                }

                using (var cmd = new NpgsqlCommand(
                    "INSERT INTO users(surname,name,otchestvo,role_id,login,password,count,active,date) " +
                    "VALUES(@s,@n,@o,@r,@l,@p,0,true,null)", conn))
                {
                    cmd.Parameters.AddWithValue("@s", TextBox3.Text);
                    cmd.Parameters.AddWithValue("@n", TextBox4.Text);
                    cmd.Parameters.AddWithValue("@o", string.IsNullOrWhiteSpace(TextBox5.Text) ? (object)DBNull.Value : TextBox5.Text);
                    cmd.Parameters.AddWithValue("@r", role.Id);
                    cmd.Parameters.AddWithValue("@l", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@p", TextBox2.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Пользователь добавлен", "Успех");
                    ClearForm();
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(GridUser.ItemsSource is DataView dataView)) return;
                using var conn = new NpgsqlConnection(ConnectionString);
                conn.Open();
                var dt = dataView.Table;
                var currentLogins = new DataTable();
                new NpgsqlDataAdapter("SELECT id,login FROM users", conn).Fill(currentLogins);

                foreach (DataRow row in dt.Rows)
                {
                    if (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added)
                    {
                        string login = row["login"].ToString();
                        int id = row["ID"] is DBNull ? -1 : Convert.ToInt32(row["ID"]);
                        if (currentLogins.AsEnumerable().Any(r => r["login"].ToString() == login && Convert.ToInt32(r["id"]) != id))
                        {
                            MessageBox.Show($"Логин '{login}' уже используется", "Ошибка");
                            return;
                        }
                    }
                }

                foreach (DataRow row in dt.Rows)
                {
                    switch (row.RowState)
                    {
                        case DataRowState.Modified: UpdateUser(conn, row); break;
                        case DataRowState.Added: InsertUser(conn, row); break;
                        case DataRowState.Deleted: DeleteUser(conn, row); break;
                    }
                }

                MessageBox.Show("Изменения сохранены", "Успех");
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private void UpdateUser(NpgsqlConnection conn, DataRow row)
        {
            using var cmd = new NpgsqlCommand(
                "UPDATE users SET surname=@s,name=@n,otchestvo=@o,role_id=@r,login=@l," +
                "password=@p,count=@c,active=@a,date=@d WHERE id=@i", conn);
            AddParameters(cmd, row);
            cmd.Parameters.AddWithValue("@i", row["ID"]);
            cmd.ExecuteNonQuery();
        }

        private void InsertUser(NpgsqlConnection conn, DataRow row)
        {
            using var cmd = new NpgsqlCommand(
                "INSERT INTO users(surname,name,otchestvo,role_id,login,password,count,active,date) " +
                "VALUES(@s,@n,@o,@r,@l,@p,@c,@a,@d)", conn);
            AddParameters(cmd, row);
            cmd.ExecuteNonQuery();
        }

        private void DeleteUser(NpgsqlConnection conn, DataRow row)
        {
            using var cmd = new NpgsqlCommand("DELETE FROM users WHERE id=@i", conn);
            cmd.Parameters.AddWithValue("@i", row["ID", DataRowVersion.Original]);
            cmd.ExecuteNonQuery();
        }

        private void AddParameters(NpgsqlCommand cmd, DataRow row)
        {
            cmd.Parameters.AddWithValue("@s", row["surname"]);
            cmd.Parameters.AddWithValue("@n", row["name"]);
            cmd.Parameters.AddWithValue("@o", row["othcestvo"] == DBNull.Value ? (object)DBNull.Value : row["othcestvo"]);
            cmd.Parameters.AddWithValue("@r", row["role_id"]);
            cmd.Parameters.AddWithValue("@l", row["login"]);
            cmd.Parameters.AddWithValue("@p", row["password"]);
            cmd.Parameters.AddWithValue("@c", row["count"]);
            cmd.Parameters.AddWithValue("@a", row["active"]);
            cmd.Parameters.AddWithValue("@d", row["date"] == DBNull.Value ? (object)DBNull.Value : row["date"]);
        }

        private void ClearForm()
        {
            TextBox1.Text = TextBox2.Text = TextBox3.Text = TextBox4.Text = TextBox5.Text = "";
            if (typeRole.Items.Count > 0) typeRole.SelectedIndex = 0;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (GridUser.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя для удаления", "Ошибка");
                return;
            }

            try
            {
                var selectedRow = (DataRowView)GridUser.SelectedItem;
                int userId = (int)selectedRow["ID"];

                using var conn = new NpgsqlConnection(ConnectionString);
                using var cmd = new NpgsqlCommand("DELETE FROM users WHERE id = @id", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@id", userId);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Пользователь успешно удален", "Успех");
                LoadUsers();
                GridUser.SelectedItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка");
            }
        }
    }
}
