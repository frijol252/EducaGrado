using EducaGrado.xDialog;
using Implementation;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EducaGrado.Administrativo.Teacher
{
    /// <summary>
    /// Lógica de interacción para TeacherScheduleAdd.xaml
    /// </summary>
    public partial class TeacherScheduleAdd : Window
    {
        int idTeacher;
        public TeacherScheduleAdd(int idTeacher)
        {
            this.idTeacher = idTeacher;
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


        #region grid
        Matter matter;
        MatterImpl matterImpl;
        Schedule sch;
        ScheduleImpl scheduleImpl;
        ClassImpl clasImpl;
        Class classs;
        private void Window_Initialized(object sender, EventArgs e)
        {
            loadGrid();
            loadGridSche();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
        public void loadGrid()
        {
            try
            {
                matterImpl = new MatterImpl();
                dgvSub.ItemsSource = null;
                dgvSub.ItemsSource = matterImpl.SelectTeacher(idTeacher).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void loadGridSche()
        {
            try
            {
                scheduleImpl = new ScheduleImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = scheduleImpl.SelectHourClassbyClass(idSubject).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        #endregion


        int idSchedule = 0;
        int idSubject = 0;
        string materiaselected = "";
        List<int> lista = new List<int>();
        private void dgvSub_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvSub.Items.Count > 0 && dgvSub.SelectedItem != null)
            {
                try
                {
                    btnAddSubject.IsEnabled = true;
                    btnNope.IsEnabled = true;
                    DataRowView dataRow = (DataRowView)dgvSub.SelectedItem;
                    idSubject = int.Parse(dataRow.Row.ItemArray[0].ToString());
                    lblmatery.Content = dataRow.Row.ItemArray[1].ToString()+" "+ dataRow.Row.ItemArray[2].ToString();
                    loadGridSche();
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Addsubject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dataRow = (DataRowView)dgvSub.SelectedItem;
                System.Windows.Forms.DialogResult result = MsgBox.Show("Desea Añadir "+ dataRow.Row.ItemArray[1].ToString()+" "+ dataRow.Row.ItemArray[2].ToString() + "?", "Atencion", MsgBox.Buttons.YesNo, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    clasImpl = new ClassImpl();
                    int res = clasImpl.UpdateTeacher(idSubject,idTeacher);
                    MsgBox.Show("Materia Añadida", "Completado", MsgBox.Buttons.YesNo, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
                    TeacherSubject teacherSubject = new TeacherSubject(idTeacher);
                    teacherSubject.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {

            }


        }
        public string ReturnDay(int i)
        {
            if (i == 2) return "Lu";
            if (i == 3) return "Ma";
            if (i == 4) return "Mi";
            if (i == 5) return "Ju";
            if (i == 6) return "Vi";
            else return "Sa";
        }

        private void btnNope_Click(object sender, RoutedEventArgs e)
        {
            dgvDatos.ItemsSource = null;
            btnAddSubject.IsEnabled = false;
            btnNope.IsEnabled = false;
        }
    }
}
