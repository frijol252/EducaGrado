using EducaGrado.xDialog;
using Implementation;
using Model;
using System;
using System.Windows;
using System.Windows.Input;

namespace EducaGrado.Administrativo.Controles.Invoice
{
    /// <summary>
    /// Lógica de interacción para DosageView.xaml
    /// </summary>
    public partial class DosageView : Window
    {
        StudentLint studentLint;
        public DosageView(StudentLint studentLint)
        {
            InitializeComponent();
            this.studentLint = studentLint;
        }
        DosageImpl dosageImpl;
        Dosage dosage;
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddGrade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (validar())
                {
                    dosageImpl = new DosageImpl();
                    dosage = new Dosage();
                    dosage.DeadLine = DateTime.Parse(txtDeadLine.Text);
                    dosage.DosageKey = txtTest.Text;
                    dosage.NroAutorization = txtNroAuthorizacion.Text;
                    dosageImpl.InsertTransaction(dosage);
                    MsgBox.Show("Cambio de ''Llave de Dosificacion'' Completa", "Completado", MsgBox.Buttons.OK, MsgBox.Icon.Info);
                    studentLint.enableGrid();
                    this.Close();
                }
                else
                {
                    MsgBox.Show("Llene todos los campos", "Atencion", MsgBox.Buttons.OK, MsgBox.Icon.Error);
                }
            }
            catch(Exception ex)
            {
                MsgBox.Show(""+ex.Message, "Atencion", MsgBox.Buttons.OK, MsgBox.Icon.Error);
            }
        }
        private bool validar()
        {
            bool ret = false;
            if (!string.IsNullOrEmpty(txtNroAuthorizacion.Text))
            {
                if (!string.IsNullOrEmpty(txtTest.Text))
                {
                    if (!string.IsNullOrEmpty(txtDeadLine.Text))
                    {
                        ret= true;
                    }
                }
            }
            return ret;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dosageImpl = new DosageImpl();
            dosage = dosageImpl.GET();
            lblAuthotization.Content = dosage.NroAutorization;
            lbldosageKey.Content = dosage.DosageKey;
            lbldeadline.Content = dosage.DeadLine.ToString();
        }
    }
}
