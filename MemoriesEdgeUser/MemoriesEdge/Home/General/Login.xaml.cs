using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace MemoriesEdge.Home.General
{
    public partial class Login : Page
    {
        public static int UserId;
        public static string UserName;
        public static string UserType;
        public Login()
        {
            InitializeComponent();
        }

        //public static string UserType { get; set; }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(txtUserName) || Validation.GetHasError(txtPassword))
            {
                MessageBox.Show("Enter Valid User Name/Password");
            }
            else
            {
                AdminService.IwcfAdminClient objService = new AdminService.IwcfAdminClient();
                objService.LoginUserCompleted += new EventHandler<AdminService.LoginUserCompletedEventArgs>(objService_LoginUserCompleted);
                objService.LoginUserAsync(txtUserName.Text,txtPassword.Password);
            }
        }

        void objService_LoginUserCompleted(object sender, AdminService.LoginUserCompletedEventArgs e)
        {
            AdminService.UserLoginRegisterUser  obj = new AdminService.UserLoginRegisterUser();
            obj = e.Result;
            if (obj != null)
            {

                UserId = obj.UserId;
                UserName = obj.UserName;
                UserType = obj.UserType;
                //MessageBox.Show(obj.Initials + obj.Name + " " + obj.UserStatus + " " + obj.UserType + " " + obj.UserId);
                switch (obj.UserType)
                {
                    case "Administrator":
                        this.NavigationService.Navigate(new Uri("/Home/Administrator/Home", UriKind.RelativeOrAbsolute));
                        break;
                    case "User":
                        this.NavigationService.Navigate(new Uri("/Home/User/Home", UriKind.RelativeOrAbsolute));
                        break;
                    case "Operator":
                        this.NavigationService.Navigate(new Uri("/Home/Operator/Home", UriKind.RelativeOrAbsolute));
                        break;

                }
            }
            else
            {
                MessageBox.Show("Invalid UserName or Password");
            }
        }

        private void hbtnForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Home/User/ForgotPassword", UriKind.RelativeOrAbsolute)); 
        }

        private void hbtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Home/General/NewUserRegistration", UriKind.RelativeOrAbsolute)); 
        }

    }
}