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
using System.Collections;

namespace MemoriesEdgeAdmin.Pages
{
    public partial class UserLoginForRegisteredUser : Page
    {
        public static int intUserId ;
        public static string strUserName, strUserType; 
        public UserLoginForRegisteredUser()
        {
            InitializeComponent();
     MemoriesEdge.clsUserLoginForRegisteredUserValidation obja = new MemoriesEdge.clsUserLoginForRegisteredUserValidation();
            UserLogin.DataContext = obja;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(txtUserName) || Validation.GetHasError(txtPassword))
            {
                MessageBox.Show("Invalid UserName/Password");
            }
            string a = txtPassword.Password;
            string b = txtUserName.Text;
           MemoriesEdge.AdminService.IwcfAdminClient obj = new MemoriesEdge.AdminService.IwcfAdminClient();
            obj.LoginUserCompleted += new EventHandler<MemoriesEdge.AdminService.LoginUserCompletedEventArgs>(obj_LoginUserCompleted);
            obj.LoginUserAsync(b,a);
        
        }

        void obj_LoginUserCompleted(object sender, MemoriesEdge.AdminService.LoginUserCompletedEventArgs e)
        {
           MemoriesEdge.AdminService.UserLoginRegisterUser objs = new MemoriesEdge.AdminService.UserLoginRegisterUser ();
            objs = e.Result;
            intUserId = objs.UserId;
            strUserName = objs.UserName;
            strUserType = objs.UserType;
            if (objs != null)
            {

                MessageBox.Show(objs.Initials + "" + objs.UserName + "" + objs.UserType + "" + objs.UserStatus);
                switch (objs.UserType)
                {
                    case "Administrator":
                        MessageBox.Show("Successfully Logged in as Admin");
                        break;
                    case "Operator":
                        MessageBox.Show("Successfully Logged in as Operator");
                        break;
                    case "RegisteredUser":
                        MessageBox.Show("Successfully Logged in as User");
                        break;
                }
                Uri obj1 = new Uri("/Pages/ChangePassword.xaml?UserName="+txtUserName.Text, UriKind.Relative);
                this.NavigationService.Navigate(obj1);
            }
            else
            {
                MessageBox.Show("Invalid Id");
            }
        }

        private void hbtnForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            //string UserName = txtUserName.Text;
            //Uri obj = new Uri("/Pages/ForgotPassword.xaml?UserName=" + UserName, UriKind.Relative);
            //this.NavigationService.Navigate(obj);
            Uri obj = new Uri("/Pages/ForgotPassword.xaml", UriKind.Relative);
            this.NavigationService.Navigate(obj);
            
        }
    }
}
