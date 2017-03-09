using LimitLessRepository.Common;
using LimitLessDataAccess;
using LimitlessEntity.Results;
using Newtonsoft.Json;
using LimitlessEntity.Entities.Models;
using System;
using System.Data.SqlClient;

namespace LimitLessRepository
{
    public class RootRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;

        public RootRepository()
        {
            _connectionString = Constants.LimitLessConnectionString;
        }

        public string Get()
        {
            var ds = SqlHelper.ExecuteDataset(_connectionString, System.Data.CommandType.StoredProcedure, SqlObject.CommandText);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
        public ListResult GetData()
        {
            var result = new ListResult();
            var ds = SqlHelper.ExecuteDataset(_connectionString, System.Data.CommandType.StoredProcedure, SqlObject.CommandText);
            result.List = JsonConvert.SerializeObject(ds.Tables[0]);
            return result;
        }
        public ListResult GetList()
        {
            var result = new ListResult();
            var ds = SqlHelper.ExecuteDataset(_connectionString, SqlObject.CommandText, SqlObject.Parameters);
            result.List = JsonConvert.SerializeObject(ds.Tables[0]);
            return result;
        }
        public GridResult GridList()
        {
            var result = new GridResult();
            var ds = SqlHelper.ExecuteDataset(_connectionString, SqlObject.CommandText, SqlObject.Parameters);
            if (ds.Tables.Count > 0)
            {
                result.List = JsonConvert.SerializeObject(ds.Tables[0]);
                result.TotalRecords = ds.Tables[0].Rows.Count;
            }
            return result;
        }


        public GridResult GridQAList()
        {
            var result = new GridResult();
            var ds = SqlHelper.ExecuteDataset(_connectionString, SqlObject.CommandText, SqlObject.Parameters);
            if (ds.Tables.Count > 0)
            {
                result.List = JsonConvert.SerializeObject(ds);
                result.TotalRecords = ds.Tables[0].Rows.Count;
            }
            return result;
        }

        public int Save()
        {

            return SqlHelper.ExecuteNonQuery(_connectionString, SqlObject.CommandText, SqlObject.Parameters);
        }

        public UserData SaveTrans()
        {
            var result = new UserData();
            var ds = SqlHelper.ExecuteDataset(_connectionString, SqlObject.CommandText, SqlObject.Parameters);
            if (ds.Tables.Count > 0)
            {
                result.UserID = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            return result;
        }
        public SelectedData GetOrgDetails()
        {
            var result = new SelectedData();
            var ds = SqlHelper.ExecuteDataset(_connectionString, SqlObject.CommandText, SqlObject.Parameters);
            result.SelectedDetails = JsonConvert.SerializeObject(ds.Tables[0]);
            return result;
        }

        public int Delete(T t)
        {
            return SqlHelper.ExecuteNonQuery(_connectionString, SqlObject.CommandText, SqlObject.Parameters);
        }
        public AnswerStatus SaveUserAnswer()
        {
            var result = new AnswerStatus();
            var ds = SqlHelper.ExecuteDataset(_connectionString, SqlObject.CommandText, SqlObject.Parameters);
            result.AnswerResult = JsonConvert.SerializeObject(ds.Tables[0]);
            return result;
        }

        public void SaveQuestionAnswer()
        {
            SqlHelper.ExecuteNonQuery(_connectionString, SqlObject.CommandText, SqlObject.Parameters);
        }

        public void SaveQuestionAnswerSubjective()
        {
            SqlHelper.ExecuteNonQuery(_connectionString, SqlObject.CommandText, SqlObject.Parameters);
        }
    }
}
