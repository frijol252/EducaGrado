using EducaGrado.xDialog;
using Implementation;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace EducaGrado.Administrativo.Students
{
    /// <summary>
    /// Lógica de interacción para SubjectAdd.xaml
    /// </summary>
    public partial class SubjectAdd : Window
    {
        int course;
        public SubjectAdd(int course)
        {
            this.course = course;
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
                dgvSub.ItemsSource = matterImpl.SelectForAddMatters(course).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void loadGridSche()
        {
            try
            {
                scheduleImpl = new ScheduleImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = scheduleImpl.SelectHourClass(course).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        #endregion

        
        int idSchedule = 0;
        int idSubject = 0;
        string materiaselected = "";
        List<int> lista = new List<int>();
        private void DgvSub_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvSub.Items.Count > 0 && dgvSub.SelectedItem != null)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)dgvSub.SelectedItem;
                    int id = int.Parse(dataRow.Row.ItemArray[0].ToString());
                    materiaselected = dataRow.Row.ItemArray[1].ToString();
                    idSubject = id;
                    lblmatery.Content = dataRow.Row.ItemArray[1].ToString();
                    dgvDatos.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void dgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvDatos.Items.Count > 0 && dgvDatos.SelectedItem != null)
            {
                try
                {
                    Addsubject.IsEnabled = true;
                    DataRowView dataRow = (DataRowView)dgvDatos.SelectedItem;
                    int index = dgvDatos.CurrentCell.Column.DisplayIndex;
                    string cellValue = dataRow.Row.ItemArray[index].ToString();
                    int count = 0;
                    if (cellValue == "O")
                    {
                        foreach (DataRowView row in dgvDatos.ItemsSource)
                        {
                            if (count == dgvDatos.Items.IndexOf(dgvDatos.CurrentItem))
                            {
                                row[dgvDatos.Columns.IndexOf(dgvDatos.CurrentColumn)] = "Seleccionado";
                            }
                            count++;
                        }
                    }
                    else if(cellValue== "Seleccionado")
                    {
                        foreach (DataRowView row in dgvDatos.ItemsSource)
                        {
                            if (count == dgvDatos.Items.IndexOf(dgvDatos.CurrentItem))
                            {
                                row[dgvDatos.Columns.IndexOf(dgvDatos.CurrentColumn)] = "O";
                            }
                            count++;
                        }
                    }
                    else { MsgBox.Show("No puede crear choques de horario", "Atencion", MsgBox.Buttons.OK, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn); }
                    

                    



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void BtnSelectSubject_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (idSubject == 0) { MessageBox.Show("Please select one Subject"); }
            //    else
            //    {
            //        dgvSub.IsEnabled = false;
            //        btnSelectSubject.IsEnabled = false;
            //        dgvDatos.IsEnabled = true;
            //        dgvDatosjue.IsEnabled = true;
            //        dgvDatosmar.IsEnabled = true;
            //        dgvDatosmier.IsEnabled = true;
            //        dgvDatossab.IsEnabled = true;
            //        dgvDatosvier.IsEnabled = true;
            //    }
            //}
            //catch
            //{

            //}
        }

        private void Addsubject_Click(object sender, RoutedEventArgs e)
        {
            List<Class> lista = new List<Class>();
            
            System.Windows.Forms.DialogResult result = MsgBox.Show("Esta seguro de Agregar "+materiaselected+"?", "Atencion", MsgBox.Buttons.YesNo, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (DataRowView row in dgvDatos.ItemsSource)
                {
                    for (int i=2; i<8; i++)
                    {
                        if (row[i].ToString() == "Seleccionado")
                        {

                            lista.Add(new Class(course, int.Parse(row[0].ToString()),idSubject,ReturnDay(i)));
                        }
                    }
                }
                clasImpl = new ClassImpl();
                clasImpl.Inserttransact(lista);
                MsgBox.Show("Clase Insertada", "Completada", MsgBox.Buttons.OK, MsgBox.Icon.Info, MsgBox.AnimateStyle.FadeIn);
                CourseSubject courseSubject = new CourseSubject(course);
                courseSubject.Show();
                this.Close();
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
       
    }
    
}
