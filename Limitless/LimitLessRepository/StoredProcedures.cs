namespace LimitLessRepository
{
    public class StoredProcedures
    {
        /// <summary>
        /// Login Procedures
        /// </summary>
        public class Login
        {
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string LoginValidation = "dbo.Login_Validation";
        }
        /// <summary>
        /// Login Procedures
        /// </summary>
        public class Subjects
        {
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string SaveSubject = "dbo.Usp_SaveSubjectDetails";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetSubjectList = "dbo.Usp_GetSubjectList";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetAllSubject = "dbo.Usp_GetAllActiveSubjects";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetSubjectById = "dbo.Usp_GetSubjectById";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string DeleteSubject = "dbo.Usp_DeleteSubject";
            

        }
        public class Lessons
        {
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string SaveLesson = "dbo.Usp_SaveLessonDetails";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetLessonList = "dbo.Usp_GetLessonList";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetLessonById = "dbo.GetLessonById";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string DeleteLesson = "dbo.Usp_DeleteLesson";
            
        }
        public class Objectives
        {
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string SaveObjective = "dbo.Usp_SaveObjectiveDetails";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetObjectiveList = "dbo.Usp_GetObjectiveList";

            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetAllObjectives = "dbo.Usp_GetAllActiveObjectives";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetObjectiveById = "dbo.Usp_GetObjectiveById";

            /// <summary>
            /// Login Validations
            /// </summary>
            public const string DeleteObjective = "dbo.Usp_DeleteObjective";
            
        }
        public class Topics
        {
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string SaveTopics = "dbo.Usp_SaveTopicsDetails";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetTopicList = "dbo.Usp_GetTopicList";

            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetAllTopics = "dbo.Usp_GetAllActiveTopics";

            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetTopicById = "dbo.Usp_GetTopicById";

            /// <summary>
            /// Login Validations
            /// </summary>
            public const string DeleteTopic = "dbo.Usp_DeleteTopic";

        }
        public class Organizations
        {
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string SaveOrganizations = "dbo.Usp_SaveOrganizationDetails";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetOrganizationList = "dbo.Usp_GetOrganizationList";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetAllOrganization = "dbo.Usp_GetAllActiveOrganization";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetOrganizationById = "dbo.Usp_GetOrganizationById";
        }
        public class Questions
        {
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string SaveQuestion = "Usp_SaveQuestionDetails";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetQuestionList = "dbo.Usp_GetQuestionList";

            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetAllQuestions = "Usp_GetAllActiveQuestions";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetQuestionType = "Usp_GetAllActiveQuestionType";

            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetQuestionById = "dbo.Usp_GetQuestionById";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string DeleteQuestion = "dbo.Usp_DeleteQuestion";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetLastQuestionId = "dbo.Usp_GetLastQuestionId";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetQuestionAnswerList = "dbo.Usp_GetQuestionAnswerList";


        }

        public class Answers
        {
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string SaveAnswer = "Usp_SaveAnswerDetails";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetAnswerList = "dbo.Usp_GetAnswerList";

            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetAnswerById = "dbo.Usp_GetAnswerById";

            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetDetailsById = "dbo.Usp_GetDetailsById";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string DeleteAnswer = "dbo.Usp_DeleteAnswer";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string SaveUserAnswer = "dbo.Usp_SaveUserAnswer";
            

        }

        

        public class SubObjectives
        {
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string SaveSubObjective = "dbo.Usp_SaveSubObjectivesDetails";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetSubObjectiveList = "dbo.Usp_GetSubObjectiveList";

            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetAllActiveSubObjective = "dbo.Usp_GetAllActiveSubObjectives";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetSubObjectiveById = "dbo.Usp_GetSubObjectiveById";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string DeleteSubObjective = "dbo.Usp_DeleteSubObjective";
            
        }
        public class Users
        {
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string SaveUser = "dbo.Usp_SaveUserDetails";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetUser = "dbo.Usp_GetUserList";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetUserById = "dbo.Usp_GetUserById";

            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetUserLivesById = "dbo.Usp_GetUserLivesByUserId";

            /// <summary>
            /// Login Validations
            /// </summary>
            public const string SaveUserLives = "dbo.Usp_SaveUserLivesDetails";

            /// <summary>
            /// Login Validations
            /// </summary>
            public const string DeleteUser = "dbo.Usp_DeleteUser";
            /// <summary>
            /// Login Validations
            /// </summary>
            public const string GetUserScoreById = "dbo.Usp_GetUserScoreById";
            
        }
        public class Spreadsheet
        {
            /// <summary>
            ///save spreadsheet
            public const string SaveSpreadsheet = "dbo.Usp_SaveSpreadsheetDetails";
        }
    }
}
