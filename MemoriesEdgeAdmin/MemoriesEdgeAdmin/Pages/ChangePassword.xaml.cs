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
    public partial class ChangePassword : Page
    {
        int UserId = MemoriesEdgeAdmin.Pages.UserLoginForRegisteredUser.intUserId;
        
        public ChangePassword()
        {
            InitializeComponent();
            clsChangePasswordValidation obj = new clsChangePasswordValidation();
            LayoutRoot.DataContext = obj;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (this.NavigationContext.QueryString.ContainsKey("UserName"))
            {
                lblUserName.Content = this.NavigationContext.QueryString["UserName"];
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(txtOldPassword) || Validation.GetHasError(txtNewPassword) || Validation.GetHasError(txtConfirmPassword))
            {
                MessageBox.Show("Enter Valid Data");
            }
            else
            {

                int UserId = MemoriesEdgeAdmin.Pages.UserLoginForRegisteredUser.intUserId;                
                    AdminService.IwcfAdminClient objS = new AdminService.IwcfAdminClient();
                    objS.GetUserPasswordCompleted += new EventHandler<AdminService.GetUserPasswordCompletedEventArgs>(objS_GetUserPasswordCompleted);
                    objS.GetUserPasswordAsync(UserId);
                
                   
            }
        }

        void objS_GetUserPasswordCompleted(object sender, AdminService.GetUserPasswordCompletedEventArgs e)
        {
            string p = e.Result;
            if (p == txtOldPassword.Password)
            {
                AdminService.IwcfAdminClient objp = new AdminService.IwcfAdminClient();
                objp.UpdatePasswordCompleted += new EventHandler<AdminService.UpdatePasswordCompletedEventArgs>(objp_UpdatePasswordCompleted);
                objp.UpdatePasswordAsync(UserId,txtNewPassword.Password);
            }
        }

        void objp_UpdatePasswordCompleted(object sender, AdminService.UpdatePasswordCompletedEventArgs e)
        {
            if (e.Result == 1)
            {
                MessageBox.Show("Password Updated Successfully");
            }
            else
            {
                MessageBox.Show("Invalid Password");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
