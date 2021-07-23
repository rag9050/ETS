using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.Reflection;
using System.IO;



namespace ets_dataaccess.Common
{
    public class CommonUtility
    {
        #region DataUtility
        /// <summary>
        /// Description : To perform database operations
        /// Name: Ramu Nimmala
        /// </summary>        
        protected class DB_UTILITY
        {
            internal static IDbDataParameter CreateParameter(string PName, DbType PType, ParameterDirection PDirection, Object PValue)
            {
                SqlCommand _Command = new SqlCommand();
                IDbDataParameter _obj = _Command.CreateParameter();
                _obj.ParameterName = PName;
                _obj.DbType = PType;
                _obj.Direction = PDirection;
                _obj.Value = PValue;

                return _obj;
            }

            internal static DataSet RunSP(string SPName, IDbDataParameter[] ParamsList)
            {
                string ConStr = ConfigurationManager.ConnectionStrings["etsconnection"].ConnectionString;
                SqlConnection _Connection = new SqlConnection(ConStr);
                SqlCommand _command = new SqlCommand(SPName, _Connection);
                _command.CommandType = CommandType.StoredProcedure;
                if (ParamsList != null)
                {
                    foreach (IDbDataParameter _Params in ParamsList)
                    {
                        _command.Parameters.Add(_Params);
                    }
                }
                DataSet _dsResultSet = new DataSet();

                try
                {
                    SqlDataAdapter _da = new SqlDataAdapter(_command);
                    _da.Fill(_dsResultSet);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return _dsResultSet;
            }

        }
        #endregion

        #region Validate Dataset
        /// <summary>
        /// Validate a dataset and its table rows count
        /// </summary>
        /// <param name="_dsResult"></param>
        /// <returns></returns>
        protected bool ValidateResultSet(DataSet _dsResult)
        {
            bool _blFalg = false;
            if (_dsResult != null && _dsResult.Tables[0].Rows.Count > 0)
            {
                _blFalg = true;
            }
            return _blFalg;
        }
        #endregion

