using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ets_dataaccess.Common;
using Model;

namespace DataAccess
{
   public class DA_Task : CommonUtility
    {
        DataSet _dsResultSet = null;
        Task _beRequirement = null;
        List<Task> ltask = null;

        /// <summary>
        /// New Requirement
        /// Name:Ravi Venkatesh Maddela
        /// Date:12-06-2017
        /// </summary>
        /// <param name="objRequirement"></param>
        /// <returns></returns>
        public bool NewTasks(Task objRequirement)
        {
            bool blResult = false;
            try
            {
                IDbDataParameter[] arrParameter = new IDbDataParameter[]{
                                DB_UTILITY.CreateParameter("@iUserCode", DbType.Int16, ParameterDirection.Input, objRequirement.CreatedByCode ),
                                DB_UTILITY.CreateParameter("@iTitle", DbType.String, ParameterDirection.Input, objRequirement.Title),
                                  DB_UTILITY.CreateParameter("@iDescription", DbType.String, ParameterDirection.Input, objRequirement.Description),
                                  DB_UTILITY.CreateParameter("@iType", DbType.Int16, ParameterDirection.Input, objRequirement.Type),
                                  DB_UTILITY.CreateParameter("@iNoOfEfforts", DbType.Int16, ParameterDirection.Input, objRequirement.EstimatedEfforts)
                };
                _dsResultSet = CommonUtility.DB_UTILITY.RunSP("usp_insertRequirement", arrParameter);

                if (ValidateResultSet(_dsResultSet))
                {
                    if(Convert.ToInt32(_dsResultSet.Tables[0].Rows[0]["RESULT"]) == 1)
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

        /// <summary>
        /// Requirements
        /// Name:Sudha Rani Lingutla
        /// Date:13-06-2017
        /// </summary>
        /// <param name="objTasks"></param>
        /// <returns></returns>
        public List<Task> GetTasks(Task objTasks)
        {
            try
            {
                IDbDataParameter[] arrParameter = new IDbDataParameter[]{
                                DB_UTILITY.CreateParameter("@iUserCode", DbType.Int16, ParameterDirection.Input, objTasks.CreatedByCode )                              
                };

                _dsResultSet = CommonUtility.DB_UTILITY.RunSP("usp_fetchTasks", arrParameter);

                if (ValidateResultSet(_dsResultSet))
                {
                    ltask = new List<Task>();
                    ltask = OBJECT_UTILITY.GetConvertCollection<Task>(_dsResultSet.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ltask;
        }

        /// <summary>
        /// Requirements
        /// Name:hanumantharao muppalla
        /// Date:24-11-2017
        /// </summary>
        /// <param name="objTasks"></param>
        /// <returns></returns>
        public List<Task> GetTaskID(string strTaskID)
        {
            try
            {
                IDbDataParameter[] arrParameter = new IDbDataParameter[]{
                                DB_UTILITY.CreateParameter("@iTaskId", DbType.String, ParameterDirection.Input, strTaskID )                              
                };

                _dsResultSet = CommonUtility.DB_UTILITY.RunSP("usp_auto_fetchTaskDetails", arrParameter);

                if (ValidateResultSet(_dsResultSet))
                {
                    ltask = new List<Task>();
                    ltask = OBJECT_UTILITY.GetConvertCollection<Task>(_dsResultSet.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ltask;
        }

        /// <summary>
        /// Task Details
        /// Name:Ravi Venkatesh Maddela
        /// Date:22-06-2017
        /// </summary>
        /// <param name="objTasks"></param>
        /// <returns></returns>
        public Task GetTaskDetails(Task objTasks)
        {
            try
            {
                IDbDataParameter[] arrParameter = new IDbDataParameter[]{
                                DB_UTILITY.CreateParameter("@iTaskId", DbType.Int16, ParameterDirection.Input, objTasks.TaskId )                              
                };

                _dsResultSet = CommonUtility.DB_UTILITY.RunSP("usp_fetchTaskDetails", arrParameter);

                if (ValidateResultSet(_dsResultSet))
                {
                    _beRequirement = OBJECT_UTILITY.GetConvert<Task>(_dsResultSet.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _beRequirement;
        }

        /// <summary>
        /// Update Task
        /// Name:Ravi Venkatesh Maddela
        /// Date:23-06-2017
        /// </summary>
        /// <param name="objRequirement"></param>
        /// <returns></returns>
        public bool UpdateTask(Task objRequirement)
        {
            bool blResult = false;
            try
            {
                IDbDataParameter[] arrParameter = new IDbDataParameter[]{
                                DB_UTILITY.CreateParameter("@iTaskId", DbType.Int16, ParameterDirection.Input, objRequirement.TaskId ),
                                 DB_UTILITY.CreateParameter("@iTitle", DbType.String, ParameterDirection.Input, objRequirement.Title ),
                                  DB_UTILITY.CreateParameter("@iType", DbType.Int16, ParameterDirection.Input, objRequirement.Type ),
                                   DB_UTILITY.CreateParameter("@iEffHours", DbType.Int16, ParameterDirection.Input, objRequirement.EstimatedEfforts ),
                                DB_UTILITY.CreateParameter("@iStatus", DbType.Int16, ParameterDirection.Input, objRequirement.TaskStatus),
                                 DB_UTILITY.CreateParameter("@iDescription", DbType.String, ParameterDirection.Input, objRequirement.Description )
                };

                _dsResultSet = CommonUtility.DB_UTILITY.RunSP("usp_UpdateTask", arrParameter);

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
       

        /// <summary>
        /// Requirements
        /// Name:Ravi Venkatesh Maddela
        /// Date:15-07-2017
        /// </summary>
        /// <param name="objTasks"></param>
        /// <returns></returns>
        public List<string> auto_fetchTaskDetails(Task objTasks)
         {
            List<string> lstId = new List<string>();
            try
            {
                IDbDataParameter[] arrParameter = new IDbDataParameter[]{
                                DB_UTILITY.CreateParameter("@iTaskID", DbType.Int32, ParameterDirection.Input, objTasks.TaskId )                              
                };

                _dsResultSet = CommonUtility.DB_UTILITY.RunSP("usp_auto_fetchTaskDetails", arrParameter);

                if (ValidateResultSet(_dsResultSet))
                {
                    ltask = new List<Task>();
                    ltask = OBJECT_UTILITY.GetConvertCollection<Task>(_dsResultSet.Tables[0]);
                    lstId = ltask.Select(s => Convert.ToString(s.TaskId)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstId;
        }

        /// <summary>
        /// Fetch Task Details by ID
        /// </summary>
        /// <param name="strUserCode"></param>
        /// <param name="strTaskID"></param>
        /// <param name="iNoOfEfforts"></param>
        public List<Task> fetchTaskId(string TaskId)
        {
            try
            {
                IDbDataParameter[] arrParameter = new IDbDataParameter[]{
                                DB_UTILITY.CreateParameter("@iTaskID", DbType.Int32, ParameterDirection.Input, TaskId )                              
                };

                _dsResultSet = CommonUtility.DB_UTILITY.RunSP("usp_fetchTaskDetails", arrParameter);

                if (ValidateResultSet(_dsResultSet))
                {
                    ltask = new List<Task>();
                    ltask = OBJECT_UTILITY.GetConvertCollection<Task>(_dsResultSet.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ltask;
        }

        /// <summary>
       /// Mapping user to task
       /// </summary>
       /// <param name="strUserCode"></param>
       /// <param name="strTaskID"></param>
       /// <param name="iNoOfEfforts"></param>
        public bool MapTaskToUser(string strUserOfficialMail, int strTaskID, int iNoOfEfforts)
        {
            bool blResult = false;
            try
            {
                IDbDataParameter[] arrParameter = new IDbDataParameter[]{
                                DB_UTILITY.CreateParameter("@iUserOfficialMailID", DbType.String, ParameterDirection.Input, strUserOfficialMail ),
                                DB_UTILITY.CreateParameter("@iTaskID", DbType.Int32, ParameterDirection.Input, strTaskID ),
                                DB_UTILITY.CreateParameter("@iEstimatedEfforts", DbType.Int32, ParameterDirection.Input, iNoOfEfforts )
                };

                _dsResultSet = CommonUtility.DB_UTILITY.RunSP("usp_insertMapUserWithTask", arrParameter);

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

