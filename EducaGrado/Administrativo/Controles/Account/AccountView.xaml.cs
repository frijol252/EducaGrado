using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace EducaGrado.Administrativo.Controles.Account
{
    /// <summary>
    /// Lógica de interacción para AccountView.xaml
    /// </summary>
    public partial class AccountView : Window
    {
        public AccountView()
        {
            InitializeComponent();
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        BitmapImage image;
        private void Window_Initialized(object sender, EventArgs e)
        {
            //lblNames.Content = Session.SessionCurrent;
            //lblmail.Content = Session.SessionEmail;
            //string rol = "";
            //switch (Session.SessionRole)
            //{
            //    case ("A"):
            //        rol = "Administrator";
            //        break;
            //    case ("P"):
            //        rol = "Teacher";
            //        break;
            //    case ("E"):
            //        rol = "Student";
            //        break;
            //}
            //lblRole.Content = rol;
            //lbluser.Content = Session.SessionUser;

            //imagesector.Source = new BitmapImage(new Uri(DBImplementation.pathImages + Session.Sessionphoto + ".png"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ModifAccount mod = new ModifAccount();
            //mod.Show();
            //this.Close();
        }
    }
}
