using Implementation;
using Model;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EducaGrado.InicioSesion
{
    /// <summary>
    /// Lógica de interacción para Index.xaml
    /// </summary>
    public partial class Index : Window
    {
        UserImpl implUser;
        UserAccount userAccount = new UserAccount();


        static byte state;
        static AesManaged aes = new AesManaged();


        public Index()
        {
            InitializeComponent();
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            ForgotPassword fp = new ForgotPassword();
            fp.Show();
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string user = txtUsername.Text.ToLower();
                string password = txtPassword.Password;
                userAccount = new UserAccount();
                if (!string.IsNullOrEmpty(user))
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        implUser = new UserImpl();
                        DataTable dt = implUser.GET(user);
                        if (dt.Rows.Count > 0)
                        {
                            userAccount.Password = Encoding.Default.GetBytes(dt.Rows[0][2].ToString());
                            userAccount.Key = Encoding.Default.GetBytes(dt.Rows[0][3].ToString());
                            userAccount.VI = Encoding.Default.GetBytes(dt.Rows[0][4].ToString());
                            state = Convert.ToByte(dt.Rows[0][5].ToString());
                            if (state == 1)
                            {

                                if (Encriptar(password, userAccount).SequenceEqual(userAccount.Password))
                                {
                                    switch (Validar(dt))
                                    {
                                        case 1:
                                            PasswordLog pl = new PasswordLog(int.Parse(dt.Rows[0][0].ToString()));
                                            pl.Show();
                                            this.Close();
                                            break;
                                        case 2:
                                            SetSession(dt);
                                            if (Session.SessionRole == 1)
                                            {
                                                Administrativo.School.ModifySchool MS = new Administrativo.School.ModifySchool();
                                                MS.Show();
                                                this.Close();
                                            }
                                            else MessageBox.Show("La unidad educativa cuenta con problemas de inicio");
                                            break;
                                        case 3:
                                            SetSession(dt);
                                            switch (Session.SessionRole)
                                            {
                                                case 1:
                                                    Administrativo.Home.HomeAdmin MS = new Administrativo.Home.HomeAdmin();
                                                    MS.Show();
                                                    this.Close();
                                                    break;
                                                case 2:
                                                    break;
                                                case 3:
                                                    break;
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }

                                else MessageBox.Show("Usuario o contraseña erroneos");
                            }
                            else MessageBox.Show("Su usuario esta bloqueado  comuniquese con su organizacion");
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña erroneos");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Llena todos los espacion");
                    }
                }
                else
                {
                    MessageBox.Show("Llena todos los espacion");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio Comunicate con educa" + ex.Message);
            }
        }

        public int Validar(DataTable dt)
        {
            if (dt.Rows[0][6].ToString() != "1")
            {
                return 1;
            }
            if (string.IsNullOrEmpty(dt.Rows[0][11].ToString()) || string.IsNullOrEmpty(dt.Rows[0][12].ToString()) || string.IsNullOrEmpty(dt.Rows[0][13].ToString()) ||
                string.IsNullOrEmpty(dt.Rows[0][14].ToString()) || string.IsNullOrEmpty(dt.Rows[0][15].ToString()))
            {
                return 2;
            }
            return 3;
        }

        public void SetSession(DataTable dt)
        {
            Session.SessionID = int.Parse(dt.Rows[0][0].ToString());
            Session.SessionRole = int.Parse(dt.Rows[0][16].ToString());
            Session.SessionCurrent = dt.Rows[0][7].ToString();
            Session.SessionPersonId = int.Parse(dt.Rows[0][8].ToString());
            Session.SessionSchoolId = int.Parse(dt.Rows[0][9].ToString());
            Session.SessionSchoolName = dt.Rows[0][10].ToString();
        }



        #region Encriptar
        static byte[] Encriptar(string texto, UserAccount userAccount)
        {
            byte[] encriptando;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform encriptador = aes.CreateEncryptor(userAccount.Key, userAccount.VI);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encriptador, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(texto);
                        encriptando = ms.ToArray();
                    }
                }
            }
            return encriptando;
        }

        #endregion

    }


}
