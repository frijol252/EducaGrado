﻿using Implementation;
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

namespace EducaGrado.InicioSesion
{
    /// <summary>
    /// Lógica de interacción para PasswordLog.xaml
    /// </summary>
    public partial class PasswordLog : Window
    {
        public PasswordLog()
        {
            InitializeComponent();
        }
        string random;
        UserAccount userito;
        UserImpl userImpl;
        private void Window_Initialized(object sender, EventArgs e)
        {

            //Random rdm = new Random();
            //random = rdm.Next(1000, 10000).ToString();
            //name.Text = Session.SessionCurrent;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (txtcode.Text == random)
            //    {
            //        if (txtpass.Password.Length >= 5)
            //        {
            //            if (txtpass.Password == txtconfirm.Password)
            //            {
            //                userito = new User(txtpass.Password,Session.SessionID);
            //                userImpl = new UserImpl();
            //                int res = userImpl.Updatepass(userito);
            //                if (res > 0)
            //                {

            //                    MessageBox.Show("Password Modifed successfully!!!");
            //                    Educa.MainWindow log = new MainWindow();
            //                    log.Show();
            //                    System.Diagnostics.Debug.WriteLine(string.Format("{0} | Change Password: ({1}).", DateTime.Now, Session.SessionEmail));
            //                    this.Close();
            //                }
            //                else MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com");
            //            }
            //            else
            //            {
            //                MessageBox.Show("Passwords do not match");
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("The password cannot be less than 5 characters");
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Code incorrect");
            //    }
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}
