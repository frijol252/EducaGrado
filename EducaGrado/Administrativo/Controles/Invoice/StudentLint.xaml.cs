using Implementation;
using Model;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EducaGrado.Administrativo.Controles.Invoice
{
    /// <summary>
    /// Lógica de interacción para StudentLint.xaml
    /// </summary>
    public partial class StudentLint : UserControl
    {
        public StudentLint()
        {
            InitializeComponent();
        }
        FeeImpl feeImpl;
        DosageImpl dosageImpl;
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        public void enableGrid()
        {
            dgvDatos.IsEnabled = true;
        }
        private void txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadDataGrid(txtsearch.Text);
        }
        public void LoadDataGrid()
        {
            try
            {
                feeImpl = new FeeImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = feeImpl.Select().DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void LoadDataGrid(string like)
        {
            try
            {
                feeImpl = new FeeImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = feeImpl.SelectLike(like).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
            if (revisionDosage()) enableGrid();
        }
        private bool revisionDosage()
        {
            Dosage dosage = new Dosage();
            dosageImpl = new DosageImpl();
            dosage = dosageImpl.GET();
            if (!string.IsNullOrEmpty(dosage.DosageKey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;

                int id = int.Parse(dataRowView[0].ToString());
                string name = dataRowView[1].ToString();
                PaymentView paymentView = new PaymentView(id,name);
                paymentView.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnDos_Click(object sender, RoutedEventArgs e)
        {
            DosageView dosageView = new DosageView(this);
            dosageView.Show();

        }
    }
}
