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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EducaGrado.Administrativo.Controles.Materias
{
    /// <summary>
    /// Lógica de interacción para MattersView.xaml
    /// </summary>
    public partial class MattersView : UserControl
    {
        CategoryMatterImpl categoryMatterImpl;
        CategoryMatter categoryMatter;
        MatterImpl matterImpl;
        Matter matter;
        int categoriaselected=-1;
        public MattersView()
        {
            InitializeComponent();
            
        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            LoadDataGrid();
            LoadDataGridMatter();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Ocultar();
        }
        private void Txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtsearch.Text == "")
                {
                    LoadDataGrid();
                    Ocultar();
                }
                else
                {
                    LoadDataGridlike(txtsearch.Text);
                    Ocultar();
                }
            }
            catch
            {

            }
        }
        private void txtsearch2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtsearch2.Text == "")
                {
                    LoadDataGridMatter();
                    Ocultar();
                }
                else
                {
                    LoadDataGridLikeMatter(txtsearch2.Text);
                    Ocultar();
                }
            }
            catch
            {

            }
        }

        #region Loads
        public void LoadDataGrid()
        {
            try
            {
                categoryMatterImpl = new CategoryMatterImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = categoryMatterImpl.Select().DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void LoadDataGridlike(string like)
        {
            try
            {
                categoryMatterImpl = new CategoryMatterImpl();
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = categoryMatterImpl.SelectLike(like).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void LoadDataGridMatter()
        {
            try
            {
                
                matterImpl = new MatterImpl();
                if(categoriaselected == -1)
                {
                    dgvDatos2.ItemsSource = null;
                    dgvDatos2.ItemsSource = matterImpl.Select().DefaultView;
                }
                else
                {
                    dgvDatos2.ItemsSource = null;
                    dgvDatos2.ItemsSource = matterImpl.SelectByCategory(categoriaselected).DefaultView;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void LoadDataGridLikeMatter(string like)
        {
            try
            {

                matterImpl = new MatterImpl();
                if (categoriaselected == -1)
                {
                    dgvDatos2.ItemsSource = null;
                    dgvDatos2.ItemsSource = matterImpl.SelectLike(like).DefaultView;
                }
                else
                {
                    dgvDatos2.ItemsSource = null;
                    dgvDatos2.ItemsSource = matterImpl.SelectLikeByCategory(categoriaselected,like).DefaultView;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void Ocultar()
        {
            dgvDatos.Columns[0].Visibility = Visibility.Hidden;
        }


        
        #endregion

        private void DgvDatos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

            if (dgvDatos.Items.Count > 0 && dgvDatos.SelectedItem != null)
            {
                try
                {

                    DataRowView dataRow = (DataRowView)dgvDatos.SelectedItem;
                    categoriaselected = int.Parse(dataRow.Row.ItemArray[0].ToString());
                    LoadDataGridMatter();
                    lblcategory.Content = dataRow.Row.ItemArray[1].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                
                string NombreCategoria = dataRowView[1].ToString();
                

                System.Windows.Forms.DialogResult result = MsgBox.Show("Estas Seguro de Eliminar " + NombreCategoria + "?", "Atencion", MsgBox.Buttons.YesNo, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    int IdCategory = int.Parse(dataRowView[0].ToString());
                    categoryMatter = new CategoryMatter();
                    categoryMatter.CategoryId = IdCategory;
                    categoryMatterImpl = new CategoryMatterImpl();
                    categoryMatterImpl.Delete(categoryMatter);
                    MsgBox.Show("Categoria Eliminada", "Completado",MsgBox.Buttons.OK);
                    LoadDataGrid();
                    categoriaselected = -1;
                    LoadDataGridMatter();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            List<CategoryMatter> categoryMatters = new List<CategoryMatter>();
            foreach (DataRowView row in dgvDatos.ItemsSource)
            {
                
                categoryMatters.Add(new CategoryMatter(int.Parse(row[0].ToString()), row[1].ToString()));
                
            }
            categoryMatterImpl = new CategoryMatterImpl();
            categoryMatterImpl.updateCategory(categoryMatters);
            MsgBox.Show("Categorias Actualizadas", "Completado", MsgBox.Buttons.OK);
            LoadDataGrid();
        }

        private void btnEdit2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                List<Matter> matters = new List<Matter>();
                foreach (DataRowView row in dgvDatos2.ItemsSource)
                {

                    matters.Add(new Matter(int.Parse(row[0].ToString()), row[1].ToString()));

                }
                matterImpl = new MatterImpl();
                matterImpl.updateMatters(matters);
                MsgBox.Show("Materias Actualizadas", "Completado", MsgBox.Buttons.OK);
                LoadDataGridMatter();
                
            }
            catch(Exception ex)
            {
                MsgBox.Show("Algo Salio Mal Comunicate con el soporte \n"+ex.Message, "Error", MsgBox.Buttons.OK);
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            categoryMatter = new CategoryMatter();
            categoryMatter.CategoryName = txtsearch.Text;
            categoryMatterImpl = new CategoryMatterImpl();
            int res = categoryMatterImpl.Insert(categoryMatter);
            if (res == 1)
            {
                MsgBox.Show("Categoria Insertada", "Completado", MsgBox.Buttons.OK);
                LoadDataGrid();
            }
        }
        private void btnAdd2_Click(object sender, RoutedEventArgs e)
        {
            if (categoriaselected != -1)
            {
                matter = new Matter();
                matter.MatterName = txtsearch2.Text;
                matter.CategoryId = categoriaselected;
                matterImpl = new MatterImpl();
                int res = matterImpl.Insert(matter);
                if (res == 1)
                {
                    MsgBox.Show("Materia Insertada", "Completado", MsgBox.Buttons.OK);
                    LoadDataGridMatter();
                }
            }
            else MsgBox.Show("Seleccione una categoria", "Atencion", MsgBox.Buttons.OK);
        }

        private void btnView_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;

                string NombreMateria = dataRowView[1].ToString();


                System.Windows.Forms.DialogResult result = MsgBox.Show("Estas Seguro de Eliminar " + NombreMateria + "?", "Atencion", MsgBox.Buttons.YesNo, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    int IdMatter = int.Parse(dataRowView[0].ToString());
                    matter = new Matter();
                    matter.MatterId= IdMatter;
                    matterImpl = new MatterImpl();
                    matterImpl.Delete(matter);
                    MsgBox.Show("Materia Eliminada", "Completado", MsgBox.Buttons.OK);
                    LoadDataGridMatter();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
