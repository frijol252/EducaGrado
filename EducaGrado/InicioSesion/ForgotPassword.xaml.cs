using EducaGrado.xDialog;
using Implementation;
using System;
using System.Windows;
using System.Windows.Input;

namespace EducaGrado.InicioSesion
{
    /// <summary>
    /// Lógica de interacción para ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }
        int code;
        UserImpl userImpl;
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            Index ind = new Index();
            ind.Show();
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                userImpl = new UserImpl();
                code = userImpl.SendEmail(txtUsername.Text);
                MsgBox.Show("Mail enviado, revise su correo","Atencion",MsgBox.Buttons.OK,MsgBox.Icon.Info);
            }
            catch (Exception ex)
            {

            }
        }

        private void btnChangePass_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCode.Text == code.ToString())
                {
                    if (txtPassword.Password.Length >= 5)
                    {
                        if (txtPassword.Password == txtPasswordCorrect.Password)
                        {
                            
                            userImpl = new UserImpl();
                            int res = userImpl.UpdateForgot(txtUsername.Text,txtPassword.Password);

                            if (res > 0)
                            {
                                
                                InicioSesion.Index log = new Index();
                                MsgBox.Show("Contraseña Actualizada", "Completado", MsgBox.Buttons.OK, MsgBox.Icon.Info);
                                log.Show();
                                this.Close();
                            }
                            else MsgBox.Show("Algo salio mal \n comunicate con el departamento de soporte \n educateam.suport@gmail.com", "Error", MsgBox.Buttons.OK, MsgBox.Icon.Error);
                        }
                        else
                        {
                            MsgBox.Show("Las contraseñas no coinciden", "Error", MsgBox.Buttons.OK, MsgBox.Icon.Error);
                        }
                    }
                    else
                    {
                        MsgBox.Show("La contraseña no puede ser tan corta", "Error", MsgBox.Buttons.OK, MsgBox.Icon.Error);
                    }
                }
                else
                {
                    MsgBox.Show("Codigo Incorrecto", "Error", MsgBox.Buttons.OK, MsgBox.Icon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MsgBox.Show("Algo salio mal \n comunicate con el departamento de soporte \n educateam.suport@gmail.com "+ ex.Message, "Error", MsgBox.Buttons.OK, MsgBox.Icon.Error);
                
            }
        }
        string random;
        private void Window_Initialized(object sender, System.EventArgs e)
        {
            
        }
    }
}
