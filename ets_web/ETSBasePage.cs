using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Globalization;

namespace ets_web
{
    public class ETSBasePage : System.Web.UI.Page
    {       

        public static Dictionary<int, string> dcRole = new Dictionary<int, string>() {{0, "ADMIN"}, {1, "MANAGER"}, {2, "MEMBER"} };
        public static Dictionary<int, string> dcType = new Dictionary<int, string>()
        { { 0, "NEW" }, { 1, "Change Request" }, { 2, "PBI" }, { 7, "Bug" }, { 4, "Test Case" }};

        public static Dictionary<int, string> dcDSRStatus = new Dictionary<int, string>() 
        { { 0, "In Progress" }, { 1, "Blocked" }, { 2, "Closed" }, { 7, "Rejected" } };

        public static Dictionary<int, string> dcNotificationType = new Dictionary<int, string>() 
        { { 0, "Inactivated" }, { 1, "Notification" }, { 2, "Alert" }, { 3, "Wishes" }, { 4, "Information" } };

        public static Dictionary<int, string> dcNotificationControl = new Dictionary<int, string>()
        { { 0, "Label" }, { 1, "Button" }, { 2, "LinkButton" }, { 3, "HyperLink" } };

        public static Dictionary<int, string> dcStatus = new Dictionary<int, string>()
        { { 0, "New" }, { 1, "Assigned" }, { 2, "InProgress" }, { 3, "Blocked" }, { 4, "Rejected" }, { 5, "Closed" },{ 6, "Done" }, {7, "Resolved"}, {8, "Done"}, {9, "ReOpen"}  };

        //getting log file path
        public string GetLogFilePath()
        {
            return Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["logpath"]));
        }

        public void ValidateUserLogin()
        {

            if (HttpContext.Current.Session["user"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }                        
        }

        public string ConvertToDBDate(string strCultureDate)
        {
            DateTime dtDate = DateTime.ParseExact(strCultureDate,
                                    "dd/MM/yyyy",
                                    CultureInfo.InvariantCulture);
            return dtDate.ToString("yyyy-MM-dd");
        }
    }
    
 public class Message
 {
    public const string ErrorMessage = "Error Occured. Contact IT Department";
    public const string NewRecordMessage = "Transaction Successful";
    public const string UpdateRecordMessage = "Record Updated Successfully";
    public const string EmptyRecords = "No Records found";
    public const string NoRecordUpdated = "No Record Updated";
    public const string TransactionFailed = "Transaction Failed";
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
            try
            {
                FileInfo _file = new FileInfo(strLogPath);
                if (_file.Length / 1048576 > 30)
                {
                    File.WriteAllText(strLogPath, String.Empty);
                }
            }
            catch (Exception ex)
            {

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
            StreamWriter _sw = new StreamWriter(LogFilePath, true);
            try
            {
                EXCEPTION_UTILITY.FileExceedSize(LogFilePath);
                if (IfThreadAbort(ex))
                {                    
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
                    
                }
            }
            finally
            {
                _sw.Close();
            }
        }
        
    }
    #endregion
}