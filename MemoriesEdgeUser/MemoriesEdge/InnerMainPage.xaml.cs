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
using MemoriesEdge.Common;

namespace MemoriesEdge
{
    public partial class InnerMainPage : Page
    {
        public InnerMainPage()
        {
            InitializeComponent();

          
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        // After the Frame navigates, ensure the HyperlinkButton representing the current page is selected
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (Home.General.Login.UserType == "Admin")
            {
                GeneralUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                AdminUpperLinks.Visibility = System.Windows.Visibility.Visible;
                OperatorUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                UserUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                /******/
                GeneralUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                AdminUpperLinks.Visibility = System.Windows.Visibility.Visible;
                OperatorUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                UserUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (Home.General.Login.UserType == "Operator")
            {
                GeneralUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                AdminUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                OperatorUpperLinks.Visibility = System.Windows.Visibility.Visible;
                UserUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                /******/
                GeneralUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                AdminUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                OperatorUpperLinks.Visibility = System.Windows.Visibility.Visible;
                UserUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (Home.General.Login.UserType == "User")
            {
                GeneralUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                AdminUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                OperatorUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                UserUpperLinks.Visibility = System.Windows.Visibility.Visible;
                /******/
                GeneralUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                AdminUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                OperatorUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                UserUpperLinks.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                GeneralUpperLinks.Visibility = System.Windows.Visibility.Visible;
                AdminUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                OperatorUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                UserUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                /******/
                GeneralUpperLinks.Visibility = System.Windows.Visibility.Visible;
                AdminUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                OperatorUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
                UserUpperLinks.Visibility = System.Windows.Visibility.Collapsed;
            }
            /*************/
            //foreach (UIElement child in LinksStackPanel1.Children)
            //{
            //    HyperlinkButton hb = child as HyperlinkButton;
            //    if (hb != null && hb.NavigateUri != null)
            //    {
            //        if (hb.NavigateUri.ToString().Equals(e.Uri.ToString()))
            //        {
            //            VisualStateManager.GoToState(hb, "ActiveLink", true);
            //        }
            //        else
            //        {
            //            VisualStateManager.GoToState(hb, "InactiveLink", true);
            //        }
            //    }
            //}

            

        }

        // If an error occurs during navigation, show an error window
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            ChildWindow errorWin = new ErrorWindow(e.Uri);
            errorWin.Show();
        }
    }
}
