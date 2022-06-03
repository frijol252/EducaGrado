using EducaGrado.xDialog;
using Implementation;
using Model;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EducaGrado.Administrativo.Students
{
    /// <summary>
    /// Lógica de interacción para StudentforCourseView.xaml
    /// </summary>
    public partial class StudentforCourseView : Window
    {
        int idCourse;
        Student stu;
        StudentImpl studentImpl;
        PersonImpl personImpl;
        Person person;
        int dis = 0;
        public StudentforCourseView(int i)
        {
            this.IdCourse = i;
            InitializeComponent();
        }
        #region getters
        public int IdCourse { get => idCourse; set => idCourse = value; }
        #endregion
        private void Window_Initialized(object sender, EventArgs e)
        {
            loadGrid();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        public void loadGrid()
        {
            try
            {
                if (dis == 0)
                {
                    studentImpl = new StudentImpl();
                    dgvDatos.ItemsSource = null;
                    dgvDatos.ItemsSource = studentImpl.Select(idCourse).DefaultView;
                }
                else
                {
                    studentImpl = new StudentImpl();
                    dgvDatos.ItemsSource = null;
                    dgvDatos.ItemsSource = studentImpl.SelectDis(idCourse).DefaultView;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void Txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (dis == 0)
                {
                    if (txtsearch.Text == "")
                    {
                        studentImpl = new StudentImpl();
                        dgvDatos.ItemsSource = null;
                        dgvDatos.ItemsSource = studentImpl.Select(idCourse).DefaultView;
                    }
                    else
                    {
                        studentImpl = new StudentImpl();
                        dgvDatos.ItemsSource = null;
                        dgvDatos.ItemsSource = studentImpl.SelectLike(idCourse, txtsearch.Text).DefaultView;
                    }
                }
                else
                {
                    if (txtsearch.Text == "")
                    {
                        studentImpl = new StudentImpl();
                        dgvDatos.ItemsSource = null;
                        dgvDatos.ItemsSource = studentImpl.SelectDis(idCourse).DefaultView;
                    }
                    else
                    {
                        studentImpl = new StudentImpl();
                        dgvDatos.ItemsSource = null;
                        dgvDatos.ItemsSource = studentImpl.SelectDisLike(idCourse, txtsearch.Text).DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void DgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatos.Items.Count > 0 && dgvDatos.SelectedItem != null)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)dgvDatos.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());

                    personImpl = new PersonImpl();
                    person = personImpl.SelectPerson(id);
                    imagesector.Source = ToImage(person.Photo);
                    lblnames.Content = dataRow.Row.ItemArray[1].ToString();
                    lblCi.Content = dataRow.Row.ItemArray[2].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public BitmapImage ToImage(byte[] array)
        {
            using (MemoryStream ms = new MemoryStream(array))
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;//CacheOption must be set after BeginInit()
                img.StreamSource = ms;
                img.EndInit();

                if (img.CanFreeze)
                {
                    img.Freeze();
                }


                return img;
            }
        }
        

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            StudentAdd stu = new StudentAdd(idCourse);
            stu.Show();
            this.Close();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            if (dis == 0)
            {
                dis = 1;
                DisStu.Content = "Enabled Students";
                DisStu.Background = (Brush)bc.ConvertFrom("#dfa752");
                templatecolumn.Header = "Habilitar";
            }
            else
            {
                dis = 0;
                DisStu.Content = "Disabled Students";
                DisStu.Background = (Brush)bc.ConvertFrom("#D15656");
                templatecolumn.Header = "Deshabilitar";
            }
            loadGrid();
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;

                string nombreestudiante = dataRowView[1].ToString();


                System.Windows.Forms.DialogResult result = MsgBox.Show("Estas Seguro de Eliminar " + nombreestudiante + "?", "Atencion", MsgBox.Buttons.YesNo, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    studentImpl = new StudentImpl();
                    int res = studentImpl.DeleteStudent(int.Parse(dataRowView[0].ToString()));
                    if (res != 0)
                    {
                        MsgBox.Show("Estudiante Eliminado ", "Atencion", MsgBox.Buttons.OK, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
                        loadGrid();
                        imagesector.Source = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        private void btnModif_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;

                int idPerson = int.Parse(dataRowView[0].ToString());
                StudentModif studentModif = new StudentModif(idCourse, idPerson);
                studentModif.Show();
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnDisgrid_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;

                string nombreestudiante = dataRowView[1].ToString();


                System.Windows.Forms.DialogResult result;
                if (dis == 0) result = MsgBox.Show("Estas Seguro de Deshabilitar " + nombreestudiante + "?", "Atencion", MsgBox.Buttons.YesNo, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
                else result = MsgBox.Show("Estas Seguro de Habilitar " + nombreestudiante + "?", "Atencion", MsgBox.Buttons.YesNo, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    studentImpl = new StudentImpl();
                    int res = studentImpl.DeleteDis(int.Parse(dataRowView[0].ToString()), dis);
                    if (res != 0)
                    {
                        MsgBox.Show("Estado de Estudiante Actualizado ", "Atencion", MsgBox.Buttons.OK, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
                        loadGrid();
                        imagesector.Source = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
