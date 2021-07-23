using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ets_dataaccess.Common;
using Model;
using System.Data;

namespace DataAccess
{
    public class DA_Notification : CommonUtility
    {
        List<Notification> lstNotification;
        DataSet _dsResultSet;
        bool blResult ;

        //getting notification data
        public List<Notification> getNotificationData(int iUserCode)
        {
            try
            {
                IDbDataParameter[] arrParameter = new IDbDataParameter[]{
                DB_UTILITY.CreateParameter("@iUserCode", DbType.Int16, ParameterDirection.Input, iUserCode)                              
            };

                _dsResultSet = CommonUtility.DB_UTILITY.RunSP("usp_fetchNotifications", arrParameter);

                if (ValidateResultSet(_dsResultSet))
                {
                    lstNotification = OBJECT_UTILITY.GetConvertCollection<Notification>(_dsResultSet.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstNotification;
        }

        //inserting new notification
        public bool InsertNotification(Notification objUser)
        {
            bool blResult;
            try
            {
                IDbDataParameter[] arrParameter = new IDbDataParameter[]{
                           
                                DB_UTILITY.CreateParameter("@iPostedby", DbType.Int32, ParameterDirection.Input, objUser.PostedBy),
                                DB_UTILITY.CreateParameter("@iTitle", DbType.String, ParameterDirection.Input, objUser.Title),
                                 DB_UTILITY.CreateParameter("@iDescription", DbType.String, ParameterDirection.Input, objUser.Description),
                                 DB_UTILITY.CreateParameter("@iType", DbType.Int32, ParameterDirection.Input, objUser.Type),
                                 DB_UTILITY.CreateParameter("@icontroltype", DbType.Int32, ParameterDirection.Input, objUser.Control),                               
                };

                _dsResultSet = CommonUtility.DB_UTILITY.RunSP("usp_insertNotifications", arrParameter);
                blResult = true;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return blResult;
        }
        public string updateNotificationData(int iUserCode)
        {
            try
            {
                IDbDataParameter[] arrParameter = new IDbDataParameter[]{
                                DB_UTILITY.CreateParameter("@ic_NotificationSNo", DbType.Int32, ParameterDirection.Input, iUserCode)                              
                };

                _dsResultSet = CommonUtility.DB_UTILITY.RunSP("usp_updateNotifications", arrParameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "successs";
        }
    }
}
