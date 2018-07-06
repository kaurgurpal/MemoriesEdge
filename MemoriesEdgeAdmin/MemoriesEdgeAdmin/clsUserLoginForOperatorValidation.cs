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
    public class clsUserLoginForOperatorValidation:INotifyPropertyChanged,IDataErrorInfo
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


        public event PropertyChangedEventHandler PropertyChanged;
        public void ChangeValue(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }

        public bool HasErrors
        {
            get { throw new NotImplementedException(); }
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

                return strMsg;
            }
        }
    }
}
