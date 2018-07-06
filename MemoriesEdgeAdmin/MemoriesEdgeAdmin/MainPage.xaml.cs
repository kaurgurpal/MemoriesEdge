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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

namespace MemoriesEdgeAdmin
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            clsAddEditOperatorValidation obj1 = new clsAddEditOperatorValidation();

            LayoutRoot.DataContext = obj1;  
        }

        
      

        // If an error occurs during navigation, show an error window
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            
        }

      
    }
}