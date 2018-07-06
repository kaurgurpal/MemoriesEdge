using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace MemoriesEdge.Web
{
    public class clsAdmin
    {
    }

    [DataContract]
    public class ManageOperator
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Phone { get; set; }
    }
    [DataContract]
    public class AddEditOperator
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string ConfirmPassword { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Initials { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime DateOfBirth { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Mobile { get; set; }
        [DataMember]
        public string EmailId { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public Int32 CountryId { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public Int32 StateId { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public Int32 CityId { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string PinCode { get; set; }
    }
    [DataContract]
    public class ManageRegisteredUsers
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Mobile { get; set; }
        [DataMember]
        public string DOB { get; set; }
    }
    [DataContract]
    public class ManageThemes
    {
        [DataMember]
        public string ThemeTitle { get; set; }
        [DataMember]
        public string ThemeId { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string ThemeFilePath { get; set; }
        [DataMember]
        public string ImagePath { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
    [DataContract]
    public class ManageFeedBack
    {
        [DataMember]
        public string FeedbackQuestion { get; set; }
        [DataMember]
        public Boolean Status { get; set; }
        [DataMember]
        public string FeedbackAnswer { get; set; }
        [DataMember]
        public int FeedbackQuestionId { get; set; }
        [DataMember]
        public int feedbackAnswerId { get; set; }
    }
    [DataContract]
    public class ViewFeedBack
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public DateTime FeedbackDate { get; set; }
        [DataMember]
        public string Question { get; set; }
        [DataMember]
        public string Answer { get; set; }
        [DataMember]
        public string AdditionalFeedback { get; set; }
    }

    [DataContract]
    public class Question
    {
        [DataMember]
        public Int32 SecurityQuestionId { get; set; }
        [DataMember]
        public string SecurityQuestion { get; set; }
    }
    [DataContract]
    public class CountryStateCity
    {
        [DataMember]
        public Int32 CountryId { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public Int32 StateId { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public Int32 CityId { get; set; }
        [DataMember]
        public string City { get; set; }
    }
    [DataContract]
    public class NewRegisteredUser
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string ConfirmPassword { get; set; }
        [DataMember]
        public string Answer { get; set; }
        [DataMember]
        public string Initials { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ImagePath { get; set; }
        [DataMember]
        public DateTime DateOfBirth { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Mobile { get; set; }
        [DataMember]
        public string EmailId { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string PinCode { get; set; }
        [DataMember]
        public int SecurityQuestionId { get; set; }
        [DataMember]
        public string SecurityAnswer { get; set; }
    }
    [DataContract]
    public class UserLoginRegisterUser
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string Initials { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string UserStatus { get; set; }
        [DataMember]
        public string UserType { get; set; }

    }

    [DataContract]
    public class ForgotPassword
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int SecurityQuestionId { get; set; }
        [DataMember]
        public string Answer { get; set; }
    }

    [DataContract]
    public class ChangePassword
    {
        [DataMember]
        public string NewPassword { get; set; }
        [DataMember]
        public string Password { get; set; }

    }

    [DataContract]
    public class UserLoginForOperator
    {
        [DataMember]
        public string UserType { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string UserStatus { get; set; }


    }


}