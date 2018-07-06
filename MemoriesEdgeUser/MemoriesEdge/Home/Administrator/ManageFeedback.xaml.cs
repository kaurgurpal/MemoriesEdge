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


namespace MemoriesEdge.Pages
{
    public partial class ManageFeedback : Page
    {
        public static int QuestionId, AnswerId;  
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
            QuestionId = 0;
            AnswerId = 0;
            BindgvFeedback();
            stck2.Visibility = System.Windows.Visibility.Collapsed;
        }

       

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionId == 0)
            {
                //objm = new AdminService.ManageFeedBack();
                //objm.FeedbackQuestionId = QuestionId;
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
            else
            {
                AdminService.ManageFeedBack obj1 = new AdminService.ManageFeedBack();
                obj1.FeedbackQuestionId = QuestionId;
                obj1.FeedbackQuestion = txtFeedBackQuestion.Text;
                AdminService.IwcfAdminClient obj = new AdminService.IwcfAdminClient();
                obj.UpdateQuestionCompleted += new EventHandler<AdminService.UpdateQuestionCompletedEventArgs>(obj_UpdateQuestionCompleted);
                obj.UpdateQuestionAsync(obj1);    
            }
            
        }

        void obj_UpdateQuestionCompleted(object sender, AdminService.UpdateQuestionCompletedEventArgs e)
        {
            if (e.Result == 1)
            {
                lblMsg.Content = "Feedback Question Updated Successfully";
            }
            else
            {
                lblMsg.Content = "Updation Failed.";
            }
            QuestionId = 0;
            BindgvFeedback();
        }

        void objn_SaveQuestionCompleted(object sender, AdminService.SaveQuestionCompletedEventArgs e)
        {
            switch (e.Result)
            { 
            
                case 0:
                    lblMsg.Content = "Feedback Question Already Exists";
                    break;
                case 1:
                    lblMsg.Content = "Feedback Question Saved Successfully";
                    break;
                case 2:
                    lblMsg.Content = "Feedback Question Updated Successfully";
                    break;
            }

            BindgvFeedback();
        }

        private void hlAnswer_Click(object sender, RoutedEventArgs e)
        {

            int aid = Convert.ToInt32((sender as HyperlinkButton).CommandParameter);
            QuestionId = Convert.ToInt32((sender as HyperlinkButton).CommandParameter);
            stck2.Visibility = System.Windows.Visibility.Visible;

            AdminService.IwcfAdminClient obj = new AdminService.IwcfAdminClient();
            obj.GetAllAnswersCompleted += new EventHandler<AdminService.GetAllAnswersCompletedEventArgs>(obj_GetAllAnswersCompleted);
            obj.GetAllAnswersAsync(aid);

             objm = new AdminService.ManageFeedBack();
             objm.FeedbackQuestionId = Convert.ToInt32((sender as HyperlinkButton).CommandParameter);
             AdminService.IwcfAdminClient objn = new AdminService.IwcfAdminClient();
             objn.DispalyQuesCompleted += new EventHandler<AdminService.DispalyQuesCompletedEventArgs>(objn_DispalyQuesCompleted);
             objn.DispalyQuesAsync(objm);
        }

        void objn_DispalyQuesCompleted(object sender, AdminService.DispalyQuesCompletedEventArgs e)
        {
            objm = e.Result as MemoriesEdge.AdminService.ManageFeedBack;
            lblFeedbackQuestion.Content = objm.FeedbackQuestion;

        }

        void obj_GetAllAnswersCompleted(object sender, AdminService.GetAllAnswersCompletedEventArgs e)
        {
           
            
            PagedCollectionView objp = new PagedCollectionView(e.Result);
            DataPager2.Source = objp;
            gvAnswer.ItemsSource = objp;
              
        }

        private void hlEdit_Click(object sender, RoutedEventArgs e)
        {
            

            objm = new AdminService.ManageFeedBack();
           QuestionId=objm.FeedbackQuestionId = Convert.ToInt32((sender as HyperlinkButton).CommandParameter); 
            AdminService.IwcfAdminClient obj1 = new AdminService.IwcfAdminClient();
            obj1.ShowQuestionStatusCompleted += new EventHandler<AdminService.ShowQuestionStatusCompletedEventArgs>(obj1_ShowQuestionStatusCompleted);
            obj1.ShowQuestionStatusAsync(objm);
         }

        //((MemoriesEdgeAdmin.AdminService.CountryStateCity)(ddlCountry.SelectedItem)).Country = obje.Country;
        void obj1_ShowQuestionStatusCompleted(object sender, AdminService.ShowQuestionStatusCompletedEventArgs e)
        {
            objm = e.Result as MemoriesEdge.AdminService.ManageFeedBack;
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

        private void hbtnEdit_Click(object sender, RoutedEventArgs e)
        {
            objm = new AdminService.ManageFeedBack();
            AnswerId = objm.feedbackAnswerId = Convert.ToInt32((sender as HyperlinkButton).CommandParameter);
            AdminService.IwcfAdminClient obj2 = new AdminService.IwcfAdminClient();
            obj2.ShowAnswerStatusCompleted += new EventHandler<AdminService.ShowAnswerStatusCompletedEventArgs>(obj2_ShowAnswerStatusCompleted);
            obj2.ShowAnswerStatusAsync(objm);
        }

        void obj2_ShowAnswerStatusCompleted(object sender, AdminService.ShowAnswerStatusCompletedEventArgs e)
        {
            objm = e.Result as MemoriesEdge.AdminService.ManageFeedBack;
            txtFeedbackAnswer.Text = objm.FeedbackAnswer;
            if (objm.Status == true)
            {
                ddlFeedbackAnswerStatus.SelectedIndex = 0;
            }
            else
            {
                ddlFeedbackAnswerStatus.SelectedIndex = 1;
            }

        }

        private void btnSaveAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (AnswerId == 0)
            {
                objm.FeedbackQuestionId = QuestionId;  
                objm.FeedbackAnswer = txtFeedbackAnswer.Text;
                if (ddlFeedbackAnswerStatus.SelectedIndex == 0)
                {
                    objm.Status = true;
                }
                else
                {
                    objm.Status = false;
                }
                AdminService.IwcfAdminClient obj = new AdminService.IwcfAdminClient();
                obj.SaveAnswersCompleted += new EventHandler<AdminService.SaveAnswersCompletedEventArgs>(obj_SaveAnswersCompleted);
                obj.SaveAnswersAsync(objm);

            }

            else
            {
                AdminService.ManageFeedBack obj = new AdminService.ManageFeedBack();
                obj.feedbackAnswerId = AnswerId;
                obj.FeedbackQuestionId = QuestionId;   
                obj.FeedbackAnswer = txtFeedbackAnswer.Text;
                AdminService.IwcfAdminClient objs = new AdminService.IwcfAdminClient();
                objs.UpdateAnswersCompleted += new EventHandler<AdminService.UpdateAnswersCompletedEventArgs>(objs_UpdateAnswersCompleted);
                objs.UpdateAnswersAsync(obj);
            }
        }

        void objs_UpdateAnswersCompleted(object sender, AdminService.UpdateAnswersCompletedEventArgs e)
        {
            if (e.Result == 1)
            {
                lblMsg.Content = "Answer Updated Successfully";
            }
            else
            {
                lblMsg.Content = "Updation Failed";
            }
            AnswerId = 0;
        }

               void obj_SaveAnswersCompleted(object sender, AdminService.SaveAnswersCompletedEventArgs e)
        {
            if (e.Result == 1)
            {
                lblMsg.Content = "Feedback Answer Saved Successfully";
            }
            else
            {
                lblMsg.Content = "Feedback Answer Already Exists";
            }
           
        }

       
    }
}
