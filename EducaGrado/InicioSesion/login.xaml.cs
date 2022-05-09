using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        public string PathImage
        {
            get { return _pathimage; }
            set { _pathimage = value; this.OnPropertyChanged("CoordenadaStream"); }
        }
        private string _pathimage = @"..\images\educadblogo.png";
        
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            EducaGrado.Administrativo.Home.HomeAdmin admin = new Administrativo.Home.HomeAdmin();
            admin.Show();
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
