using System.Windows;

namespace Demo
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            // Закрываем текущее окно
            this.Close();

            // При необходимости, можно открыть другое окно, например окно входа
            // MainWindow mainWindow = new MainWindow();
            // mainWindow.Show();
        }
    }
}
