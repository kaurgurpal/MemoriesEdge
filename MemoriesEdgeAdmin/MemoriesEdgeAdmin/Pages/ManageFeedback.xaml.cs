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
using System.Windows.Data;
using System.Runtime.Serialization;
using System.Collections;

namespace MemoriesEdgeAdmin.Pages
{
    public partial class ManageFeedback : Page
    {

        AdminService.ManageFeedBack objm;
        public ManageFeedback()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public void BindgvFeedback()
        {
            AdminService.IwcfAdminClient obj = new AdminService.IwcfAdminClient();
            obj.GetAllQuestionsCompleted += new EventHandler<AdminService.GetAllQuestionsCompletedEventArgs>(obj_GetAllQuestionsCompleted);
            obj.GetAllQuestionsAsync();
        }

        void obj_GetAllQuestionsCompleted(object sender, AdminService.GetAllQuestionsCompletedEventArgs e)
        {
              PagedCollectionView objp = new PagedCollectionView(e.Result);
              DataPager1.Source = objp;
              gvQuestions.ItemsSource = objp;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BindgvFeedback();
            stck2.Visibility = System.Windows.Visibility.Collapsed;
        }

       

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            objm.FeedbackQuestion = txtFeedBackQuestion.Text;
            if (ddlFeedbackQuestionStatus.SelectedIndex == 0)
            {
                objm.Status = true;
            }
            else
            {
                objm.Status = false;
            }
            
            AdminService.IwcfAdminClient objn = new AdminService.IwcfAdminClient();
            objn.SaveQuestionCompleted += new EventHandler<AdminService.SaveQuestionCompletedEventArgs>(objn_SaveQuestionCompleted);
            objn.SaveQuestionAsync(objm);
            
        }

        void objn_SaveQuestionCompleted(object sender, AdminService.SaveQuestionCompletedEventArgs e)
        {
            if (e.Result == 1)
            {
                lblMsg.Content = "Feedback Question Saved Successfully";
            }
            else
            {
                lblMsg.Content = "Feedback Question Already Exists";
            }
            BindgvFeedback();
        }

        private void hlAnswer_Click(object sender, RoutedEventArgs e)
        {
            objm = new AdminService.ManageFeedBack();
            int aid = Convert.ToInt32((sender as HyperlinkButton).CommandParameter);
            stck2.Visibility = System.Windows.Visibility.Visible;
            
            AdminService.IwcfAdminClient obj = new AdminService.IwcfAdminClient();
            obj.GetAllAnswersCompleted += new EventHandler<AdminService.GetAllAnswersCompletedEventArgs>(obj_GetAllAnswersCompleted);
            obj.GetAllAnswersAsync(aid);
        }

        void obj_GetAllAnswersCompleted(object sender, AdminService.GetAllAnswersCompletedEventArgs e)
        {
            MemoriesEdgeAdmin.AdminService.ManageFeedBack obj = new AdminService.ManageFeedBack();
            obj = e.Result as AdminService.ManageFeedBack;
            lblFeedbackQuestion.Content = objm.FeedbackQuestion; 
            //PagedCollectionView objp = new PagedCollectionView(e.Result);
            //DataPager2.Source = objp;
            //gvAnswer.ItemsSource = objp;
              
        }

        private void hlEdit_Click(object sender, RoutedEventArgs e)
        {
            objm = new AdminService.ManageFeedBack();
            objm.FeedbackQuestionId = Convert.ToInt32((sender as HyperlinkButton).CommandParameter); 
            AdminService.IwcfAdminClient obj1 = new AdminService.IwcfAdminClient();
            obj1.ShowQuestionStatusCompleted += new EventHandler<AdminService.ShowQuestionStatusCompletedEventArgs>(obj1_ShowQuestionStatusCompleted);
            obj1.ShowQuestionStatusAsync(objm);
         }

        //((MemoriesEdgeAdmin.AdminService.CountryStateCity)(ddlCountry.SelectedItem)).Country = obje.Country;
        void obj1_ShowQuestionStatusCompleted(object sender, AdminService.ShowQuestionStatusCompletedEventArgs e)
        {
            objm = e.Result as MemoriesEdgeAdmin.AdminService.ManageFeedBack;
            txtFeedBackQuestion.Text = objm.FeedbackQuestion;
            if (objm.Status == true)
            {
                ddlFeedbackQuestionStatus.SelectedIndex = 0;
            }
            else
            {
                ddlFeedbackQuestionStatus.SelectedIndex = 1;
            }
        }

        private void btnBackToFeedbackQuestion_Click(object sender, RoutedEventArgs e)
        {
            stck2.Visibility = System.Windows.Visibility.Collapsed;
        }

    }
}
