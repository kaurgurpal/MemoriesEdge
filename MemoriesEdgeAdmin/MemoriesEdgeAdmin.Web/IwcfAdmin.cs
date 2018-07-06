using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MemoriesEdgeAdmin.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IwcfAdmin" in both code and config file together.
    [ServiceContract]
    public interface IwcfAdmin
    {
        //Manage Operator
        [OperationContract]
        IList<ManageOperator> GetAllUsers();
        [OperationContract]
        IList<ManageOperator> GetUser(string UserName);
        [OperationContract]
        int ChangeStatus(int UserId);

        //Manage Registered Users
        [OperationContract]
        IList<ManageRegisteredUsers> DisplayAllUserDetail();
        [OperationContract]
        ManageRegisteredUsers DisplaySpecificUser(ManageRegisteredUsers manage);

       //New User Registration
        [OperationContract]
        IList<Question> GetSecurityQuestions();
        [OperationContract]
        IList<CountryStateCity> GetCountry();
        [OperationContract]
        IList<CountryStateCity> GetState(int CountryId);
        [OperationContract]
        IList<CountryStateCity> GetCity(int StateId);
        [OperationContract]
        int CheckAvailability(string UserName);
        [OperationContract]
        int InsertNewUser(NewRegisteredUser objNewUser);

        //  AddEdit Operator
        //[OperationContract]
        //IList<AddEditOperator> GetCountries(int UserId);
        //[OperationContract]
        //IList<AddEditOperator> GetStates(int UserId);
        //[OperationContract]
        //IList<AddEditOperator> GetCities(int UserId);
        [OperationContract]
        IList<CountryStateCity> GetAllCountry();
        [OperationContract]
        IList<CountryStateCity> GetAllState(int CountryId);
        [OperationContract]
        IList<CountryStateCity> GetAllCity(int StateId);
        [OperationContract]
        int CheckUserAvailability(string UserName);
        [OperationContract]
        int AddNewUser(AddEditOperator objNewUser);
        [OperationContract]
        AddEditOperator GetDetail(AddEditOperator entity);
   


        //UserLoginForRegisterdUser
        [OperationContract]
        UserLoginRegisterUser LoginUser(string UserName, string Password);

        //Forgot Password
        [OperationContract]
        ForgotPassword GetPassword(string UserName);

        //Change Password
        [OperationContract]
        string GetUserPassword(int UserId);
        [OperationContract]
        int UpdatePassword(int UserId, string NewPassword);


        //ManageFeedback
        [OperationContract]
        IList<ManageFeedBack> GetAllQuestions();
        [OperationContract]
        IList<ManageFeedBack> GetAllAnswers(int aid);
        [OperationContract]
        int SaveQuestion(ManageFeedBack objF);
        [OperationContract]
        ManageFeedBack ShowQuestionStatus(ManageFeedBack entity);


    }

}