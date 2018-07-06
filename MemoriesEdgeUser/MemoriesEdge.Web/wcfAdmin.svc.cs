using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;

namespace MemoriesEdge.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "wcfAdmin" in code, svc and config file together.
   [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class wcfAdmin : IwcfAdmin
    {
        public void DoWork()
        {
        }
        MemoriesEdgeDataContext objAlpha = new MemoriesEdgeDataContext();
        public IList<ManageOperator> GetAllUsers()
        {
            var user = from us in objAlpha.OperatorMasters.AsEnumerable()
                       select new ManageOperator
                       {
                           UserId = us.Operator_ID,
                           UserName = us.User_Name,
                           Name = us.Name,
                           Phone = us.Phone,
                           Status = us.User_Status
                       };
            return user.ToList();
        }

        public IList<ManageOperator> GetUser(string UserName)
        {
            var a = from p in objAlpha.OperatorMasters.AsEnumerable()
                    where p.User_Name.ToLower().Contains(UserName.ToLower())
                    select new ManageOperator
                    {
                        UserId = p.Operator_ID,
                        UserName = p.User_Name,
                        Name = p.Name,
                        Phone = p.Phone,
                        Status = p.User_Status
                    };
            return a.ToList();
        }

        public int ChangeStatus(int UserId)
        {
            var CheckStatus = (from a in objAlpha.OperatorMasters
                               where a.Operator_ID == UserId
                               select a).SingleOrDefault();
            if (CheckStatus != null)
            {
                if (CheckStatus.User_Status == "AC")
                {
                    CheckStatus.User_Status = "IA";
                    return 0;
                }
                else
                {

                    CheckStatus.User_Status = "AC";
                    return 1;
                }
            }
            else
            {
                return 2;
            }
        }

        public IList<ManageRegisteredUsers> DisplayAllUserDetail()
        {
            var a = from user in objAlpha.OperatorMasters.AsEnumerable()
                    select new ManageRegisteredUsers
                    {
                        UserName = user.User_Name,
                        Name = user.Name,
                        Phone = user.Phone,
                        Mobile = user.Mobile,
                        DOB = (user.DOB).ToString(),
                        Status = user.User_Status
                    };
            return a.ToList();
        }

        public ManageRegisteredUsers DisplaySpecificUser(ManageRegisteredUsers manage)
        {
            ManageRegisteredUsers obj = new ManageRegisteredUsers();
            var a = (from p in objAlpha.OperatorMasters.AsEnumerable()
                     where p.User_Name == manage.UserName
                     select p).SingleOrDefault();
            if (a != null)
            {
                obj.UserName = a.User_Name;
                obj.Name = a.Name;
                obj.Phone = a.Phone;
                obj.Mobile = a.Mobile;
                obj.DOB = (a.DOB).ToString();
                obj.Status = a.User_Status;
                return obj;
            }
            else
            {
                return obj;
            }
        }

        public IList<Question> GetSecurityQuestions()
        {
            var Ques = from que in objAlpha.SecurityQuestionMasters.AsEnumerable()
                       where que.Status == true
                       select new Question
                       {
                           SecurityQuestion = que.Security_Question,
                           SecurityQuestionId = que.Security_Question_ID
                       };
            List<Question> obj = new List<Question>();
            obj.Add(new Question() { SecurityQuestionId = -1, SecurityQuestion = "[Please Select]" });
            var Result = obj.Union(Ques.ToList());
            return Result.ToList();
        }

        public IList<CountryStateCity> GetCountry()
        {
            var Country = from cntry in objAlpha.CountryMasters.AsEnumerable()
                          where cntry.Status == true
                          select new CountryStateCity
                          {
                              CountryId = cntry.Country_ID,
                              Country = cntry.Country
                          };
            List<CountryStateCity> obj = new List<CountryStateCity>();
            obj.Add(new CountryStateCity() { CountryId = -1, Country = "[Please Select]" });
            var Result = obj.Union(Country.ToList());
            return Result.ToList();
        }

        public IList<CountryStateCity> GetState(int CountryId)
        {
            var State = from stat in objAlpha.StateMasters.AsEnumerable()
                        where stat.Status == true && stat.Country_ID == CountryId
                        select new CountryStateCity
                        {
                            State = stat.State,
                            StateId = stat.State_ID
                        };
            List<CountryStateCity> obj = new List<CountryStateCity>();
            obj.Add(new CountryStateCity() { StateId = -1, State = "[Please Select]" });
            var Result = obj.Union(State.ToList());
            return Result.ToList();
        }

        public IList<CountryStateCity> GetCity(int StateId)
        {

            var City = from C in objAlpha.CityMasters
                       where C.Status == true && C.State_ID == StateId
                       select new CountryStateCity
                       {
                           City = C.City,
                           CityId = C.City_ID
                       };
            List<CountryStateCity> obj = new List<CountryStateCity>();
            obj.Add(new CountryStateCity() { CityId = -1, City = "[Please Select]" });
            var Result = obj.Union(City.ToList());
            return Result.ToList();
        }

        public int CheckAvailability(string UserName)
        {
            if (UserName != null)
            {
                var CheckUser = (from check in objAlpha.UserMasters
                                 where check.User_Name == UserName
                                 select check).SingleOrDefault();
                if (CheckUser == null)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public int InsertNewUser(NewRegisteredUser objNewUser)
        {
            if (objNewUser != null)
            {
                var CheckUser = (from check in objAlpha.UserMasters
                                 where check.User_Name == objNewUser.UserName
                                 select check).SingleOrDefault();
                if (CheckUser == null)
                {
                    UserMaster objMaster = new UserMaster();
                    objMaster.User_Name = objNewUser.UserName;
                    objMaster.User_Password = objNewUser.Password;
                    objMaster.Security_Question_ID = objNewUser.SecurityQuestionId;
                    objMaster.Security_Answer = objNewUser.Answer;
                    objMaster.User_Status = "AC";
                    objMaster.User_Type = "User";
                    objAlpha.UserMasters.InsertOnSubmit(objMaster);
                    objAlpha.SubmitChanges();
                    UserDetail objDetail = new UserDetail();
                    objDetail.User_ID = objMaster.User_ID;
                    objDetail.Initial = objNewUser.Initials;
                    objDetail.Name = objNewUser.Name;
                    objDetail.DOB = Convert.ToDateTime(objNewUser.DateOfBirth);
                    objDetail.Gender = objNewUser.Gender;
                    objDetail.Phone = objNewUser.Phone;
                    objDetail.Mobile = objNewUser.Mobile;
                    objDetail.Email_Id = objNewUser.EmailId;
                    objAlpha.UserDetails.InsertOnSubmit(objDetail);
                    objAlpha.SubmitChanges();
                    UserAddress objAddress = new UserAddress();
                    objAddress.User_ID = objMaster.User_ID;
                    objAddress.Address = objNewUser.Address;
                    objAddress.Country = objNewUser.Country;
                    objAddress.State = objNewUser.State;
                    objAddress.City = objNewUser.City;
                    objAddress.Pin_Code = objNewUser.PinCode;
                    objAlpha.UserAddresses.InsertOnSubmit(objAddress);
                    objAlpha.SubmitChanges();
                    UserImage objImage = new UserImage();
                    objImage.User_ID = objMaster.User_ID;
                    objImage.Image_Path = objNewUser.ImagePath;
                    objAlpha.UserImages.InsertOnSubmit(objImage);
                    objAlpha.SubmitChanges();


                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                return 0;

            }
        }

        public IList<CountryStateCity> GetAllCountry()
        {
            var Country = from cntry in objAlpha.CountryMasters.AsEnumerable()
                          where cntry.Status == true
                          select new CountryStateCity
                          {
                              CountryId = cntry.Country_ID,
                              Country = cntry.Country
                          };
            List<CountryStateCity> obj = new List<CountryStateCity>();
            obj.Add(new CountryStateCity() { CountryId = -1, Country = "[Please Select]" });
            var Result = obj.Union(Country.ToList());
            return Result.ToList();
        }

        public IList<CountryStateCity> GetAllState(int CountryId)
        {
            var State = from stat in objAlpha.StateMasters.AsEnumerable()
                        where stat.Status == true && stat.Country_ID == CountryId
                        select new CountryStateCity
                        {
                            State = stat.State,
                            StateId = stat.State_ID
                        };
            List<CountryStateCity> obj = new List<CountryStateCity>();
            obj.Add(new CountryStateCity() { StateId = -1, State = "[Please Select]" });
            var Result = obj.Union(State.ToList());
            return Result.ToList();
        }

        public IList<CountryStateCity> GetAllCity(int StateId)
        {
            var City = from C in objAlpha.CityMasters
                       where C.Status == true && C.State_ID == StateId
                       select new CountryStateCity
                       {
                           City = C.City,
                           CityId = C.City_ID
                       };
            List<CountryStateCity> obj = new List<CountryStateCity>();
            obj.Add(new CountryStateCity() { CityId = -1, City = "[Please Select]" });
            var Result = obj.Union(City.ToList());
            return Result.ToList();
        }

        public int CheckUserAvailability(string UserName)
        {

            if (UserName != null)
            {
                var CheckUser = (from check in objAlpha.OperatorMasters
                                 where check.User_Name == UserName
                                 select check).SingleOrDefault();
                if (CheckUser == null)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public int AddNewUser(AddEditOperator objNewUser)
        {
            if (objNewUser != null)
            {
                var CheckUser = (from check in objAlpha.OperatorMasters
                                 where check.User_Name == objNewUser.UserName
                                 select check).SingleOrDefault();
                if (CheckUser == null)
                {
                    OperatorMaster objMaster = new OperatorMaster();

                    objMaster.User_Name = objNewUser.UserName;
                    objMaster.User_Password = objNewUser.Password;
                    objMaster.Last_Login = DateTime.Now;
                    objMaster.User_Status = objNewUser.Status;
                    objMaster.Initial = objNewUser.Initials;
                    objMaster.Name = objNewUser.Name;
                    objMaster.DOB = objNewUser.DateOfBirth;
                    objMaster.Gender = objNewUser.Gender;
                    objMaster.Phone = objNewUser.Phone;
                    objMaster.Mobile = objNewUser.Mobile;
                    objMaster.Email_ID = objNewUser.EmailId;
                    objAlpha.OperatorMasters.InsertOnSubmit(objMaster);
                    objAlpha.SubmitChanges();
                    OperatorAddress objDetail = new OperatorAddress();
                    objDetail.Operator_ID = objMaster.Operator_ID;
                    objDetail.Address = objNewUser.Address;
                    objDetail.City = objNewUser.City;
                    objDetail.State = objNewUser.State;
                    objDetail.Country = objNewUser.Country;
                    objDetail.Pin_Code = objNewUser.PinCode;
                    objAlpha.OperatorAddresses.InsertOnSubmit(objDetail);
                    objAlpha.SubmitChanges();

                    return 1;
                }
                else
                {

                    return 0;
                }

            }
            else
            {
                //Update

                var a = (from b in objAlpha.OperatorMasters
                         where b.Operator_ID == objNewUser.UserId
                         select b).SingleOrDefault();
                if (a != null)
                {
                    OperatorMaster objMaster = new OperatorMaster();
                    objMaster.User_Name = objNewUser.UserName;
                    objMaster.User_Password = objNewUser.Password;
                    objMaster.Last_Login = DateTime.Now;
                    objMaster.User_Status = objNewUser.Status;
                    objMaster.Initial = objNewUser.Initials;
                    objMaster.Name = objNewUser.Name;
                    objMaster.DOB = objNewUser.DateOfBirth;
                    objMaster.Gender = objNewUser.Gender;
                    objMaster.Phone = objNewUser.Phone;
                    objMaster.Mobile = objNewUser.Mobile;
                    objMaster.Email_ID = objNewUser.EmailId;
                    objAlpha.SubmitChanges();
                    OperatorAddress objDetail = new OperatorAddress();
                    objDetail.Operator_ID = objMaster.Operator_ID;
                    objDetail.Address = objNewUser.Address;
                    objDetail.City = objNewUser.City;
                    objDetail.State = objNewUser.State;
                    objDetail.Country = objNewUser.Country;
                    objDetail.Pin_Code = objNewUser.PinCode;
                    objAlpha.SubmitChanges();
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
           
        }

        public AddEditOperator GetDetail(AddEditOperator entity)
        {
            AddEditOperator obj = new AddEditOperator();
            var a = (from p in objAlpha.OperatorMasters
                     join detail in objAlpha.OperatorAddresses
                      on p.Operator_ID equals detail.Operator_ID
                     join C in objAlpha.CountryMasters on detail.Country equals C.Country
                     join S in objAlpha.StateMasters on detail.State equals S.State
                     join CI in objAlpha.CityMasters on detail.City equals CI.City
                     where p.Operator_ID == entity.UserId
                     select new
                     {
                         p.User_Name,
                         p.User_Password,
                         p.Initial,
                         p.Name,
                         p.DOB,
                         p.Gender,
                         p.Phone,
                         p.Mobile,
                         p.Email_ID,
                         p.Last_Login,
                         detail.Address,
                         detail.Pin_Code,
                         detail.Country,
                         detail.City,
                         detail.State,
                         C.Country_ID,
                         S.State_ID,
                         CI.City_ID
                     }).SingleOrDefault();
            if (a != null)
            {

                obj.UserName = a.User_Name;
                obj.Password = a.User_Password;
                obj.ConfirmPassword = a.User_Password;
                obj.Initials = a.Initial;
                obj.Name = a.Name;
                obj.DateOfBirth = Convert.ToDateTime(a.DOB);
                obj.Gender = a.Gender;
                obj.Phone = a.Phone;
                obj.Mobile = a.Mobile;
                obj.EmailId = a.Email_ID;
                obj.Address = a.Address;
                obj.PinCode = a.Pin_Code;
                obj.Country = a.Country;
                obj.State = a.State;
                obj.City = a.City;
                obj.CountryId = a.Country_ID;
                obj.StateId = a.State_ID;
                obj.CityId = a.City_ID;
                return obj;
            }
            else
            {
                return obj;
            }

        }

        public UserLoginRegisterUser LoginUser(string UserName, string Password)
        {
            var a = (from user in objAlpha.UserMasters
                     join detail in objAlpha.UserDetails
                     on user.User_ID equals detail.User_ID
                     where user.User_Name == UserName
                     select new
                     {
                         UserId = user.User_ID,
                         Initials = detail.Initial,
                         Name = detail.Name,
                         Password = user.User_Password,
                         UserStatus = user.User_Status,
                         UserType = user.User_Type,
                         LastLogin = user.Last_Login
                     }).SingleOrDefault();

            UserLoginRegisterUser obj = new UserLoginRegisterUser();
            if (a != null)
            {
                if (a.Password == Password)
                {
                    if (a.UserStatus == "AC")
                    {
                        var s = (from bc in objAlpha.UserMasters
                                 where bc.User_Name == UserName
                                 select bc).SingleOrDefault();
                        s.Last_Login = DateTime.Now;
                        objAlpha.SubmitChanges();
                        obj.UserId = a.UserId;
                        obj.UserName = a.Name;
                        obj.UserStatus = a.UserStatus;
                        obj.UserType = a.UserType;
                        return obj;
                    }
                    else
                    {
                        obj = null;
                        return obj;
                    }
                }
                else
                {
                    obj = null;
                    return obj;
                }
            }
            else
            {
                obj = null;
                return obj;
            }
           
        }

        public ForgotPassword GetPassword(string UserName)
        {
            ForgotPassword obj = new ForgotPassword();
            if (UserName != null)
            {
                var a = (from u in objAlpha.UserMasters
                         where u.User_Name == UserName
                         select u).SingleOrDefault();
                if (a != null)
                {
                    var b = (from user in objAlpha.UserMasters
                             where user.User_Name == UserName
                             select new
                             {
                                 SecurityQuestionId = user.Security_Question_ID,
                                 Answer = user.Security_Answer,
                                 Password = user.User_Password
                             }).SingleOrDefault();
                    obj.SecurityQuestionId = Convert.ToInt32(b.SecurityQuestionId);
                    obj.Password = b.Password;
                    obj.Answer = b.Answer;
                    return obj;
                }
                else
                {
                    obj = null;
                    return obj;
                }
            }
            else
            {
                obj = null;
                return obj;
            }
        }

        public string GetUserPassword(int UserId)
        {
            string Password = "";
            if (UserId != 0)
            {
                var User = (from data in objAlpha.UserMasters
                            where data.User_ID == UserId
                            select new
                            {
                                Password = data.User_Password
                            }).SingleOrDefault();
                Password = User.Password;
                return Password;
            }
            else
            {
                return Password;
            }
        }

        public int UpdatePassword(int UserId, string NewPassword)
        {
            ChangePassword obj = new ChangePassword();
            if (UserId != 0)
            {
                var pass = (from mas in objAlpha.UserMasters
                            where mas.User_ID == UserId
                            select mas).SingleOrDefault();

                if (pass != null)
                {
                    pass.User_Password = NewPassword;
                    objAlpha.SubmitChanges();
                    return 1;

                }

                else
                {

                    return 0;
                }
            }
            else
            {

                return 0;
            }
        }

        public IList<ManageFeedBack> GetAllQuestions()
        {

            var a = from q in objAlpha.FeedbackQuestionMasters.AsEnumerable()
                    select new ManageFeedBack
                    {
                        FeedbackQuestionId = q.Feedback_Question_ID,
                        FeedbackQuestion = q.Feedback_Question,
                        Status = Convert.ToBoolean(q.Status)
                    };
            return a.ToList();
        }

        public IList<ManageFeedBack> GetAllAnswers(int aid)
        {

            var a = from b in objAlpha.FeedbackAnswerMasters
                    join s in objAlpha.FeedbackQuestionMasters
                    on b.Feedback_Question_ID equals s.Feedback_Question_ID
                    where b.Feedback_Question_ID == aid
                    select new ManageFeedBack
                    {
                        feedbackAnswerId = b.Feedback_Answer_ID,
                        FeedbackAnswer = b.Feedback_Answer,
                        Status = Convert.ToBoolean(b.Status)
                    };
            return a.ToList();
        }

        public int SaveQuestion(ManageFeedBack objF)
        {
            if (objF.FeedbackQuestionId == 0)
            {
                var a = (from b in objAlpha.FeedbackQuestionMasters
                         where b.Feedback_Question.ToLower() == objF.FeedbackQuestion.ToLower()
                         select b).SingleOrDefault();
                if (a == null)
                {

                    FeedbackQuestionMaster obj = new FeedbackQuestionMaster();
                    obj.Feedback_Question = objF.FeedbackQuestion;
                    obj.Status = objF.Status;
                    objAlpha.FeedbackQuestionMasters.InsertOnSubmit(obj);
                    objAlpha.SubmitChanges();
                    return 1;

                }


                else
                {
                    return 0;

                }
            }

            else
            {
                return 0;
            }
        }

        public int UpdateQuestion(ManageFeedBack entity)
        {

            var r = (from p in objAlpha.FeedbackQuestionMasters
                     where p.Feedback_Question_ID == entity.FeedbackQuestionId
                     select p).SingleOrDefault();
            if (r != null)
            {
                r.Feedback_Question_ID = entity.FeedbackQuestionId;
                r.Feedback_Question = entity.FeedbackQuestion;
                objAlpha.SubmitChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public ManageFeedBack ShowQuestionStatus(ManageFeedBack entity)
        {
            var a = (from b in objAlpha.FeedbackQuestionMasters
                     where b.Feedback_Question_ID == entity.FeedbackQuestionId
                     select new
                     {
                         b.Feedback_Question,
                         b.Status
                     }).SingleOrDefault();
            if (a != null)
            {
                entity.FeedbackQuestion = a.Feedback_Question;
                entity.Status = Convert.ToBoolean(a.Status);
                return entity;
            }
            else
            {
                return entity;
            }
        }

        public ManageFeedBack DispalyQues(ManageFeedBack entity)
        {
            var a = (from b in objAlpha.FeedbackQuestionMasters
                     where b.Feedback_Question_ID == entity.FeedbackQuestionId
                     select new
                     {
                         b.Feedback_Question,
                     }).SingleOrDefault();
            if (a != null)
            {
                entity.FeedbackQuestion = a.Feedback_Question;
                return entity;

            }
            else
            {
                return entity;
            }
        }

        public ManageFeedBack ShowAnswerStatus(ManageFeedBack entity)
        {
            var a = (from b in objAlpha.FeedbackAnswerMasters
                     where b.Feedback_Answer_ID == entity.feedbackAnswerId
                     select new
                     {
                         b.Feedback_Answer,
                         b.Status
                     }).SingleOrDefault();
            if (a != null)
            {
                entity.FeedbackAnswer = a.Feedback_Answer;
                entity.Status = Convert.ToBoolean(a.Status);
                return entity;
            }
            else
            {
                return entity;
            }
        }

        public int SaveAnswers(ManageFeedBack entity)
        {

            if (entity.feedbackAnswerId == 0)
            {
                var a = (from b in objAlpha.FeedbackAnswerMasters
                         join c in objAlpha.FeedbackQuestionMasters
                         on b.Feedback_Question_ID equals c.Feedback_Question_ID
                         where b.Feedback_Answer.ToLower() == entity.FeedbackAnswer.ToLower()
                         select b).SingleOrDefault();
                if (a == null)
                {

                    FeedbackAnswerMaster obj = new FeedbackAnswerMaster();
                    obj.Feedback_Question_ID = entity.FeedbackQuestionId;
                    obj.Feedback_Answer = entity.FeedbackAnswer;
                    obj.Status = entity.Status;
                    objAlpha.FeedbackAnswerMasters.InsertOnSubmit(obj);
                    objAlpha.SubmitChanges();
                    return 1;

                }

                else
                {
                    return 0;

                }

            }

            else
            {
                return 0;

            }
        }

        public int UpdateAnswers(ManageFeedBack entity)
        {

            var a = (from p in objAlpha.FeedbackAnswerMasters
                     where p.Feedback_Answer_ID == entity.feedbackAnswerId
                     select p).SingleOrDefault();
            if (a != null)
            {
                a.Feedback_Question_ID = entity.FeedbackQuestionId;
                a.Feedback_Answer_ID = entity.feedbackAnswerId;
                a.Feedback_Answer = entity.FeedbackAnswer;
                objAlpha.SubmitChanges();
                return 1;

            }
            else
            {
                return 0;
            }
        }

        public IList<ViewFeedBack> GetUsersFeedback()
        {
            var a = from user in objAlpha.UserMasters
                    join feed in objAlpha.UserFeedbackMasters
                    on user.User_ID equals feed.User_ID
                    join fi in objAlpha.UserFeedbackDetails
                    on feed.User_Feedback_ID equals fi.User_Feedback_ID
                    join fqi in objAlpha.FeedbackAnswerMasters
                    on fi.Feedback_Question_ID equals fqi.Feedback_Question_ID
                    join fqm in objAlpha.FeedbackQuestionMasters
                    on fqi.Feedback_Question_ID equals fqm.Feedback_Question_ID
                    select new ViewFeedBack
                    {
                        UserId = Convert.ToInt32(feed.User_ID),
                        UserName = user.User_Name,
                        FeedbackDate = Convert.ToDateTime(feed.Feedback_Date),

                    };
            return a.ToList();
        }
    }
}
