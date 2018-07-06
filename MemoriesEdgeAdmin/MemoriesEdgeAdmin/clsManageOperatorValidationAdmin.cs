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
    public class clsManageOperatorValidationAdmin:INotifyPropertyChanged,IDataErrorInfo
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

        private string strStatus;
        public string Status
        {
            get { return strStatus; }
            set { strStatus = value;
            ChangeValue("Status");
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
                string strMsg = "" ;
                if (columnName == "UserName")
                {
                    if (UserName == null)
                    {
                        strMsg = "Enter your UserName";
                    }
                    //int f = 0;
                    //if (UserName != null)
                    //{
                    //    for (int i = 0; i < UserName.Length; i++)
                    //    {
                    //        if (Convert.ToInt32(UserName[i]) >= 65 && Convert.ToInt32(UserName[i]) <= 90 || Convert.ToInt32(UserName[i]) >= 97 && Convert.ToInt32(UserName[i]) <= 122)
                    //        {
                    //            continue;
                    //        }
                    //        else
                    //        {
                    //            f = 1;
                    //            break;
                    //        }
                    //    }
                    //    strMsg = f == 0 ? "" : "Enter Valid Name";
                    //}
                    //else
                    //{
                    //    strMsg = "Enter your UserName.";
                    //}
                }
                if (columnName == "Name")
                {
                    if (Name == null)
                    {
                        strMsg = "Enter your Name";
                    }
                    //int f=0;
                    //if (Name != null)
                    //{
                    //     for (int i = 0; i < Name.Length; i++)
                    //        {
                    //            if (Convert.ToInt32(Name[i]) >= 65 && Convert.ToInt32(Name[i]) <= 90 || Convert.ToInt32(Name[i]) >= 97 && Convert.ToInt32(Name[i]) <= 122)
                    //            {
                    //                continue;
                    //            }
                    //            else
                    //            {
                    //                f = 1;
                    //                break;
                    //            }
                    //        }
                    //        strMsg = f == 0 ? "" : "Enter Valid Name";
                    //    }
                    //else
                    //{
                    //    strMsg = "Enter Valid Name";
                    //}

                    }
                   
                if (columnName == "Status")
                {
                    if (ComboBox.SelectedValueProperty == null)
                    {
                        strMsg = "Please Select Status";
                    }
                }
                return strMsg;    
            } 

            }
        }
    }
