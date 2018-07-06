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
using System.Windows.Data;
using System.Runtime.Serialization;

namespace MemoriesEdge.Home.Administrator
{
    public partial class ManageOperator : Page
    {
        public ManageOperator()
        {
            InitializeComponent();
            clsManageOperatorValidationAdmin obj = new clsManageOperatorValidationAdmin();
            ManageOperatorGrid.DataContext = obj;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AdminService.IwcfAdminClient objm = new AdminService.IwcfAdminClient();
            objm.GetAllUsersCompleted += new EventHandler<AdminService.GetAllUsersCompletedEventArgs>(objm_GetAllUsersCompleted);
            objm.GetAllUsersAsync(objm);
        }

        void objm_GetAllUsersCompleted(object sender, AdminService.GetAllUsersCompletedEventArgs e)
        {
            PagedCollectionView objp = new PagedCollectionView(e.Result);
            DataPager1.Source = objp;
            dgvManageOperator.ItemsSource = objp;
        }

        private void btnAddNewOperator_Click(object sender, RoutedEventArgs e)
        {
            int UserId = 0;
            Uri obj = new Uri("/Pages/AddEditOperator.xaml?uid=" + UserId, UriKind.Relative);
            this.NavigationService.Navigate(obj);
        }

        public void BinDgvManage()
        {
            string str = txtUserName.Text;
            AdminService.IwcfAdminClient obj = new AdminService.IwcfAdminClient();
            obj.GetUserCompleted += new EventHandler<AdminService.GetUserCompletedEventArgs>(obj_GetUserCompleted);
            obj.GetUserAsync(str);
        }

        void obj_GetUserCompleted(object sender, AdminService.GetUserCompletedEventArgs e)
        {
            PagedCollectionView objp = new PagedCollectionView(e.Result);
            DataPager1.Source = objp;
            dgvManageOperator.ItemsSource = objp;

            lblMsg.Content = "Search Completed.";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(txtUserName) || Validation.GetHasError(txtName))
            {
                MessageBox.Show("Enter Valid Data");
            }
            else
            {
                BinDgvManage();

            }
        }

        private void hlEdit_Click(object sender, RoutedEventArgs e)
        {

            int UserId = Convert.ToInt32((sender as HyperlinkButton).CommandParameter);
            Uri obj = new Uri("/Pages/AddEditOperator.xaml?uid=" + UserId, UriKind.Relative);
            this.NavigationService.Navigate(obj);
        }

    }
}
