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

namespace EducaGrado.Administrativo.Teacher
{
    /// <summary>
    /// Lógica de interacción para TeacherList.xaml
    /// </summary>
    public partial class TeacherList : UserControl
    {
        Model.Teacher teacher;
        TeacherImpl teacherimpl;
        PersonImpl personImpl;
        Person person;
        int dis = 0;
        public TeacherList()
        {
            InitializeComponent();
        }

        #region Grid
        //Control del grid
        public void loadGrid()
        {
            
            try
            {
                if (dis == 0)
                {
                    teacherimpl = new TeacherImpl();
                    dgvDatos.ItemsSource = null;
                    dgvDatos.ItemsSource = teacherimpl.Select().DefaultView;
                }
                else
                {
                    teacherimpl = new TeacherImpl();
                    dgvDatos.ItemsSource = null;
                    dgvDatos.ItemsSource = teacherimpl.SelectDis().DefaultView;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void Txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //if (dis == 0)
                //{
                //    if (txtsearch.Text == "")
                //    {
                //        studentImpl = new StudentImpl();
                //        dgvDatos.ItemsSource = null;
                //        dgvDatos.ItemsSource = studentImpl.Select(idCourse).DefaultView;
                //    }
                //    else
                //    {
                //        studentImpl = new StudentImpl();
                //        dgvDatos.ItemsSource = null;
                //        dgvDatos.ItemsSource = studentImpl.SelectLike(idCourse, txtsearch.Text).DefaultView;
                //    }
                //}
                //else
                //{
                //    if (txtsearch.Text == "")
                //    {
                //        studentImpl = new StudentImpl();
                //        dgvDatos.ItemsSource = null;
                //        dgvDatos.ItemsSource = studentImpl.SelectDis(idCourse).DefaultView;
                //    }
                //    else
                //    {
                //        studentImpl = new StudentImpl();
                //        dgvDatos.ItemsSource = null;
                //        dgvDatos.ItemsSource = studentImpl.SelectDisLike(idCourse, txtsearch.Text).DefaultView;
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void Window_Initialized(object sender, EventArgs e)
        {
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadGrid();
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
        #endregion


        private void BtnDisabledView_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            if (dis == 0)
            {
                dis = 1;
                btnDisabledView.Content = "Profesores Habilitados";
                btnDisabledView.Background = (Brush)bc.ConvertFrom("#dfa752");
                templatecolumn.Header = "Habilitar";
            }
            else
            {
                dis = 0;
                btnDisabledView.Content = "Profesores Deshabilitados";
                btnDisabledView.Background = (Brush)bc.ConvertFrom("#D15656");
                templatecolumn.Header = "Deshabilitar";
            }
            loadGrid();
        }

        private void Disabledbtn_Click(object sender, RoutedEventArgs e)
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
                    teacherimpl = new TeacherImpl();
                    int res = teacherimpl.DeleteDis(int.Parse(dataRowView[0].ToString()), dis);
                    if (res != 0)
                    {
                        MsgBox.Show("Estado de Profesor Actualizado ", "Atencion", MsgBox.Buttons.OK, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
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
       
        

        private void Subjects_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;

                TeacherSubject ts = new TeacherSubject(int.Parse(dataRowView[0].ToString()));
                ts.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            TeacherAdd teacherAdd = new TeacherAdd();
            teacherAdd.Show();
        }

        private void btnModif_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                int id = int.Parse(dataRowView[0].ToString());                TeacherModif ts = new TeacherModif(id,this);
                ts.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
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
                    teacherimpl = new TeacherImpl();
                    int res = teacherimpl.DeleteTeacher(int.Parse(dataRowView[0].ToString()));
                    if (res != 0)
                    {
                        MsgBox.Show("Profesor Eliminado ", "Atencion", MsgBox.Buttons.OK, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
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
