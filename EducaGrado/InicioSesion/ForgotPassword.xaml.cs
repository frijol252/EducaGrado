using System.Windows;
using System.Windows.Input;

namespace EducaGrado.InicioSesion
{
    /// <summary>
    /// Lógica de interacción para ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            Index ind = new Index();
            ind.Show();
            this.Close();
        }
    }
}
