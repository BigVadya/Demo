using System;
using System.Windows;
using Npgsql;

namespace Demo
{
    public partial class MainWindow : Window
    {
        private const string ConnStr = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;";
        private int failedAttempts = 0;

        public MainWindow() => InitializeComponent();

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLogin.Text) || string.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("Логин и пароль обязательны!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var conn = new NpgsqlConnection(ConnStr))
                {
                    conn.Open();
                    var user = GetUser(conn, txtLogin.Text);
                    if (user == null)
                    {
                        MessageBox.Show("Пользователь не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    ProcessAuthWithCaptcha(user, conn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProcessAuthWithCaptcha(User user, NpgsqlConnection conn)
        {
            // Показываем капчу при любой неудачной попытке
            var captchaWindow = new CaptchaWindow();
            if (captchaWindow.ShowDialog() != true ||
                captchaWindow.EnteredCode != captchaWindow.CaptchaCode)
            {
                MessageBox.Show("Неверный код капчи!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверяем пароль только после успешной капчи
            if (txtPassword.Password != user.Password)
            {
                failedAttempts++;
                int attempts = user.Count + 1;

                if (attempts >= 3)
                {
                    BlockUser(conn, txtLogin.Text);
                    return;
                }

                UpdateCount(conn, txtLogin.Text, attempts);
                MessageBox.Show($"Неверный пароль! Осталось попыток: {3 - attempts}",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Успешная авторизация
            failedAttempts = 0;
            ResetCount(conn, txtLogin.Text);
            OpenAppWindow(user);
        }

        private User GetUser(NpgsqlConnection conn, string login)
        {
            using (var cmd = new NpgsqlCommand("SELECT surname, password, role_id, count, date, active FROM users WHERE login = @login", conn))
            {
                cmd.Parameters.AddWithValue("@login", login);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) return null;
                    return new User
                    {
                        Surname = reader.GetString(0),
                        Password = reader.GetString(1),
                        RoleId = reader.GetInt32(2),
                        Count = reader.GetInt32(3),
                        Date = reader.IsDBNull(4) ? null : (DateTime?)reader.GetDateTime(4),
                        Active = reader.GetBoolean(5)
                    };
                }
            }
        }

        private void BlockUser(NpgsqlConnection conn, string login)
        {
            ExecuteQuery(conn, "UPDATE users SET active = false, count = 0 WHERE login = @login", ("@login", login));
            MessageBox.Show("Аккаунт заблокирован!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UpdateCount(NpgsqlConnection conn, string login, int count) =>
            ExecuteQuery(conn, "UPDATE users SET count = @count WHERE login = @login", ("@count", count), ("@login", login));

        private void ResetCount(NpgsqlConnection conn, string login) =>
            ExecuteQuery(conn, "UPDATE users SET count = 0, date = NOW() WHERE login = @login", ("@login", login));

        private void ExecuteQuery(NpgsqlConnection conn, string sql, params (string, object)[] parameters)
        {
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                foreach (var (name, value) in parameters) cmd.Parameters.AddWithValue(name, value);
                cmd.ExecuteNonQuery();
            }
        }

        private void OpenAppWindow(User user)
        {
            MessageBox.Show($"Добро пожаловать, {user.Surname}!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            Window nextWindow = user.RoleId == 3 ? (Window)new UserWindow() : new AdminWindow();
            nextWindow.Show();
            this.Close();
        }

        private class User
        {
            public string Surname { get; set; }
            public string Password { get; set; }
            public int RoleId { get; set; }
            public int Count { get; set; }
            public DateTime? Date { get; set; }
            public bool Active { get; set; }
        }
    }
}