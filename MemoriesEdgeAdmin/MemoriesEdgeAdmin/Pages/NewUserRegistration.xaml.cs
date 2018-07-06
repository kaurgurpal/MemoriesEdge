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
    public partial class NewUserRegistration : Page
    {
        

        public void BindddlSecurityQuestion()
        {
            AdminService.IwcfAdminClient objs = new AdminService.IwcfAdminClient();
            objs.GetSecurityQuestionsCompleted += new EventHandler<AdminService.GetSecurityQuestionsCompletedEventArgs>(objs_GetSecurityQuestionsCompleted);
            objs.GetSecurityQuestionsAsync();
        }

        void objs_GetSecurityQuestionsCompleted(object sender, AdminService.GetSecurityQuestionsCompletedEventArgs e)
        {
            IList obj = e.Result as IList;
            ddlSecurityQuestion.ItemsSource = obj;
            ddlSecurityQuestion.DisplayMemberPath = "SecurityQuestion";
            ddlSecurityQuestion.SelectedValuePath = "SecurityQuestionId";
            ddlSecurityQuestion.SelectedIndex = 0;
        }

        public void BindddlCountry()
        {
            AdminService.IwcfAdminClient objC = new AdminService.IwcfAdminClient();
            objC.GetCountryCompleted += new EventHandler<AdminService.GetCountryCompletedEventArgs>(objC_GetCountryCompleted);
            objC.GetCountryAsync();
        }

        void objC_GetCountryCompleted(object sender, AdminService.GetCountryCompletedEventArgs e)
        {
            IList obj = e.Result as IList;
            ddlCountry.ItemsSource  = obj;
            ddlCountry.DisplayMemberPath = "Country";
            ddlCountry.SelectedValuePath = "CountryId";
            ddlCountry.SelectedIndex = 0;
        }

        public void BindddlCity(int s)
        {
           // int s = Convert.ToInt32(ddlState.SelectedValue);
            AdminService.IwcfAdminClient objCt = new AdminService.IwcfAdminClient();
            objCt.GetCityCompleted += new EventHandler<AdminService.GetCityCompletedEventArgs>(objCt_GetCityCompleted);
           objCt.GetCityAsync(s);
        }

        void objCt_GetCityCompleted(object sender, AdminService.GetCityCompletedEventArgs e)
        {
            IList obj = e.Result as IList;
            ddlCity.ItemsSource = obj;
            ddlCity.DisplayMemberPath = "City";
            ddlCity.SelectedValuePath = "CityId";
            ddlCity.SelectedIndex = 0;
        }

        public void BindddlState(int s)
        {
          // int s = Convert.ToInt32(ddlCountry.SelectedValue);
            AdminService.IwcfAdminClient objS = new AdminService.IwcfAdminClient();
            objS.GetStateCompleted += new EventHandler<AdminService.GetStateCompletedEventArgs>(objS_GetStateCompleted);
           objS.GetStateAsync(s);
        }

        void objS_GetStateCompleted(object sender, AdminService.GetStateCompletedEventArgs e)
        {
            IList obje = e.Result as IList;
            ddlState.ItemsSource = obje;
            ddlState.DisplayMemberPath = "State";
            ddlState.SelectedValuePath = "StateId";
            ddlState.SelectedIndex = 0;
        }

        public NewUserRegistration()
        {
            InitializeComponent();
            clsNewUserValidation obj = new clsNewUserValidation();
            LayoutRoot.DataContext = obj;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }



        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(txtUserName) || Validation.GetHasError(txtPassword) || Validation.GetHasError(txtConfirmPassword) || Validation.GetHasError(txtSecurityAnswer) || Validation.GetHasError(txtName)|| Validation.GetHasError(dtDOB) || Validation.GetHasError(txtPhoneNumber) || Validation.GetHasError(txtMobileNumber) || Validation.GetHasError(txtEmailID) || Validation.GetHasError(txtAddress) || Validation.GetHasError(txtPinCode))
            {
                MessageBox.Show("Enter Valid Data");
            }
            else
            {
                AdminService.NewRegisteredUser obj1 = new AdminService.NewRegisteredUser();
                obj1.UserName = txtUserName.Text;
                obj1.Password = txtPassword.Password;
                obj1.ConfirmPassword = txtConfirmPassword.Password;
                obj1.SecurityQuestionId = Convert.ToInt32(ddlSecurityQuestion.SelectedValue);
                obj1.SecurityAnswer = txtSecurityAnswer.Text;
                obj1.Name = txtName.Text;
                obj1.Initials = (ddlInitials.SelectedItem as ComboBoxItem).Content.ToString();
                obj1.DateOfBirth = Convert.ToDateTime(dtDOB.SelectedDate);
                obj1.Gender = rbtnMale.IsChecked == true ? "Male" : "Female";
                obj1.Phone = txtPhoneNumber.Text;
                obj1.Mobile = txtMobileNumber.Text;
                obj1.EmailId = txtEmailID.Text;
                obj1.Address = txtAddress.Text;
                obj1.PinCode = txtPinCode.Text;
                obj1.Country = ((MemoriesEdgeAdmin.AdminService.CountryStateCity)(ddlCountry.SelectedItem)).Country.ToString();
                obj1.State = ((MemoriesEdgeAdmin.AdminService.CountryStateCity)(ddlState.SelectedItem)).State.ToString();
                obj1.City = ((MemoriesEdgeAdmin.AdminService.CountryStateCity)(ddlCity.SelectedItem)).City.ToString();
                AdminService.IwcfAdminClient objss = new AdminService.IwcfAdminClient();
                objss.InsertNewUserCompleted += new EventHandler<AdminService.InsertNewUserCompletedEventArgs>(objss_InsertNewUserCompleted);
                objss.InsertNewUserAsync(obj1);
            }
        }

        void objss_InsertNewUserCompleted(object sender, AdminService.InsertNewUserCompletedEventArgs e)
        {
            if (e.Result == 1)
            {
                MessageBox.Show("Record Inserted Successfully");
            }
            else
            {
                MessageBox.Show("Record Insertion Failed");
            }
        }

      

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BindddlSecurityQuestion();
            BindddlCountry();

        }

        private void ddlCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindddlState(Convert.ToInt32(ddlCountry.SelectedValue));
        }

        private void ddlState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindddlCity(Convert.ToInt32(ddlState.SelectedValue));
        }

        private void hbtnCheckAvailability_Click(object sender, RoutedEventArgs e)
        {
            string UserName = txtUserName.Text;
            AdminService.IwcfAdminClient objs = new AdminService.IwcfAdminClient();
            objs.CheckAvailabilityCompleted += new EventHandler<AdminService.CheckAvailabilityCompletedEventArgs>(objs_CheckAvailabilityCompleted);
            objs.CheckAvailabilityAsync(UserName);
        }

        void objs_CheckAvailabilityCompleted(object sender, AdminService.CheckAvailabilityCompletedEventArgs e)
        {
            if (e.Result == 0)
            {
                MessageBox.Show("User Already Exists");
            }
        }

       

    }
}
