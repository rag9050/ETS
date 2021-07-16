using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using ets_dataaccess.Common;

namespace DataAccess
{
    public class DA_User : CommonUtility
    {
        DataSet _dsResultSet = null;
        User beMember = null;
        List<User> lstUser = null;

        /// <summary>
        /// Check User Login
        /// Name:Ravi Venkatesh Maddela
        /// Date:10-06-2017
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public User CheckUserLogin(User objUser)
        {
            try
            {
                IDbDataParameter[] arrParameter = new IDbDataParameter[]{
                                DB_UTILITY.CreateParameter("@iUMID", DbType.String, ParameterDirection.Input, objUser.LoginId ),
                                DB_UTILITY.CreateParameter("@iPassword", DbType.String, ParameterDirection.Input, objUser.Password),                                 
                };

                _dsResultSet = CommonUtility.DB_UTILITY.RunSP("usp_fetchLoginUser", arrParameter);

                if (ValidateResultSet(_dsResultSet))
                {
                    beMember = OBJECT_UTILITY.GetConvert<User>(_dsResultSet.Tables[0]);
               }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return beMember;
        }

        /// <summary>
        /// Change user password
        /// Name:Hanumanth Rao Muppalla
        /// Date:12-08-2017
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public bool ChangeUserPassword(User objUser)
        {
            bool blResult = false;
            try
            {
                IDbDataParameter[] arrParameter = new IDbDataParameter[]{
                                DB_UTILITY.CreateParameter("@iUserCode", DbType.Int32, ParameterDirection.Input, objUser.UserCode ),
                                DB_UTILITY.CreateParameter("@iNewPassword", DbType.String, ParameterDirection.Input, objUser.Password),                                 
                };

                _dsResultSet = CommonUtility.DB_UTILITY.RunSP("usp_updateUserPassword", arrParameter);

                if (ValidateResultSet(_dsResultSet))
                {
                    if (Convert.ToInt16(_dsResultSet.Tables[0].Rows[0]["RESULT"]) == 1)
                    {
                        blResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return blResult;
        }

        public bool ResetPassword(User objUser)
        {
            bool blResult = false;
            try
            {
                IDbDataParameter[] arrParameter = new IDbDataParameter[]{
                                DB_UTILITY.CreateParameter("@iUserMailID", DbType.String, ParameterDirection.Input, objUser.OfficialMailID ),
                                DB_UTILITY.CreateParameter("@iNewPassword", DbType.String, ParameterDirection.Input, objUser.Password),                                 
                };

                _dsResultSet = CommonUtility.DB_UTILITY.RunSP("usp_updateResetPassword", arrParameter);

                if (ValidateResultSet(_dsResultSet))
                {
                    if (Convert.ToInt16(_dsResultSet.Tables[0].Rows[0]["RESULT"]) == 1)
                    {
                        blResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return blResult;
        }

        public List<string> autoGetUserMailID(string strPreKey)
        {
            List<string> lstMailID = new List<string>();
            try
            {
                IDbDataParameter[] arrParameter = new IDbDataParameter[]{
                                DB_UTILITY.CreateParameter("@iOfficialMailID", DbType.String, ParameterDirection.Input, strPreKey )                                 
                };

                _dsResultSet = CommonUtility.DB_UTILITY.RunSP("usp_auto_fetchUserData", arrParameter);

                if (ValidateResultSet(_dsResultSet))
                {
                    lstUser = OBJECT_UTILITY.GetConvertCollection<User>(_dsResultSet.Tables[0]);
                    lstMailID = lstUser.Select(m => m.OfficialMailID).ToList<string>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstMailID;
        }

    }
}
