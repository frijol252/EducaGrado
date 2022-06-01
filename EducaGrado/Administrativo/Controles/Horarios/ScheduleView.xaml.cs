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

namespace EducaGrado.Administrativo.Controles.Horarios
{
    /// <summary>
    /// Lógica de interacción para ScheduleView.xaml
    /// </summary>
    public partial class ScheduleView : UserControl
    {
        ScheduleImpl scheduleImpl;
        Schedule schedule;
        DataTable horarios;
        public ScheduleView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
        }

        private void btnAddSchedule_Click(object sender, RoutedEventArgs e)
        {
            scheduleImpl = new ScheduleImpl();
            schedule = new Schedule();
            if (!string.IsNullOrEmpty(DPHoraInicio.Text) && !string.IsNullOrEmpty(DPHoraFinal.Text))
            {
                schedule.StartHour = (DateTime)DPHoraInicio.SelectedTime;
                schedule.FinishHour = (DateTime)DPHoraFinal.SelectedTime;

                if (Validar())
                {
                    System.Windows.Forms.DialogResult result = MsgBox.Show("Quiere Insertar " + DPHoraInicio.Text + " a " + DPHoraFinal.Text + "?", "Atencion", MsgBox.Buttons.YesNo, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {

                        scheduleImpl.Insert(schedule);
                        MsgBox.Show("Horario Añdadido", "Completado", MsgBox.Buttons.OK);
                        LoadDataGrid();
                    }
                }

                
            }
            else MsgBox.Show("Seleccione Horarios", "Atencion", MsgBox.Buttons.OK);
        }

        private bool Validar()
        {
            bool check = true;

            if (DPHoraInicio.SelectedTime < DPHoraFinal.SelectedTime)
            {
                foreach (DataRow row in horarios.Rows)
                {
                    if (DPHoraInicio.SelectedTime == DateTime.Parse(row[1].ToString()) && DPHoraFinal.SelectedTime == DateTime.Parse(row[2].ToString()))
                    {
                        MsgBox.Show("No se puede insertar un horario ya existente", "Atencion", MsgBox.Buttons.OK);
                        return false;
                    }
                    else
                    {
                        if (DPHoraInicio.SelectedTime > DateTime.Parse(row[1].ToString()) && DPHoraInicio.SelectedTime < DateTime.Parse(row[2].ToString()))
                        {
                            MsgBox.Show("Seleccione Horarios que no tengan choques", "Atencion", MsgBox.Buttons.OK);
                            check = false;
                        }

                        else if (DPHoraFinal.SelectedTime > DateTime.Parse(row[1].ToString()) && DPHoraFinal.SelectedTime < DateTime.Parse(row[2].ToString()))
                        {
                            MsgBox.Show("Seleccione Horarios que no tengan choques", "Atencion", MsgBox.Buttons.OK);
                            check = false;
                        }
                    }

                }
            }
            else
            {
                MsgBox.Show("La hora inicial no puede ser mayor a la final", "Atencion", MsgBox.Buttons.OK);
                check = false;
            }
            return check;
        }

        public void LoadDataGrid()
        {
            try
            {
                scheduleImpl = new ScheduleImpl();
                dgvDatosHorario.ItemsSource = null;
                horarios = scheduleImpl.Select();
                dgvDatosHorario.ItemsSource = horarios.DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void dgvDatosHorario_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;

                string NombreHorario = dataRowView[1].ToString();
                string NombreHorarioFinal = dataRowView[2].ToString();
                
                System.Windows.Forms.DialogResult result = MsgBox.Show("Estas Seguro de Eliminar " + NombreHorario +" a "+ NombreHorarioFinal+"?", "Atencion", MsgBox.Buttons.YesNo, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    int IdSchedule = int.Parse(dataRowView[0].ToString());
                    schedule = new Schedule();
                    schedule.ScheduleId = IdSchedule;
                    scheduleImpl = new ScheduleImpl();
                    scheduleImpl.Delete(schedule);
                    MsgBox.Show("Horario Eliminada", "Completado", MsgBox.Buttons.OK);
                    LoadDataGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        
    }
}
