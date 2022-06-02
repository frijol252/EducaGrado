using EducaGrado.xDialog;
using Implementation;
using Model;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EducaGrado.Administrativo.Students
{
    /// <summary>
    /// Lógica de interacción para CourseSubject.xaml
    /// </summary>
    public partial class CourseSubject : Window
    {
        int course;
        Class cl;
        ClassImpl clImpl;
        public CourseSubject(int Course)
        {
            this.course = Course;
            InitializeComponent();
        }
        #region control 
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Pe
        private void Window_Initialized(object sender, EventArgs e)
        {
            loadGrid();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Ocultar();
        }
        public void Ocultar()
        {
        }
        public void loadGrid()
        {
            try
            {
                clImpl = new ClassImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = clImpl.Select(course).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        int ids = 0;
        private void DgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        #endregion

        #region search
        private void Txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //try
            //{
            //    if (txtsearch.Text == "")
            //    {
            //        clImpl = new ClassImpl();
            //        dgvDatos.ItemsSource = null;
            //        dgvDatos.ItemsSource = clImpl.Select(Course).DefaultView;
            //        Ocultar();
            //    }
            //    else
            //    {
            //        clImpl = new ClassImpl();
            //        dgvDatos.ItemsSource = null;
            //        dgvDatos.ItemsSource = clImpl.Selectlike(Course, txtsearch.Text).DefaultView;
            //        Ocultar();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }
        #endregion

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            SubjectAdd sa = new SubjectAdd(course);
            sa.Show();
            this.Close();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    clImpl = new ClassImpl();
            //    clImpl.DeleteTransaction(ids);

            //    loadGrid();
            //    Ocultar();
            //    MessageBox.Show("Class Delete Success");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com");
            //}
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                string NombreMateria = dataRowView[1].ToString();
                System.Windows.Forms.DialogResult result = MsgBox.Show("Estas Seguro de Eliminar " + NombreMateria + "?", "Atencion", MsgBox.Buttons.YesNo, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    int IdCategory = int.Parse(dataRowView[0].ToString());
                    cl = new Model.Class();
                    cl.ClassId = IdCategory;
                    clImpl = new ClassImpl();
                    clImpl.Delete(cl);
                    MsgBox.Show("Categoria Eliminada", "Completado", MsgBox.Buttons.OK);
                    loadGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnViewModif_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                string NombreMateria = dataRowView[1].ToString();
                int IdCategory = int.Parse(dataRowView[0].ToString());
                SubjectModif subjectModif = new SubjectModif(course, IdCategory);
                subjectModif.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
