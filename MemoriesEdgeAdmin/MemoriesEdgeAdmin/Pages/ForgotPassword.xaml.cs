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
    public partial class ForgotPassword : Page
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (this.NavigationContext.QueryString.ContainsKey("UserName"))
            //{
            //    lblUserName.Content = this.NavigationContext.QueryString["UserName"];
            //}
        }

        private void btnRecover_Click(object sender, RoutedEventArgs e)
        {
            AdminService.IwcfAdminClient objs = new AdminService.IwcfAdminClient();
            objs.GetPasswordCompleted += new EventHandler<AdminService.GetPasswordCompletedEventArgs>(objs_GetPasswordCompleted);
            objs.GetPasswordAsync(txtUserName.Text);
        }

        void objs_GetPasswordCompleted(object sender, AdminService.GetPasswordCompletedEventArgs e)
        {
            AdminService.ForgotPassword obje = new AdminService.ForgotPassword();
            obje = e.Result;
            if (e.Result != null)
            {
                lblYourPassword.Content = obje.Password;
            }
            else
            {
                MessageBox.Show("Invalid UserName");
            }
        }

        

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AdminService.IwcfAdminClient obj = new AdminService.IwcfAdminClient();
            obj.GetSecurityQuestionsCompleted += new EventHandler<AdminService.GetSecurityQuestionsCompletedEventArgs>(obj_GetSecurityQuestionsCompleted);
            obj.GetSecurityQuestionsAsync();
        
        }

        void obj_GetSecurityQuestionsCompleted(object sender, AdminService.GetSecurityQuestionsCompletedEventArgs e)
        {

            IList obj = e.Result as IList;
            ddlSecurityQuestion.ItemsSource = obj;
            ddlSecurityQuestion.DisplayMemberPath = "SecurityQuestion";
            ddlSecurityQuestion.SelectedValuePath = "SecurityQuestionId";
            ddlSecurityQuestion.SelectedIndex = 0;
        }

    }
}
