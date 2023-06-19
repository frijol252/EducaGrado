using EducaGrado.Profesor.Claims;
using Model;
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

namespace EducaGrado.Estudiante.Home
{
    /// <summary>
    /// Lógica de interacción para IndexStudent.xaml
    /// </summary>
    public partial class IndexStudent : Window
    {
        public IndexStudent()
        {
            InitializeComponent();
        }


        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {

            Session.setnulls();
            InicioSesion.Index index = new InicioSesion.Index();
            index.Show();
            this.Close();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
             int index = ListViewMenu.SelectedIndex;
             //MoverCursorMenu(index);

             switch (index)
             {
                 case 0:
                     GridPrincipal.Children.Clear();
                     break;
                 case 4:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new EducaGrado.Profesor.Claims.ClaimsView());
                    break;
                 default:
                     break;
             }
            
        }

        private void ListTeacher_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new EducaGrado.Estudiante.Schedules.StudentSchedules());
        }

        private void btnNotas_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new EducaGrado.Estudiante.Grades.StudentGrade());
        }
    }
}
