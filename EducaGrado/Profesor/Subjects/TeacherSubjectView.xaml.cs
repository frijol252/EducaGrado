using Implementation;
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

namespace EducaGrado.Profesor.Subjects
{
    /// <summary>
    /// Lógica de interacción para TeacherSubjectView.xaml
    /// </summary>
    public partial class TeacherSubjectView : UserControl
    {
        public TeacherSubjectView()
        {
            InitializeComponent();
        }

        ModalityImpl modalityImpl;
        MatterImpl matterImpl;
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            crearData();
            loadGrid();
            
        }
        public void loadGrid()
        {
            try
            {
                matterImpl = new MatterImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = matterImpl.SelectTeacher().DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        
       
        private void ClickSection_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                int IdCategory = int.Parse(dataRowView[0].ToString());
                StudenGrade studenGrade = new StudenGrade(dgvDatos.CurrentColumn.DisplayIndex-2,IdCategory);
                studenGrade.Show();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private DataGridTemplateColumn CreateTextBoxColumn(string header,string texto)
        {
            var col = new DataGridTemplateColumn();
            col.Header = header;
            var template = new DataTemplate();
            var textBlockFactory = new FrameworkElementFactory(typeof(Button));
            textBlockFactory.Name = "btnSection";
            textBlockFactory.SetValue(Button.ContentProperty, texto);
            textBlockFactory.SetValue(Button.WidthProperty, 100.0);
            textBlockFactory.SetValue(Button.BackgroundProperty, new SolidColorBrush(Colors.LightSalmon));
            textBlockFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(ClickSection_Click));
            template.VisualTree = textBlockFactory;

            col.CellTemplate = template;

            return col;
        }

        public void crearData()
        {
            modalityImpl = new ModalityImpl();
            DataTable modality = new DataTable();
            int type = 0;
            string typeq = "";
            modality = modalityImpl.Select();
            foreach (DataRow d in modality.Rows)
            {
                type = int.Parse(d[2].ToString());
                typeq = d[3].ToString();
            }
            for (int i = 1; i <= type; i++)
            {
                dgvDatos.Columns.Add(CreateTextBoxColumn((typeq+" "+i), (typeq + " " + i)));
            }

        }

    }
}