        #region Mail sending
        /// <summary>
        /// Description:  Mail sending
        /// Author name: Ramu Nimmala
        /// </summary>       
        public class COMMON_UTILITY
        {
            //configure mail
            public void SendMail(string FromID, string FromPWD, string DestinationID, string Subject, string Details, Char Mode)
            {
                try
                {
                    MailMessage _objMailMessage = new MailMessage();
                    _objMailMessage.From = new MailAddress(FromID);

                    switch (Mode)
                    {
                        case 'T': _objMailMessage.To.Add(DestinationID);
                            break;
                        case 'C': _objMailMessage.CC.Add(DestinationID);
                            break;
                        case 'B': _objMailMessage.Bcc.Add(DestinationID);
                            break;
                    }

                    _objMailMessage.Subject = Subject;
                    _objMailMessage.Body = Details;
                    _objMailMessage.IsBodyHtml = true;
                    _objMailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                    //sending client
                    SendClientMail(_objMailMessage, FromPWD);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            //send mail
            private void SendClientMail(MailMessage _MailContent, string Password)
            {
                try
                {
                    SmtpClient client = new SmtpClient();
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;
                    client.Timeout = 20000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(_MailContent.From.ToString(), Password);
                    client.Send(_MailContent);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region Object Converter
        /// <summary>
        /// Description : Converting data table into object
        /// Name: Ramu Nimmala
        /// </summary>        
        protected class OBJECT_UTILITY
        {
            //individual object converter
            public static T GetConvert<T>(DataTable dtData) where T : new()
            {
                Type objType = typeof(T);
                Hashtable hashtable = new Hashtable();
                PropertyInfo[] properties = objType.GetProperties();
                foreach (PropertyInfo info in properties)
                {
                    hashtable[info.Name.ToUpper()] = info;
                }
                T newObject = new T();
                foreach (DataRow _EachRow in dtData.Rows)
                {
                    for (int Index = 0; Index < dtData.Columns.Count; Index++)
                    {
                        string ColumnName = dtData.Columns[Index].ColumnName.ToUpper();
                        PropertyInfo info = (PropertyInfo)hashtable[ColumnName];
                        if ((info != null) && info.CanWrite && _EachRow[ColumnName] != DBNull.Value)
                        {
                            info.SetValue(newObject, _EachRow[ColumnName], null);
                        }
                    }
                }
                return newObject;
            }

            //for list of objects
            public static List<T> GetConvertCollection<T>(DataTable dtData) where T : new()
            {
                Type objType = typeof(T);
                Hashtable hashtable = new Hashtable();
                PropertyInfo[] properties = objType.GetProperties();
                foreach (PropertyInfo info in properties)
                {
                    hashtable[info.Name.ToUpper()] = info;
                }
                List<T> lstT = new List<T>();
                foreach (DataRow _EachRow in dtData.Rows)
                {
                    T newObject = new T();
                    for (int Index = 0; Index < dtData.Columns.Count; Index++)
                    {
                        string ColumnName = dtData.Columns[Index].ColumnName.ToUpper();
                        PropertyInfo info = (PropertyInfo)hashtable[ColumnName];
                        if ((info != null) && info.CanWrite && _EachRow[ColumnName] != DBNull.Value)
                        {
                            info.SetValue(newObject, _EachRow[ColumnName], null);
                        }
                    }
                    lstT.Add(newObject);
                }
                return lstT;
            }

        }
        #endregion
    }


   #region Exception log
        /// <summary>
        /// Description: log exception
        /// Name : Ramu Nimmala
        /// </summary>        
        public static class EXCEPTION_UTILITY
        {
            /// <summary>
            /// if file size exceeds 30 mb file content will be cleared
            /// </summary>
            /// <param name="strLogPath"></param>
            private static void FileExceedSize(string strLogPath)
            {
                FileInfo _file = new FileInfo(strLogPath);
                if (_file.Length / 1048576 > 30)
                {
                    File.WriteAllText(strLogPath, String.Empty);
                }
            }

            /// <summary>
            /// To skip thread abort exception
            /// </summary>
            /// <param name="ex"></param>
            /// <returns></returns>
            private static bool IfThreadAbort(Exception ex)
            {
                if (ex.Message == "Thread was being aborted.")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            /// <summary>
            /// Writing exception to log file  along with user id including form name in which the exception occured
            /// </summary>
            /// <param name="ex">exception</param>
            /// <param name="LogFilePath">File path</param>
            /// <param name="UserID">User ID</param>
            /// <param name="FormName">Form Name</param>
            /// <param name="ProjectName">Project Name</param>
            /// <param name="MethodName">Method Name</param>
            public static void WriteToLog(Exception ex, string LogFilePath, string UserID, string FormName, string ProjectName, string MethodName)
            {
                EXCEPTION_UTILITY.FileExceedSize(LogFilePath);
                if (IfThreadAbort(ex))
                {
                    StreamWriter _sw = new StreamWriter(LogFilePath, true);
                    Random rand = new Random();
                    string ID = Convert.ToString(rand.Next()) + "-" + string.Format("{0:ddMMyyyyHHmmss}", System.DateTime.Now);
                    _sw.WriteLine("Id: {0}", ID);
                    _sw.WriteLine("UserID: {0}", UserID);
                    _sw.WriteLine("Form Name: {0}", FormName);
                    _sw.WriteLine("project Name: {0}", ProjectName);
                    _sw.WriteLine("Method: {0}", MethodName);
                    _sw.WriteLine("Type: {0}", ex.GetType());
                    _sw.WriteLine("Source: {0}", ex.Source);
                    _sw.WriteLine("Message: {0}", ex.Message);
                    _sw.WriteLine("------------------------------------------------------------------------------------");
                    _sw.WriteLine();
                    _sw.Close();
                }
            }
        }
        #endregion
   
}
