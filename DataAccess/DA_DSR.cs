using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ets_dataaccess.Common;
using System.Data;
using Model;


namespace DataAccess
{
    public class DA_DSR : CommonUtility
    {
        //object declarations
        DataSet _dsResultSet = null;
        DSR _beRequirement = null;
        List<DSR> lstDSR = null;

        /// <summary>
        /// Employee DSR Report
        /// Name:Ramu Nimmala
        /// Date:30-09-2017
        /// </summary>
        /// <param name="objRequirement"></param>
        /// <returns></returns>
        public List<DSR> GetDSR(string strDate)
        {
            try
            {
                IDbDataParameter[] arrParameter = new IDbDataParameter[]{
                                DB_UTILITY.CreateParameter("@iDate", DbType.String, ParameterDirection.Input, strDate )                              
                };

                _dsResultSet = CommonUtility.DB_UTILITY.RunSP("usp_fetchDSR", arrParameter);

                if (ValidateResultSet(_dsResultSet))
                {
                    lstDSR = new List<DSR>();
                    lstDSR = OBJECT_UTILITY.GetConvertCollection<DSR>(_dsResultSet.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstDSR;
        }

        /// <summary>
        /// New DSR Submission
        /// </summary>
        /// <param name="objRequirement"></param>
        /// <returns></returns>
        public bool insertDSR(Task objRequirement)
        {
            bool blResult = false;
            try
            {
                IDbDataParameter[] arrParameter = new IDbDataParameter[]{
                                DB_UTILITY.CreateParameter("@ixml", DbType.Xml, ParameterDirection.Input, objRequirement.strXMLData ),
                                DB_UTILITY.CreateParameter("@icreatedate",DbType.DateTime,ParameterDirection.Input,DateTime.Now)
                };
                _dsResultSet = CommonUtility.DB_UTILITY.RunSP("usp_insertDSR", arrParameter);

                if (ValidateResultSet(_dsResultSet))
                {
                    if (Convert.ToInt32(_dsResultSet.Tables[0].Rows[0]["RESULT"]) == 1)
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

    }
}
