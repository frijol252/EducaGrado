using Implementation;
using Model;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace EducaGrado.Administrativo.Students
{
    /// <summary>
    /// Lógica de interacción para CoursesView.xaml
    /// </summary>
    public partial class CoursesView : UserControl
    {
        Course course = new Course();
        CourseImpl courseImpl;
        public CoursesView()
        {
            InitializeComponent();
        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            LoadDataGrid();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Ocultar();
        }
        public void LoadDataGrid()
        {
            try
            {
                courseImpl = new CourseImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = courseImpl.Select().DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void LoadDataGridlike(string like)
        {
            try
            {
                courseImpl = new CourseImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = courseImpl.SelectLike(like).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void Ocultar()
        {
            dgvDatos.Columns[0].Visibility = Visibility.Hidden;
        }


        private void DgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatos.Items.Count > 0 && dgvDatos.SelectedItem != null)
            {
                try
                {

                    DataRowView dataRow = (DataRowView)dgvDatos.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());

                    course.Idcourse = id;
                    lblcourse.Content = dataRow.Row.ItemArray[1].ToString();
                    lblsection.Content = dataRow.Row.ItemArray[2].ToString();
                    btnStu.IsEnabled = true;
                    btnSubs.IsEnabled = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtsearch.Text == "")
                {
                    LoadDataGrid();
                    Ocultar();
                }
                else
                {
                    LoadDataGridlike(txtsearch.Text);
                    Ocultar();
                }
            }
            catch
            {

            }
        }



        private void BtnStu_Click(object sender, RoutedEventArgs e)
        {
            StudentforCourseView s = new StudentforCourseView(course.Idcourse);
            s.Show();

        }

        private void BtnSubs_Click(object sender, RoutedEventArgs e)
        {
            CourseSubject sl = new CourseSubject(course.Idcourse);
            sl.Show();
        }
    }
}
