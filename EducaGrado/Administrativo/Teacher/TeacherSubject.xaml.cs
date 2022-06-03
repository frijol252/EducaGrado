using EducaGrado.xDialog;
using Implementation;
using Model;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EducaGrado.Administrativo.Teacher
{
    /// <summary>
    /// Lógica de interacción para TeacherSubject.xaml
    /// </summary>
    public partial class TeacherSubject : Window
    {
        int idTeacher;
        ScheduleImpl scheduleImpl;
        ClassImpl classImpl;
        public TeacherSubject(int idTeacher)
        {
            this.idTeacher = idTeacher;
            InitializeComponent();
            loadGrid();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Initialized(object sender, EventArgs e)
        {

        }

        public void loadGrid()
        {
            try
            {
                scheduleImpl = new ScheduleImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = scheduleImpl.SelectHourClassTeacher(idTeacher).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void dgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatos.Items.Count > 0 && dgvDatos.SelectedItem != null)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)dgvDatos.SelectedItem;
                    int index = dgvDatos.CurrentCell.Column.DisplayIndex;
                    string cellValue = dataRow.Row.ItemArray[index].ToString();
                    System.Windows.Forms.DialogResult result = MsgBox.Show("Esta seguro de desasingnar " + cellValue + "?", "Atencion", MsgBox.Buttons.YesNo, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        int id = int.Parse(dataRow.Row.ItemArray[0].ToString());
                        classImpl = new ClassImpl();
                        classImpl.DeleteTeacher(id, idTeacher, ReturnDay(index));
                        MsgBox.Show("Clase Desasignada", "Completado", MsgBox.Buttons.OK, MsgBox.Icon.Info);
                        loadGrid();
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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

        private void btnAddClass_Click(object sender, RoutedEventArgs e)
        {
            TeacherScheduleAdd teacherAdd = new TeacherScheduleAdd(idTeacher);
            teacherAdd.Show();
            this.Close();
        }
    }
}
