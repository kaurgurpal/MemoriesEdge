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

namespace MemoriesEdge.Home.Administrator
{
    public partial class AddEditOperator : Page
    {
        public static int id;
        public static int CountryId, StateId, CityId;
        static AdminService.AddEditOperator obje;
        public AddEditOperator()
        {
            InitializeComponent();
            clsAddEditOperatorValidation obj1 = new clsAddEditOperatorValidation();
            AddEditGrid.DataContext = obj1;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (this.NavigationContext.QueryString.ContainsKey("uid"))
            {
                id = Convert.ToInt32(this.NavigationContext.QueryString["uid"]);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BindAllCountries();

            if (id != 0)
            {
                obje = new AdminService.AddEditOperator();
                obje.UserId = id;
                AdminService.IwcfAdminClient obj = new AdminService.IwcfAdminClient();
                obj.GetDetailCompleted += new EventHandler<AdminService.GetDetailCompletedEventArgs>(obj_GetDetailCompleted);
                obj.GetDetailAsync(obje);
            }
            else
            {

            }
        }


        public void BindAllCountries()
        {
            AdminService.IwcfAdminClient obj = new AdminService.IwcfAdminClient();
            obj.GetAllCountryCompleted += new EventHandler<AdminService.GetAllCountryCompletedEventArgs>(obj_GetAllCountryCompleted);
            obj.GetAllCountryAsync();
        }

        void obj_GetAllCountryCompleted(object sender, AdminService.GetAllCountryCompletedEventArgs e)
        {
            IList objd = e.Result as IList;
            ddlCountry.ItemsSource = objd;
            ddlCountry.DisplayMemberPath = "Country";
            ddlCountry.SelectedValuePath = "CountryId";
            ddlCountry.SelectedIndex = 0;

        }


        public void BindAllCity(int s)
        {
            AdminService.IwcfAdminClient obje = new AdminService.IwcfAdminClient();
            obje.GetAllCityCompleted += new EventHandler<AdminService.GetAllCityCompletedEventArgs>(obje_GetAllCityCompleted);
            obje.GetAllCityAsync(s);
        }

        void obje_GetAllCityCompleted(object sender, AdminService.GetAllCityCompletedEventArgs e)
        {
            IList obj = e.Result as IList;
            ddlCity.ItemsSource = obj;
            ddlCity.DisplayMemberPath = "City";
            ddlCity.SelectedValuePath = "CityId";
            ddlCity.SelectedIndex = 0;
        }

        public void BindAllStates(int a)
        {
            AdminService.IwcfAdminClient objs = new AdminService.IwcfAdminClient();
            objs.GetAllStateCompleted += new EventHandler<AdminService.GetAllStateCompletedEventArgs>(objs_GetAllStateCompleted);
            objs.GetAllStateAsync(a);
        }

        void objs_GetAllStateCompleted(object sender, AdminService.GetAllStateCompletedEventArgs e)
        {
            IList obje = e.Result as IList;
            ddlState.ItemsSource = obje;
            ddlState.DisplayMemberPath = "State";
            ddlState.SelectedValuePath = "StateId";
            ddlState.SelectedIndex = 0;
        }

        void obj_GetDetailCompleted(object sender, AdminService.GetDetailCompletedEventArgs e)
        {
            obje = e.Result as MemoriesEdge.AdminService.AddEditOperator;
            txtUserName.Text = obje.UserName;
            txtPassword.Password = obje.Password;
            txtConfirmPassword.Password = obje.Password;
            ddlInitials.SelectedValue = obje.Initials;
            txtName.Text = obje.Name;
            dtDOB.Text = (obje.DateOfBirth).ToString();
            if (obje.Gender == "Male")
            {
                rbtnMale.IsChecked = true;
                rbtnFemale.IsChecked = false;
            }
            else
            {
                rbtnMale.IsChecked = false;
                rbtnFemale.IsChecked = true;
            }
            txtPhone.Text = obje.Phone;
            txtMobile.Text = obje.Mobile;
            txtEmailId.Text = obje.EmailId;
            txtPinCode.Text = obje.PinCode;
            CountryId = obje.CountryId;
            StateId = obje.StateId;
            CityId = obje.CityId;
            //BindAllStates(CountryId);
            //BindAllCity(StateId); 
            ((MemoriesEdge.AdminService.CountryStateCity)(ddlCountry.SelectedItem)).Country = obje.Country;

            ddlCountry_SelectionChanged(null, null);
            obj_GetAllCountryCompleted(null, null);
            // // objs_GetAllStateCompleted(null, null);
            //  //(ddlCountry.SelectedItem as ComboBoxItem).Content = obje.Country;
            //  //(ddlState.SelectedItem as ComboBoxItem).Content = obje.State;
            //  //(ddlCity.SelectedItem as ComboBoxItem).Content = obje.City;
            ////' ddlCity.SelectedValue = obje.City;
            // // ddlState.SelectedValue = obje.State;
            txtAddress.Text = obje.Address;
            txtPinCode.Text = obje.PinCode;

        } 

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(txtUserName) || Validation.GetHasError(txtPassword) || Validation.GetHasError(txtPhone) || Validation.GetHasError(txtMobile) || Validation.GetHasError(txtEmailId) || Validation.GetHasError(txtAddress) || Validation.GetHasError(txtPinCode))
            {
                MessageBox.Show("Enter Valid Data");
            }

