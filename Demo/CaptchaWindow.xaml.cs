using System;
using System.Windows;

namespace Demo
{
    public partial class CaptchaWindow : Window
    {
        public string CaptchaCode { get; private set; }
        public string EnteredCode { get; private set; }

        private readonly Random rnd = new Random();

        public CaptchaWindow()
        {
            InitializeComponent();
            GenerateNewCaptcha();
        }

        private void GenerateNewCaptcha()
        {
            CaptchaCode = rnd.Next(1000, 9999).ToString();
            captchaText.Text = $"Введите код: {CaptchaCode}";
            txtCaptcha.Clear();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            EnteredCode = txtCaptcha.Text.Trim();

            if (string.IsNullOrEmpty(EnteredCode))
            {
                MessageBox.Show("Введите код!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (EnteredCode != CaptchaCode)
            {
                MessageBox.Show("Неверный код. Новая капча сгенерирована.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                GenerateNewCaptcha();
                return;
            }

            DialogResult = true;
            Close();
        }

        private void RefreshCaptcha_Click(object sender, RoutedEventArgs e) => GenerateNewCaptcha();
    }
}
