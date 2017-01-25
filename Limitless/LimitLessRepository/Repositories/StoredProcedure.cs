using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitLessRepository.Repositories
{
    /// <summary>
    /// this is used to declare and set the Param data
    /// </summary>
    struct ParamData
    {
        public string pName, pValue;
        public SqlDbType pDataType;
        public ParamData(string pName, SqlDbType pDataType, string pValue)
        {
            this.pName = pName;
            this.pDataType = pDataType;
            this.pValue = pValue;

        }

    }
    /// <summary>
    /// This class is used to execute the Stored procedures passed as  collection
    /// </summary>
    public class Execute
    {
        /// <summary>
        /// Excecute Sps
        /// </summary>
        /// <param name="spCollection"></param>
        /// <param name="Connection"></param>
        /// <returns></returns>
        public static bool ExecuteSps(StoredProcedureCollection spCollection, SqlConnection Connection)
        {
            try
            {
                foreach (StoredProcedure spData in spCollection)
                {
                    SqlCommand cmd = new SqlCommand();
                    int i = 0;
                    if (Connection.State != ConnectionState.Open)
                        Connection.Open();
                    cmd.Connection = Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = spData.ProcName;
                    IEnumerator myEnumerator = spData.GetParams().GetEnumerator();
                    while (myEnumerator.MoveNext())
                    {
                        ParamData pData = (ParamData)myEnumerator.Current;
                        cmd.Parameters.Add(pData.pName, pData.pDataType);
                        cmd.Parameters[i].Value = pData.pValue;
                        i = i + 1;
                    }
                    cmd.ExecuteNonQuery();
                }
                return true;


            }
            catch (Exception exc)
            {
                return false;
            }
        }
    }
    /// <summary>
    /// Stored Procedure class with n no of parameters
    /// </summary>

    public class StoredProcedure
    {
        private string sProcName;
        private ArrayList sParams = new ArrayList();
        /// <summary>
        /// set the parameters
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pDataType"></param>
        /// <param name="pValue"></param>
        /// <returns></returns>
        /// 

        public void SetParam(string pName, SqlDbType pDataType, string pValue)
        {

            ParamData pData = new ParamData(pName, pDataType, pValue);
            sParams.Add(pData);
        }

        /// <summary>
        /// get the Parameter list
        /// </summary>
        /// <returns></returns>
        public ArrayList GetParams()
        {
            if (!(sParams == null))
            {
                return sParams;
            }
            else
            {
                return null;

            }

        }

        public string ProcName
        {
            get
            {
                return sProcName;
            }
            set
            {
                sProcName = value;
            }
        }

    }
    /// <summary>
    /// This class used to mailtain the collection of stored procedures
    /// </summary>
    public class StoredProcedureCollection : System.Collections.CollectionBase
    {
        public void add(StoredProcedure value)
        {
            List.Add(value);
        }
        public void Remove(int index)
        {

            if (index > Count - 1 || index < 0)
            {
                Console.WriteLine("No data to remove");
            }
            else
            {
                List.RemoveAt(index);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public StoredProcedure Item(int Index)
        {
            return (StoredProcedure)List[Index];
        }
    }
}