            if (id == 0)
            {
                AdminService.AddEditOperator obj1 = new AdminService.AddEditOperator();
                obj1.UserName = txtUserName.Text;
                obj1.Password = txtPassword.Password;
                obj1.ConfirmPassword = txtConfirmPassword.Password;
                obj1.Name = txtName.Text;
                obj1.Initials = (ddlInitials.SelectedItem as ComboBoxItem).Content.ToString();
                obj1.DateOfBirth = Convert.ToDateTime(dtDOB.SelectedDate);
                obj1.Gender = rbtnMale.IsChecked == true ? "Male" : "Female";
                obj1.Phone = txtPhone.Text;
                obj1.Mobile = txtMobile.Text;
                obj1.EmailId = txtEmailId.Text;
                obj1.Address = txtAddress.Text;
                obj1.PinCode = txtPinCode.Text;
                obj1.Country = ((MemoriesEdge.AdminService.CountryStateCity)(ddlCountry.SelectedItem)).Country.ToString();
                obj1.State = ((MemoriesEdge.AdminService.CountryStateCity)(ddlState.SelectedItem)).State.ToString();
                obj1.City = ((MemoriesEdge.AdminService.CountryStateCity)(ddlCity.SelectedItem)).City.ToString();
                AdminService.IwcfAdminClient objss = new AdminService.IwcfAdminClient();
                objss.AddNewUserCompleted += new EventHandler<AdminService.AddNewUserCompletedEventArgs>(objss_AddNewUserCompleted);
                objss.AddNewUserAsync(obj1);
            }
            else
            {

            }


        }

        void objss_AddNewUserCompleted(object sender, AdminService.AddNewUserCompletedEventArgs e)
        {
            if (e.Result == 1)
            {
                MessageBox.Show("Record Insertion Successfully.");
            }
            else
            {
                MessageBox.Show("Record Updated Successfully.");
            }
        }

        private void hbtnCheckAvailability_Click(object sender, RoutedEventArgs e)
        {
            string UserName = txtUserName.Text;
            AdminService.IwcfAdminClient objx = new AdminService.IwcfAdminClient();
            objx.CheckUserAvailabilityCompleted += new EventHandler<AdminService.CheckUserAvailabilityCompletedEventArgs>(objx_CheckUserAvailabilityCompleted);
            objx.CheckUserAvailabilityAsync(UserName);

        }

        void objx_CheckUserAvailabilityCompleted(object sender, AdminService.CheckUserAvailabilityCompletedEventArgs e)
        {
            if (e.Result == 0)
            {
                MessageBox.Show("User Already Exists");
            }
            else
            {
                MessageBox.Show("User does not Exists");
            }
        }

        private void ddlCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Convert.ToInt32(this.NavigationContext.QueryString["uid"]) == 0)
            {
                CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
            }
            BindAllStates(CountryId);
        }

        private void ddlState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Convert.ToInt32(this.NavigationContext.QueryString["uid"]) == 0)
            {
                StateId = Convert.ToInt32(ddlState.SelectedValue);
            }

            BindAllCity(StateId);
        }

    }
}
