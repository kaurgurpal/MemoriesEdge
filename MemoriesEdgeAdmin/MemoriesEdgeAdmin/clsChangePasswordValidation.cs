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

namespace MemoriesEdgeAdmin
{
    public class clsChangePasswordValidation:INotifyPropertyChanged,IDataErrorInfo
    {
        private string strOldPassword;

        public string OldPassword
        {
            get { return strOldPassword; }
            set { strOldPassword = value;
                ChangeValue("OldPassword"); }
            
        }

        private string strNewPassword;

        public string NewPassword
        {
            get { return strNewPassword; }
            set { strNewPassword = value;
                ChangeValue("NewPassword"); }
           
        }

        private String strConfirmPassword;

        public String ConfirmPassword
        {
            get { return strConfirmPassword; }
            set { strConfirmPassword = value;
            ChangeValue("ConfirmPassword");
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
            get {

                string strMsg = "";
                if (columnName == "OldPassword")
                {
                    if (OldPassword == null)
                    { strMsg = "User Name Required"; 
                    
                    }
                }

                if (columnName == "NewPassword")
                {
                    if (NewPassword == null)
                        strMsg = "NewPassword Required";
                }
                if (columnName == "ConfirmPassword")
                {
                    if (ConfirmPassword != null)
                    {
                        if (ConfirmPassword != NewPassword)
                        {
                            strMsg = "NewPassword and Confirm Password must match.";
                        }
                    }
                    else
                    {
                        strMsg = "Enter Confirm Password";
                    }
                }

                return strMsg;
            
            }
        }
    }
}
