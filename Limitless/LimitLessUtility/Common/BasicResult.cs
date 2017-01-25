using System;
using System.Runtime.Serialization;

namespace LimitLessUtility.Common
{
    /// <summary>
    /// a basic result of an operation
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "")]
    public class BasicResult
    {
        #region " Public Properties "

        /// <summary>
        /// did the operation complete okay?
        /// </summary>
        [DataMember]
        public bool Success { get; set; }

        /// <summary>
        /// if the operation did not complete okay, this should hold the error message
        /// </summary>
        [DataMember]
        public string Error { get; set; }

        #endregion

        #region " Constructors "

        /// <summary>
        /// create a basic operation result
        /// </summary>
        public BasicResult()
        {
            Success = true;
        }

        /// <summary>
        /// create a basic operation result
        /// </summary>
        /// <param name="success">set the default state of the Success flag</param>
        public BasicResult(bool success = true)
        {
            Success = success;
        }

        #endregion

        #region " Public Methods "

        /// <summary>
        /// set the error message on the operation result
        /// </summary>
        /// <param name="ex">the exception object</param>
        /// <param name="success">set the default state of the Success flag</param>
        /// <param name="logError">write the error in the text log?</param>
        public void SetError(Exception ex, bool success = false, bool logError = false)
        {
            //if (logError)
            //    Logger.Current.LogErrorFormat(MethodBase.GetCurrentMethod(), "Basic Result exception : {0} ", ex);

            SetError(ex.Message, success);
        }

        /// <summary>
        /// set the error message on the operation result
        /// </summary>
        /// <param name="error">the error message</param>
        /// <param name="success">set the default state of the Success flag</param>
        /// <param name="logError">write the error in the text log?</param>
        public void SetError(string error, bool success = false, bool logError = false)
        {
            //if (logError)
            //    Logger.Current.LogErrorFormat(MethodBase.GetCurrentMethod(), "Basic Result exception : {0} ", error);

            Success = success;
            Error = string.IsNullOrEmpty(error) ? string.Empty : error.Replace("\r", "").Replace("\n", " ");
        }

        #endregion
    }
}
