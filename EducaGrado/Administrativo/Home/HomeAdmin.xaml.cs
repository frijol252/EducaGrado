using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
                    GridPrincipal.Children.Clear();
                    break;
                case 1:

                    break;
                case 2:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new EducaGrado.Administrativo.Teacher.TeacherList());

                    break;
                case 3:
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
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(new EducaGrado.Administrativo.Controles.Horarios.ScheduleView());
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
            Session.setnulls();
            EducaGrado.InicioSesion.Index m = new EducaGrado.InicioSesion.Index();
            m.Show();
            this.Close();
        }

        private void ListStudent_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void Pagos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(new EducaGrado.Administrativo.Controles.Invoice.StudentLint());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Revision_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(new EducaGrado.Administrativo.Controles.Invoice.InvoiceView());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
