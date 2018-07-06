using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace MemoriesEdge
{
    public class clsAddEditOperatorValidation:INotifyPropertyChanged,IDataErrorInfo
    {
        private string strUserName;
        public string UserName
        {
            get { return strUserName; }
            set
            {
                strUserName = value;
                ChangeValue("UserName");
            }
        }

        private string strPassword;
        public string Password
        {
            get { return strPassword; }
            set
            {
                strPassword = value;
                ChangeValue("Password");
            }
        }

        private string strConfirmPassword;
        public string ConfirmPassword
        {
            get { return strConfirmPassword; }
            set
            {
                strConfirmPassword = value;
                ChangeValue("ConfirmPassword");
            }
        }



        private string strName;
        public string Name
        {
            get { return strName; }
            set
            {
                strName = value;
                ChangeValue("Name");
            }
        }

        private int intInitials;

        public int Initials
        {
            get { return intInitials; }
            set
            {
                intInitials = value;
                ChangeValue("Initials");
            }
        }


        private string strDob;

        public string DateOfBirth
        {
            get { return strDob; }
            set
            {
                strDob = value;
                ChangeValue("DateOfBirth");
            }
        }
        private string strPhone;

        public string Phone
        {
            get { return strPhone; }
            set
            {
                strPhone = value;
                ChangeValue("Phone");
            }
        }

        private string strMobile;

        public string Mobile
        {
            get { return strMobile; }
            set
            {
                strMobile = value;
                ChangeValue("Mobile");
            }
        }

        private string strEmailId;

        public string EmailId
        {
            get { return strEmailId; }
            set
            {
                strEmailId = value;
                ChangeValue("EmailId");
            }
        }
        private string strAddress;

        public string Address
        {
            get { return strAddress; }
            set
            {
                strAddress = value;
                ChangeValue("Address");
            }
        }
        private int intCity;

        public int City
        {
            get { return intCity; }
            set
            {
                intCity = value;
                ChangeValue("City");
            }
        }

        private int intState;

        public int State
        {
            get { return intState; }
            set
            {
                intState = value;
                ChangeValue("State");
            }
        }

        private int intCountry;

        public int Country
        {
            get { return intCountry; }
            set
            {
                intCountry = value;
                ChangeValue("Country");
            }
        }

        private string strPinCode;

        public string PinCode
        {
            get { return strPinCode; }
            set
            {
                strPinCode = value;
                ChangeValue("PinCode");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void ChangeValue(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }


        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string strMsg = "";
                if (columnName == "UserName")
                {
                    if (UserName == null)
                        strMsg = "User Name Required";
                }
                if (columnName == "Password")
                {
                    if (Password == null)
                        strMsg = "Password Required";
                }

                if (columnName == "ConfirmPassword")
                {
                    if (ConfirmPassword != null)
                    {
                        if (ConfirmPassword != Password)
                        {
                            strMsg = "Password and Confirm Password must match.";
                        }
                    }
                    else
                    {
                        strMsg = "Enter Confirm Password";
                    }
                }

                if (columnName == "Name")
                {
                    if (Name == null)
                        strMsg = "Name Required";
                }
                if (columnName == "DateOfBirth")
                {
                    if (DateOfBirth == null)
                        strMsg = "Date of Birth Required";
                }

                if (columnName == "Phone")
                {
                    if (Phone == null)
                    {
                        strMsg = "Enter your Phone Number.";
                    }
                    else
                    {
                        int F = 0;
                        for (int i = 0; i < 6; i++)
                        {
                            if (Convert.ToInt32(Phone[i]) >= 48 && Convert.ToInt32(Phone[i]) <= 57)
                            {
                                continue;
                            }
                            else
                            {
                                F = 1;
                                break;
                            }
                        }
                        strMsg = F == 0 ? "" : "Enter Valid PhoneNumber";
                    }
                }

                if (columnName == "Mobile")
                {
                    if (Mobile == null)
                    {
                        strMsg = "Enter your Mobile Number.";
                    }
                    else
                    {
                        int F = 0;
                        for (int i = 0; i < 6; i++)
                        {
                            if (Convert.ToInt32(Mobile[i]) >= 48 && Convert.ToInt32(Mobile[i]) <= 57)
                            {
                                continue;
                            }
                            else
                            {
                                F = 1;
                                break;
                            }
                        }
                        strMsg = F == 0 ? "" : "Enter Valid MobileNumber";
                    }
                }

                if (columnName == "EmailId")
                {

                    //int f = 0;
                    //int counter = 0;
                    if (EmailId != null)
                    {
                        if (EmailId == null)
                        {
                            strMsg = "Enter your EmailId";
                        }
                        //if (Convert.ToInt32(EmailId[0]) >= 65 && Convert.ToInt32(EmailId[0]) <= 90 || Convert.ToInt32(EmailId[0]) >= 97 && Convert.ToInt32(EmailId[0]) <= 122)
                        //{
                        //    f = 0;
                        //}
                        //else
                        //{
                        //    f = 1;
                        //    strMsg = "Invalid Email Address";
                        //}
                        //if (f == 0)
                        //{
                        //    if (EmailId.IndexOf("@") == EmailId.LastIndexOf("@"))
                        //    {
                        //        f = 0;
                        //    }
                        //    else
                        //    {
                        //        f = 1;
                        //        strMsg = "Invalid Email Address";
                        //    }
                        //}
                        //if (f == 0)
                        //{
                        //    for (int i = EmailId.IndexOf("@"); i < EmailId.Length; i++)
                        //    {
                        //        if (Convert.ToInt32(EmailId[i]) == 46)
                        //        {
                        //            counter++;
                        //        }
                        //    }
                        //    if (counter > 0)
                        //    {
                        //        f = 0;
                        //    }
                        //    else
                        //    {

                        //        f = 1;
                        //        strMsg = "Invalid Email Address";
                        //    }
                        //}
                        //if (f == 0)
                        //{
                        //    for (int i = EmailId.IndexOf("@"); i < EmailId.Length; i++)
                        //    {
                        //        if (i != EmailId.Length - 1)
                        //        {
                        //            if (Convert.ToInt32(EmailId[i]) == 46 && Convert.ToInt32(EmailId[i + 1]) == 46)
                        //            {
                        //                f = 1;
                        //                strMsg = "Invalid Email Address";
                        //                break;
                        //            }
                        //        }

                        //    }
                        //}
                        //if (f == 0)
                        //{
                        //    for (int i = 0; i < EmailId.Length; i++)
                        //    {
                        //        if (Convert.ToInt32(EmailId[i]) >= 65 && Convert.ToInt32(EmailId[i]) <= 90 || Convert.ToInt32(EmailId[i]) >= 97 && Convert.ToInt32(EmailId[i]) <= 122 || Convert.ToInt32(EmailId[i]) >= 48 && Convert.ToInt32(EmailId[i]) <= 57 || Convert.ToInt32(EmailId[i]) == 46 || Convert.ToInt32(EmailId[i]) == 64 || Convert.ToInt32(EmailId[i]) == 96)
                        //        {
                        //            f = 0;
                        //        }
                        //        else
                        //        {
                        //            f = 1;

                        //            strMsg = "Invalid Email Address";
                        //        }
                        //    }
                        //}
                        //if (f == 0)
                        //{
                        //    for (int i = 0; i < EmailId.IndexOf("@"); i++)
                        //    {
                        //        if (i != EmailId.IndexOf("@") - 1)
                        //        {
                        //            if (Convert.ToInt32(EmailId[i]) == 46 && Convert.ToInt32(EmailId[i + 1]) == 46)
                        //            {
                        //                f = 1;
                        //                strMsg = "Invalid Email Address";
                        //                break;
                        //            }
                        //        }
                        //    }
                        //}

                    }

                }

                if (columnName == "Address")
                {
                    if (Address == null)
                    {
                        strMsg = "Enter your Address.";

                    }
                    else
                    {
                        strMsg = "";
                    }
                }
                if (columnName == "PinCode")
                {
                    if (PinCode == null)
                    {
                        strMsg = "Please enter PinCode";
                    }
                    else
                    {
                        if (PinCode.Length == 6)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                if (Convert.ToInt32(PinCode[i]) >= 48 && Convert.ToInt32(PinCode[i]) <= 57)
                                {

                                }
                                else
                                {
                                    strMsg = "Please enter valid pincode";
                                    break;
                                }
                            }
                        }
                        else
                        {
                            strMsg = "Please enter valid pincode";
                        }
                    }
                }
                return strMsg;
            }
        }
    }
}
