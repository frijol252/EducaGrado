using Implementation;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EducaGrado.Profesor.Schedules
{
    /// <summary>
    /// Lógica de interacción para TeacherSchedules.xaml
    /// </summary>
    public partial class TeacherSchedules : UserControl
    {
        public TeacherSchedules()
        {
            InitializeComponent();
        }
        ScheduleImpl scheduleImpl;
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            loadGrid();
        }
        public void loadGrid()
        {
            try
            {
                scheduleImpl = new ScheduleImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = scheduleImpl.SelectHourClassTeacher().DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
