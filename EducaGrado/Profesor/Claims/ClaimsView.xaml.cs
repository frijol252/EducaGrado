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
using EducaGrado.xDialog;
using Implementation;
namespace EducaGrado.Profesor.Claims
{
    /// <summary>
    /// Lógica de interacción para ClaimsView.xaml
    /// </summary>
    public partial class ClaimsView : UserControl
    {
        public ClaimsView()
        {
            InitializeComponent();
        }
        UserImpl userImpl;
        private void btnAddSend_Click(object sender, RoutedEventArgs e)
        {
            userImpl=new UserImpl();
            userImpl.SendEmailClaim(txtMessage.Text);
            MsgBox.Show("Gracias por ayudar a EducaTeam a mejorar tu experiencia", "Eres lo Maximo", MsgBox.Buttons.OK);
        }
    }
}
