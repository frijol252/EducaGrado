using EducaGrado.xDialog;
using Implementation;
using Model;
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

namespace EducaGrado.Administrativo.Controles.Invoice
{
    /// <summary>
    /// Lógica de interacción para PayerViewAdd.xaml
    /// </summary>
    public partial class PayerViewAdd : Window
    {
        string nit;
        PayerImpl payerImpl;
        Payer payer;
        public PayerViewAdd(string nit)
        {
            InitializeComponent();
            this.nit = nit;
            lblNIT.Content = nit;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnModifySchool_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                payer = new Payer(0, lblNIT.Content.ToString(), txtBuseness.Text);
                payerImpl = new PayerImpl();
                int res = payerImpl.Insert(payer);
                if (res > -1)
                {
                    MsgBox.Show("Contribuidor Insertado", "Completado", MsgBox.Buttons.OK, MsgBox.Icon.Info);
                    this.Close();

                }
            }
            catch(Exception ex)
            {
                MsgBox.Show("Comuniquese con el equipo de educa", "Error", MsgBox.Buttons.OK, MsgBox.Icon.Error);
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
