using Implementation;
using Model;
using System;
using System.Windows;
using System.Windows.Input;

namespace EducaGrado.Administrativo.School
{
    /// <summary>
    /// Lógica de interacción para ModifySchool.xaml
    /// </summary>
    public partial class ModifySchool : Window
    {
        public ModifySchool()
        {
            InitializeComponent();
        }
        SchoolType schoolType;
        Modality modality;
        SchoolImpl schoolImpl;
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnModifySchool_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                byte notas = byte.Parse(txtnotas.Text);
                byte examen = byte.Parse(txtexamen.Text);
                string calificacion = comboCalificacion.Text;
                string cursos = txtcursos.Text;
                schoolType = new SchoolType();
                modality = new Modality();
                schoolType.Cursos = cursos;
                modality.NumberGrades = notas;
                modality.NumberTest = examen;
                modality.TypeQualify = calificacion;
                if (!string.IsNullOrEmpty(notas.ToString()) || !string.IsNullOrEmpty(examen.ToString()) || !string.IsNullOrEmpty(calificacion.ToString()) || !string.IsNullOrEmpty(cursos.ToString()))
                {
                    schoolImpl = new SchoolImpl();
                    schoolImpl.UpdateTypeWork(schoolType, modality, Session.SessionSchoolId);
                    MessageBox.Show("Tipo de trabajo de la escuela establecido");
                    Administrativo.Home.HomeAdmin ha = new Home.HomeAdmin();
                    ha.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio comuniquese con el soporte" + ex.Message);
            }
        }
    }
}
