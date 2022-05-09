using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Implementation;
using Model;

namespace EducaGrado.InicioSesion
{
    /// <summary>
    /// Lógica de interacción para Index.xaml
    /// </summary>
    public partial class Index : Window
    {
        UserImpl implUser;
        private bool accept = false;
        private bool revisara = false;

        public Index()
        {
            InitializeComponent();
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            ForgotPassword fp = new ForgotPassword();
            fp.Show();
            this.Close();
        }
    }

    /*
     #region Properiarities

        #endregion
        #region Getters/Setters

        #endregion
        #region Constructor
       
        #endregion
     */
}
