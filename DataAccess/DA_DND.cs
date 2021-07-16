using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ets_dataaccess.Common;

namespace DataAccess
{
    public class DA_DND : CommonUtility
    {
        DataSet _dsResultSet = null;

        /// <summary>
        /// New DSR Submission
        /// </summary>
        /// <param name="objRequirement"></param>
        /// <returns></returns>
        public void insertDSR(string strName, string strEMail, string strContactNo, string strMessage)
        {
            try
            {
                IDbDataParameter[] arrParameter = new IDbDataParameter[]{
                    DB_UTILITY.CreateParameter("@iName", DbType.String, ParameterDirection.Input, strName ),
                    DB_UTILITY.CreateParameter("@iContactNo", DbType.String, ParameterDirection.Input, strContactNo ),
                    DB_UTILITY.CreateParameter("@iMailId", DbType.String, ParameterDirection.Input, strEMail ),
                    DB_UTILITY.CreateParameter("@iMessage", DbType.String, ParameterDirection.Input, strMessage )
                };
                _dsResultSet = CommonUtility.DB_UTILITY.RunSP("usp_insertDotnetDrive", arrParameter);               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
