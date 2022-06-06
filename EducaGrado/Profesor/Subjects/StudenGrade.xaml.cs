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
using System.Windows.Shapes;

namespace EducaGrado.Profesor.Subjects
{
    /// <summary>
    /// Lógica de interacción para StudenGrade.xaml
    /// </summary>
    public partial class StudenGrade : Window
    {
        public StudenGrade()
        {
            InitializeComponent();
        }

        private void btnAddGrade_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        /*
         
                int i = 5; //Set this equal to desired column index.... 
                ContentPresenter myCp = dgvDatos.Columns[i].GetCellContent(dgvDatos.Items[0]) as ContentPresenter;
                var myTemplate = myCp.ContentTemplate;
                TextBox mytxtbox = myTemplate.FindName("feesAmountTextBox", myCp) as TextBox;
                mytxtbox.Name = "a";
                MessageBox.Show(mytxtbox.Text);
         */
    }
}
