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

namespace EducaGrado.Administrativo.Home
{
    /// <summary>
    /// Lógica de interacción para HomeAdmin.xaml
    /// </summary>
    public partial class HomeAdmin : Window
    {
        public HomeAdmin()
        {
            InitializeComponent();
        }
        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            //MoverCursorMenu(index);

            switch (index)
            {
                case 0:
                    
                    break;
                case 1:
                    
                    break;
                case 3:
                    
                    break;
                case 5:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new EducaGrado.Administrativo.Students.CoursesView());
                    break;
                default:
                    break;
            }
        }

        private void MateriasButton_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new EducaGrado.Administrativo.Controles.Materias.MattersView());
        }

        private void HorariosButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CrudTeacher_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ListTeacher_Click(object sender, RoutedEventArgs e)
        {
            
        }


        //Falta Terminar este boton
        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            EducaGrado.InicioSesion.Index m = new EducaGrado.InicioSesion.Index();
            m.Show();
            this.Close();
        }

        private void ListStudent_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
