using EducaGrado.xDialog;
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
                byte notas=0;
                byte examen = 0;
                byte porcentajenotas = 0;
                byte pocentajetest = 0;
                string calificacion = comboCalificacion.Text;
                string cursos = txtcursos.Text;
                if (!string.IsNullOrEmpty(txtnotas.Text) || !string.IsNullOrEmpty(txtexamen.Text) || !string.IsNullOrEmpty(txtPorcentaje.Text) 
                    || !string.IsNullOrEmpty(txtPorcentajeTest.Text)
                    || !string.IsNullOrEmpty(cursos.ToString()) || !string.IsNullOrEmpty(cursos.ToString()))
                {
                    if (byte.TryParse(txtnotas.Text, out notas) && byte.TryParse(txtexamen.Text, out examen) &&
                    byte.TryParse(txtPorcentaje.Text, out porcentajenotas) && byte.TryParse(txtPorcentajeTest.Text, out pocentajetest))
                    {
                        byte suma = (byte)(porcentajenotas + pocentajetest);
                        if (suma == 100)
                        {
                            schoolType = new SchoolType();
                            modality = new Modality();
                            schoolType.Cursos = cursos;
                            modality.NumberGrades = notas;
                            modality.NumberTest = examen;
                            modality.TypeQualify = calificacion;
                            modality.PercentGrades = porcentajenotas;
                            modality.PercentTest = pocentajetest;


                            schoolImpl = new SchoolImpl();
                            schoolImpl.UpdateTypeWork(schoolType, modality, Session.SessionSchoolId);
                            MsgBox.Show("Tipo de trabajo de la escuela establecido", "Completado", MsgBox.Buttons.OK);
                            Administrativo.Home.HomeAdmin ha = new Home.HomeAdmin();
                            ha.Show();
                            this.Close();
                        }
                        else
                        {
                            MsgBox.Show("La suma de porcentajes tiene que dar 100%", "Error", MsgBox.Buttons.OK,MsgBox.Icon.Error);
                        }
                        
                    }
                    else
                    {
                        MsgBox.Show("No se puede introducir texto", "Atencion", MsgBox.Buttons.OK, MsgBox.Icon.Error);
                    }
                }
                else
                {
                    MsgBox.Show("Llene todos los espacios", "Atencion", MsgBox.Buttons.OK, MsgBox.Icon.Error);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio comuniquese con el soporte" + ex.Message);
            }
        }
    }
}
